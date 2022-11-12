using FoodDeliveryApp.Data.Model;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FoodDeliveryApp.Data.Repository
{
    public class OrdersRepository : GenericImmutableDataRepository<Order>, IOrderRepository
    {
        public OrdersRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task<Order?> Get(int id)
        {
            var e = await _context.Set<Order>().Include(nameof(Order.OrderItems)).FirstAsync(a => a.Id == id);
            if (e != null)
                _context.Entry(e).State = EntityState.Detached;
            return e;
        }

        public async Task<IEnumerable<Order>> GetAllUserOrders(string userId)
        {
            return await _context.Set<Order>().AsNoTracking().Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<IPagedList<Order>> GetAllUserOrdersPaged(string userId, int page, int count)
        {
            return await _context.Set<Order>().Where(a=>a.UserId==userId).ToPagedListAsync(page, count);
        }
    }
}
