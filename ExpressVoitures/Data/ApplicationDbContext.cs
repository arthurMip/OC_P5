using ExpressVoitures.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<BuyingInfo> BuyingInfos { get; set; }
        public DbSet<FixingInfo> FixingInfos { get; set; }
        public DbSet<SellingInfo> SellingInfos { get; set; }
        public DbSet<ManufacturingInfo> ManufacturingInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Car>()
                .HasMany(c => c.Images)
                .WithOne()
                .HasForeignKey(i => i.CarId);

            builder.Entity<Car>()
                .HasOne(c => c.BuyingInfo)
                .WithOne()
                .HasForeignKey<BuyingInfo>(b => b.CarId);

            builder.Entity<Car>()
                .HasOne(c => c.FixingInfo)
                .WithOne()
                .HasForeignKey<FixingInfo>(f => f.CarId);

            builder.Entity<Car>()
                .HasOne(c => c.SellingInfo)
                .WithOne()
                .HasForeignKey<SellingInfo>(s => s.CarId);

            builder.Entity<Car>()
                .HasOne(c => c.ManufacturingInfo)
                .WithOne()
                .HasForeignKey<ManufacturingInfo>(m => m.CarId);
        }
    }
}
