namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhotoLibraryLst")]
    public partial class PhotoLibraryLst
    {
        public int Id { get; set; }

        public int? PhotoCateId { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string URLImage { get; set; }

        [StringLength(200)]
        public string ImagesThumb { get; set; }

        public DateTime? Created { get; set; }

        public virtual PhotoLibraryCategory PhotoLibraryCategory { get; set; }
    }
}
