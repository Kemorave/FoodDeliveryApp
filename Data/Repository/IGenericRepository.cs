using FoodDeliveryApp.Data.Model;
using Microsoft.EntityFrameworkCore;
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
    public interface IOrderRepository : IImmutableDataRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllUserOrders(string userId);
        Task<IPagedList<Order>> GetAllUserOrdersPaged(string userId,int page,int count);
    }
    public interface IItemsRepository : IGenericRepository<Item>
    {
        Task<IEnumerable<Item>> GetAllUserItems(string userId);
    }
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        IItemsRepository Items { get; }
        int Save();
    }


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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IOrderRepository Orders { get; }
        public IItemsRepository Items { get; }

        public UnitOfWork(ApplicationDbContext bookStoreDbContext,
            IOrderRepository booksRepository,
            IItemsRepository catalogueRepository)
        {
            this._context = bookStoreDbContext;

            this.Orders = booksRepository;
            this.Items = catalogueRepository;
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
