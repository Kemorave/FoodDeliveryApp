using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Data.Model;
using Microsoft.AspNetCore.Identity;
using FoodDeliveryApp.Data.Repository;

namespace FoodDeliveryApp.Areas.Identity.Pages.Account.Manage.Items
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateModel(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager = null)
        {
            this.unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        { 

            var userId = (await _userManager.GetUserAsync(User)).Id;
            Item.UserId = userId;
           await unitOfWork.Items.Add(Item);
              unitOfWork.Save();

            return RedirectToPage("./Index");
        }
    }
}
