using Microsoft.CodeAnalysis;
using MVCSmallFarm.Models.dbs;

namespace MVCSmallFarm.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly SmallFarmContext _db;

    public ShoppingCartRepository(SmallFarmContext db)
    {
        _db = db;
    }
    public async Task SaveOrder(List<ShoppingCartItem> items, string userid, string ip)
    {
        var o = new Order()
        {
            UserId = userid,
            Email = "",
            OrderDate = DateTime.Now,
            IsReceived = false,
            IsPaid = false,
            IsNormal = true,
            UserIp = ip
        };

        await _db.Orders.AddAsync(o);
        await _db.SaveChangesAsync();


        OrderDetail od = null;
        foreach (var item in items)
        {
            od = new OrderDetail()
            {
                OrderId = o.OrderId,
                ProductId = item.ProductId,
                Amount = item.Amount,
                Price = (decimal)item.Product.Price
            };

            await _db.OrderDetails.AddAsync(od);
        }

        await _db.SaveChangesAsync();
    }
}
