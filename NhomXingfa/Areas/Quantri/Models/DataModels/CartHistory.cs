namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CartHistory")]
    public partial class CartHistory
    {
        public int Id { get; set; }

        public int? CartID { get; set; }

        public DateTime? TimeHistory { get; set; }

        [StringLength(400)]
        public string ContentHistory { get; set; }

        public int? UserLogin { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual User User { get; set; }
    }
}
