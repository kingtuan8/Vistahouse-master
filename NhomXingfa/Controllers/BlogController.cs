using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Areas.Quantri.Utilities;
using NhomXingfa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NhomXingfa.Controllers
{
    public class BlogController : Controller
    {
        XingFaEntities db = new XingFaEntities();
        // GET: Blog
        public ActionResult Index(int? id)
        {
            NewsViewModel model = new NewsViewModel();

            model.categories = db.Categories.Where(q => q.IsActive == true && q.TypeCate == WebConstants.CategoryNews).OrderBy(a=>a.Sort).ToList();

            model.category = db.Categories.Find(id);

            if (id == null)
            {
                model.blogs = db.Blogs.Where(q => q.IsActive == true && q.TypeBlog == WebConstants.BlogNews).OrderByDescending(a=>a.LastModify).ToList();

                model.recent = db.Blogs.Where(q => q.IsActive == true && q.TypeBlog == WebConstants.BlogNews).OrderByDescending(a => a.LastModify).ToList();
            }
            else
            {
                model.blogs = db.Blogs.Where(q => q.IsActive == true && q.TypeBlog == WebConstants.BlogNews && q.CategoryID == id).OrderByDescending(a => a.LastModify).ToList();
                model.recent = db.Blogs.Where(q => q.IsActive == true && q.TypeBlog == WebConstants.BlogNews && q.CategoryID == id).OrderByDescending(a=>a.LastModify).ToList();
            }
            //System.Globalization.CultureInfo
            return View(model);
        }
        public ActionResult Detail(int? id)
        {
            var model = new DetailNewsViewModel();
            var x = db.Blogs.Find(id);

            x.CountView += 1;
            db.SaveChanges();

            model.blog = x;
            model.category = db.Categories.Find(model.blog.CategoryID);
            model.categories = db.Categories.Where(q => q.TypeCate == WebConstants.CategoryNews).ToList();
            model.recents = db.Blogs.Where(q => q.CategoryID == model.blog.CategoryID && q.IsActive == true && q.CategoryID != id).Take(8).ToList();
            model.docnhieu = db.Blogs.Where(q => q.CategoryID == model.blog.CategoryID && q.IsActive == true && q.CategoryID != id).OrderByDescending(o => o.CountView).Take(8).ToList();

            return View(model);
        }

        public ActionResult ChuyenMuc(int? id)
        {
            NewsViewModel model = new NewsViewModel();

            model.categories = db.Categories.Where(q => q.IsActive == true && q.TypeCate == WebConstants.CategoryNews).ToList();

            model.category = db.Categories.Find(id);

            if (id == null)
            {
                model.blogs = db.Blogs.Where(q => q.IsActive == true && q.TypeBlog == WebConstants.BlogNews).OrderByDescending(a => a.LastModify).ToList();

                model.recent = db.Blogs.Where(q => q.IsActive == true && q.TypeBlog == WebConstants.BlogNews).OrderByDescending(a => a.LastModify).ToList();
            }
            else
            {
                model.blogs = db.Blogs.Where(q => q.IsActive == true && q.TypeBlog == WebConstants.BlogNews && q.CategoryID == id).OrderByDescending(a => a.LastModify).ToList();
                model.recent = db.Blogs.Where(q => q.IsActive == true && q.TypeBlog == WebConstants.BlogNews && q.CategoryID == id).OrderByDescending(a => a.LastModify).ToList();
            }
            //System.Globalization.CultureInfo
            return View(model);
        }
    }
}