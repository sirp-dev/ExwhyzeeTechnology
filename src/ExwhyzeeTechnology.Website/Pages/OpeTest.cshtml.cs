using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExwhyzeeTechnology.Website.Pages
{
    [AllowAnonymous]
    public class OpeTestModel : PageModel
    {
        private readonly UserManager<Profile> _userManager;

        private readonly IEmailSender _sender;
        public OpeTestModel(IEmailSender sender, UserManager<Profile> userManager)
        {
            _sender = sender;
            _userManager = userManager;
        }
        public bool Result = false;
        public async Task<IActionResult> OnGetAsync()
        {
           
             return Page();
        }
    }
}
