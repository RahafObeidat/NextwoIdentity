using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NextwoIdentity.Data;
using NextwoIdentity.Models;

namespace NextwoIdentity.Controllers
{
    public class CategoryController : Controller
    {
        private readonly NextwoDbContext DbContext;

        public CategoryController(NextwoDbContext _dbContext)
        {
            DbContext = _dbContext;

        }
        public ActionResult Index()
        {
            return View(DbContext.Categories);

        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                //    if(DbContext.Categories.Where(c => c.CategoryName == category.CategoryName).ToList().Count() > 0)
                //    {
                //        ModelState.AddModelError(string.Empty, "this Name is already exist");
                //    }
                DbContext.Categories.Add(category);
                DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }

            var data = DbContext.Categories.Find(id);
            if (data == null)
            {
                return RedirectToAction("index");
            }
            return View(data);
        }
        [HttpGet]

        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }
            var data = DbContext.Categories.Find(id);
            if (data == null)
            {
                return RedirectToAction("index");
            }
            return View(data);
        }
        [HttpPost]

        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                DbContext.Categories.Update(category);
                DbContext.SaveChanges();
                return RedirectToAction("index");
            }
            return View(category);


        }

        [HttpGet]

        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index");
            }
            var data = DbContext.Categories.Find(id);
            if (data == null)
            {
                return RedirectToAction("index");
            }
            return View(data);
        }
        [HttpPost]

        public IActionResult DeleteCategory(Category category)
        {

            DbContext.Categories.Remove(category);
            DbContext.SaveChanges();
            return RedirectToAction("index");

        }

       
    }
}


