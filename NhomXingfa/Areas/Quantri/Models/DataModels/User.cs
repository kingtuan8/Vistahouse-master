namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Blogs = new HashSet<Blog>();
            CartHistories = new HashSet<CartHistory>();
            Products = new HashSet<Product>();
            RolesUsers = new HashSet<RolesUser>();
            Slides = new HashSet<Slide>();
            Users1 = new HashSet<User>();
        }

        public int UserID { get; set; }

        [StringLength(20)]
        public string UserName { get; set; }

        [StringLength(68)]
        public string HashPass { get; set; }

        public bool? Active { get; set; }

        public DateTime? Created { get; set; }

        public int? CreatedBy { get; set; }

        public int? CustomerID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Blog> Blogs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartHistory> CartHistories { get; set; }

        public virtual CustomerOrder CustomerOrder { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolesUser> RolesUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Slide> Slides { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users1 { get; set; }

        public virtual User User1 { get; set; }
    }
}
