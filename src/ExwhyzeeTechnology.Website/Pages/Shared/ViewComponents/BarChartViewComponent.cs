﻿
using ExwhyzeeTechnology.Application.Dtos;
using ExwhyzeeTechnology.Persistence.EF.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExwhyzeeTechnology.Pages.Shared.ViewComponents
{
    public class BarChartViewComponent : ViewComponent
    {
        private readonly DashboardDbContext _context;

        public BarChartViewComponent(
            DashboardDbContext context)
        {
            _context = context;
        }

        public string UserInfo { get; set; }

        public async Task<IViewComponentResult> InvokeAsync(List<QueryDataList> data, string title)
        {
            
            ViewBag.DataTitle = title;
             
            ViewBag.outcomex = data;
            ViewBag.sn = title.ToLower() + "12";
            return View();
        }
    }

}
