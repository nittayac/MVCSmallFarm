using MVCSmallFarm.Models.dbs;

namespace MVCSmallFarm.Repositories
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> GetAllCategory();
    }
}
