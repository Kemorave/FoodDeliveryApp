using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace FoodDeliveryApp.Data.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        protected GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public GenericImmutableDataRepository<T> ReadOnly() => new GenericImmutableDataRepository<T>(_context);
        public async Task<T?> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        { 
            return await _context.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }

        public async Task<IPagedList<T>> GetPaged(int page, int count)
        {
            return await _context.Set<T>().ToPagedListAsync(page, count);
        }
    }
}
