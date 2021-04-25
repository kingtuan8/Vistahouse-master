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
using NhomXingfa.Areas.Quantri.Utilities;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    [AuthorizeCustom(Roles = "Admin")]
    public class CustomerFeedbacksController : BaseController
    {
        private XingFaEntities db = new XingFaEntities();

        // GET: Quantri/CustomerFeedbacks
        public ActionResult Index()
        {
            return View(db.CustomerFeedbacks.ToList().OrderBy(a=>a.ThuTu));
        }

        // GET: Quantri/CustomerFeedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerFeedback customerFeedback = db.CustomerFeedbacks.Find(id);
            if (customerFeedback == null)
            {
                return HttpNotFound();
            }
            return View(customerFeedback);
        }

        // GET: Quantri/CustomerFeedbacks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quantri/CustomerFeedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerName,CustomerImage,CustomerInfo,Feedback,ThuTu,IsActive,Created,LastModify")] CustomerFeedback customerFeedback, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (HinhAnh == null)
                {
                    customerFeedback.CustomerImage = "NoImage.png";
                }
                else
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);

                    var ext = Path.GetExtension(HinhAnh.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + DateTime.Now.Millisecond + ext; //appending the name with id  
                                                                                     // store the file inside ~/project folder(Img)  

                        var path = Path.Combine(Server.MapPath(WebConstants.imgCusFeedback), myfile);
                        //var dir = Directory.CreateDirectory(path);
                        //HinhAnh.SaveAs(Path.Combine(path, myfile));

                        customerFeedback.CustomerImage = myfile;
                        HinhAnh.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }
                customerFeedback.Created = DateTime.Now;
                customerFeedback.LastModify = DateTime.Now;
                db.CustomerFeedbacks.Add(customerFeedback);
                db.SaveChanges();
                Success(string.Format("Thêm mới Phản hồi thành công.", ""), true);
                return RedirectToAction("Index", "CustomerFeedbacks");
            }

            return View(customerFeedback);
        }

        // GET: Quantri/CustomerFeedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerFeedback customerFeedback = db.CustomerFeedbacks.Find(id);
            if (customerFeedback == null)
            {
                return HttpNotFound();
            }
            return View(customerFeedback);
        }

        // POST: Quantri/CustomerFeedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerName,CustomerImage,CustomerInfo,Feedback,ThuTu,IsActive,Created,LastModify")] CustomerFeedback customerFeedback, HttpPostedFileBase HinhAnh)
        {
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
                };
                if (HinhAnh == null)
                {
                    customerFeedback.CustomerImage = customerFeedback.CustomerImage;
                }
                else
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);

                    var ext = Path.GetExtension(HinhAnh.FileName);
                    if (allowedExtensions.Contains(ext)) //check what type of extension  
                    {
                        string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                        string myfile = name + "_" + DateTime.Now.Millisecond + ext; //appending the name with id  
                                                                                     // store the file inside ~/project folder(Img)  

                        var path = Path.Combine(Server.MapPath(WebConstants.imgCusFeedback), myfile);
                        //var dir = Directory.CreateDirectory(path);
                        //HinhAnh.SaveAs(Path.Combine(path, myfile));

                        customerFeedback.CustomerImage = myfile;
                        HinhAnh.SaveAs(path);
                    }
                    else
                    {
                        ViewBag.message = "Please choose only Image file";
                    }
                }
                customerFeedback.Created = customerFeedback.Created;
                customerFeedback.LastModify = DateTime.Now;
                db.Entry(customerFeedback).State = EntityState.Modified;
                db.SaveChanges();
                Success(string.Format("Chỉnh sửa Phản hồi thành công.", ""), true);
                return RedirectToAction("Index", "CustomerFeedbacks");
            }
            return View(customerFeedback);
        }

        // GET: Quantri/CustomerFeedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerFeedback customerFeedback = db.CustomerFeedbacks.Find(id);
            if (customerFeedback == null)
            {
                return HttpNotFound();
            }
            return View(customerFeedback);
        }

        // POST: Quantri/CustomerFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerFeedback customerFeedback = db.CustomerFeedbacks.Find(id);
            db.CustomerFeedbacks.Remove(customerFeedback);
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
