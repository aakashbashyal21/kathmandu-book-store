using BookStore.Application.Common.Interfaces;
using BookStore.Domain.Common;
using BookStore.Domain.Entities;
using Domain.Entities;
using IdentityServer4.EntityFramework.Options;
using Infrastructure.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class BookDbContext : IdentityDbContext<Identity.ApplicationUser>, IApplicationDbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookStatus> BookStatus { get; set; }
        public DbSet<BookLending> BookLendings { get; set; }
        public DbSet<BookReservation> BookReservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
            modelBuilder.Entity<IdentityRole>().ToTable("Roles").HasKey(x => x.Id);
        }
    }
}
