using ExpressVoitures.Models.Car;

namespace ExpressVoitures.Services;

public interface ICarService
{
    Task<IEnumerable<CarViewModel>> GetCarsAsync(bool isAdmin);
    Task<CarViewModel?> GetCarByIdAsync(int id, bool isAdmin);
    Task<UpdateCarViewModel?> GetUpdateCarViewModelByIdAsync(int id);
    Task<bool> AddCarAsync(CreateCarViewModel car);
    Task<bool> UpdateCarAsync(UpdateCarViewModel car);
    Task<CarViewModel?> DeleteCarAsync(int id);
}
