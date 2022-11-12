using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FoodDeliveryApp.Data.Repository
{
    public class GenericImmutableDataRepository<T> : IImmutableDataRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public GenericImmutableDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T?> Get(int id)
        {
            var e = await _context.Set<T>().FindAsync(id);
            if (e != null)
                _context.Entry(e).State = EntityState.Detached;
            return e;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual async Task<IPagedList<T>> GetPaged(int page, int count)
        {
            return await _context.Set<T>().AsNoTracking().ToPagedListAsync(page, count);
        }
    }
}
