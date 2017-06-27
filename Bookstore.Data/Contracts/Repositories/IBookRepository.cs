using Bookstore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Data.Contracts.Repositories
{
    public interface IBookRepository : IRepository<BookEntity>
    {
    }
}
