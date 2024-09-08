﻿using ExpressVoitures.Data.Entities;
using ExpressVoitures.Data.Repositories;
using ExpressVoitures.Models.Car;

namespace ExpressVoitures.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
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

    public async Task<IEnumerable<CarViewModel>> GetAvailableCarsAsync()
    {
        return (await _carRepository.GetAvailableCarsAsync()).Select(MapCarToCarViewModel);
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


    public Task<bool> AddCarAsync(CreateCarViewModel car)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCarAsync(CreateCarViewModel car)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteCarAsync(int id)
    {
        return _carRepository.DeleteCarAsync(id);
    }
}
