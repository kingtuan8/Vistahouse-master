using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhomXingfa.Controllers
{
    public class Q_AsController : Controller
    {
        XingFaEntities db = new XingFaEntities();
        // GET: Q_A
        public ActionResult Index()
        {
            QAViewModel model = new QAViewModel();
            model.lstQA = db.Q_A.Where(a => a.IsActive == true).OrderBy(a => a.ThuTu).ToList();
            return View(model);
        }
    }
}