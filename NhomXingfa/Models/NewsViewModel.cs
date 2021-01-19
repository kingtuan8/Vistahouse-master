using NhomXingfa.Areas.Quantri.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NhomXingfa.Models
{
    public class NewsViewModel
    {
        public List<Blog> blogs { get; set; }
        public List<Category> categories { get; set; }
        public Category category { get; set; }
        public List<Blog> recent { get; set; }
    }
    public class DetailNewsViewModel
    {
        public Blog blog { get; set; }
        public List<Category> categories { get; set; }
        public Category category { get; set; }
        public List<Blog> recents { get; set; }
    }
}