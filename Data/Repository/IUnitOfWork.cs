namespace FoodDeliveryApp.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Orders { get; }
        IItemsRepository Items { get; }
        int Save();
    }
}
