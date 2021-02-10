using BookStore.Application.Common.Model;
using BookStore.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IBookService
    {
        Task<string> GetCoverPath(int id);
        Task<PaginatedList<BookListViewModel>> GetAllBooks(int pageNumber, int pageSize);
        Task<int> AddNewBook(BookCreateViewModel book);
        Task<int> UpdateBook(int bookId, BookEditViewModel book);
        Task<BookEditViewModel> GetBookById(int bookId);
        Task DeleteBook(int bookId);

        Task RequestForBorrow(int bookId, string userId);

        Task<bool> CheckIfBorrowed(int bookId, string userId);

    }
}
