namespace FoodDeliveryApp.Data.Repository
{
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
