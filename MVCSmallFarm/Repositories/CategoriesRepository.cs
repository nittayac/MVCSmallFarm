using Microsoft.EntityFrameworkCore;
using MVCSmallFarm.Models.dbs;

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
    }
}
