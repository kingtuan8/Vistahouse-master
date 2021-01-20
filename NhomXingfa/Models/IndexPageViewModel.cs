﻿using NhomXingfa.Areas.Quantri.Models.DataModels;
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
    }
}