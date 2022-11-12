using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.Data.Model
{
    public interface IEntity
    {
        [Key]
        public int Id { get; set; }
    }
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItem>  OrderItems { get; set; }
    }
    public class OrderItem : BaseEntity
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public decimal TotalPrice { get; set; } 

    }
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string UserId { get; set; }
    }
}
