using BookStore.Application.Common.Model;
using BookStore.Application.ViewModel;
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
        Task ModifyBookRequest(int requestId);
        Task SendEmailForApprove(BookApproveViewModel model);

        Task<string> GetExpiryDate(string requesterId, int bookId);
    }
}
