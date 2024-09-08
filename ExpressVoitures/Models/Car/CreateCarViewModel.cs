namespace ExpressVoitures.Models.Car;

public class CreateCarViewModel
{
    public int Id { get; set; }
    public bool Visible { get; set; }
    public int Year { get; set; }
    public string VinCode { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Finish { get; set; }
    public decimal ByingPrice { get; set; }
    public DateTime ByingDate { get; set; }
    public string FixingDescription { get; set; }
    public decimal FixingCost { get; set; }
    public DateTime AvailableDate { get; set; }
    public DateTime SellingDate { get; set; }
    public decimal  SellingPrice { get; set; }
}
