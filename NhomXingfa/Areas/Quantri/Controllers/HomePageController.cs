using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    public class HomePageController : Controller
    {
        // GET: Quantri/HomePage
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult testing()
        {
            return View();
        }
    }
}