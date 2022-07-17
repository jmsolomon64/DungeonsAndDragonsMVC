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
        private readonly IRaceService _race;

        public RaceController(IRaceService race)
        {
            _race = race;
        }

        // GET: RaceController
        public ActionResult Index()
        {
            _race.SetUserId(GetUserId());
            var models = _race.GetRaces();
            
            return View(models);
        }

        // GET: RaceController/Details/5
        public ActionResult Details(int id)
        {
            _race.SetUserId(GetUserId());
            var model = _race.ViewRace(id);

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

            _race.SetUserId(GetUserId());

            if (_race.CreateRace(model))
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
            _race.SetUserId(GetUserId());
            var model = _race.GenerateRaceEdit(id);

            return View(model);
        }

        // POST: RaceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RaceEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            _race.SetUserId(GetUserId());
            model.RaceId = id;

            if(_race.EditRace(id, model))
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
            _race.SetUserId(GetUserId());
            var model = _race.ViewRace(id);

            return View(model);
        }

        // POST: RaceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RaceDetails model)
        {
            _race.SetUserId(GetUserId());
            if (_race.DeleteRace(id))
            {
                TempData["SaveResult"] = "Race was deleted";
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
