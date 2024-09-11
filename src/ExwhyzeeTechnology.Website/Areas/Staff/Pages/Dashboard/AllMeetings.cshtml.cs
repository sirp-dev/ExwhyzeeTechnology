using ExwhyzeeTechnology.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Web.Areas.Staff.Pages.Dashboard
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class AllMeetingsModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public AllMeetingsModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<Meeting> Meeting { get; set; }

        public async Task OnGetAsync()
        {
            Meeting = await _context.Meetings.Include(x => x.MeetingAttendances).ToListAsync();
        }
    }
}