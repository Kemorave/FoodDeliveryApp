using FoodDeliveryApp.Data.Model;
using FoodDeliveryApp.Data.Repository;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDeliveryApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUnitOfWork unitOfWork; 

        public IndexModel(ILogger<IndexModel> logger, IUnitOfWork unitOfWork  )
        {
            _logger = logger;
            this.unitOfWork = unitOfWork; 
        }
        public IEnumerable<Item> Items { get; set; }
        public async Task OnGet()
        {
            Items = await unitOfWork.Items.GetAll();
           // for (int i = 0; i < 10; i++)
           // {
           //  await   unitOfWork.Items.Add(new Item()
           //     {
           //         PictureUrl = "https://media.moddb.com/images/members/5/4550/4549205/duck.jpg"
           //,
           //         Price = 1400,
           //         Name = "Banana duck",
           //         UserId = "Nope",
           //     });
           // }
           // unitOfWork.Complete();

        }
    }
}