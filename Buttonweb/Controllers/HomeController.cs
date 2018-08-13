using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Buttonweb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
        
        [HttpPost]
        public ActionResult CreateOrder(string txtOrder, string api_key)
        {
            try
            {
                UseButton.API.CreateOrder(txtOrder, api_key);
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = this.ViewData
                };
            }

            return View("Index");
        }

        [HttpGet]
        public ActionResult GetOrder(string txtButtonOrderID, string api_key)
        {
            try
            {
                ViewData["ButtonOrder"] = UseButton.API.GetOrder(txtButtonOrderID, api_key);
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = this.ViewData
                };
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult UpdateOrder(string txtUpdateOrder, string txtUpdateButtonOrderID, string api_key)
        {
            try
            {
                ViewData["UpdatedOrder"] = UseButton.API.UpdateOrder(txtUpdateOrder, txtUpdateButtonOrderID, api_key);
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = this.ViewData
                };
            }

            return View("Index");
        }

        [HttpPost]
        public ActionResult DeleteOrder(string txtButtonOrderID, string api_key)
        {
            try
            {
                ViewData["DeleteButtonOrder"] = UseButton.API.DeleteOrder(txtButtonOrderID, api_key);
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return new ViewResult
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = this.ViewData
                };
            }

            return View("Index");
        }
    }
}