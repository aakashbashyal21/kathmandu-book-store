using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappings;
using BookStore.Application.Common.Model;
using BookStore.Application.ViewModel;
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
                    .Include(x => x.Book).Select(
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

            await ModifyBookRequest(model.RequestId);

            await _dbContext.BookLendings.AddAsync(bookApprove);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ModifyBookRequest(int requestId)
        {
            var bookReservation = await _dbContext.BookReservations.FirstAsync(r => r.Id == requestId);
            bookReservation.Status = Domain.BorrowStatus.Approved;

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

        public async Task<string> GetExpiryDate(string requesterId, int bookId)
        {
            var bookLend = await _dbContext.BookLendings
                .FirstAsync(x => x.BookId == bookId && x.BorrowerId.Equals(requesterId));

            return bookLend.DueDate.Date.ToShortDateString();
        }
    }
}
