using BookStore.Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace BookStore.Domain.Entities
{
    public class Book: AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int Pages { get; set; }
        public string CoverImageUrl { get; set; }
        public BookStatus BookStatus { get; set; }
    }
}