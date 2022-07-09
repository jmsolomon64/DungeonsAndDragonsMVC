using DungeonsAndDragons.Data;
using DungeonsAndDragons.Model.Character;
using DungeonsAndDragons.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DungeonsAndDragons.MVC.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        //Dependency Injection
        public CharacterController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        //Index GET
        [ActionName("Index")]
        public IActionResult Index()
        {
            ClaimsPrincipal currentUser = this.User;

            var currentUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var service = new CharacterService(currentUserId, _ctx);
            var model = service.GetCharacters();

            return View(model);
        }


        //NEED TO ADDRESS THIS
        [ActionName("Create")]
        public IActionResult Create()
        {
            var service = CreateCharacterService();

            ViewData["RaceId"] = new SelectList(service.RacesList(), "Id", "Name");
            ViewData["ClassId"] = new SelectList(service.ClassesList(), "Id", "Name");
            //ViewData["RaceId"] = service.RacesList();
            //ViewData["ClassId"] = service.ClassesList();

            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CharacterCreate model)
        {
            //Error Handling
            if (!ModelState.IsValid) return View(model);

            var service = CreateCharacterService();

            //If bool comes back as true then block executes

            if(service.CreateCharacter(model))
            {
                TempData["SaveResult"] = "Your character was created.";
                return RedirectToAction("Index");
            }

            //if bool came back false, previous block will be skipped and resumed at this point
            ModelState.AddModelError("", "Your character could not be created.");

            return View(model);
        }

        //GET
        [ActionName("Details")]
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var service = CreateCharacterService();

            var model = service.FindCharacterById(id);

            return View(model);
        }

        //GET
        [ActionName("Edit")]
        public ActionResult Edit (int id)
        {
            var service = CreateCharacterService();
            var model = service.CharacterEditGenerator(id);
            
            return View(model); 
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (int id, CharacterEdit model)
        {
            if (!ModelState.IsValid) return View(model); //returns model if it's not valid

            var service = CreateCharacterService();

            model.CharacterId = id;

            //checks to see if models changes can be saved
            if(service.UpdateCharacter(model))
            {
                TempData["SaveResult"] = "Your character was updated."; //Message that will be sent to user
                return RedirectToAction("Index"); //returns to Index page
            }

            ModelState.AddModelError("", "Your character could not be updated.");
            return View(model);
        }

        //GET
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateCharacterService();
            var model = service.FindCharacterById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CharacterDetailView model)
        {
            var service = CreateCharacterService();
            if(service.DeleteCharacter(id))
            {
                TempData["SaveResult"] = "Your character was deleted";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private CharacterService CreateCharacterService()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var service = new CharacterService(currentUserId, _ctx);
            return service;
        }
    }
}
