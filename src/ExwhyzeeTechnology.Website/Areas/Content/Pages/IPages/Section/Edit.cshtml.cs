﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using ExwhyzeeTechnology.Domain.Models;
using ExwhyzeeTechnology.Application.Services.AWS;
using ExwhyzeeTechnology.Application.Dtos.AwsDtos;

namespace ExwhyzeeTechnology.Website.Areas.Content.Pages.IPages.Section
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,Editor")]

    public class EditModel : PageModel
    {
        private readonly ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext _context;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;

        public EditModel(ExwhyzeeTechnology.Persistence.EF.SQL.DashboardDbContext context, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _config = config;
            _storageService = storageService;
        }


        [BindProperty]
        public IFormFile? imagefile { get; set; }

        [BindProperty]
        public IFormFile? imagefile2 { get; set; }

        [BindProperty]
        public IFormFile? videofile { get; set; }

        [BindProperty]
        public PageSection PageSection { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PageSection = await _context.PageSections
                .Include(p => p.WebPage).FirstOrDefaultAsync(m => m.Id == id);

            if (PageSection == null)
            {
                return NotFound();
            }
            ViewData["TemplateTypeId"] = new SelectList(_context.TemplateTypes, "Id", "Name");
            ViewData["WebPageId"] = new SelectList(_context.WebPages, "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //image
            if (imagefile != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await imagefile.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(imagefile.FileName);
                    var docName = $"{Guid.NewGuid()}{fileExt}";
                    // call server

                    var s3Obj = new Application.Dtos.AwsDtos.S3Object()
                    {
                        BucketName = await _storageService.BucketName(),
                        InputStream = memoryStream,
                        Name = docName
                    };

                    var cred = new AwsCredentials()
                    {
                        AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                        SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                    };

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, PageSection.ImageKey);
                    // 
                    if (xresult.Message.Contains("200"))
                    {
                        PageSection.ImageUrl = xresult.Url;
                        PageSection.ImageKey = xresult.Key;
                    }
                    else
                    {
                        TempData["error"] = "unable to upload image";
                        //return Page();
                    }
                }
                catch (Exception c)
                {

                }
            }


            //image 2
            if (imagefile2 != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await imagefile2.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(imagefile2.FileName);
                    var docName = $"{Guid.NewGuid()}{fileExt}";
                    // call server

                    var s3Obj = new Application.Dtos.AwsDtos.S3Object()
                    {
                        BucketName = await _storageService.BucketName(),
                        InputStream = memoryStream,
                        Name = docName
                    };

                    var cred = new AwsCredentials()
                    {
                        AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                        SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                    };

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, "");
                    // 
                    if (xresult.Message.Contains("200"))
                    {
                        PageSection.SecondImageUrl = xresult.Url;
                        PageSection.SecondImageKey = xresult.Key;
                    }
                    else
                    {
                        TempData["error"] = "unable to upload image";
                        //return Page();
                    }
                }
                catch (Exception c)
                {

                }
            }


            //video
            if (videofile != null)
            {
                try
                {
                    // Process file
                    await using var memoryStream = new MemoryStream();
                    await videofile.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(videofile.FileName);
                    var docName = $"{Guid.NewGuid()}{fileExt}";
                    // call server

                    var s3Obj = new Application.Dtos.AwsDtos.S3Object()
                    {
                        BucketName = await _storageService.BucketName(),
                        InputStream = memoryStream,
                        Name = docName
                    };

                    var cred = new AwsCredentials()
                    {
                        AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                        SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                    };

                    var xresult = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, PageSection.VideoKey);
                    // 
                    if (xresult.Message.Contains("200"))
                    {
                        PageSection.VideoUrl = xresult.Url;
                        PageSection.VideoKey = xresult.Key;
                    }
                    else
                    {
                        TempData["error"] = "unable to upload image";
                        //return Page();
                    }
                }
                catch (Exception c)
                {

                }
            }


            _context.Attach(PageSection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); TempData["success"] = "Successful";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PageSectionExists(PageSection.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new {id = PageSection.Id });
        }

        private bool PageSectionExists(long id)
        {
            ViewData["TemplateTypeId"] = new SelectList(_context.TemplateTypes, "Id", "Name");

            return _context.PageSections.Any(e => e.Id == id);
        }
    }
}