using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.ViewModel
{
    public class BookApproveViewModel
    {
        public int RequestId { get; set; }
        public int BookId { get; set; }
        public string RequesterId { get; set; }
        public string RequesterName { get; set; }
    }
}
