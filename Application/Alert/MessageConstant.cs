using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Common
{


    public static class AlertMessage
    {
        public const string BookRequestSuccess = "Book borrow request has been done, An email will be send after the book has been approved.";
        public const string DuplicateBookRequest = "Book Request has been done already, Please wait for the book request to be approved.";
        public const string ApproveBookRequest = "Book Request has been aprove, User has been sent an email.";
        public const string DisApproveBookRequest = "Book Request has been disapprove, User has been sent an email.";


        public const string Error = "Error occured while completing your request.";
    }

}
