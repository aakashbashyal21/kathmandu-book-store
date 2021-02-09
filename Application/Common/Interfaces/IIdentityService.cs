using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserEmailAsync(string userId);
        Task<string> GetBorrowerFullName(string userId);

        Task<string> GetFirstName(string userId);
        // Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        //    Task<Result> DeleteUserAsync(string userId);
    }
}
