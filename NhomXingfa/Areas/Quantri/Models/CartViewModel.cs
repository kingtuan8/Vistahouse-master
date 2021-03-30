using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NhomXingfa.Areas.Quantri.Models.DataModels;

namespace NhomXingfa.Areas.Quantri.Models
{
    public class CartViewModel
    {
        public Cart cart { get; set; }
        public List<CartDetail> lstDetail { get; set; }
        public CustomerOrder cust { get; set; }
        public List<CartHistory> lstHistory { get; set; }
    }
}