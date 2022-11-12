using FoodDeliveryApp.Data.Model;
using X.PagedList;

namespace FoodDeliveryApp.Data.Repository
{
    public interface IOrderRepository : IImmutableDataRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllUserOrders(string userId);
        Task<IPagedList<Order>> GetAllUserOrdersPaged(string userId,int page,int count);
    }
}
