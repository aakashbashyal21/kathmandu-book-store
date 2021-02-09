using BookStore.Domain.Entities;

namespace Domain.Entities
{
    public class BookStatus
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BookCount { get; set; }
        public Book Books { get; set; }
    }
}