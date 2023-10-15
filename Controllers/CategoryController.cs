using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _applicationDbContext.Categories;
            return View(objCategoryList);
        }

        //GET

        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot be the same as the Name");
            }
            if (ModelState.IsValid)
            {
                _applicationDbContext.Categories.Add(obj);
                _applicationDbContext.SaveChanges();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        //GET

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _applicationDbContext.Categories.Find(id);
            //var categoryFromDb = _applicationDbContext.Categories.SingleorDefault(c => c.Id == id);
            //var categoryFromDb = _applicationDbContext.Categories.FirstOrDefault(c => c.Id == id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot be the same as the Name");
            }
            if (ModelState.IsValid)
            {
                _applicationDbContext.Categories.Update(obj);
                _applicationDbContext.SaveChanges();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }


        //GET

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _applicationDbContext.Categories.Find(id);
            //var categoryFromDb = _applicationDbContext.Categories.SingleorDefault(c => c.Id == id);
            //var categoryFromDb = _applicationDbContext.Categories.FirstOrDefault(c => c.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
           
            var obj = _applicationDbContext.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
                _applicationDbContext.Categories.Remove(obj);
                _applicationDbContext.SaveChanges();
               TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
            
        }

    } 
}


