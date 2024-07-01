using MVCSmallFarm.Services;

namespace MVCSmallFarm.ViewModels;

public class ShoppingCartViewModel
{
    public ShppingCartService ShoppingCartSV { get; set; }
    public decimal SCTotal { get; set; }
}
