using System;
using Bookstore.Data.Contracts.Repositories;

namespace Bookstore.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }

        void Commit();
    }
}
