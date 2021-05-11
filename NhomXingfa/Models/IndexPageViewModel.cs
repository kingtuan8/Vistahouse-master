using NhomXingfa.Areas.Quantri.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhomXingfa.Models
{
    public class IndexPageViewModel
    {
        public List<Product> goidinhky { get; set; }
        public List<Product> products { get; set; }
        public List<CartViewModel> carts { get; set; }
        public List<Slide> lstBannerHomePage { get; set; }
        public List<Blog> lstLuaChon { get; set; }
        public Blog OurStory { get; set; }
        public List<CustomerFeedback> lstCustomerFeedback { get; set; }
        public List<Customer> lstCustomerParner { get; set; }
        public List<PhotoLibraryLst> lstPhotos { get; set; }
        
    }

    public class CartViewModel
    {
        public string Name { get; set; }
        public string Descript { get; set; }
        public string Image { get; set; }
        public int? ID { get; set; }
        public decimal? Price { get; set; }
        public bool? IsProduct { get; set; }
        public int? Quantity { get; set; }
        public int? CateSPDon { get; set; }
        public int? SoLuongChai { get; set; }
        public int? SoNgayShip { get; set; }
    }

    public class CartDetailViewModel
    {
        public List<CartViewModel> cart { get; set; }
        public CustomerOrder customer { get; set; }
        public Blog thongtinthanhtoan { get; set; }
    }

    public class DetailOrderViewModel
    {
        public Cart Cart { get; set; }
        public List<CartDetail> cartDetails { get; set; }

        public List<CartHistory> carthistory { get; set; }
    }

    public class DetailPageViewModel
    {
        public Product product { get; set; }
        public List<Product> products { get; set; }
        public List<ProductImage> images { get; set; }
        public List<Blog> tintuc { get; set; }
        public string CategoryTitle { get; set; }
    }

    public class GoiDinhKyViewModel
    {
        public List<Product> products { get; set; }
        public List<Q_A> lstQAs { get; set; }
        public List<CustomerFeedback> lstCustomerFeed { get; set; }
    }

    public class SanPhamDonDM
    {
        public List<Product> spdon { get; set; }
        public string Title { get; set; }
    }

    //Danny
    public class FooterViewModel
    {
        public Blog footerInfo { get; set; }
    }
}