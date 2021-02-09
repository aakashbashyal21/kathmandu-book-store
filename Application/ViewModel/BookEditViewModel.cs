using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStore.Application.ViewModel
{
    public class BookEditViewModel : BookCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingImageUrl { get; set; }
    }
}
