using Bookstore.Core.Entities;
using Bookstore.Data.Contracts;
using Bookstore.Services.Contracts;
using Bookstore.Services.DTOs;
using Bookstore.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Services.Providers
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork unitOfWork;

        public BookService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public DataServiceMessage<IEnumerable<BookDisplayDTO>> GetAll()
        {
            ActionState actionState = ActionState.Empty;
            string message = String.Empty;
            IEnumerable<BookDisplayDTO> data = null;

            try
            {
                IEnumerable<BookEntity> books = unitOfWork.Books.GetAll();
                data = books.Select(book =>
                new BookDisplayDTO
                {
                    EnryptedId = book.Id.ToString(),
                    Title = book.Title,
                    Price = book.Price,
                    Author = book.Author
                })
                .OrderBy(book => book.Title)
                .ToList();

                actionState = data.Count() != 0 ? ActionState.Success : ActionState.NotFound;
            }
            catch (Exception ex)
            {
                actionState = ActionState.Exception;
                message = ExceptionMessageBuilder.BuildMessage(ex);
            }

            return new DataServiceMessage<IEnumerable<BookDisplayDTO>>(actionState, message, data);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
