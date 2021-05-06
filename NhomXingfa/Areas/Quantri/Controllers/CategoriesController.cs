using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NhomXingfa.Areas.Quantri.Models.DataModels;
using PagedList;
using NhomXingfa.Areas.Quantri.Utilities;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    public class CategoriesController : BaseController
    {
        private XingFaEntities db = new XingFaEntities();

        // GET: Quantri/Categories
        [Authorize]
        public ActionResult Index()
        {
            ViewData["ListCate"] = db.Categories.Where(c => c.TypeCate == WebConstants.CategoryProduct && c.Parent == 0).ToList();
            return View(db.Categories.ToList());
        }

        public ActionResult _PartialIndex(int? pageNumber, int? pageSize, string TenChungLoai, int? DanhMucCha, string IsLe,
                                          string SEOKeywords)
        {
            TenChungLoai = TenChungLoai ?? "";
            ViewBag.TenChungLoai = TenChungLoai;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 10;

            if (pageSize == -1)
            {
                pageSize = db.Categories.Where(c => c.TypeCate == WebConstants.CategoryProduct).ToList().Count;
            }
            ViewBag.PageSize = pageSize;

            var lstCates = db.Categories.Where(c => c.TypeCate == WebConstants.CategoryProduct).ToList();
            if (!string.IsNullOrEmpty(TenChungLoai))
            {
                lstCates = lstCates.Where(s => s.CategoryName.ToUpper().Contains(TenChungLoai.ToUpper())).ToList();
            }
            ViewBag.TenChungLoai = TenChungLoai;

            if (!string.IsNullOrEmpty(DanhMucCha.ToString()))
            {
                lstCates = lstCates.Where(s => s.Parent == DanhMucCha).ToList();
            }
            ViewBag.DanhMucCha = DanhMucCha;

            if (!string.IsNullOrEmpty(IsLe.ToString()))
            {
                lstCates = lstCates.Where(s => s.IsLe == Convert.ToBoolean(IsLe)).ToList();
            }
            ViewBag.DanhMucCha = DanhMucCha;

            if (!string.IsNullOrEmpty(SEOKeywords))
            {
                lstCates = lstCates.Where(s => s.SEOKeywords.ToUpper().Contains(SEOKeywords.ToUpper())).ToList();
            }
            ViewBag.SEOKeywords = SEOKeywords;

            lstCates = lstCates.OrderBy(s => s.Sort).ToList();
            ViewBag.STT = pageNumber * pageSize - pageSize + 1;
            int count = lstCates.ToList().Count();
            ViewBag.TotalRow = count;
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/Quantri/Views/Categories/_PartialIndex.cshtml", lstCates.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
            }
            return View(lstCates.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
        }

        // GET: Quantri/Categories/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Quantri/Categories/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewData["ListCate"] = db.Categories.Where(c => c.TypeCate == WebConstants.CategoryProduct && c.Parent == 0).ToList();
            return View();
        }

        // POST: Quantri/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,Parent,DisplayMenu,IsActive,Sort,TypeCate,SEOTitle,SEOUrlRewrite,SEOKeywords,SEOMetadescription,IsLe")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.Parent = 0;
                category.TypeCate = WebConstants.CategoryProduct;
                category.SEOUrlRewrite = Helpers.ConvertToUpperLower(category.CategoryName);
                if (category.SEOKeywords == null)
                    category.SEOKeywords = " ";
                db.Categories.Add(category);
                db.SaveChanges();
                Success(string.Format("Thêm mới <b>{0}</b> thành công.", category.CategoryName), true);
                //return RedirectToAction("Index");
                return RedirectToAction("Edit", "Categories", new { id = category.CategoryID, Area = "Quantri" });
            }

            return View(category);
        }

        // GET: Quantri/Categories/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewData["ListCate"] = db.Categories.Where(c => c.TypeCate == WebConstants.CategoryProduct && c.Parent == 0).ToList();
            return View(category);
        }

        // POST: Quantri/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,Parent,DisplayMenu,IsActive,Sort,TypeCate,SEOTitle,SEOUrlRewrite,SEOKeywords,SEOMetadescription, IsLe")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.TypeCate = WebConstants.CategoryProduct;
                category.SEOUrlRewrite = Helpers.ConvertToUpperLower(category.CategoryName);
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                Success(string.Format("Sửa <b>{0}</b> thành công. <b>{0}</b>.", category.CategoryName), true);
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "Categories", new { Area = "Quantri" });
            }
            return View(category);
        }

        // GET: Quantri/Categories/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Quantri/Categories/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            var lstprod = db.Products.Where(a => a.CategoryID == id).ToList();
            if(lstprod ==null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }  
            else
            {
                foreach(var i in lstprod)
                {
                    Product product = db.Products.Find(i.ProductID);
                   
                    var prodImage = db.ProductImages.Where(a => a.ProductID == i.ProductID).ToList();
                    var prodGroup = db.ProductGroups.Where(a => a.ProductID == i.ProductID).ToList();
                    var prodCart = db.CartDetails.Where(a => a.ProductID == i.ProductID).ToList();
                    if (prodImage == null && prodGroup == null && prodCart == null)
                    {

                        db.Products.Remove(product);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (prodGroup != null)
                    {
                        foreach (var j in prodGroup)
                        {
                            ProductGroup proGro = db.ProductGroups.Find(j.ProductGroupID);
                            db.ProductGroups.Remove(proGro);
                        }
                    }
                    else if (prodCart != null)
                    {
                        foreach (var k in prodCart)
                        {
                            CartDetail cdetail = db.CartDetails.Find(k.Id);
                            Cart c = db.Carts.Find(cdetail.CarID);
                            db.Carts.Remove(c);
                            db.CartDetails.Remove(cdetail);
                        }
                    }
                    else
                    {
                        foreach (var l in prodImage)
                        {
                            ProductImage prImage = db.ProductImages.Find(l.ImageID);
                            db.ProductImages.Remove(prImage);
                        }

                    }
                    db.Products.Remove(product);
                    db.SaveChanges();
                }    
            }    
            db.Categories.Remove(category);
            db.SaveChanges();
            Danger(string.Format(" Đã xóa thành công.", ""), true);
            return RedirectToAction("Index", "Categories");
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
