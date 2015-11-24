using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Phase1()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Phase2()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Phase3()
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