using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.ViewModel
{
    public class BookReturnViewModel
    {
        public int RequestId { get; set; }
        public int BookId { get; set; }
        public string BorrowerId { get; set; }
        public string Borrower { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
    }
}
