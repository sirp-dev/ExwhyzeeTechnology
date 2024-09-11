﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.CM
{
    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<CompanyProgram> CompanyProgram { get;set; }
        public CompanyProgramCategory Category { get; set; }

        public async Task OnGetAsync(long id)
        {
            CompanyProgram = await _context.CompanyPrograms
                .Include(c => c.CompanyProgramCategory)
                .Where(x=>x.CompanyProgramCategoryId == id)
                .ToListAsync();

            Category = await _context.CompanyProgramCategories.FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
