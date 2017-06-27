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
    public class HomeController : Controller
    {
        private readonly IBookService service;

        public HomeController(IBookService service)
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}