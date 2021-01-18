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
    }
}