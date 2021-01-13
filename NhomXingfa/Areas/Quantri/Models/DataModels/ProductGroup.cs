namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductGroup")]
    public partial class ProductGroup
    {
        public int ProductGroupID { get; set; }

        public int? ProductID { get; set; }

        public int? GroupCode { get; set; }

        public int? Sort { get; set; }

        public virtual Product Product { get; set; }
    }
}
