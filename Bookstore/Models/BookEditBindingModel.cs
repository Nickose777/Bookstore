using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bookstore.Models
{
    public class BookEditBindingModel
    {
        public string EncryptedId { get; set; }

        [Display(Name = "Book's title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Book's price")]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [Display(Name = "Book's author")]
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }
    }
}