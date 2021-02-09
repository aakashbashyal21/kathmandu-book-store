using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Common.Model
{
    public class EmailConfirmModel
    {
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
        public bool EmailSent { get; set; }
        public bool EmailVerified { get; set; }
    }
}

