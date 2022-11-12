using System.ComponentModel.DataAnnotations;

namespace FoodDeliveryApp.Data.Model
{
    public interface IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
