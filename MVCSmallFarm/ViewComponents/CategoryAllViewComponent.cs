using Microsoft.AspNetCore.Mvc;
using MVCSmallFarm.Repositories;
using MVCSmallFarm.ViewModels;

namespace MVCSmallFarm.ViewComponents;

public class CategoryAllViewComponent : ViewComponent
{
    private ICategoriesRepository _catrepo;
    public CategoryAllViewComponent(ICategoriesRepository catrepo)
    {
        _catrepo = catrepo;
    }
    public async Task<IViewComponentResult> InvokeAsync(int pgview, int flg)
    {
        var  lcat = await _catrepo.GetAllCategoryManage();
        return View("CategoryAllView", lcat.ToList());
    }
}
