using Bookstore.Core.Entities;
using Bookstore.Data.Contracts.Repositories;

namespace Bookstore.Data.Repositories
{
    class BookRepository : RepositoryBase<BookEntity>, IBookRepository
    {
        public BookRepository(BookstoreDbContext context)
            : base(context) { }
    }
}
