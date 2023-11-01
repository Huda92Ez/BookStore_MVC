using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext Db)
        {
            _db= Db;
            
        }
        public IActionResult Index()
        {
            List<Category> catList = _db.Categories.ToList();
            return View(catList);
        }

        [HttpGet]
        public IActionResult Create()
        {
          
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category catObj)
        {

            //if(catObj.Name == catObj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "The name can not exactly match the Display Order.");

            //}

            //if (catObj.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", " Test is invalid value!!");

            //}
            if (ModelState.IsValid)
            {
                _db.Categories.Add(catObj);
                _db.SaveChanges();
                TempData["Success"] = "The category has been added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
            
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue || id.Value == 0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.Find(id);
            if (category == null)
            { 
                return NotFound(); 
            }
            

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category catObj)
        {

           
            if (ModelState.IsValid)
            {
                _db.Categories.Update(catObj);
                _db.SaveChanges();
                TempData["Success"] = "The category has been updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }


        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue || id.Value == 0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }


            return View(category);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePo(int?id)
        {
            if (!id.HasValue || id.Value == 0)
            {
                return NotFound();
            }
            Category? category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

          
                _db.Categories.Remove(category);
                _db.SaveChanges();
            TempData["Success"] = "The category has been deleted successfully";
            return RedirectToAction("Index");
            }
            


        }
}

