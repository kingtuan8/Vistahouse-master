using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NhomXingfa.Areas.Quantri.Models.DataModels;

namespace NhomXingfa.Areas.Quantri.Models
{
    public class IndexViewModel
    {
        public int TongSP { get; set; }
        public int KHContact { get; set; }
        public int TongThanhVien { get; set; }
        public int TongDonHang { get; set; }
        public List<Cart> lstCart { get; set; }
        public List<CustomerContact> lstCusContact { get; set; }
    }
}