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
                .HasOne(c => c.Image)
                .WithOne()
                .HasForeignKey<Image>(i => i.CarId);

            builder.Entity<Image>().HasKey(i => i.CarId);

            builder.Entity<Car>()
                .HasOne(c => c.BuyingInfo)
                .WithOne()
                .HasForeignKey<BuyingInfo>(b => b.CarId);

            builder.Entity<BuyingInfo>().HasKey(b => b.CarId);

            builder.Entity<Car>()
                .HasOne(c => c.FixingInfo)
                .WithOne()
                .HasPrincipalKey<Car>(c => c.Id)
                .HasForeignKey<FixingInfo>(f => f.CarId);

            builder.Entity<FixingInfo>().HasKey(f => f.CarId);


            builder.Entity<Car>()
                .HasOne(c => c.SellingInfo)
                .WithOne()
                .HasPrincipalKey<Car>(c => c.Id)
                .HasForeignKey<SellingInfo>(s => s.CarId);
            
            builder.Entity<SellingInfo>().HasKey(s => s.CarId);

            builder.Entity<Car>()
                .HasOne(c => c.ManufacturingInfo)
                .WithOne()
                .HasPrincipalKey<Car>(c => c.Id)
                .HasForeignKey<ManufacturingInfo>(m => m.CarId);

            builder.Entity<ManufacturingInfo>().HasKey(m => m.CarId);
        }
    }
}
