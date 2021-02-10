using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.ViewModel
{
    public class BookApproveListViewModel : BookApproveViewModel
    {
        public int LendId { get; set; }
        public string Author { get; set; }

        public string Title { get; set; }

        public string BorrowerId { get; set; }

        public DateTime IssueDate { get; set; }
    }
}
