namespace Bookstore.Services.DTOs
{
    public class BookDisplayDTO
    {
        public string EnryptedId { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Author { get; set; }
    }
}
