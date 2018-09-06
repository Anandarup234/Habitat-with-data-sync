namespace Sitecore.Foundation.SyncItems.DataModels
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProductDataContext : DbContext
    {
        public ProductDataContext()
            : base("name=ProductDataContext")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProductDataContext>());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tax> Taxes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().HasRequired(x => x.Tax).WithOptional();
        }
    }
}
