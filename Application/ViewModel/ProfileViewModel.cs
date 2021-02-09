using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Application.ViewModel
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }
        [Display(Name = "First Name")]

        public string FirstName { get; set; }
        [Display(Name = "Last Name")]

        public string LastName { get; set; }
        [Display(Name = "Date Of Birth")]

        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Phone Number")]

        public string PhoneNumber { get; set; }
    }
}
