using MVCSmallFarm.Models.dbs;

namespace MVCSmallFarm.Repositories;

public interface IShoppingCartRepository
{
    Task SaveOrder(List<ShoppingCartItem> items, string userid, string ip);
}
