using BookStore.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<string> GetUserEmailAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return $"{user.Email}";
        }
        public async Task<string> GetBorrowerFullName(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<string> GetFirstName(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return $"{user.FirstName}";
        }
    }

}
