namespace ExpressVoitures.Data.Entities;

public class SellingInfo
{
    public int CarId { get; set; }
    public DateTime AvailableDate { get; set; }
    public DateTime? SellingDate { get; set; }
    public decimal Price { get; set; }
}
