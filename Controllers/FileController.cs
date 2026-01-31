using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Csharp_modelContoroling_lesson.Controllers
{
    public class FileController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment; // This valuable is information about web hosting,
                                                                  // in which this code working.
        public FileController(IWebHostEnvironment webHostEnvironment) //Contructor that takes parameter of web hosting.
        {
            _webHostEnvironment = webHostEnvironment; // Cause _webHostEnvironment is private we saving parameter
                                                      // into this field of class to use in other methods.
        }

        [HttpGet] // Attribute of mashrutization in ASP . This attribute using to show when them need to use.
                  // This Attribute provides getting data from something.
        public IActionResult Index() // Main method with View that shows 
        {
            var dir = Path.Combine(_webHostEnvironment.WebRootPath, "Upload"); // Varuable of directory where files saving.
            Directory.CreateDirectory(dir); // Getting this directory

            var files = Directory.GetFiles(dir) //Getting files from this directory.
                .Select(Path.GetFileName) // Selecting all file names.
                .ToList(); // Transform them all into List to return.
            return View(files); // returning list of files.
        }


        [HttpPost] //Provides changing something in data.
        public async Task<IActionResult> Upload(IFormFile file) 
        {
            if (file == null || file.Length == 0) //Checking for file is he exists or is he have something in him.
            {
                return RedirectToAction(nameof(Index)); 
            }

            if (file.Length > 2000000) // Checking for file that too big.
            {
                return RedirectToAction(nameof(Index));
            }

            var dir = Path.Combine(_webHostEnvironment.WebRootPath, "Upload"); // Combining path to have directory.
            Directory.CreateDirectory(dir); //Creating by directory new directory

            var safeName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}"; // creating safename to prevent conflicts.
            var path = Path.Combine(dir, safeName);// creating new path for this file.

            using var stream = System.IO.File.Create(path); //Creating file stream with this path.

            await file.CopyToAsync(stream);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Download(string path)
        {
            Console.WriteLine("Start Function");
            if (string.IsNullOrEmpty(path)) //Checking for file
            {
                return BadRequest();
            }
            Console.WriteLine(path);
            Console.WriteLine(_webHostEnvironment.WebRootPath);

            var dir = Path.Combine(_webHostEnvironment.WebRootPath, "Upload");
            Console.WriteLine(dir);

            var pathOfFile = Path.Combine(dir, path);
            Console.WriteLine(pathOfFile);
            Console.WriteLine("Combined paths");
            if (!System.IO.File.Exists(pathOfFile))
            {
                return NotFound();
            }

            var bytes = System.IO.File.ReadAllBytes(pathOfFile); //Reading all file.
            Console.WriteLine("Found and read file.");

            Console.WriteLine("End of Function");
            return File(bytes, "application/octet-stream", pathOfFile);
            
        }
    }
}
