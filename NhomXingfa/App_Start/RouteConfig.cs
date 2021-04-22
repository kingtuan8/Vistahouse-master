using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NhomXingfa
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //     name: "product",
            //     url: "goi-dinh-ki",
            //     defaults: new { controller = "Product", action = "GoiSanPham", id = UrlParameter.Optional }
            // );

            routes.MapRoute(
                name: "product4",
                url: "san-pham",
                defaults: new { controller = "Product", action = "SanPhamDon", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "product2",
                 url: "san-pham/{url}-{id}",
                 defaults: new { controller = "product", action = "index", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                 name: "product5",
                 url: "danh-muc/{url}-{id}",
                 defaults: new { controller = "product", action = "sanpham", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "product",
                url: "goi-dinh-ki/{url}-{id}",
                defaults: new { controller = "product", action = "GoiSanPham", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "chitiet",
                url: "san-pham-ct/{url}-{id}",
                defaults: new { controller = "product", action = "detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "tintuc",
               url: "tin-tuc",
               defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "chuyenmuc",
              url: "chuyen-muc-{id}",
              defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
          );

            //Câu hỏi thường gặp
            routes.MapRoute(
               name: "CauHoiThuongGap",
               url: "cau-hoi-thuong-gap",
               defaults: new { controller = "Q_As", action = "Index", id = UrlParameter.Optional }
           );

            //OurStory
            routes.MapRoute(
               name: "OurStory",
               url: "ve-chung-toi",
               defaults: new { controller = "About", action = "AboutUs", id = UrlParameter.Optional }
           );

            //Thongtindinhduong
            routes.MapRoute(
               name: "ThongTinDinhDuong",
               url: "thong-tin-dinh-duong",
               defaults: new { controller = "About", action = "ThongTinDinhDuong", id = UrlParameter.Optional }
           );
            //ChinhSach
            routes.MapRoute(
               name: "ChinhSachBanHang",
               url: "chinh-sach-ban-hang",
               defaults: new { controller = "About", action = "ChinhSach", id = UrlParameter.Optional }
           );
            // routes.MapRoute(
            //    name: "tintuc1",
            //    url: "tin-tuc/{url}-{id}",
            //    defaults: new { controller = "news", action = "index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
               name: "tintucdetails",
               url: "tin-tuc/{url}-{id}",
               defaults: new { controller = "Blog", action = "Detail", id = UrlParameter.Optional }
           );


            //các bài viết
            routes.MapRoute(
               name: "baiviet",
               url: "bai-viet/{url}-{id}",
               defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional }
           );

           
           

            //các bài viết
            routes.MapRoute(
               name: "Contact",
               url: "lien-he",
               defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            

        }
    }
}
