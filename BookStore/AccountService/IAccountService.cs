using BookStore.Application.Common.Model;
using BookStore.Application.ViewModel;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreUI.AccountService
{
    public interface IAccountService
    {
        Task<ApplicationUser> GetUserByIdAsync(string email);

        Task<ApplicationUser> GetUserByEmailAsync(string email);

        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);

        Task<IdentityResult> UpdateUserAsync(ProfileViewModel userModel);


        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);

        Task SignOutAsync();

        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);

        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);

        Task GenerateEmailConfirmationTokenAsync(ApplicationUser user);

        Task GenerateForgotPasswordTokenAsync(ApplicationUser user);

        Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model);

        Task<IdentityResult> AddRoleUserAsync(SignUpUserModel user, string role);
    }
}
