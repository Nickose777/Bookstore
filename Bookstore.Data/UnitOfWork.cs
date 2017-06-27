using Bookstore.Data.Contracts;
using Bookstore.Data.Contracts.Repositories;
using Bookstore.Data.Repositories;

namespace Bookstore.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookstoreDbContext context;

        public UnitOfWork()
        {
            this.context = new BookstoreDbContext();
        }

        public IBookRepository Books
        {
            get { return new BookRepository(context); }
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
