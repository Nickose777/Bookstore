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
                    message = "Added book";
                }
                catch (Exception ex)
                {
                    actionState = ActionState.Exception;
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                }
            }

            return new ServiceMessage(actionState, message);
        }

        public ServiceMessage Edit(BookEditDTO bookEditDTO)
        {
            ActionState actionState = ActionState.Empty;
            string message = String.Empty;

            if (Validate(bookEditDTO, ref actionState, ref message))
            {
                try
                {
                    int id = Convert.ToInt32(bookEditDTO.EncryptedId);
                    BookEntity bookEntity = unitOfWork.Books.Get(id);

                    if (bookEntity != null)
                    {
                        bookEntity.Title = bookEditDTO.Title;
                        bookEntity.Price = bookEditDTO.Price;
                        bookEntity.Author = bookEditDTO.Author;

                        unitOfWork.Commit();

                        actionState = ActionState.Success;
                        message = "Edited book";
                    }
                    else
                    {
                        actionState = ActionState.NotFound;
                        message = "Book was not found";
                    }
                }
                catch (Exception ex)
                {
                    actionState = ActionState.Exception;
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                }
            }

            return new ServiceMessage(actionState, message);
        }

        public DataServiceMessage<BookEditDTO> Get(string encryptedId)
        {
            ActionState actionState = ActionState.Empty;
            string message = String.Empty;
            BookEditDTO data = null;

            try
            {
                int id = Convert.ToInt32(encryptedId);
                BookEntity bookEntity = unitOfWork.Books.Get(id);

                if (bookEntity != null)
                {
                    data = new BookEditDTO
                    {
                        EncryptedId = encryptedId,
                        Title = bookEntity.Title,
                        Price = bookEntity.Price,
                        Author = bookEntity.Author
                    };
                    actionState = ActionState.Success;
                    message = "Got book";
                }
                else
                {
                    actionState = ActionState.NotFound;
                    message = "Book was not found";
                }
            }
            catch (Exception ex)
            {
                actionState = ActionState.Exception;
                message = ExceptionMessageBuilder.BuildMessage(ex);
            }

            return new DataServiceMessage<BookEditDTO>(actionState, message, data);
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

        private bool Validate(BookAddDTO book, ref ActionState actionState, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(book.Title))
            {
                isValid = false;
                actionState = ActionState.Exception;
                message = "Title cannot be empty";
            }
            else if (String.IsNullOrEmpty(book.Author))
            {
                isValid = false;
                actionState = ActionState.Exception;
                message = "Author cannot be empty";
            }
            else if (book.Price < 0)
            {
                isValid = false;
                actionState = ActionState.Exception;
                message = "Price must be positive";
            }

            return isValid;
        }

        private bool Validate(BookEditDTO book, ref ActionState actionState, ref string message)
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(book.Title))
            {
                isValid = false;
                actionState = ActionState.Exception;
                message = "Title cannot be empty";
            }
            else if (String.IsNullOrEmpty(book.Author))
            {
                isValid = false;
                actionState = ActionState.Exception;
                message = "Author cannot be empty";
            }
            else if (book.Price < 0)
            {
                isValid = false;
                actionState = ActionState.Exception;
                message = "Price must be positive";
            }

            return isValid;
        }
    }
}
