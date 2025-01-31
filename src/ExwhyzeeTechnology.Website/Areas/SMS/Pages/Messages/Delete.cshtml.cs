﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;

namespace ExwhyzeeTechnology.Website.Areas.SMS.Pages.Messages
{
    public class DeleteModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public DeleteModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SmsMessage SmsMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SmsMessage = await _context.SmsMessages
                .Include(s => s.SmsMessageCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (SmsMessage == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SmsMessage = await _context.SmsMessages.FindAsync(id);

            if (SmsMessage != null)
            {
                _context.SmsMessages.Remove(SmsMessage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { id = SmsMessage.SmsMessageCategoryId });
        }
    }
}
