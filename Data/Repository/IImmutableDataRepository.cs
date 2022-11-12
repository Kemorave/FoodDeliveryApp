using X.PagedList;

namespace FoodDeliveryApp.Data.Repository
{
    public interface IImmutableDataRepository<T> where T : class
    {
        Task<T?> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IPagedList<T>> GetPaged(int page, int count);
        Task Add(T entity);
    }
}
