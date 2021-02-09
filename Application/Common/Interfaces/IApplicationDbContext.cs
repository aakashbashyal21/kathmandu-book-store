using BookStore.Domain.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookStatus> BookStatus { get; set; }
        public DbSet<BookLending> BookLendings { get; set; }
        public DbSet<BookReservation> BookReservations { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));    
            
    }
}
 