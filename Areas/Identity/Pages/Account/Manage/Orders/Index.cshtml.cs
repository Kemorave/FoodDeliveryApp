using FoodDeliveryApp.Data.Model;
using FoodDeliveryApp.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace FoodDeliveryApp.Areas.Identity.Pages.Account.Manage.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        private readonly UserManager<ApplicationUser> _userManager;
        public IndexModel(IOrderRepository context,
UserManager<ApplicationUser> userManager)
        {
            _orderRepository = context;
            _userManager = userManager;
        }
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; }
        public IPagedList<Order> PagedResult { get; set; }

        public async Task OnGetAsync()
        {
            if (PageNumber <= 0)
            {
                PageNumber = 1;
            }
            var userId = (await _userManager.GetUserAsync(User)).Id; 
            PagedResult = await _orderRepository.GetAllUserOrdersPaged(userId, PageNumber, 6); 
        }
    }
}
