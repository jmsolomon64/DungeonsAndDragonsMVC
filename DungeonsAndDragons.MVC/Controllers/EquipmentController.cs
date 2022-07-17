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
        private readonly IEquipmentService _equipment;

        public EquipmentController(IEquipmentService equipment)
        {
            _equipment = equipment;
        }

        // GET: EquipmentController
        [ActionName("Index")]
        public ActionResult Index()
        {
            _equipment.SetUserId(GetUserId());
            var model = _equipment.GetAllEquipment();

            return View(model);
        }

        // GET: EquipmentController/Details/5
        [ActionName("Details")]
        public ActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            _equipment.SetUserId(GetUserId());
            var model = _equipment.ViewItem(id);

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

            _equipment.SetUserId(GetUserId());

            if(_equipment.CreateEquipment(model))
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
            _equipment.SetUserId(GetUserId());
            var model = _equipment.GenerateUpdateEquipment(id);

            return View(model);
        }

        // POST: EquipmentController/Edit/5
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EquipmentUpdate model)
        {
            if (!ModelState.IsValid) return View(model);

            _equipment.SetUserId(GetUserId());
            model.EquipmentId = id;

            if(_equipment.UpdateEquipment(model))
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
            _equipment.SetUserId(GetUserId());
            var model = _equipment.ViewItem(id);

            return View(model);
        }

        // POST: EquipmentController/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EquipmentDetail model)
        {
            _equipment.SetUserId(GetUserId());

            if(_equipment.DeleteEquipment(id))
            {
                TempData["SaveResults"] = "Equipment Deleted";
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
