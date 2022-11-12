namespace FoodDeliveryApp.Data.Model
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
