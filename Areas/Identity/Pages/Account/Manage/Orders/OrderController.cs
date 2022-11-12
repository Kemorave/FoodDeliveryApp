using FoodDeliveryApp.Data;
using FoodDeliveryApp.Data.Model;
using FoodDeliveryApp.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryApp.Areas.Identity.Pages.Account.Manage.Orders
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork unitOfWork;

        private readonly UserManager<ApplicationUser> _userManager;
        public OrderController(ApplicationDbContext context, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public class CreateOrderModel
        {
            public List<OrderItem> Items { get; set; }
        }
        [HttpPost("Order/Create")]
        public async Task<IActionResult> Create([FromBody] CreateOrderModel? createOrderModel)
        {
            if (createOrderModel?.Items == null||createOrderModel.Items.Count==0)
            {
                return BadRequest("Null data");
            }
            var userId = (await _userManager.GetUserAsync(User))?.Id;
            if (string.IsNullOrEmpty(userId))
            {
                return Forbid();
            }

            Order order = new Order();
            foreach (var orItem in createOrderModel.Items)
            {
                var item = await unitOfWork.Items.ReadOnly().Get(orItem.ItemId);
                if (item == null)
                {
                    return NotFound($"Item with ID {orItem.ItemId} was not found");
                }
                orItem.ItemName = item.Name;
                orItem.TotalPrice = item.Price * orItem.Quantity;
                order.TotalPrice += orItem.TotalPrice;
            }

            order.OrderItems = createOrderModel.Items;
            order.UserId = userId;
            await unitOfWork.Orders.Add(order);
            unitOfWork.Save();
            return Created("", order);
        }


        [HttpPost("Order/Details")]
        public IActionResult Details([FromBody] Order dummy)
        {
            int id = Convert.ToInt32(dummy.Id);

            Order? order = _context.Orders
                .Include(nameof(Order.OrderItems))
                .FirstOrDefault(a => a.Id == id)
               ;

            if (order == null)
                return NotFound();

            return PartialView("~/Areas/Identity/Pages/Account/Manage/Orders/_PopupPartial.cshtml", order);
        }
    }
}
