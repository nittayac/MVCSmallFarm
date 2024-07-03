using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.ViewModels;

namespace MVCSmallFarm.Repositories;

public interface IShoppingCartRepository
{
    Task SaveOrder(List<ShoppingCartItem> items, string userid, string ip);
    Task<List<OrderHistory>> OrderHistory(string userid);
}
