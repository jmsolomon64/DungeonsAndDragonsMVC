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
        private readonly ICharacterService _character;

        public CharacterController(ICharacterService character)
        {
            _character = character;
        }



        //Dependency Injection
        //public CharacterController(ApplicationDbContext ctx)
        //{
        //    _ctx = ctx;
        //}

        //Index GET
        [ActionName("Index")]
        public IActionResult Index()
        {
            _character.SetUserId(GetUserId());
            var model = _character.GetCharacters();

            return View(model);
        }


        //NEED TO ADDRESS THIS
        [ActionName("Create")]
        public IActionResult Create()
        {
            _character.SetUserId(GetUserId());
            ViewData["RaceId"] = new SelectList(_character.RacesList(), "Id", "Name");
            ViewData["ClassId"] = new SelectList(_character.ClassesList(), "Id", "Name");
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



            //If bool comes back as true then block executes
            _character.SetUserId(GetUserId());
            if (_character.CreateCharacter(model))
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

            _character.SetUserId(GetUserId());
            var model = _character.FindCharacterById(id);

            return View(model);
        }

        //GET
        [ActionName("Edit")]
        public ActionResult Edit (int id)
        {
            _character.SetUserId(GetUserId());

            var model = _character.CharacterEditGenerator(id);
            
            return View(model); 
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (int id, CharacterEdit model)
        {
            if (!ModelState.IsValid) return View(model); //returns model if it's not valid


            model.CharacterId = id;

            _character.SetUserId(GetUserId());

            //checks to see if models changes can be saved
            if (_character.UpdateCharacter(model))
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
            _character.SetUserId(GetUserId());
            var model = _character.FindCharacterById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CharacterDetailView model)
        {
            _character.SetUserId(GetUserId());
            if(_character.DeleteCharacter(id))
            {
                TempData["SaveResult"] = "Your character was deleted";
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
