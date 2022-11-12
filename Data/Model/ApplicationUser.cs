using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FoodDeliveryApp.Data.Model
{

    public enum UserClass {
        [Display(Name = "Normal user")]
        Normal,

        [Display(Name = "VIP user")] VIP }
    public class ApplicationUser : IdentityUser
    {
        public UserClass UserClass { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
