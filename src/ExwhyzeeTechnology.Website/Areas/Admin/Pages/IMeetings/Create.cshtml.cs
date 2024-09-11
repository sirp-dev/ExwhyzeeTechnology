﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using ExwhyzeeTechnology.Domain.Models;
using Amazon.Runtime;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.IMeetings
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Secretary")]

    public class CreateModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public CreateModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Meeting Meeting { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
           

            _context.Meetings.Add(Meeting);
            await _context.SaveChangesAsync(); TempData["success"] = "Successful";

            return RedirectToPage("./Details", new {id = Meeting.Id});
        }
    }
}
