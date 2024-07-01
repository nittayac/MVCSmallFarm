using Microsoft.EntityFrameworkCore;
using MVCSmallFarm.Models.dbs;

namespace MVCSmallFarm.Services;

public class CommentsService
{
    private readonly SmallFarmContext _db;
    public CommentsService(SmallFarmContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ProductWithComment>> CommentsByProductId(int id)
    {
        var cs = _db.ProductWithComments
                .OrderBy(c => c.RunningNumber)
                .Where(i => i.IsShow == true)
                .Where(p => p.ProductId == id);

        return await cs.ToListAsync();
    }
}
