using BookStore.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.ViewModel
{
    public class BookRequestViewModel
    {
        public int Id { get; set; }
        public BorrowStatus Status { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Requester { get; set; }
        public string RequesterId { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
