namespace MVCSmallFarm.ViewModels;

public class OrderHistory
{
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public DateTime OrderDate { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public string ImageUrl { get; set; }
}
