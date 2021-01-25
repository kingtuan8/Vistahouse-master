namespace NhomXingfa.Areas.Quantri.Models.DataModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class XingFaEntities : DbContext
    {
        public XingFaEntities()
            : base("name=XingFaEntities")
        {
        }

        public virtual DbSet<Advertise> Advertises { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BlogComment> BlogComments { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartDetail> CartDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerContact> CustomerContacts { get; set; }
        public virtual DbSet<CustomerContactType> CustomerContactTypes { get; set; }
        public virtual DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }
        public virtual DbSet<CustomerOrder> CustomerOrders { get; set; }
        public virtual DbSet<Information> Information { get; set; }
        public virtual DbSet<MenuImage> MenuImages { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionRole> PermissionRoles { get; set; }
        public virtual DbSet<PhotoLibraryCategory> PhotoLibraryCategories { get; set; }
        public virtual DbSet<PhotoLibraryLst> PhotoLibraryLsts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductGroup> ProductGroups { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductImageCategory> ProductImageCategories { get; set; }
        public virtual DbSet<ProductRating> ProductRatings { get; set; }
        public virtual DbSet<ProductTab> ProductTabs { get; set; }
        public virtual DbSet<Q_A> Q_A { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolesUser> RolesUsers { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertise>()
                .Property(e => e.AdvertiseCode)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.AdvertiseImage)
                .IsUnicode(false);

            modelBuilder.Entity<Advertise>()
                .Property(e => e.AdvertiseURL)
                .IsUnicode(false);

            modelBuilder.Entity<BlogComment>()
                .Property(e => e.Images)
                .IsUnicode(false);

            modelBuilder.Entity<BlogComment>()
                .Property(e => e.Commenter)
                .IsUnicode(false);

            modelBuilder.Entity<Cart>()
                .Property(e => e.ShipDelivery)
                .HasPrecision(30, 10);

            modelBuilder.Entity<Cart>()
                .Property(e => e.Total)
                .HasPrecision(30, 10);

            modelBuilder.Entity<Cart>()
                .Property(e => e.TotalConLai)
                .HasPrecision(30, 10);

            modelBuilder.Entity<Cart>()
                .HasMany(e => e.CartDetails)
                .WithOptional(e => e.Cart)
                .HasForeignKey(e => e.CarID);

            modelBuilder.Entity<CartDetail>()
                .Property(e => e.Price)
                .HasPrecision(30, 10);

            modelBuilder.Entity<CartDetail>()
                .Property(e => e.Total)
                .HasPrecision(30, 10);

            modelBuilder.Entity<CartDetail>()
                .Property(e => e.ml)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .Property(e => e.SEOUrlRewrite)
                .IsUnicode(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryIDParent);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products1)
                .WithOptional(e => e.Category1)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.ProductImageCategories)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CategoryIDParent);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.ProductImageCategories1)
                .WithOptional(e => e.Category1)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerImage)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerContact>()
                .Property(e => e.CustomerEmail)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerContact>()
                .Property(e => e.CustomerPhone)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerContactType>()
                .HasMany(e => e.CustomerContacts)
                .WithOptional(e => e.CustomerContactType)
                .HasForeignKey(e => e.ContactTypeId);

            modelBuilder.Entity<CustomerOrder>()
                .HasMany(e => e.Carts)
                .WithOptional(e => e.CustomerOrder)
                .HasForeignKey(e => e.CustID);

            modelBuilder.Entity<CustomerOrder>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.CustomerOrder)
                .HasForeignKey(e => e.CustomerID);

            modelBuilder.Entity<MenuImage>()
                .Property(e => e.ImageURL)
                .IsUnicode(false);

            modelBuilder.Entity<Permission>()
                .Property(e => e.PermissionCode)
                .IsUnicode(false);

            modelBuilder.Entity<PhotoLibraryCategory>()
                .HasMany(e => e.PhotoLibraryLsts)
                .WithOptional(e => e.PhotoLibraryCategory)
                .HasForeignKey(e => e.PhotoCateId);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.PriceSale)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.Price1)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.PriceSale1)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .Property(e => e.Images)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ImagesThumb)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.SEOUrlRewrite)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.PhiShip)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ProductImage>()
                .Property(e => e.URLImage)
                .IsUnicode(false);

            modelBuilder.Entity<ProductImage>()
                .Property(e => e.ImagesThumb)
                .IsUnicode(false);

            modelBuilder.Entity<ProductImageCategory>()
                .Property(e => e.URLImage)
                .IsUnicode(false);

            modelBuilder.Entity<ProductImageCategory>()
                .Property(e => e.ImagesThumb)
                .IsUnicode(false);

            modelBuilder.Entity<ProductRating>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<ProductRating>()
                .Property(e => e.EmailReview)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.HashPass)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Blogs)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Slides)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.CreateBy);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Users1)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.CreatedBy);
        }
    }
}
