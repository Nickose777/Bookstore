using Bookstore.Models;
using Bookstore.Services.Contracts;
using Bookstore.Services.DTOs;
using Bookstore.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService service;

        public BookController(IBookService service)
        {
            this.service = service;
        }

        public ActionResult List()
        {
            ActionResult result = null;

            DataServiceMessage<IEnumerable<BookDisplayDTO>> serviceMessage = service.GetAll();
            switch (serviceMessage.ActionState)
            {
                case ActionState.Empty:
                case ActionState.NotFound:
                    result = new EmptyResult();
                    break;
                case ActionState.Success:
                    result = View("List", serviceMessage.Data);
                    break;
                case ActionState.Exception:
                    result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, serviceMessage.Message);
                    break;
            }

            return result;
        }

        public ActionResult New()
        {
            return View("Create");
        }

        public ActionResult Create(BookAddBindingModel book)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            BookAddDTO bookDTO = new BookAddDTO
            {
                Title = book.Title,
                Price = book.Price,
                Author = book.Author
            };
            ServiceMessage serviceMessage = service.Add(bookDTO);

            ActionResult result = null;

            switch (serviceMessage.ActionState)
            {
                case ActionState.Empty:
                    result = new EmptyResult();
                    break;
                case ActionState.Success:
                    result = RedirectToAction("Index", "Book");
                    break;
                case ActionState.Exception:
                    result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, serviceMessage.Message);
                    break;
            }

            return result;
        }

        [HttpGet]
        public ActionResult Get(string id)
        {
            DataServiceMessage<BookEditDTO> serviceMessage = service.Get(id);

            ActionResult result = null;

            switch (serviceMessage.ActionState)
            {
                case ActionState.Empty:
                    result = new EmptyResult();
                    break;
                case ActionState.Success:
                    BookEditBindingModel book = new BookEditBindingModel
                    {
                        EncryptedId = serviceMessage.Data.EncryptedId,
                        Title = serviceMessage.Data.Title,
                        Price = serviceMessage.Data.Price,
                        Author = serviceMessage.Data.Author
                    };
                    result = PartialView("Edit", book);
                    break;
                case ActionState.NotFound:
                    result = new HttpNotFoundResult(serviceMessage.Message);
                    break;
                case ActionState.Exception:
                    result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, serviceMessage.Message);
                    break;
                default:
                    break;
            }

            return result;
        }

        public ActionResult Edit(BookEditBindingModel book)
        {
            ActionResult result = null;

            if (ModelState.IsValid)
            {
                BookEditDTO bookEditDTO = new BookEditDTO
                {
                    EncryptedId = book.EncryptedId,
                    Author = book.Author,
                    Title = book.Title,
                    Price = book.Price
                };

                ServiceMessage serviceMessage = service.Edit(bookEditDTO);

                switch (serviceMessage.ActionState)
                {
                    case ActionState.Empty:
                        result = new EmptyResult();
                        break;
                    case ActionState.Success:
                        result = PartialView(book);
                        break;
                    case ActionState.NotFound:
                    case ActionState.Exception:
                        result = Content(serviceMessage.Message);
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
    }
}