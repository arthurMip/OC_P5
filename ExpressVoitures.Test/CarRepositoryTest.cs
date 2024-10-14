using ExpressVoitures.Data;
using ExpressVoitures.Data.Entities;
using ExpressVoitures.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Test
{
    public class CarRepositoryTest : IDisposable
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CarRepositoryTestDatabase")
                .Options;

            var context = new ApplicationDbContext(options);
            return context;
        }

        public void Dispose()
        {
            var context = GetInMemoryDbContext();
            context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AddEntity_ShouldAddToDatabase()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new CarRepository(context);
            var entity = new Car
            {
                Id = 1,
                BuyingInfo = new BuyingInfo
                {
                    Price = 1000,
                    Date = DateTime.Now
                },
                FixingInfo = new FixingInfo
                {
                    Description = "Fixing",
                    Cost = 100
                },
                ManufacturingInfo = new ManufacturingInfo
                {
                    Year = 2021,
                    Brand = "Brand",
                    Model = "Model",
                    Finish = "Finish",
                    VinCode = "VinCode"
                },
                SellingInfo = new SellingInfo
                {
                    Price = 2000,
                    AvailableDate = DateTime.Now,
                    SellingDate = DateTime.Now
                }
            };

            // Act
            await repository.AddCarAsync(entity);
            context.SaveChanges();

            // Assert
            Assert.Contains(entity, context.Cars);
        }

        [Fact]
        public async Task GetEntityById_ShouldReturnCorrectEntity()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new CarRepository(context);
            var entity = new Car
            {
                Id = 1,
                BuyingInfo = new BuyingInfo
                {
                    Price = 1000,
                    Date = DateTime.Now
                },
                FixingInfo = new FixingInfo
                {
                    Description = "Fixing",
                    Cost = 100
                },
                ManufacturingInfo = new ManufacturingInfo
                {
                    Year = 2021,
                    Brand = "Brand",
                    Model = "Model",
                    Finish = "Finish",
                    VinCode = "VinCode"
                },
                SellingInfo = new SellingInfo
                {
                    Price = 2000,
                    AvailableDate = DateTime.Now,
                    SellingDate = DateTime.Now
                }
            };

            await repository.AddCarAsync(entity);
            context.SaveChanges();

            // Act
            var result = await repository.GetCarByIdAsync(1);

            // Assert
            Assert.Equal(entity, result);
        }

        [Fact]
        public async Task UpdateEntity_ShouldUpdateEntity()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new CarRepository(context);
            var entity = new Car
            {
                Id = 1,
                BuyingInfo = new BuyingInfo
                {
                    Price = 1000,
                    Date = DateTime.Now
                },
                FixingInfo = new FixingInfo
                {
                    Description = "Fixing",
                    Cost = 100
                },
                ManufacturingInfo = new ManufacturingInfo
                {
                    Year = 2021,
                    Brand = "Brand",
                    Model = "Model",
                    Finish = "Finish",
                    VinCode = "VinCode"
                },
                SellingInfo = new SellingInfo
                {
                    Price = 2000,
                    AvailableDate = DateTime.Now,
                    SellingDate = DateTime.Now
                }
            };

            await repository.AddCarAsync(entity);
            context.SaveChanges();

            entity.BuyingInfo.Price = 2000;
            entity.FixingInfo.Description = "New Fixing";
            entity.ManufacturingInfo.Year = 2022;
            entity.SellingInfo.Price = 3000;

            // Act
            await repository.UpdateCarAsync(entity);
            context.SaveChanges();

            // Assert
            var result = await repository.GetCarByIdAsync(1);
            Assert.Equal(2000, result?.BuyingInfo.Price);
            Assert.Equal("New Fixing", result?.FixingInfo.Description);
            Assert.Equal(2022, result?.ManufacturingInfo.Year);
            Assert.Equal(3000, result?.SellingInfo.Price);
        }

        [Fact]
        public async Task DeleteEntity_ShouldDeleteEntity()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var repository = new CarRepository(context);
            var entity = new Car
            {
                Id = 1,
                BuyingInfo = new BuyingInfo
                {
                    Price = 1000,
                    Date = DateTime.Now
                },
                FixingInfo = new FixingInfo
                {
                    Description = "Fixing",
                    Cost = 100
                },
                ManufacturingInfo = new ManufacturingInfo
                {
                    Year = 2021,
                    Brand = "Brand",
                    Model = "Model",
                    Finish = "Finish",
                    VinCode = "VinCode"
                },
                SellingInfo = new SellingInfo
                {
                    Price = 2000,
                    AvailableDate = DateTime.Now,
                    SellingDate = DateTime.Now
                }
            };

            await repository.AddCarAsync(entity);
            context.SaveChanges();

            // Act
            await repository.DeleteCarAsync(1);
            context.SaveChanges();

            // Assert
            var result = await repository.GetCarByIdAsync(1);
            Assert.Null(result);
        }

    }
}