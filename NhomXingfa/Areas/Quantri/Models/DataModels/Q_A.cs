namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Q_A
    {
        public int Id { get; set; }

        public string Question { get; set; }

        [Column(TypeName = "ntext")]
        public string Answer { get; set; }

        public bool? IsActive { get; set; }

        public int? ThuTu { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? LastModify { get; set; }
    }
}
