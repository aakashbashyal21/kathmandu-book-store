using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using System;

namespace Domain.Entities
{
    public class BookLending : AuditableEntity
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Remarks { get; set; }
        public DateTime IssuedDate { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string BorrowerId { get; set; }
    }
}