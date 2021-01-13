using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NhomXingfa.Areas.Quantri.Models.DataModels;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    public class SlidesController : Controller
    {
        private XingFaEntities db = new XingFaEntities();

        // GET: Quantri/Slides
        [Authorize]
        public ActionResult Index()
        {
            var slides = db.Slides.Include(s => s.User);
            return View(slides.ToList());
        }


        [Authorize]
        public ActionResult ForHomepage()
        {
            var model = db.Slides.Where(q => q.CategoryID == 0);
            return View(model);
        }

        [Authorize]
        public JsonResult UploadImage()
        {
            string result = "DONE";

            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string x = "";//file.FileName;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];

                            x = fname;
                        }
                        else
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var ext = Path.GetExtension(file.FileName);
                            string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                            //fname = file.FileName + "-" + DateTime.Now.ToString("HHmmssddMMyyyy");
                            fname = name + "_" + DateTime.Now.Millisecond + ext;

                            x = fname;
                        }

                        fname = Path.Combine(Server.MapPath("~/Photos/Slides/"), fname);
                        file.SaveAs(fname);

                        result = x;
                    }
                }
                else
                {
                    result = "FAIL";
                }
            }
            catch
            {
                result = "FAIL";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult CreateSlide(string urlimg, string slogan1, string slogan2, string title, string link, string target, int? sort)
        {
            string result = "FAIL";
            try
            {
                Slide s = new Slide();
                s.ImageURL = urlimg;
                s.Slogun1 = slogan1;
                s.Slogun2 = slogan2;
                s.SlideTitle = title;
                s.LinkBanner = link;
                s.LinkTarget = target;
                s.Sort = sort;
                s.CategoryID = 0;
                s.CreateBy = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).UserID;
                s.Created = DateTime.Now;
                s.IsActive = true;
                db.Slides.Add(s);

                db.SaveChanges();
                result = "DONE";
            }
            catch
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Quantri/Slides/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // GET: Quantri/Slides/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CreateBy = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Quantri/Slides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SlideID,ImageURL,Slogun1,Slogun2,SlideTitle,LinkBanner,LinkTarget,CategoryID,IsActive,Sort,Created,CreateBy")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                db.Slides.Add(slide);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreateBy = new SelectList(db.Users, "UserID", "UserName", slide.CreateBy);
            return View(slide);
        }

        // GET: Quantri/Slides/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreateBy = new SelectList(db.Users, "UserID", "UserName", slide.CreateBy);
            return View(slide);
        }

        // POST: Quantri/Slides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SlideID,ImageURL,Slogun1,Slogun2,SlideTitle,LinkBanner,LinkTarget,CategoryID,IsActive,Sort,Created,CreateBy")] Slide slide)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slide).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreateBy = new SelectList(db.Users, "UserID", "UserName", slide.CreateBy);
            return View(slide);
        }

        // GET: Quantri/Slides/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slide slide = db.Slides.Find(id);
            if (slide == null)
            {
                return HttpNotFound();
            }
            return View(slide);
        }

        // POST: Quantri/Slides/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slide slide = db.Slides.Find(id);
            db.Slides.Remove(slide);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
