using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Application.ViewModel
{
    public class BookCreateViewModel
    {
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the title of your book")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the author name")]
        public string Author { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the genre of the book")]
        public string Genre { get; set; }
        
        [Required]
        public string Language { get; set; }

        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total pages of book")]
        public int? TotalPages { get; set; }

        [Required(ErrorMessage = "Please enter the number of book")]
        [Display(Name = "Total Books in Stock")]
        public int BookCount { get; set; }

        [Display(Name = "Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

    }
}
