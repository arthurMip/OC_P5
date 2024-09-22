using ExpressVoitures.Data.Entities;
using ExpressVoitures.Data.Repositories;
using ExpressVoitures.Models.Car;

namespace ExpressVoitures.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public CarService(ICarRepository carRepository, IWebHostEnvironment hostingEnvironment)
    {
        _carRepository = carRepository;
        _hostingEnvironment = hostingEnvironment;
    }

    private static CarViewModel MapCarToCarViewModel(Car car)
    {
        return new CarViewModel
        {
            Id = car.Id,
            Price = car.SellingInfo.Price,
            Year = car.ManufacturingInfo.Year,
            Model = car.ManufacturingInfo.Model,
            Brand = car.ManufacturingInfo.Brand,
            Finish = car.ManufacturingInfo.Finish,
            ImageUrl = $"./images/{car.Image.FileName}",
        };
    }

    public async Task<CarViewModel?> GetCarByIdAsync(int id)
    {
        Car? car = await _carRepository.GetCarByIdAsync(id);
        if (car == null)
        {
            return null;
        }
        return MapCarToCarViewModel(car);
    }


    public async Task<bool> AddCarAsync(CreateCarViewModel car)
    {
        var model = new Car
        {
            BuyingInfo = new BuyingInfo
            {
                Price = car.BuyingPrice,
                Date = car.BuyingDate,
            },
            FixingInfo = new FixingInfo
            {
                Description = car.FixingDescription,
                Cost = car.FixingCost,
            },
            ManufacturingInfo = new ManufacturingInfo
            {
                VinCode = car.VinCode,
                Year = car.ManufacturingYear,
                Brand = car.Brand,
                Model = car.Model,
                Finish = car.Finish,
            },
            SellingInfo = new SellingInfo
            {
                AvailableDate = car.AvailableDate,
                SellingDate = car.SellingDate,
                Price = car.SellingPrice,
            },
        };


        if (car.Image?.Length > 0)
        {
            string uniqueFileName = Guid.NewGuid().ToString().Substring(24) + Path.GetExtension(car.Image.FileName);
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqueFileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await car.Image.CopyToAsync(fileStream);

            model.Image = new Image
            {
                FileName = uniqueFileName,
            };
        }

        return await _carRepository.AddCarAsync(model);
    }

    public Task<bool> UpdateCarAsync(CreateCarViewModel car)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCarAsync(int id)
    {
        return _carRepository.DeleteCarAsync(id);
    }

    public async Task<IEnumerable<CarViewModel>> GetCarsAsync(bool onlyAvailable)
    {
        if (onlyAvailable)
        {
            return (await _carRepository.GetAvailableCarsAsync()).Select(MapCarToCarViewModel);
        }
        else
        {
            return (await _carRepository.GetAllCarsAsync()).Select(MapCarToCarViewModel);
        }
    }
}
