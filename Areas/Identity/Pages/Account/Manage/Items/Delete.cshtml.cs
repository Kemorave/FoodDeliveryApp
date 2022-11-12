using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Data.Model;
using FoodDeliveryApp.Data.Repository;

namespace FoodDeliveryApp.Areas.Identity.Pages.Account.Manage.Items
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork = null)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
      public Item Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var item = await unitOfWork.Items.Get(id??0);

            if (item == null)
            {
                return NotFound();
            }
            else 
            {
                Item = item;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            var item = await unitOfWork.Items.Get(id??0);

            if (item != null)
            {
                Item = item;
                unitOfWork.Items.Delete(Item);
                unitOfWork.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
