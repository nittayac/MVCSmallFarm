using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace MVCSmallFarm.Services;

public class ShppingCartService
{
    private readonly SmallFarmContext _db;

    public string Id { get; set; }

    public List<ShoppingCartItem> SCItem { get; set; }

    public ShppingCartService(SmallFarmContext db) 
    {
        _db = db;
    }

    public static ShppingCartService CreateShoppingCart(IServiceProvider services) {
        ISession ss = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
        var db = services.GetService<SmallFarmContext>();

        string cartId = ss.GetString("CardId");

        if (string.IsNullOrEmpty(cartId))
        {
            cartId = Guid.NewGuid().ToString(); 
        }

        ss.SetString("CardId", cartId);

        return new ShppingCartService(db)
        {
            Id = cartId
        };
    }

    public List<ShoppingCartItem> MyShoppingCart() 
    {
        return SCItem ??
               ( SCItem = _db.ShoppingCartItems
                .Where(id => id.ShoppingCartId == Id)
                .Include(p => p.Product).ToList());
    }

    public void Add(ProductCatViewModel data) 
    {
        var item = _db.ShoppingCartItems
            .Where(p => p.ProductId == data.ProductId
            && p.ShoppingCartId == Id).FirstOrDefault();

        if (item == null) //Ad new a product
        {
            item = new ShoppingCartItem()
            {
                ShoppingCartId = Id,
                ProductId = data.ProductId,
                Amount =1
            };
            _db.ShoppingCartItems.Add(item);
        }
        else { //Add the amount of the product
            item.Amount++;
        }
        _db.SaveChanges();
    }


    public void  Remove(ProductCatViewModel data){
        var item =( _db.ShoppingCartItems
         .Where(p => p.ShoppingCartId == Id
         && p.ProductId == data.ProductId).FirstOrDefault());

        if (item != null)
        {
            if (item.Amount > 1)
            {
                item.Amount--;
            }
            else {
                _db.ShoppingCartItems.Remove(item);
            }

            _db.SaveChanges(true);  
        }
    }

    public async Task Clear() {

        var item = await _db.ShoppingCartItems
            .Where(id => id.ShoppingCartId == Id).ToListAsync();

        if (item != null)
        {
            _db.ShoppingCartItems.RemoveRange(item);
            await _db.SaveChangesAsync();
        }
    }

    public decimal Total() {
        var total = _db.ShoppingCartItems
              .Where(sc => sc.ShoppingCartId == Id)
              .Select(p => p.Product.Price * p.Amount)
              .Sum();

        return (decimal)total;   
    }




}
