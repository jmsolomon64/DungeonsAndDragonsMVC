using DungeonsAndDragons.Data;
using DungeonsAndDragons.Model.Class;
using DungeonsAndDragons.Model.Classes;
using DungeonsAndDragons.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DungeonsAndDragons.MVC.Controllers
{
    public class ClassesController : Controller
    {

        private readonly IClassesService _classes;

        public ClassesController(IClassesService classes)
        {
            _classes = classes;
        }

        // GET: ClassesController
        [ActionName("Index")]
        public IActionResult Index()
        {
            _classes.SetUserId(GetUserId());
            var models = _classes.GetClasses();

            return View(models);
        }

        // GET: ClassesController/Details/5
        [ActionName("Details")]
        public IActionResult Details(int id)
        {
            _classes.SetUserId(GetUserId());
            var model = _classes.ViewClass(id);

            return View(model);
        }

        // GET: ClassesController/Create
        [ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassesController/Create
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClassCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            _classes.SetUserId(GetUserId());

            if (_classes.CreateClasses(model))
            {
                TempData["SaveResult"] = "Your Class was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your character could not be created.");

            return View(model);
        }

        // GET: ClassesController/Edit/5
        [ActionName("Edit")]
        public IActionResult Edit(int id)
        {
            _classes.SetUserId(GetUserId());
            var model = _classes.GenerateClassUpdate(id);

            return View(model);
        }

        // POST: ClassesController/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ClassUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            _classes.SetUserId(GetUserId());
            model.ClassId = id;

            if(_classes.UpdateClass(id, model))
            {
                TempData["SaveResult"] = "Your class was updated";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your class could not be updated.");
            return View(model);
        }

        // GET: ClassesController/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            _classes.SetUserId(GetUserId());
            var model = _classes.ViewClass(id);

            return View(model);
        }

        // POST: ClassesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, ClassDetails model)
        {
            _classes.SetUserId(GetUserId());
            if (_classes.DeleteClass(id))
            {
                TempData["SaveResult"] = "Your class was deleted";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private Guid GetUserId()
        {
            string userId = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            if (userId == null) return default;
            return Guid.Parse(userId);
        }
    }
}
