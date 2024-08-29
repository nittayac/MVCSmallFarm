using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.ViewModels;

namespace MVCSmallFarm.Repositories
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> GetAllCategory();

        Task<List<CategoryViewModel>> GetAllCategoryManage();
        Task<CategoryViewModel> GetCategoryById(int? id);
    }
}
