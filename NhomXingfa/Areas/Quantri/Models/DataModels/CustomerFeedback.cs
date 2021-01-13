namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerFeedback")]
    public partial class CustomerFeedback
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string CustomerName { get; set; }

        [StringLength(100)]
        public string CustomerImage { get; set; }

        [StringLength(500)]
        public string CustomerInfo { get; set; }

        public string Feedback { get; set; }

        public int? ThuTu { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? LastModify { get; set; }
    }
}
