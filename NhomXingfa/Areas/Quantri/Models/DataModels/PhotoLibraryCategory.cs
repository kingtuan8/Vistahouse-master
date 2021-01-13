namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhotoLibraryCategory")]
    public partial class PhotoLibraryCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhotoLibraryCategory()
        {
            PhotoLibraryLsts = new HashSet<PhotoLibraryLst>();
        }

        public int Id { get; set; }

        [StringLength(300)]
        public string LibraryName { get; set; }

        public string LibraryDesc { get; set; }

        public DateTime? Created { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhotoLibraryLst> PhotoLibraryLsts { get; set; }
    }
}
