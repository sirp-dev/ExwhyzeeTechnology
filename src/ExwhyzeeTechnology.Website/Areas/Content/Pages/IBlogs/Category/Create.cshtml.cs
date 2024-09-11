﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Domain.Models;

namespace ExwhyzeeTechnology.Website.Areas.Content.Pages.IBlogs.Category
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Editor")]

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
        public BlogCategory BlogCategory { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            

            _context.BlogCategories.Add(BlogCategory);
            await _context.SaveChangesAsync(); TempData["success"] = "Successful";

            return RedirectToPage("./Index");
        }
    }
}