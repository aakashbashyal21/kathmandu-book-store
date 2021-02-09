using BookStore.Domain;
using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using System;

namespace Domain.Entities
{
    public class BookReservation
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public BorrowStatus Status { get; set; }
        public int BookId { get; set; }
        public string RequesterId { get; set; }
        public Book Book { get; set; }
    }
}