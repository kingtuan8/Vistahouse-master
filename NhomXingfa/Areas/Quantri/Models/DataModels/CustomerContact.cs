namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerContact")]
    public partial class CustomerContact
    {
        public int CustomerContactID { get; set; }

        [StringLength(100)]
        public string CustomerName { get; set; }

        [StringLength(50)]
        public string CustomerEmail { get; set; }

        [StringLength(15)]
        public string CustomerPhone { get; set; }

        [StringLength(500)]
        public string CustomerAddress { get; set; }

        public string CustomerContent { get; set; }

        public int? ContactTypeId { get; set; }

        public int? StatusReply { get; set; }

        public DateTime? DateReply { get; set; }

        public string AdminNote { get; set; }

        public DateTime? Created { get; set; }

        public virtual CustomerContactType CustomerContactType { get; set; }
    }
}
