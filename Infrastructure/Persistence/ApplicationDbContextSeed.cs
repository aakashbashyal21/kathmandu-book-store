using BookStore.Domain.Entities;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<Identity.ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var readerRole = new IdentityRole("Reader");

            if (roleManager.Roles.All(r => r.Name != readerRole.Name))
            {
                await roleManager.CreateAsync(readerRole);
            }

            var administrator = new Identity.ApplicationUser
            {
                Id = "b2c3196c-fbe7-473d-9a0f-b491f2918a6c",
                UserName = "administrator@localhost",
                Email = "administrator@localhost",
                FirstName = "admin",
                LastName = "localhost",
                EmailConfirmed = true
            };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }

        }
        public static async Task SeedSampleDataAsync(BookDbContext context)
        {
            try
            {
                if (!context.Books.Any())
                {
                    var book = new Book
                    {
                        Title = "War and Peace",
                        Author = "Leo Tolstoy",
                        Language = "Russian",
                        Genre = "Novel (Historical novel)",
                        Pages = 1225,

                        Description = "Anna Karenina is a novel by the Russian author Leo Tolstoy, " +
                        "first published in book form in 1878. Many writers consider Anna Karenina the " +
                        "greatest work of literature ever, and Tolstoy himself called it his first true novel",

                        CoverImageUrl = "books/cover/930eabb0-64a8-4425-bbf2-555053f087ea_Tolstoy_-_War_and_Peace_-_first_edition,_1869.jpg",

                        CreatedBy = "b2c3196c-fbe7-473d-9a0f-b491f2918a6c",
                        Created = DateTime.UtcNow

                    };


                    book.BookStatus = new BookStatus
                    {
                        BookCount = 25
                    };

                    context.Books.Add(book);


                    await context.SaveChangesAsync();
                }
            }
            // Seed, if necessary
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
