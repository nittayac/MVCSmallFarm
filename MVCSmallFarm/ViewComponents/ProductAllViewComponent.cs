
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.Repositories;
using MVCSmallFarm.ViewModels;
using System.Threading.Tasks;
namespace MVCSmallFarm.ViewComponents;


public class ProductAllViewComponent :  ViewComponent
{
    private readonly IProductRepository _prdrepo;
    private readonly SmallFarmContext _db;
    public ProductAllViewComponent(SmallFarmContext db, IProductRepository prdrepo)
    {
        //_prdrepo = prdrepo;
        _db = db;
        _prdrepo = prdrepo;
    }
    public async Task<IViewComponentResult> InvokeAsync(bool showPrevious,bool showUpcoming)
    {
                    ViewData["Events"] = await _prdrepo.GetAllProduct();
                    return View();
    }

}
 