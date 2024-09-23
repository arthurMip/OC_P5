﻿using ExpressVoitures.Models.Car;
using ExpressVoitures.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Controllers;

[Authorize]
public class CarController : Controller
{
    private readonly ICarService _carService;
    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    public async Task<IActionResult> Index()
    {
        var cars =  await _carService.GetCarsAsync(false);
        
        return View(cars);
    }

    public IActionResult Details(int id)
    {
        return View();
    }

    [Authorize(Roles="Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Create(CreateCarViewModel car)
    {
        if (!ModelState.IsValid)
        {
            return View(car);
        }

        var succeded = await _carService.AddCarAsync(car);

        if (!succeded)
        {
            ModelState.AddModelError(string.Empty, "Failed to add car.");
            return View(car);
        }

        return RedirectToAction("CreateSuccess");
    }

    public IActionResult CreateSuccess()
    {
        return View();
    }

    [Authorize(Roles="Admin")]
    public IActionResult Edit(int id)
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult Edit(EditCarViewModel model)
    {
        return RedirectToAction("Index");
    }
}
