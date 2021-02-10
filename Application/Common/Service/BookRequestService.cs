using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappings;
using BookStore.Application.Common.Model;
using BookStore.Application.ViewModel;
using BookStore.Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Service
{
    public class BookRequestService : IBookRequestService
    {

        private readonly IConfiguration _configuration;

        private IEmailService _emailService;
        private IDateTime _dateTimeService;
        private IIdentityService _identityService;
        private ICurrentUserService _currentUserService;
        readonly IApplicationDbContext _dbContext;
        public BookRequestService(IApplicationDbContext dbContext,
            IDateTime dateTimeService,
            IEmailService emailService,
            IIdentityService identityService,
            IConfiguration configuration,
            ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _identityService = identityService;
            _dateTimeService = dateTimeService;
            _configuration = configuration;
            _emailService = emailService;
            _dbContext = dbContext;
        }
        public async Task<PaginatedList<BookRequestViewModel>> GetRequestedBookList(int pageNumber, int pageSize)
        {
            var bookReservationList = await _dbContext.BookReservations
                    .Include(x => x.Book).Where(x => x.Status == Domain.BorrowStatus.Requested).Select(
                    book => new BookRequestViewModel
                    {
                        Id = book.Id,
                        BookId = book.Book.Id,
                        RequesterId = book.RequesterId,
                        Author = book.Book.Author,
                        Title = book.Book.Title,
                        Status = book.Status,
                        RequestDate = book.RequestDate
                    }).PaginatedListAsync(pageNumber, pageSize);

            foreach (var y in bookReservationList.Items)
            {
                y.Requester = await _identityService.GetUserEmailAsync(y.RequesterId);
            }

            return bookReservationList;
        }
        public async Task ApproveBook(BookApproveViewModel model)
        {
            if (GetBorrowStatus(model.Status) == Domain.BorrowStatus.Approved)
            {
                var bookApprove = new BookLending()
                {
                    DueDate = _dateTimeService.Now.AddDays(15),
                    Remarks = $"Book request approved for {await _identityService.GetBorrowerFullName(model.RequesterId) }",
                    IssuedDate = _dateTimeService.Now,
                    BookId = model.BookId,
                    BorrowerId = model.RequesterId,
                    CreatedBy = _currentUserService.UserId,
                    Created = _dateTimeService.Now
                };

                await _dbContext.BookLendings.AddAsync(bookApprove);
                await _dbContext.SaveChangesAsync();

                await SendEmailForApprove(model);
            }
            else if (GetBorrowStatus(model.Status) == Domain.BorrowStatus.Rejected)
                await SendBookNotApproveEmail(model);


            await ModifyBookRequest(model.RequestId, model.Status);
        }
        public BorrowStatus GetBorrowStatus(string status)
        {
            var dict = new Dictionary<string, BorrowStatus> {
                { "Reject", BorrowStatus.Rejected },
                { "Approve", BorrowStatus.Approved }
             };
            return dict[status];
        }

        public async Task ModifyBookRequest(int requestId, string status)
        {


            var bookReservation = await _dbContext.BookReservations.Where(r => r.Id == requestId).FirstOrDefaultAsync();
            bookReservation.Status = GetBorrowStatus(status);

            await _dbContext.SaveChangesAsync();
        }

        public async Task SendEmailForApprove(BookApproveViewModel model)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;

            string confirmationLink = _configuration.GetSection("Application:BookDetails").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { await _identityService.GetUserEmailAsync(model.RequesterId) },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("{{UserName}}", await _identityService.GetFirstName(model.RequesterId)),
                        new KeyValuePair<string, string>("{{ExpireDate}}", await GetExpiryDate(model.RequesterId, model.BookId)),
                        new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, model.BookId))
                    }
            };

            await _emailService.SendEmailForBookApproved(options);
        }

        public async Task SendBookNotApproveEmail(BookApproveViewModel model)
        {
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { await _identityService.GetUserEmailAsync(model.RequesterId) },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                    {
                        new KeyValuePair<string, string>("{{UserName}}", await _identityService.GetFirstName(model.RequesterId))
                }
            };

            await _emailService.SendEmailForBookNotApproved(options);
        }

        public async Task<string> GetExpiryDate(string requesterId, int bookId)
        {
            var bookLend = await _dbContext.BookLendings
                .FirstAsync(x => x.BookId == bookId && x.BorrowerId.Equals(requesterId));

            return bookLend.DueDate.Date.ToShortDateString();
        }

        public async Task<PaginatedList<BookApproveListViewModel>> GetApprovedBook(int currentpage, int pageSize)
        {
            var bookApproveList = await _dbContext.BookLendings
                              .Include(x => x.Book).Select(
                              lend => new BookApproveListViewModel
                              {
                                  BorrowerId = lend.BorrowerId,
                                  LendId = lend.Id,
                                  BookId = lend.Book.Id,
                                  Author = lend.Book.Author,
                                  Title = lend.Book.Title,
                                  DueDate = lend.DueDate,
                                  IssueDate = lend.IssuedDate
                              }).PaginatedListAsync(currentpage, pageSize);

            foreach (var y in bookApproveList.Items)
            {
                y.Borrower = await _identityService.GetUserEmailAsync(y.BorrowerId);
            }

            return bookApproveList;
        }
        public async Task<PaginatedList<BookApproveListViewModel>> GetApprovedBookForUser(int currentpage, int pageSize)
        {
            var bookApproveList = await _dbContext.BookLendings
                              .Include(x => x.Book).Select(lend => new BookApproveListViewModel
                              {
                                  BorrowerId = lend.BorrowerId,
                                  LendId = lend.Id,
                                  BookId = lend.Book.Id,
                                  Author = lend.Book.Author,
                                  Title = lend.Book.Title,
                                  DueDate = lend.DueDate,
                                  IssueDate = lend.IssuedDate
                              }).ToListAsync();

            foreach (var y in bookApproveList)
            {
                y.Borrower = await _identityService.GetUserEmailAsync(y.BorrowerId);
            }

            var result = bookApproveList
                .Where(x => x.BorrowerId.Equals(_currentUserService.UserId))
                .PageList(currentpage, pageSize);

            return result;
        }
    }
}
