
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.Repositories;
using MVCSmallFarm.ViewModels;
using System.Threading.Tasks;

namespace MVCSmallFarm.ViewComponents;



public class ProductAllViewComponent :  ViewComponent
{
    private readonly IProductRepository _prdrepo;
    private readonly ICategoriesRepository _catrepo;
    public ProductAllViewComponent(ICategoriesRepository catrepo, IProductRepository prdrepo)
    {
        //_prdrepo = prdrepo;
        _catrepo = catrepo;
        _prdrepo = prdrepo;
    }
    public async Task<IViewComponentResult> InvokeAsync(int pgview, int flg,ProductCatViewModel pc)
    {

            var pd = await _prdrepo.GetAllProduct();
           // ViewData["Events"] = pd;
            return View("ProductAllView",pd.ToList());
   
    }

}
 