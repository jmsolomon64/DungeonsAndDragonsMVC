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

        private readonly ApplicationDbContext _ctx;

        public ClassesController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }


        // GET: ClassesController
        [ActionName("Index")]
        public IActionResult Index()
        {
            var service = CreateClassesService();
            var models = service.GetClasses();

            return View(models);
        }

        // GET: ClassesController/Details/5
        [ActionName("Details")]
        public IActionResult Details(int id)
        {
            var service = CreateClassesService();
            var model = service.ViewClass(id);

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

            var service = CreateClassesService();

            if(service.CreateClasses(model))
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
            var service = CreateClassesService();
            var model = service.GenerateClassUpdate(id);

            return View(model);
        }

        // POST: ClassesController/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ClassUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateClassesService();
            model.ClassId = id;

            if(service.UpdateClass(id, model))
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
            var service = CreateClassesService();
            var model = service.ViewClass(id);

            return View(model);
        }

        // POST: ClassesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, ClassDetails model)
        {
            var service = CreateClassesService();
            if(service.DeleteClass(id))
            {
                TempData["SaveResult"] = "Your class was deleted";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private ClassesService CreateClassesService()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var service = new ClassesService(currentUserId, _ctx);
            return service;
        }
    }
}
