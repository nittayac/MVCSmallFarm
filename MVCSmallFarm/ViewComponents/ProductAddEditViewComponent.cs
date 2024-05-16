
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.Repositories;
using MVCSmallFarm.ViewModels;
using System.Threading.Tasks;
namespace MVCSmallFarm.ViewComponents;


public class ProductAddEditViewComponent :  ViewComponent
{
    private readonly IProductRepository _prdrepo;
    private readonly ICategoriesRepository _catrepo;
    public ProductAddEditViewComponent(ICategoriesRepository catrepo, IProductRepository prdrepo)
    {
        //_prdrepo = prdrepo;
        _catrepo = catrepo;
        _prdrepo = prdrepo;
    }
    public async Task<IViewComponentResult> InvokeAsync(bool showPrevious,int showUpcoming)
        {

            ProductCatViewModel pd = await _prdrepo.GetAllProductById(showUpcoming);
            if (pd == null)
            {
                ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName");
            }
            else
            {
                ViewData["Category"] = new SelectList(await _catrepo.GetAllCategory(), "CategoryId", "CategoryName", pd.CategoryId);
            }
       
            return View(pd);
        }

 }
    

