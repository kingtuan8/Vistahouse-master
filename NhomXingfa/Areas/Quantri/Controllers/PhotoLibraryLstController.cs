using NhomXingfa.Areas.Quantri.Models;
using NhomXingfa.Areas.Quantri.Models.DataModels;
using NhomXingfa.Areas.Quantri.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    [AuthorizeCustom(Roles = "Admin")]
    public class PhotoLibraryLstController : Controller
    {
        FilesHelper2 h;
        XingFaEntities db = new XingFaEntities();

        string tempPath = "~/Upload/";
        string serverMapPath = "~/Photos/PhotoLibrary/";
        // GET: Quantri/PhotoLibraryLst

        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }

        private string UrlBase = "/Photos/PhotoLibrary/";
        string DeleteURL = "/PhotoLibraryLst/DeleteFile/?file=";
        string DeleteType = "GET";

        public PhotoLibraryLstController()
        {
            h = new FilesHelper2(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
        }
        public ActionResult Index()
        {
            var model = db.PhotoLibraryCategories;
            return View(model);
        }

        [Authorize]
        public ActionResult ListOfPhoto()
        {
            ViewData["lstCatePhoto"] = db.PhotoLibraryCategories.ToList();
            return View();
        }


        public ActionResult _PartialListOfPhoto(int? pageNumber, int? pageSize, string hinhsp, int? DanhMucCha)
        {
            hinhsp = hinhsp ?? "";
            ViewBag.hinhsp = hinhsp;
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 10;

            if (pageSize == -1)
            {
                pageSize = db.PhotoLibraryLsts.ToList().Count;
            }
            ViewBag.PageSize = pageSize;

            var lstprod = db.PhotoLibraryLsts.OrderByDescending(p => p.Id).ToList();

            if (!string.IsNullOrEmpty(hinhsp))
            {
                lstprod = lstprod.Where(s => s.Title.ToUpper().Contains(hinhsp.ToUpper())).ToList();
            }
            ViewBag.hinhsp = hinhsp;


            if (!string.IsNullOrEmpty(DanhMucCha.ToString()))
            {
                lstprod = lstprod.Where(s => s.PhotoCateId == DanhMucCha).ToList();
            }
            ViewBag.DanhMucCha = DanhMucCha;

            lstprod = lstprod.OrderByDescending(p => p.Id).ToList();
            ViewBag.STT = pageNumber * pageSize - pageSize + 1;
            int count = lstprod.ToList().Count();
            ViewBag.TotalRow = count;
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/Quantri/Views/PhotoLibraryLst/_PartialListOfPhoto.cshtml", lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
            }
            return View(lstprod.ToList().ToPagedList(pageNumber ?? 1, pageSize ?? 2));
        }

        public JsonResult ClearCatalogue(int? id)
        {
            PhotoLibraryLst cate = db.PhotoLibraryLsts.Find(id);
            //Xóa hình ảnh đã tồn tại, trừ hình ảnh mặc định.

            string fullPath = Request.MapPath(WebConstants.PhotoLib + "/" + cate.URLImage);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db.PhotoLibraryLsts.Remove(cate);
            db.SaveChanges();
            return this.Json(
                   "OK", JsonRequestBehavior.AllowGet
                 );
        }

        //public JsonResult GetChildCategory(int? cateid)
        //{
        //    var model = db.Categories.Where(q => q.Parent == cateid).Select(s => new
        //    {
        //        CateID = s.CategoryID,
        //        CateName = s.CategoryName
        //    }).ToList();

        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult Upload(int? childid, int? parentid)
        {
            //productid = 1;
            childid = 0;
            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;

            h.UploadAndShowResults(CurrentContext, resultList, parentid, childid);


            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(files, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetFileList()
        {
            var list = h.GetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteFile(string file)
        {
            h.DeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteImage(int? pid)
        {
            var img = db.PhotoLibraryLsts.Find(pid);

            db.PhotoLibraryLsts.Remove(img);

            db.SaveChanges();

            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}