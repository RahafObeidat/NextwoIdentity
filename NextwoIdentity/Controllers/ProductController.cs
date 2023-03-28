using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NextwoIdentity.Data;
using NextwoIdentity.Models;

namespace NextwoIdentity.Controllers
{

    [Authorize]
    public class ProductController : Controller
    {
        private readonly NextwoDbContext DbContext;

        public ProductController(NextwoDbContext _dbContext)
        {
            DbContext = _dbContext;
        
        }


        [HttpGet]
        public IActionResult AllProducts()
        {

            return View(DbContext.Products.Include(x => x.Category));
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                DbContext.Products.Add(product);
                DbContext.SaveChanges();
                return RedirectToAction("AllProducts");
            }
            return View(product);
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult CreateProduct()
        {
            ViewBag.AllCat = new SelectList(DbContext.Categories, "CategoryId", "CategoryName");
            return View();
        }


        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AllProducts");
            }

            var data = DbContext.Products.Find(id);
            if (data == null)
            {
                return RedirectToAction("AllProducts");
            }
            return View(data);
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AllProducts");
            }
            var data = DbContext.Products.Find(id);
            if (data == null)
            {
                return RedirectToAction("AllProducts");
            }
            return View(data);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                DbContext.Products.Update(product);
                DbContext.SaveChanges();
                return RedirectToAction("AllProducts");
            }
            return View(product);


        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AllProducts");
            }
            var data = DbContext.Products.Find(id);
            if (data == null)
            {
                return RedirectToAction("AllProducts");
            }
            return View(data);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteProduct(Product product)
        {

            DbContext.Products.Remove(product);
            DbContext.SaveChanges();
            return RedirectToAction("AllProducts");

        }





        //public IActionResult CreateCategory(Category cat)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        DbContext.Categories.Add(cat);
        //        DbContext.SaveChanges();
        //        return RedirectToAction();
        //    }
        //    return View(cat);
        //}
        //[HttpGet]
        //[Authorize(Roles = "Administrator")]
        //public IActionResult CreateCategory()
        //{
        //    ViewBag.AllCat = new SelectList(DbContext.Categories, "CategoryId", "CategoryName");
        //    return View();
        //}


        public IActionResult Index()
        {
            return View();
        }
    }
}
