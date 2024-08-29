using Microsoft.AspNetCore.Mvc;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.Repositories;
using MVCSmallFarm.ViewModels;

namespace MVCSmallFarm.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoriesRepository _catrepo;

        public CategoryController(ICategoriesRepository catrepo)
        {
            _catrepo = catrepo;
        }
        public IActionResult Index()
        {
            return View();
        }


        //public async Task<IActionResult> CateAddEditView(int id, int flg)
        //{
        //    var cat = await _catrepo.GetCategoryById(id);
        //    return PartialView("CategoryAddEditView", cat);
        //}
    }
}
