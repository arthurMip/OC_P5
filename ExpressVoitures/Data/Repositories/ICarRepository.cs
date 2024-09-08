using ExpressVoitures.Data.Entities;

namespace ExpressVoitures.Data.Repositories;

public interface ICarRepository
{
    Task<bool> AddCarAsync(Car car);
    Task<bool> DeleteCarAsync(int id);
    Task<bool> UpdateCarAsync(Car car);
    Task<Car?> GetCarByIdAsync(int id);
    Task<IEnumerable<Car>> GetAllCarsAsync();
    Task<IEnumerable<Car>> GetAvailableCarsAsync();
}
