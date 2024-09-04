namespace ExpressVoitures.Data.Entities;

public class Car
{
    public int Id { get; set; }
    public bool Visible { get; set; }
    public BuyingInfo BuyingInfo { get; set; }
    public FixingInfo FixingInfo { get; set; }
    public SellingInfo SellingInfo { get; set; }
    public ManufacturingInfo ManufacturingInfo { get; set; }
    public List<Image> Images { get; set; } = [];
}
