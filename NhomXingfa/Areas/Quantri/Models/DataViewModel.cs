using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhomXingfa.Areas.Quantri.Models
{
    public class DataViewModel
    {
    }

    public class GroupProduct
    {
        public string Title { get; set; }
        public int GroupID { get; set; }
        public int? IsGroup { get; set; }
    }
    public class GroupCheck
    {
        public List<GroupProduct> allows { get; set; }
        public List<GroupProduct> available { get; set; }
    }
}