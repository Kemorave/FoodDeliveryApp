using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDeliveryApp.Data;
using FoodDeliveryApp.Data.Model;
using FoodDeliveryApp.Data.Repository;

namespace FoodDeliveryApp.Areas.Identity.Pages.Account.Manage.Items
{
    public class EditModel : PageModel
    {

        private readonly IUnitOfWork unitOfWork;
        public EditModel(IUnitOfWork unitOfWork = null)
        {
            this.unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Item Item { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null  )
            {
                return NotFound();
            }

            var item =  await unitOfWork.Items.Get(id ?? 0);
            if (item == null)
            {
                return NotFound();
            }
            Item = item;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        { 
            unitOfWork.Items.Update(Item);

            try
            {
                  unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(Item.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ItemExists(int id)
        {
          return unitOfWork.Items.Get( id)!=null;
        }
    }
}
