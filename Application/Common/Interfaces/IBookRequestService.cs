using BookStore.Application.Common.Model;
using BookStore.Application.ViewModel;
using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IBookRequestService
    {
        Task<PaginatedList<BookRequestViewModel>> GetRequestedBookList(int pageNumber, int pageSize);
        Task ApproveBook(BookApproveViewModel model);
        //Task DisapproveBook(BookApproveViewModel model);
        Task ModifyBookRequest(int requestId, string status);
        Task SendEmailForApprove(BookApproveViewModel model);
        Task<string> GetExpiryDate(string requesterId, int bookId);
        Task<PaginatedList<BookApproveListViewModel>> GetApprovedBook(int currentpage, int pageSize);
        Task<PaginatedList<BookApproveListViewModel>> GetAllBookForUser(int currentpage, int pageSize);
        Task<PaginatedList<BookApproveListViewModel>> GetApprovedBookForUser(int currentpage, int pageSize);
        Task ReturnBook(BookReturnViewModel model);
    }
}
