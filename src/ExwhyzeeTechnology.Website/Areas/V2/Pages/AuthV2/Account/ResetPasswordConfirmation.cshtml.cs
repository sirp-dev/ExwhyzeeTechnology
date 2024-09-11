﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ExwhyzeeTechnology.Website.V2.Pages.Authv2.Account
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;

        public ResetPasswordConfirmationModel(DashboardDbContext context)
        {
            _context = context;
        }

        public string TemplateLogin { get; set; }
        public SuperSetting SuperSetting { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var check = await _context.SuperSettings.FirstOrDefaultAsync();
            if (check == null)
            {
                return RedirectToPage("/AuthVadmin/Readonly", new { area = "V2" });
            }
            TemplateLogin = check.LoginTemplateKey;
            SuperSetting =check;
            return Page();
        }
    }
}