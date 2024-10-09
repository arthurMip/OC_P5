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
            .Include(c => c.ManufacturingInfo)
            .Include(c => c.SellingInfo)
            .Include(c => c.FixingInfo)
            .Include(c => c.BuyingInfo)
            .Include(c => c.Image)
            .Where(c => c.Visible)
            .Where(c => DateTime.Now >= c.SellingInfo.AvailableDate)
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
        _context.Cars.Update(car);
        return await  _context.SaveChangesAsync() > 0;
    }
}
