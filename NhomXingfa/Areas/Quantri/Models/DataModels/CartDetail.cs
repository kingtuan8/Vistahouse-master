namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CartDetail
    {
        public int Id { get; set; }

        public int? CarID { get; set; }

        public int? ProductID { get; set; }

        public decimal? Price { get; set; }

        public int? Qty { get; set; }

        public decimal? Total { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
