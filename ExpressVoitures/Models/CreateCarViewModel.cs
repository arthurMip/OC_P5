using System.ComponentModel.DataAnnotations;

namespace ExpressVoitures.Models;

public class CreateCarViewModel
{
    public int Id { get; set; }

    [Required]
    public bool Visible { get; set; }
    [Required]
    [Range(1970, 2100)]
    public int ManufacturingYear { get; set; }
    [Required]
    public string VinCode { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public string Finish { get; set; }
    [Required]
    public DateTime BuyingDate { get; set; }
    [Required]
    [Range(0, 1000000)]
    public decimal BuyingPrice { get; set; }
    [Required]
    public string FixingDescription { get; set; }
    [Required]
    [Range(0, 1000000)]
    public decimal FixingCost { get; set; }
    [Required]
    public DateTime AvailableDate { get; set; }
    public DateTime? SellingDate { get; set; }
    [Required]
    [Range(0, 1000000)]
    public decimal SelingPrice { get; set; }
}
