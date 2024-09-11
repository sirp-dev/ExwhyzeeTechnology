﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using ExwhyzeeTechnology.Domain.Models;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.IProposalPage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager")]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IList<Proposal> Proposal { get;set; }

        public async Task OnGetAsync()
        {
            Proposal = await _context.Proposals
                .Include(p => p.SubmittedBy).ToListAsync();
        }
    }
}