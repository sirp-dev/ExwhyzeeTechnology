using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExwhyzeeTechnology.Website.Areas.Management.Pages.Users
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin")]

    public class UpdateModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
