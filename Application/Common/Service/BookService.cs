using BookStore.Application.Common.Interfaces;
using BookStore.Application.Common.Mappings;
using BookStore.Application.Common.Model;
using BookStore.Application.ViewModel;
using BookStore.Domain.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Service
{
    public class BookService : IBookService
    {
        private ICurrentUserService _currentUserService;
        readonly IApplicationDbContext _dbContext;
        public BookService(IApplicationDbContext dbContext,
            ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
            _dbContext = dbContext;
        }
        public async Task<int> AddNewBook(BookCreateViewModel model)
        {
            var newBook = new Book()
            {
                Author = model.Author,
                Genre = model.Genre,
                Title = model.Title,
                Language = model.Language,
                Pages = (int)model.TotalPages,
                Description = model.Description,
                CoverImageUrl = model.CoverImageUrl
            };
            var bookStatus = new BookStatus() { BookCount = model.BookCount };
            newBook.BookStatus = bookStatus;

            await _dbContext.Books.AddAsync(newBook);
            await _dbContext.SaveChangesAsync();

            return newBook.Id;
        }

        public async Task DeleteBook(int bookId)
        {
            _dbContext.Books.Remove(await _dbContext.Books.Where(x => x.Id == bookId)
                             .Include(i => i.BookStatus).FirstOrDefaultAsync());
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PaginatedList<BookListViewModel>> GetAllBooks(int pageNumber, int pageSize)
        {
            var books = await _dbContext.Books
                .Include(i => i.BookStatus).Select(book => new BookListViewModel()
                {
                    BookCount = book.BookStatus.BookCount,
                    Author = book.Author,
                    Genre = book.Genre,
                    Description = book.Description,
                    Id = book.Id,
                    Language = book.Language,
                    Title = book.Title,
                    TotalPages = book.Pages,
                    CoverImageUrl = book.CoverImageUrl
                }).PaginatedListAsync(pageNumber, pageSize);
            return books;
        }

        public async Task<BookEditViewModel> GetBookById(int bookId)
        {
            return await _dbContext.Books.Where(x => x.Id == bookId)
                            .Include(i => i.BookStatus)
                            .Select(book => new BookEditViewModel()
                            {
                                BookCount = book.BookStatus.BookCount,
                                Author = book.Author,
                                Genre = book.Genre,
                                Description = book.Description,
                                Id = book.Id,
                                Language = book.Language,
                                Title = book.Title,
                                TotalPages = book.Pages,
                                ExistingImageUrl = book.CoverImageUrl
                            }).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateBook(int bookId, BookEditViewModel model)
        {
            var book = await _dbContext.Books.Where(x => x.Id == bookId).FirstOrDefaultAsync();
            book.Author = model.Author;
            book.Genre = model.Genre;
            book.Title = model.Title;
            book.Language = model.Language;
            book.Pages = (int)model.TotalPages;
            book.Description = model.Description;
            book.CoverImageUrl = model.ExistingImageUrl;
            await _dbContext.SaveChangesAsync();
            return book.Id;
        }

        public async Task<string> GetCoverPath(int bookId)
        {
            return await _dbContext.Books.Where(x => x.Id == bookId).Select(x => x.CoverImageUrl)
                .FirstOrDefaultAsync();
        }

        public async Task RequestForBorrow(int bookId, string userId)
        {
            var newRequest = new BookReservation()
            {
                RequesterId = userId,
                BookId = bookId,
                Status = Domain.BorrowStatus.Requested,
                RequestDate = DateTime.UtcNow
            };
            await _dbContext.BookReservations.AddAsync(newRequest);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckIfBorrowed(int bookId, string userId)
        {

            if (await _dbContext.BookReservations
                .AnyAsync(x => x.BookId == bookId && x.RequesterId.Equals(userId) && x.Status != Domain.BorrowStatus.Approved)) return true;

            else return false;
        }
    }
}
