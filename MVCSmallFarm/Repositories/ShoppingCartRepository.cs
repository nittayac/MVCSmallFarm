using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.ViewModels;

namespace MVCSmallFarm.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly SmallFarmContext _db;

    public ShoppingCartRepository(SmallFarmContext db)
    {
        _db = db;
    }

    public async Task<List<OrderHistory>> OrderHistory(string userid)
    {
        var oh =await  (from pd in _db.Products
                        from o in _db.Orders
                        from od in _db.OrderDetails
                        from c in _db.Categories
                        where pd.ProductId == od.ProductId
                        && (pd.CategoryId == c.CategoryId)
                        && (o.OrderId == od.OrderId)
                        && (o.UserId == userid)
                        orderby o.OrderDate descending
                        select new OrderHistory
                        {
                            ProductName = pd.ProductName,
                            CategoryName = c.CategoryName,
                            OrderDate = o.OrderDate,
                            Amount = od.Amount,
                            Price = od.Price,
                            Total = od.Total,
                            ImageUrl = pd.ImageUrl
                        }).ToListAsync() ;


        return oh ;
    }

    public async Task SaveOrder(List<ShoppingCartItem> items, string userid, string ip)
    {
        var o = new Order()
        {
            UserId = userid,
            Email = "",
            OrderDate = DateTime.Now,
            PaidDate = DateTime.Now,
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
