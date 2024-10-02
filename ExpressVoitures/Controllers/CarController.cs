﻿using ExpressVoitures.Data.Entities;
using ExpressVoitures.Models.Car;
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

    [AllowAnonymous]
    [HttpGet("/api/cars")]
    public async Task<IActionResult> GetCars()
    {
        var cars = await _carService.GetCarsAsync(false);
        return Ok(cars);
    }

    [Authorize(Roles="Admin,User")]
    public async Task<IActionResult> Index()
    {
        var cars =  await _carService.GetCarsAsync(false);
        
        return View(cars);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var car = await _carService.GetCarByIdAsync(id);
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

    public IActionResult CreateSuccess()
    {
        return View();
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

        return RedirectToAction("CreateSuccess");
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

        return View("DeletedSuccess", deletedCar);

    }
}
