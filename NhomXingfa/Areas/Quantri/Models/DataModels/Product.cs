namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            CartDetails = new HashSet<CartDetail>();
            ProductGroups = new HashSet<ProductGroup>();
            ProductImages = new HashSet<ProductImage>();
            ProductRatings = new HashSet<ProductRating>();
            ProductTabs = new HashSet<ProductTab>();
        }

        public int ProductID { get; set; }

        [StringLength(200)]
        public string ProductCode { get; set; }

        [StringLength(200)]
        public string ProductName { get; set; }

        public bool? IsProduct { get; set; }

        [StringLength(50)]
        public string Capacity { get; set; }

        public decimal? Price { get; set; }

        public int? PricePhanTram { get; set; }

        public decimal? PriceSale { get; set; }

        [StringLength(50)]
        public string Capacity1 { get; set; }

        public decimal? Price1 { get; set; }

        public int? PricePhanTram1 { get; set; }

        public decimal? PriceSale1 { get; set; }

        public int? CategoryIDParent { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(100)]
        public string Images { get; set; }

        [StringLength(100)]
        public string ImagesThumb { get; set; }

        [StringLength(400)]
        public string ShortDescription { get; set; }

        public string Content { get; set; }

        public bool? InStock { get; set; }

        public bool? IsSale { get; set; }

        public bool? IsNew { get; set; }

        public bool? isHot { get; set; }

        public int? Rating { get; set; }

        public bool? IsActive { get; set; }

        public int? CountView { get; set; }

        public DateTime? Created { get; set; }

        public int? CreatedBy { get; set; }

        [StringLength(200)]
        public string SEOTitle { get; set; }

        [StringLength(200)]
        public string SEOUrlRewrite { get; set; }

        [StringLength(200)]
        public string SEOKeywords { get; set; }

        public string SEOMetadescription { get; set; }

        public int? SoLuongChai { get; set; }

        public decimal? PhiShip { get; set; }

        public int? SoNgayShip { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartDetail> CartDetails { get; set; }

        public virtual Category Category { get; set; }

        public virtual Category Category1 { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductGroup> ProductGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImage> ProductImages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductRating> ProductRatings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTab> ProductTabs { get; set; }
    }
}
