using BookStore.Application.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);

        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions);

        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);

        Task SendEmailForBookApproved(UserEmailOptions userEmailOptions);
    }
}
