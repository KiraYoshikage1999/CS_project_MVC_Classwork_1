using CS_project_MVC_Classwork_1.Models;
using Microsoft.AspNetCore.Mvc;

namespace CS_project_MVC_Classwork_1.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Coffee", Description = "Very energizing drink", Price = 10 },
            new Product { Id = Guid.NewGuid(), Name = "Tea", Description = "Good english (indian) drink", Price = 5 },
            new Product { Id = Guid.NewGuid(), Name = "Cola", Description = "Sugar drink", Price = 3 }
        };

        public IActionResult Index()
        {

            return View(_products);
        }

        public IActionResult Details(Guid id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult DeleteProduct()
        {
            return View(_products);
        }


        [HttpPost]
        public IActionResult DeleteProduct(Guid id)
        {
            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _products.Remove(product);
            return View(_products);
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View(_products);
        }
        [HttpPost]
        public IActionResult AddProduct(string Name , string Description , decimal Price )
        {
            var newProduct = new Product { Id = Guid.NewGuid(), Name = Name, Description = Description, Price = Price };
            _products.Add(newProduct);

            return View(_products);
        }
        [HttpGet]
        public IActionResult SearchProduct(string Name)
        {

            if (string.IsNullOrEmpty(Name))
            {
                return View(); // Пустая страница поиска
            }
            //foreach (var product in _products)
            //{
            //    if (product.Name == Name)
            //    {
            //        return View(product);
            //    }
            //    else
            //    {
            //        return NotFound();
            //    }
            //}

            //Got clue from clause how to return product , he gave me more elegant way to find all product , not only one:
            var foundProducts = _products
                .Where(p => p.Name.Contains(Name, StringComparison.OrdinalIgnoreCase))
                .ToList();
            //-----
            ViewBag.SearchQuery = Name;

            return View(foundProducts);
        }
        //[Http]
        //public IActionResult SearchProduct()
        //{

        //}

    }
}
