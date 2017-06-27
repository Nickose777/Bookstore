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

        public ServiceMessage Add(BookAddDTO bookAddDTO)
        {
            ActionState actionState = ActionState.Empty;
            string message = String.Empty;

            if (Validate(bookAddDTO, ref actionState, ref message))
            {
                try
                {
                    BookEntity bookEntity = new BookEntity
                    {
                        Title = bookAddDTO.Title,
                        Price = bookAddDTO.Price,
                        Author = bookAddDTO.Author
                    };

                    unitOfWork.Books.Add(bookEntity);
                    unitOfWork.Commit();

                    actionState = ActionState.Success;
                }
                catch (Exception ex)
                {
                    actionState = ActionState.Exception;
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                }
            }

            return new ServiceMessage(actionState, message);
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

                bool anyBooks = data.Count() != 0;
                actionState = anyBooks ? ActionState.Success : ActionState.NotFound;
                message = anyBooks ? "Got all books" : "No books found";
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

        private bool Validate(BookAddDTO bookAddDTO, ref ActionState actionState, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(bookAddDTO.Title))
            {
                isValid = false;
                actionState = ActionState.Exception;
                message = "Title cannot be empty";
            }
            else if (String.IsNullOrEmpty(bookAddDTO.Author))
            {
                isValid = false;
                actionState = ActionState.Exception;
                message = "Author cannot be empty";
            }
            else if (bookAddDTO.Price < 0)
            {
                isValid = false;
                actionState = ActionState.Exception;
                message = "Price must be positive";
            }

            return isValid;
        }
    }
}
