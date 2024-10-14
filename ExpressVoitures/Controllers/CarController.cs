using ExpressVoitures.Models.Car;
using ExpressVoitures.Models.Shared;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Controllers;

[Authorize(Roles="Admin")]
public class CarController : Controller
{
    private readonly ICarService _carService;
    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [Authorize(Roles="Admin,User")]
    public async Task<IActionResult> Index()
    {
        bool isAdmin = User.IsInRole("Admin");

        var cars =  await _carService.GetCarsAsync(isAdmin);
        
        return View(cars);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        bool isAdmin = User.IsInRole("Admin");

        var car = await _carService.GetCarByIdAsync(id, isAdmin);

        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCarViewModel car)
    {
        if (!ModelState.IsValid)
        {
            return View(car);
        }

        var succeded = await _carService.AddCarAsync(car);

        if (!succeded)
        {
            ViewBag.Error = "Failed to add car.";
            return View(car);
        }

        return RedirectToAction("CreateSuccess");
    }



    public async Task<IActionResult> Update(int id)
    {
        var model = await _carService.GetUpdateCarViewModelByIdAsync(id);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateCarViewModel car)
    {
        if (!ModelState.IsValid)
        {
            return View(car);
        }

        var succeded = await _carService.UpdateCarAsync(car);

        if (!succeded)
        {
            ViewBag.Error = "Failed to update car.";
            return View(car);
        }

        return RedirectToAction("UpdateSuccess");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedCar = await _carService.DeleteCarAsync(id);

        if (deletedCar == null)
        {
            ViewBag.Error = "Failed to delete car.";
            return RedirectToAction("Details", new { id });
        }

        return RedirectToAction("DeletedSuccess", deletedCar);

    }

    public IActionResult CreateSuccess()
    {
        SuccessViewModel model = new()
        {
            Title = "Merci !",
            Description = "votre voiture a bien été publiée"
        };

        return View("SuccessPage", model);
    }

    public IActionResult UpdateSuccess()
    {
        SuccessViewModel model = new()
        {
            Title = "Merci !",
            Description = "votre voiture a bien été mise à jour"
        };

        return View("SuccessPage", model);
    }

    public IActionResult DeleteSuccess(CarViewModel car)
    {
        SuccessViewModel model = new()
        {
            Title = $"{car.Year} {car.Brand} {car.Model} {car.Finish}",
            Description = "votre voiture a bien été supprimées"
        };

        return View("SuccessPage", model);
    }
}
