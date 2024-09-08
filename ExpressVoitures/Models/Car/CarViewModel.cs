namespace ExpressVoitures.Models.Car;

public class CarViewModel
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int Year { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public string Finish { get; set; }
    public string ImageUrl { get; set; }
}
