﻿using NhomXingfa.Areas.Quantri.Models;
using NhomXingfa.Areas.Quantri.Models.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace NhomXingfa.Areas.Quantri.Controllers
{
    
    public class ProductImageController : Controller
    {
        //WNKEntities db = new WNKEntities();
        FilesHelper h;
        string tempPath = "~/Upload/";
        string serverMapPath = "~/Photos/Products/";

        
        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }

        private string UrlBase = "/Photos/Products/";
        string DeleteURL = "/ProductImage/DeleteFile/?file=";
        string DeleteType = "GET";
        public ProductImageController()
        {
            h = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
        }

        // GET: ProductImage
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload(int? productid)
        {
            //productid = 1;

            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;

            h.UploadAndShowResults(CurrentContext, resultList, productid);


            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error");
            }
            else
            {
                return Json(files);
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

        
    }
}