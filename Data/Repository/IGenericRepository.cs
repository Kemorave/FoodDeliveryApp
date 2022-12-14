using X.PagedList;

namespace FoodDeliveryApp.Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IPagedList<T>> GetPaged(int page, int count);
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        GenericImmutableDataRepository<T> ReadOnly();
    }
}
