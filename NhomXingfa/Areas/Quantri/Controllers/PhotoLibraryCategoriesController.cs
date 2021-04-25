using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Areas.Quantri.Utilities;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    [AuthorizeCustom(Roles = "Admin")]
    public class PhotoLibraryCategoriesController : Controller
    {
        private XingFaEntities db = new XingFaEntities();

        // GET: Quantri/PhotoLibraryCategories
        public ActionResult Index()
        {
            return View(db.PhotoLibraryCategories.ToList());
        }

        // GET: Quantri/PhotoLibraryCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoLibraryCategory photoLibraryCategory = db.PhotoLibraryCategories.Find(id);
            if (photoLibraryCategory == null)
            {
                return HttpNotFound();
            }
            return View(photoLibraryCategory);
        }

        // GET: Quantri/PhotoLibraryCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quantri/PhotoLibraryCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LibraryName,LibraryDesc,Created")] PhotoLibraryCategory photoLibraryCategory)
        {
            if (ModelState.IsValid)
            {
                photoLibraryCategory.Created = DateTime.Now;
                db.PhotoLibraryCategories.Add(photoLibraryCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(photoLibraryCategory);
        }

        // GET: Quantri/PhotoLibraryCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoLibraryCategory photoLibraryCategory = db.PhotoLibraryCategories.Find(id);
            if (photoLibraryCategory == null)
            {
                return HttpNotFound();
            }
            return View(photoLibraryCategory);
        }

        // POST: Quantri/PhotoLibraryCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LibraryName,LibraryDesc,Created")] PhotoLibraryCategory photoLibraryCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(photoLibraryCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(photoLibraryCategory);
        }

        // GET: Quantri/PhotoLibraryCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoLibraryCategory photoLibraryCategory = db.PhotoLibraryCategories.Find(id);
            if (photoLibraryCategory == null)
            {
                return HttpNotFound();
            }
            return View(photoLibraryCategory);
        }

        // POST: Quantri/PhotoLibraryCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhotoLibraryCategory photoLibraryCategory = db.PhotoLibraryCategories.Find(id);
            db.PhotoLibraryCategories.Remove(photoLibraryCategory);
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
