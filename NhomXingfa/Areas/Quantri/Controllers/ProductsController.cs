﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Areas.Quantri.Utilities;
using PagedList;
using NhomXingfa.Areas.Quantri.Models;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    [AuthorizeCustom(Roles = "Admin")]
    public class ProductsController : BaseController
    {
        private XingFaEntities db = new XingFaEntities();

        // GET: Products

        #region List of product
        [Authorize]
        public ActionResult Index()
        {
            ViewData["ListCateParent"] = db.Categories.Where(c => c.Parent == 0 && c.TypeCate == WebConstants.CategoryProduct && c.IsLe == false).ToList();
            ViewData["ListCate"] = db.Categories.Where(c => c.Parent != 0 && c.TypeCate == WebConstants.CategoryProduct).ToList();
            return View();
        }
        public ActionResult _PartialIndex(int? pageNumber, int? pageSize, string MaSP, string SanPham, int? DanhMucCha, int? DanhMucCon,
                                          string SEOKeywords)
        {
            SanPham = SanPham ?? "";
            ViewBag.SanPham = SanPham;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 10;

            if (pageSize == -1)
            {
                pageSize = db.Products.Where(p => p.IsProduct == true).ToList().Count;
            }
            ViewBag.PageSize = pageSize;

            var lstprod = db.Products.Where(p => p.IsProduct == true).ToList();

            if (!string.IsNullOrEmpty(MaSP))
            {
                lstprod = lstprod.Where(s => s.ProductCode.ToUpper().Contains(MaSP.ToUpper())).ToList();
            }
            ViewBag.MaSP = MaSP;

            if (!string.IsNullOrEmpty(SanPham))
            {
                lstprod = lstprod.Where(s => s.ProductName.ToUpper().Contains(SanPham.ToUpper())).ToList();
            }
            ViewBag.SanPham = SanPham;

            if (!string.IsNullOrEmpty(DanhMucCha.ToString()))
            {
                lstprod = lstprod.Where(s => s.CategoryIDParent == DanhMucCha).ToList();
            }
            ViewBag.DanhMucCha = DanhMucCha;

            if (!string.IsNullOrEmpty(DanhMucCon.ToString()))
            {
                lstprod = lstprod.Where(s => s.CategoryID == DanhMucCon).ToList();
            }
            ViewBag.DanhMucCon = DanhMucCon;

            if (!string.IsNullOrEmpty(SEOKeywords))
            {
                lstprod = lstprod.Where(s => s.SEOKeywords.ToUpper().Contains(SEOKeywords.ToUpper())).ToList();
            }
            ViewBag.SEOKeywords = SEOKeywords;

            lstprod = lstprod.OrderBy(s => s.ThuTu).ToList();
            ViewBag.STT = pageNumber * pageSize - pageSize + 1;
            int count = lstprod.ToList().Count();
            ViewBag.TotalRow = count;
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/Quantri/Views/Products/_PartialIndex.cshtml", lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
            }
            return View(lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
        }
        #endregion
        #region Danh sách gói sản phẩm
        public ActionResult GoiSPLst()
        {
            ViewData["ListCateParent"] = db.Categories.Where(c => c.Parent == 0 && c.TypeCate == WebConstants.CategoryProduct && c.IsLe == false).ToList();
            ViewData["ListCate"] = db.Categories.Where(c => c.Parent != 0 && c.TypeCate == WebConstants.CategoryProduct).ToList();
            return View();
        }

        public ActionResult _PartialGoiSPLst(int? pageNumber, int? pageSize, string MaSP, string SanPham, int? DanhMucCha, int? DanhMucCon,
                                          string SEOKeywords)
        {
            SanPham = SanPham ?? "";
            ViewBag.SanPham = SanPham;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 10;

            if (pageSize == -1)
            {
                pageSize = db.Products.Where(p => p.IsProduct == false).ToList().Count;
            }
            ViewBag.PageSize = pageSize;

            var lstprod = db.Products.Where(p => p.IsProduct == false).ToList();

            if (!string.IsNullOrEmpty(MaSP))
            {
                lstprod = lstprod.Where(s => s.ProductCode.ToUpper().Contains(MaSP.ToUpper())).ToList();
            }
            ViewBag.MaSP = MaSP;

            if (!string.IsNullOrEmpty(SanPham))
            {
                lstprod = lstprod.Where(s => s.ProductName.ToUpper().Contains(SanPham.ToUpper())).ToList();
            }
            ViewBag.SanPham = SanPham;

            if (!string.IsNullOrEmpty(DanhMucCha.ToString()))
            {
                lstprod = lstprod.Where(s => s.CategoryIDParent == DanhMucCha).ToList();
            }
            ViewBag.DanhMucCha = DanhMucCha;

            if (!string.IsNullOrEmpty(DanhMucCon.ToString()))
            {
                lstprod = lstprod.Where(s => s.CategoryID == DanhMucCon).ToList();
            }
            ViewBag.DanhMucCon = DanhMucCon;

            if (!string.IsNullOrEmpty(SEOKeywords))
            {
                lstprod = lstprod.Where(s => s.SEOKeywords.ToUpper().Contains(SEOKeywords.ToUpper())).ToList();
            }
            ViewBag.SEOKeywords = SEOKeywords;

            lstprod = lstprod.OrderBy(s => s.ThuTu).ToList();
            ViewBag.STT = pageNumber * pageSize - pageSize + 1;
            int count = lstprod.ToList().Count();
            ViewBag.TotalRow = count;
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/Quantri/Views/Products/_PartialGoiSPLst.cshtml", lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
            }
            return View(lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
        }
        #endregion
        // GET: Quantri/Products/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Quantri/Products/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoryIDParent = new SelectList(db.Categories.Where(c => c.Parent == 0 && c.TypeCate == WebConstants.CategoryProduct), "CategoryID", "CategoryName");
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.TypeCate == WebConstants.CategoryProduct && c.IsLe == true), "CategoryID", "CategoryName");
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Quantri/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ProductID,ProductCode,ProductName,IsProduct,Capacity,Price,PricePhanTram,PriceSale,Capacity1,Price1,PricePhanTram1,PriceSale1," +
            "CategoryIDParent,CategoryID,Images,ImagesThumb,Images1, ImagesThumb1, ShortDescription,Content,InStock,IsSale,IsNew,Rating,IsActive,CountView,Created,CreatedBy,SEOTitle,SEOUrlRewrite,SEOKeywords,SEOMetadescription," +
            "SoLuongChai,PhiShip,SoNgayShip,ThuTu")] Product product,
                                   HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh1)
        {
            if (ModelState.IsValid)
            {

                var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (HinhAnh == null)
                {
                    product.Images = "NoImage.png";
                }
                else
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);

                    var ext = Path.GetExtension(HinhAnh.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + DateTime.Now.Millisecond + ext; //appending the name with id  
                        var path = Path.Combine(Server.MapPath(WebConstants.ImgProduct), myfile);                                              // store the file inside ~/project folder(Img)  

                        //var path = Path.Combine(Server.MapPath(WebConstants.ImgProduct), myfile);
                        //var dir = Directory.CreateDirectory(path);
                        //HinhAnh.SaveAs(Path.Combine(path, myfile));

                        product.Images = myfile;
                        HinhAnh.SaveAs(path);

                        product.ImagesThumb = CreateThumb(myfile);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }
                if (HinhAnh1 == null)
                {
                    product.Images1 = "NoImage.png";
                }
                else
                {
                    var fileName1 = Path.GetFileName(HinhAnh1.FileName);

                    var ext = Path.GetExtension(HinhAnh1.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName1); //getting file name without extension  
                        string myfile = name + "_" + DateTime.Now.Millisecond + ext; //appending the name with id  
                                                                                     // store the file inside ~/project folder(Img)  
                        var path = Path.Combine(Server.MapPath(WebConstants.ImgProduct), myfile);

                        //var dir = Directory.CreateDirectory(path);
                        //HinhAnh.SaveAs(Path.Combine(path, myfile));

                        product.Images1 = myfile;
                        HinhAnh1.SaveAs(path);

                        product.ImagesThumb1 = CreateThumb(myfile);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }
                product.CategoryIDParent = product.CategoryID;
                //product.IsProduct = true;


                product.SEOUrlRewrite = Helpers.ConvertToUpperLower(product.ProductName);
                product.Created = DateTime.Now;
                product.CreatedBy = db.Users.FirstOrDefault(q => q.UserName == User.Identity.Name).UserID;
                //product.CreatedBy = 1;
                db.Products.Add(product);
                db.SaveChanges();
                Success(string.Format("Thêm mới sản phẩm<b>{0}</b> thành công.", product.ProductName), true);
                return RedirectToAction("Details", "Products", new { id = product.ProductID });
            }

            ViewBag.CategoryIDParent = new SelectList(db.Categories.Where(c => c.Parent == 0), "CategoryID", "CategoryName", product.CategoryIDParent);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", product.CreatedBy);
            Success(string.Format("Thêm mới sản phẩm<b>{0}</b> thành công.", product.ProductName), true);
            //return RedirectToAction("Index");
            return View(product);
        }

        // GET: Quantri/Products/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryIDParent = new SelectList(db.Categories.Where(c => c.Parent == 0 && c.TypeCate == WebConstants.CategoryProduct), "CategoryID", "CategoryName", product.CategoryIDParent);
            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.TypeCate == WebConstants.CategoryProduct && c.IsLe == product.Category.IsLe), "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", product.CreatedBy);
            return View(product);
        }

        // POST: Quantri/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ProductID,ProductCode,ProductName,IsProduct,Capacity,Price,PricePhanTram,PriceSale,Capacity1,Price1,PricePhanTram1,PriceSale1," +
            "CategoryIDParent,CategoryID,Images,ImagesThumb,Images1,ImagesThumb1,ShortDescription,Content,InStock,IsSale,IsNew,Rating,IsActive,CountView,Created,CreatedBy,SEOTitle,SEOUrlRewrite,SEOKeywords,SEOMetadescription," +
            "SoLuongChai,PhiShip,SoNgayShip,ThuTu")] Product product,
            HttpPostedFileBase HinhAnh, HttpPostedFileBase HinhAnh1)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
                };

                if (HinhAnh == null)
                {
                    product.Images = product.Images;
                }
                else
                {

                    //Xóa hình ảnh đã tồn tại, trừ hình ảnh mặc định.
                    if (product.Images != "NoImage.png")
                    {
                        string fullPath = Request.MapPath(WebConstants.ImgProduct + "/" + product.Images);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }

                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var ext = Path.GetExtension(HinhAnh.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + DateTime.Now.Millisecond + ext; //appending the name with id  
                                                                                     // store the file inside ~/project folder(Img)  

                        var path = Path.Combine(Server.MapPath(WebConstants.ImgProduct), myfile);
                        //var dir = Directory.CreateDirectory(path);
                        //HinhAnh.SaveAs(Path.Combine(path, myfile));

                        product.Images = myfile;
                        HinhAnh.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }

                if (HinhAnh1 == null)
                {
                    product.Images1 = product.Images1;
                }
                else
                {

                    //Xóa hình ảnh đã tồn tại, trừ hình ảnh mặc định.
                    if (product.Images1 != "NoImage.png")
                    {
                        string fullPath = Request.MapPath(WebConstants.ImgProduct + "/" + product.Images1);
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);
                        }
                    }

                    var fileName = Path.GetFileName(HinhAnh1.FileName);
                    var ext = Path.GetExtension(HinhAnh1.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + DateTime.Now.Millisecond + ext; //appending the name with id  
                                                                                     // store the file inside ~/project folder(Img)  

                        var path = Path.Combine(Server.MapPath(WebConstants.ImgProduct), myfile);
                        //var dir = Directory.CreateDirectory(path);
                        //HinhAnh.SaveAs(Path.Combine(path, myfile));

                        product.Images1 = myfile;
                        HinhAnh1.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }

                //product.IsProduct = true;

                product.Created = product.Created;
                product.CreatedBy = product.CreatedBy;

                product.SEOUrlRewrite = Helpers.ConvertToUpperLower(product.ProductName);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                Success(string.Format("Chỉnh sửa thông tin sản phẩm <b>{0}</b> thành công.", product.ProductName), true);

                return RedirectToAction("Details", "Products", new { id = product.ProductID });
            }
            ViewBag.CategoryIDParent = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryIDParent);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.CreatedBy = new SelectList(db.Users, "UserID", "UserName", product.CreatedBy);
            return View(product);
        }

        // GET: Quantri/Products/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Quantri/Products/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            string isGoi = "0";
            if (product.IsProduct == true)
            {
                isGoi = "1";
            }


            var prodImage = db.ProductImages.Where(a => a.ProductID == id).ToList();
            var prodGroup = db.ProductGroups.Where(a => a.ProductID == id).ToList();
            var prodCart = db.CartDetails.Where(a => a.ProductID == id).ToList();
            if (prodImage == null && prodGroup == null && prodCart == null)
            {

                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (prodGroup != null)
            {
                foreach (var i in prodGroup)
                {
                    ProductGroup proGro = db.ProductGroups.Find(i.ProductGroupID);
                    db.ProductGroups.Remove(proGro);
                }
            }
            else if (prodCart != null)
            {
                foreach (var i in prodCart)
                {
                    CartDetail cdetail = db.CartDetails.Find(i.Id);
                    Cart c = db.Carts.Find(cdetail.CarID);
                    db.Carts.Remove(c);
                    db.CartDetails.Remove(cdetail);
                }
            }
            else
            {
                foreach (var i in prodImage)
                {
                    ProductImage prImage = db.ProductImages.Find(i.ImageID);
                    db.ProductImages.Remove(prImage);
                }

            }

            db.Products.Remove(product);
            db.SaveChanges();
            Success(string.Format(" <b>{0}</b> thành công.", product.ProductName), true);
            if (isGoi == "1")
                return RedirectToAction("Index", "Products");
            else
                return RedirectToAction("GoiSPLst", "Products");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public string CreateThumb(string myfile)
        {

            string pathOnServer = Path.Combine(Server.MapPath(WebConstants.ImgProduct));

            var fullPath = Path.Combine(pathOnServer, Path.GetFileName(myfile));
            string URLthumb = "";
            string fileThumb = "";
            var ThumbfullPath = Path.Combine(pathOnServer, "_thumbs");
            fileThumb = DateTime.Now.ToString("HHmmss") + "_" + myfile.Remove(myfile.Length - 4, 4) + ".80x80.jpg";
            var ThumbfullPath2 = Path.Combine(ThumbfullPath, fileThumb);
            using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(fullPath)))
            {
                var thumbnail = new WebImage(stream).Resize(200, 200);
                thumbnail.Save(ThumbfullPath2, "jpg");
                URLthumb = ThumbfullPath2;
            }

            return fileThumb;
        }


        public PartialViewResult GetImageforProduct(int? productid)
        {
            var model = db.ProductImages.Where(q => q.ProductID == productid).OrderBy(a=>a.ThuTu).ToList();

            return PartialView("_image", model);
        }
        //Get group products
        public PartialViewResult GetGroupProduct(int? productid)
        {
            GroupCheck model = new GroupCheck();

            model = GroupCheck(productid);

            return PartialView("~/Areas/Quantri/Views/Products/_group.cshtml", model);
        }
        public GroupCheck GroupCheck(int? productid)
        {
            GroupCheck model = new GroupCheck();

            var x = db.ProductGroups.Where(q => q.ProductID == productid).ToList();

            List<GroupProduct> allows = new List<GroupProduct>();
            List<GroupProduct> available = new List<GroupProduct>();

            var y = x.FirstOrDefault(q => q.GroupCode == WebConstants.ProductMoi); // mới nhất

            GroupProduct g1 = new GroupProduct();
            g1.Title = "Sản Phẩm Mới Nhất";
            g1.GroupID = 0;
            g1.IsGroup = 1;

            if (y != null)
            {
                g1.GroupID = y.ProductGroupID;

                allows.Add(g1);
            }
            else
            {
                available.Add(g1);
            }

            var y2 = x.FirstOrDefault(q => q.GroupCode == WebConstants.ProductDangKM); /// khuyến mãi

            GroupProduct g2 = new GroupProduct();
            g2.Title = "Sản Phẩm Đang Khuyến Mãi";
            g2.GroupID = 0;
            g2.IsGroup = 2;

            if (y2 != null)
            {
                g2.GroupID = y2.ProductGroupID;

                allows.Add(g2);
            }
            else
            {
                available.Add(g2);
            }

            var y3 = x.FirstOrDefault(q => q.GroupCode == WebConstants.ProductNoiBat); //nổi bật

            GroupProduct g3 = new GroupProduct();
            g3.Title = "Sản Phẩm Nổi Bật";
            g3.GroupID = 0;
            g3.IsGroup = 3;

            if (y3 != null)
            {
                g3.GroupID = y3.ProductGroupID;

                allows.Add(g3);
            }
            else
            {
                available.Add(g3);
            }

            var y4 = x.FirstOrDefault(q => q.GroupCode == WebConstants.ProductBanChay); /// bán chạy

            GroupProduct g4 = new GroupProduct();
            g4.Title = "Sản Phẩm Bán Chạy";
            g4.GroupID = 0;
            g4.IsGroup = 4;
            if (y4 != null)
            {
                g4.GroupID = y4.ProductGroupID;

                allows.Add(g4);
            }
            else
            {
                available.Add(g4);
            }

            model.allows = allows;
            model.available = available;


            return model;
        }

        //Thêm xóa Product Group
        public PartialViewResult SaveGroupProduct(int? groupid, int? productid, int? xaction, int? groupcode)
        {
            if (xaction == 0) // xóa
            {
                var gr = db.ProductGroups.Find(groupid);

                db.ProductGroups.Remove(gr);

                db.SaveChanges();
            }
            else // thêm
            {
                var gr = new ProductGroup();
                gr.GroupCode = groupcode;
                gr.ProductID = productid;
                gr.Sort = 1;

                db.ProductGroups.Add(gr);

                db.SaveChanges();
            }


            GroupCheck model = new GroupCheck();

            model = GroupCheck(productid);

            return PartialView("~/Areas/Quantri/Views/Products/_group.cshtml", model);
        }

        public JsonResult XoaGroupProduct(int? groupid)
        {
            string result = "FAIL";

            try
            {
                var q = db.ProductGroups.Find(groupid);

                db.ProductGroups.Remove(q);

                db.SaveChanges();

                result = "DONE";
            }
            catch
            {

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteProductImage(int? imageid)
        {
            string result = "FAIL";
            try
            {
                var img = db.ProductImages.Find(imageid);
                db.ProductImages.Remove(img);
                db.SaveChanges();
                result = "DONE";
            }
            catch
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult UpdateThuTuProductImage(int? imageid, int? thutu)
        {
            string result = "FAIL";
            try
            {
                var img = db.ProductImages.Find(imageid);
                img.ThuTu = thutu;
                db.SaveChanges();
                result = "DONE";
            }
            catch
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ChangeProductAvatar(int? imageid)
        {
            string result = "FAIL";
            try
            {
                var img = db.ProductImages.Find(imageid);
                var product = db.Products.Find(img.ProductID);

                product.Images = img.URLImage;
                product.ImagesThumb = img.ImagesThumb;

                db.SaveChanges();
                result = "DONE";
            }
            catch
            {

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MoiNhat()
        {
            var model = db.ProductGroups.Where(q => q.GroupCode == 1).ToList();
            return View(model);
        }

        public ActionResult TieuBieu()
        {
            var model = db.ProductGroups.Where(q => q.GroupCode == 2).ToList();
            return View(model);
        }

        public ActionResult NoiBat()
        {
            var model = db.ProductGroups.Where(q => q.GroupCode == 3).ToList();
            return View(model);
        }

        public ActionResult BanChay()
        {
            var model = db.ProductGroups.Where(q => q.GroupCode == 4).ToList();
            return View(model);
        }

        public JsonResult FillChungLoaiSP(bool IsLe)
        {
            //var tmp = from s in db.MODELDEVICEs
            //          where s.IDCate == idCate
            //          select s.NameModel;
            //var sItems = new SelectList(tmp);
            //return Json(sItems, JsonRequestBehavior.AllowGet);
            //ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.TypeCate == WebConstants.CategoryProduct), "CategoryID", "CategoryName");
            var le = Convert.ToBoolean(IsLe);
            var result = new SelectList(db.Categories.Where(c => c.TypeCate == WebConstants.CategoryProduct && c.IsLe == le), "CategoryID", "CategoryName");
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
