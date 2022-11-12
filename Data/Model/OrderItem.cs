namespace FoodDeliveryApp.Data.Model
{
    public class OrderItem : BaseEntity
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public decimal TotalPrice { get; set; } 

    }
}
