using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Services.DTOs
{
    public class BookEditDTO
    {
        public string EncryptedId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Author { get; set; }
    }
}
