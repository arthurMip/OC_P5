using ExpressVoitures.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Controllers;

public class CarController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Details(int id)
    {
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(CreateCarViewModel model)
    {
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        return View();
    }

    [HttpPost]
    public IActionResult Edit(EditCarViewModel model)
    {
        return RedirectToAction("Index");
    }
}
