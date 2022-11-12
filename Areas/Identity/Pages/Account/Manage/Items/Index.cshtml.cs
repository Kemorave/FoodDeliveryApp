using FoodDeliveryApp.Data.Model;
using FoodDeliveryApp.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Packaging;

namespace FoodDeliveryApp.Areas.Identity.Pages.Account.Manage.Items
{
    public class IndexModel : PageModel
    { 
        private readonly IItemsRepository itemsRepository;

        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(  IItemsRepository itemsRepository, UserManager<ApplicationUser> userManager = null)
        { 
            this.itemsRepository = itemsRepository;
            _userManager = userManager;
        }

        public IList<Item> Items { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Items = new List<Item>();

            var userId = (await _userManager.GetUserAsync(User)).Id;
            Items.AddRange(await itemsRepository.GetAllUserItems(userId)); 
        }
    }
}
