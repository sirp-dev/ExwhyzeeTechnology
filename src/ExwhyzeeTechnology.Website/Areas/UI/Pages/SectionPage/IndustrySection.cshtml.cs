using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExwhyzeeTechnology.Website.Areas.UI.Pages.SectionPage
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "mSuperAdmin,SubAdmin")]

    public class IndustrySectionModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public IndustrySectionModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public List<string> ImagePaths { get; private set; } // List to temporarily store the image paths

        public void OnGet()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            string folderPath = Path.Combine(webRootPath, "templatesample/industry"); // Path to your image folder
            ImagePaths = new List<string>();
            if (Directory.Exists(folderPath))
            {
                IEnumerable<string> ImagePath = Directory.GetFiles(folderPath);

                foreach (var d in ImagePath.ToArray())
                {

                    string fileName = Path.GetFileName(d);

                    ImagePaths.Add(fileName);
                }
                ImagePaths.Sort();
            }
        }
    }
}