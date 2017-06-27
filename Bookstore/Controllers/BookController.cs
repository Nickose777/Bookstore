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

        public ActionResult Index()
        {
            ActionResult result = null;

            DataServiceMessage<IEnumerable<BookDisplayDTO>> serviceMessage = service.GetAll();
            switch (serviceMessage.ActionState)
            {
                case ActionState.Empty:
                    result = new EmptyResult();
                    break;
                case ActionState.Success:
                    result = View(serviceMessage.Data);
                    break;
                case ActionState.NotFound:
                    result = HttpNotFound();
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
    }
}