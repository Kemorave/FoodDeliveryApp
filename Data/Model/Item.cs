namespace FoodDeliveryApp.Data.Model
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string UserId { get; set; }
    }
}
