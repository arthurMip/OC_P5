using ExpressVoitures.Models.Car;

namespace ExpressVoitures.Services;

public interface ICarService
{
    Task<IEnumerable<CarViewModel>> GetAvailableCarsAsync();
    Task<CarViewModel?> GetCarByIdAsync(int id);
    Task<bool> AddCarAsync(CreateCarViewModel car);
    Task<bool> UpdateCarAsync(CreateCarViewModel car);
    Task<bool> DeleteCarAsync(int id);
}
