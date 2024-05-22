
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

        if (pgview == 1)  //ViewAll
        {
            ViewData["Events"] = await _prdrepo.GetAllProduct();
            return View("ProductAllView");
        }
        else if (pgview == 2)
        {
            ProductCatViewModel pd = new();
            if (flg == 0) //Add
            {
                //pd.CategoryId = 0;
                //pd.IsPromotion = true;
                ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName");
            }
            else if (flg == 1) //Edit
            {
                pd = await _prdrepo.GetAllProductById(flg);
                ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName", pd.CategoryId);
            }

            return View("ProductAddEditView", pd);

        }

        return View("ProductAddEditView", pc);



    }

}
 