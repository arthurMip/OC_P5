using ExpressVoitures.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpressVoitures.Data.Repositories;

public class CarRepository(ApplicationDbContext context) : ICarRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> AddCarAsync(Car car)
    {
        _context.Add(car);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCarAsync(int id)
    {
        Car? car = await _context.Cars.FindAsync(id);
        if (car == null)
        {
            return false;
        }
        _context.Cars.Remove(car);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
        return await _context.Cars
            .Include(c => c.ManufacturingInfo)
            .Include(c => c.SellingInfo)
            .Include(c => c.FixingInfo)
            .Include(c => c.BuyingInfo)
            .Include(c => c.Image)
            .ToListAsync();
    }

    public async Task<IEnumerable<Car>> GetAvailableCarsAsync()
    {
        return await _context.Cars
            .Where(c => c.Visible)
            .Include(c => c.ManufacturingInfo)
            .Include(c => c.SellingInfo)
            .Include(c => c.FixingInfo)
            .Include(c => c.BuyingInfo)
            .Include(c => c.Image)
            .ToListAsync();
    }

    public Task<Car?> GetCarByIdAsync(int id)
    {
        return _context.Cars
            .Include(c => c.ManufacturingInfo)
            .Include(c => c.SellingInfo)
            .Include(c => c.FixingInfo)
            .Include(c => c.BuyingInfo)
            .Include(c => c.Image)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> UpdateCarAsync(Car car)
    {
        var existingCar = await _context.Cars.FindAsync(car.Id);
        if (existingCar == null)
        {
            return false;
        }

        existingCar.Visible = car.Visible;
        existingCar.BuyingInfo = car.BuyingInfo;
        existingCar.FixingInfo = car.FixingInfo;
        existingCar.SellingInfo = car.SellingInfo;
        existingCar.ManufacturingInfo = car.ManufacturingInfo;
        existingCar.Image = car.Image;

        _context.Cars.Update(existingCar);
        return await  _context.SaveChangesAsync() > 0;
    }
}
