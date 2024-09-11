﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using ExwhyzeeTechnology.Domain.Models;

namespace ExwhyzeeTechnology.Website.Areas.Admin.Pages.IReport
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Manager,Admin")]

    public class IndexModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public IndexModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        public IQueryable<Report> Report { get; set; }
        public IQueryable<string> Events { get; set; }
        public string Desc { get; set; }
        public string PreviousWeek { get; set; }
        public string PreviousWeekTitle { get; set; }
        public string NextWeek { get; set; }
        public string NextWeekTitle { get; set; }
        public string Title { get; set; }

        public async Task OnGetAsync(string searchdate = null)
        {
            //DateTime startOfWeek = DateTime.Now.AddDays(-1 * Convert.ToInt32(DateTime.Now.DayOfWeek)).AddDays(1);
            //DateTime endOfWeek = startOfWeek.AddDays(6);
            DateTime querydate = DateTime.Today;
            if (searchdate == null)
            {

                Report = from s in _context.Report.Include(x => x.User).OrderByDescending(x => x.Date)
                                 .Where(ob => ob.Date.Date == DateTime.UtcNow.Date)
                             select s;
                searchdate = DateTime.UtcNow.Date.ToString("ddd dd MMM, yyyy");
            }
            else
            {
                querydate = DateTime.Parse(searchdate).Date;

                Report = from s in _context.Report.Include(x => x.User).OrderByDescending(x => x.Date)
                                 .Where(ob => ob.Date.Date == querydate.Date)
                             select s;
                searchdate = querydate.Date.ToString("ddd dd MMM, yyyy");
            }
            int gdhd = Report.Count();
            TempData["date"] = searchdate;


        }

    }
}