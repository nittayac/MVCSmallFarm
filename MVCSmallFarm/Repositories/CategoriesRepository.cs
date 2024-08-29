using Microsoft.EntityFrameworkCore;
using MVCSmallFarm.Models.dbs;
using MVCSmallFarm.ViewModels;

namespace MVCSmallFarm.Repositories
{
    public class CategoriesRepository :ICategoriesRepository
    {
        private SmallFarmContext _db;
        public CategoriesRepository(SmallFarmContext db) {
            _db = db;
        }
        public async Task<List<Category>> GetAllCategory()
        {
            Category items = new Category();
            items.CategoryId = 0;
            items.CategoryName = "Please choose a category";
            items.Description = "Default";

            List<Category> ls = await (_db.Categories.OrderBy(c => c.CategoryName).ToListAsync());
            ls.Insert(0, items);

            return ls;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryManage()
        {
            var c = await (from ct in _db.Categories
                           select new CategoryViewModel
                           {
                               CategoryId = ct.CategoryId,
                               CategoryName = ct.CategoryName,
                               Description = ct.Description,
                               IsActive = ct.IsActive
                           }).ToListAsync();


            CategoryViewModel items = new CategoryViewModel();
      
            return c;
        }

        public async Task<CategoryViewModel> GetCategoryById(int? id)
        {
            try
            {
                var c = await(from ct in _db.Categories
                              where ct.CategoryId == id
                              select new CategoryViewModel
                              {
                                  CategoryId = ct.CategoryId,
                                  CategoryName = ct.CategoryName,
                                  Description = ct.Description,
                                  IsActive = ct.IsActive
                              }).FirstOrDefaultAsync();

                return c;
            }
            catch
            {
                CategoryViewModel c = new CategoryViewModel();
                return c;
            }

        }
    }
}
