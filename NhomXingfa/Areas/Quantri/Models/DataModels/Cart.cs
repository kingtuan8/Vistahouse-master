namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cart")]
    public partial class Cart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cart()
        {
            CartDetails = new HashSet<CartDetail>();
        }

        public int Id { get; set; }

        [StringLength(100)]
        public string CarCode { get; set; }

        public int? CustID { get; set; }

        [StringLength(300)]
        public string AddresDelivery { get; set; }

        [StringLength(20)]
        public string CustPhone { get; set; }

        public DateTime? TimeDelivery { get; set; }

        public decimal? ShipDelivery { get; set; }

        public int? Qty { get; set; }

        public decimal? Total { get; set; }

        [StringLength(50)]
        public string VouchersCode { get; set; }

        public double? VourcerMoney { get; set; }

        public decimal? TotalConLai { get; set; }

        public string Notes { get; set; }

        public int? StatusOrder { get; set; }

        public int? StyleShip { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
