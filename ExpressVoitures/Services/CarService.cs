using ExpressVoitures.Data.Entities;
using ExpressVoitures.Data.Repositories;
using ExpressVoitures.Models.Car;

namespace ExpressVoitures.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private const decimal _profit = 500;

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
            ImageUrl = $"/images/{car.Image.FileName}",
        };
    }

    private static UpdateCarViewModel MapCarUpdateCarViewModel(Car car)
    {
        return new UpdateCarViewModel
        {
            Id = car.Id,
            Visible = car.Visible,
            BuyingPrice = car.BuyingInfo.Price,
            BuyingDate = car.BuyingInfo.Date,
            FixingDescription = car.FixingInfo.Description,
            FixingCost = car.FixingInfo.Cost,
            ManufacturingYear = car.ManufacturingInfo.Year,
            Brand = car.ManufacturingInfo.Brand,
            Model = car.ManufacturingInfo.Model,
            Finish = car.ManufacturingInfo.Finish,
            VinCode = car.ManufacturingInfo.VinCode,
            AvailableDate = car.SellingInfo.AvailableDate,
            SellingDate = car.SellingInfo.SellingDate,
        };
    }

    public async Task<CarViewModel?> GetCarByIdAsync(int id)
    {
        Car? car = await _carRepository.GetCarByIdAsync(id);
        return car == null ? null : MapCarToCarViewModel(car);
    }


    public async Task<bool> AddCarAsync(CreateCarViewModel car)
    {
        var model = new Car
        {
            Visible = car.Visible,
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
                Price = car.BuyingPrice + car.FixingCost + _profit,
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

    public async Task<IEnumerable<CarViewModel>> GetCarsAsync(bool getAll)
    {
        if (getAll)
        {
            return (await _carRepository.GetAllCarsAsync()).Select(MapCarToCarViewModel);
        }
        else
        {
            return (await _carRepository.GetAvailableCarsAsync()).Select(MapCarToCarViewModel);
        }
    }

    public async Task<CarViewModel?> DeleteCarAsync(int id)
    {
        var car = await _carRepository.GetCarByIdAsync(id);
        if (car == null)
        {
            return null;
        }
        bool deleted = await _carRepository.DeleteCarAsync(id);
        if (deleted && car.Image != null)
        {
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", car.Image.FileName);
            File.Delete(filePath);
        }

        return MapCarToCarViewModel(car);
    }

    public async Task<UpdateCarViewModel?> GetUpdateCarViewModelByIdAsync(int id)
    {
        var car = await _carRepository.GetCarByIdAsync(id);
        if (car == null)
        {
            return null;
        }

        return MapCarUpdateCarViewModel(car);
    }

    public async Task<bool> UpdateCarAsync(UpdateCarViewModel car)
    {
        var model = await _carRepository.GetCarByIdAsync(car.Id);

        if (model == null) return false;

        model.Visible = car.Visible;
        model.BuyingInfo.Price = car.BuyingPrice;
        model.BuyingInfo.Date = car.BuyingDate;
        model.FixingInfo.Description = car.FixingDescription;
        model.FixingInfo.Cost = car.FixingCost;
        model.ManufacturingInfo.VinCode = car.VinCode;
        model.ManufacturingInfo.Year = car.ManufacturingYear;
        model.ManufacturingInfo.Brand = car.Brand;
        model.ManufacturingInfo.Model = car.Model;
        model.ManufacturingInfo.Finish = car.Finish;
        model.SellingInfo.AvailableDate = car.AvailableDate;
        model.SellingInfo.SellingDate = car.SellingDate;
        model.BuyingInfo.Date = car.BuyingDate;
        model.SellingInfo.Price = car.BuyingPrice + car.FixingCost + _profit;

        if (car.Image?.Length > 0)
        {
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.Image.FileName.Split("?")[0]);


            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await car.Image.CopyToAsync(fileStream);

            model.Image.FileName = Path.GetFileName(filePath) + "?v=" + Guid.NewGuid().ToString().Substring(24);
        }

        return await _carRepository.UpdateCarAsync(model);
    }
}