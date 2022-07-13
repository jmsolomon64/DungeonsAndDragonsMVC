using DungeonsAndDragons.Data;
using DungeonsAndDragons.Model.Equipment;
using DungeonsAndDragons.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DungeonsAndDragons.MVC.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        public EquipmentController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        // GET: EquipmentController
        [ActionName("Index")]
        public ActionResult Index()
        {
            var service = CreateEquipmentService();
            var model = service.GetAllEquipment();

            return View(model);
        }

        // GET: EquipmentController/Details/5
        [ActionName("Details")]
        public ActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var service = CreateEquipmentService();
            var model = service.ViewItem(id);

            return View(model);
        }

        // GET: EquipmentController/Create
        [ActionName("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquipmentController/Create
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipmentCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateEquipmentService();

            if(service.CreateEquipment(model))
            {
                TempData["SaveResult"] = "Your character was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your character could not be created");
            return View(model);
        }

        // GET: EquipmentController/Edit/5
        [ActionName("Edit")]
        public ActionResult Edit(int id)
        {
            var service = CreateEquipmentService();
            var model = service.GenerateUpdateEquipment(id);

            return View(model);
        }

        // POST: EquipmentController/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EquipmentUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateEquipmentService();
            model.EquipmentId = id;

            if(service.UpdateEquipment(model))
            {
                TempData["SaveResults"] = "Equipment was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Equipment could not be updated.");
            return View(model);
        }

        // GET: EquipmentController/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateEquipmentService();
            var model = service.ViewItem(id);

            return View(model);
        }

        // POST: EquipmentController/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EquipmentDetail model)
        {
            var service = CreateEquipmentService();

            if(service.DeleteEquipment(id))
            {
                TempData["SaveResults"] = "Equipment Deleted";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private EquipmentService CreateEquipmentService()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var service = new EquipmentService(currentUserId, _ctx);
            return service;
        }
    }
}
