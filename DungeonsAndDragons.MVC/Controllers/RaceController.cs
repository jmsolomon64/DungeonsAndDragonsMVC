using DungeonsAndDragons.Data;
using DungeonsAndDragons.Model.Race;
using DungeonsAndDragons.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DungeonsAndDragons.MVC.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public RaceController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }


        // GET: RaceController
        public ActionResult Index()
        {
            var service = CreateRaceService();
            var models = service.GetRaces();
            
            return View(models);
        }

        // GET: RaceController/Details/5
        public ActionResult Details(int id)
        {
            var service = CreateRaceService();
            var model = service.ViewRace(id);

            return View(model);
        }

        // GET: RaceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RaceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RaceCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateRaceService();

            if(service.CreateRace(model))
            {
                TempData["SaveResult"] = "Your class was created";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your class could not be created");

            return View(model);
        }

        // GET: RaceController/Edit/5
        public ActionResult Edit(int id)
        {
            var service = CreateRaceService();
            var model = service.GenerateRaceEdit(id);

            return View(model);
        }

        // POST: RaceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RaceEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateRaceService();
            model.RaceId = id;

            if(service.EditRace(id, model))
            {
                TempData["SaveResult"] = "Race was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Race could not be updated.");
            return View(model);
        }

        // GET: RaceController/Delete/5
        public ActionResult Delete(int id)
        {
            var service = CreateRaceService();
            var model = service.ViewRace(id);

            return View(model);
        }

        // POST: RaceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RaceDetails model)
        {
            var service = CreateRaceService();
            if(service.DeleteRace(id))
            {
                TempData["SaveResult"] = "Race was deleted";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private RaceService CreateRaceService()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var service = new RaceService(currentUserId, _ctx);
            return service;
        }
    }
}
