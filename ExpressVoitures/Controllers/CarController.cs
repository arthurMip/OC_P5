using ExpressVoitures.Models.Car;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpressVoitures.Controllers;

[Authorize]
public class CarController : Controller
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    public CarController(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public IActionResult Index()
    {
        return View();
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

        if (car.Image?.Length > 0)
        {
            string uniqueFileName = Guid.NewGuid().ToString().Substring(24) + Path.GetExtension(car.Image.FileName);
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", uniqueFileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await car.Image.CopyToAsync(fileStream);
        }

        return RedirectToAction("Index");
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
