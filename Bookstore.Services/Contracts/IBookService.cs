using Bookstore.Services.DTOs;
using Bookstore.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Services.Contracts
{
    public interface IBookService : IDisposable
    {
        ServiceMessage Add(BookAddDTO bookAddDTO);

        DataServiceMessage<IEnumerable<BookDisplayDTO>> GetAll();
    }
}
