using FoodDeliveryApp.Data.Model;

namespace FoodDeliveryApp.Data.Repository
{
    public interface IItemsRepository : IGenericRepository<Item>
    {
        Task<IEnumerable<Item>> GetAllUserItems(string userId);
    }
}
