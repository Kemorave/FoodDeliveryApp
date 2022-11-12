using FoodDeliveryApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FoodDeliveryApp.Data.Repository
{
    public class ItemsRepository : GenericRepository<Item>, IItemsRepository
    {
        public ItemsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Item>> GetAllUserItems(string userId)
        {
            return await _context.Set<Item>().AsNoTracking().Where(a => a.UserId == userId).ToListAsync();
        }
    }
}
