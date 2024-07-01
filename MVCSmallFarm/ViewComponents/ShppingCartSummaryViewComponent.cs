using Microsoft.AspNetCore.Mvc;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.Services;

namespace MVCSmallFarm.ViewComponents;

public class ShppingCartSummaryViewComponent :ViewComponent
{
    private readonly ShppingCartService _shoppingcart;
    public ShppingCartSummaryViewComponent(ShppingCartService shoppingcart)
    {
        _shoppingcart = shoppingcart;
    }
    public IViewComponentResult Invoke() {
        var item = _shoppingcart.MyShoppingCart();
        return View(item.Count);

    }
}
