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
        public IActionResult Index()
        {
            ClaimsPrincipal currentUser = this.User;

            var currentUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var service = new CharacterService(currentUserId, _ctx);
            var model = service.GetCharacters();

            return View(model);
        }

        public IActionResult Create()
        {
            ViewData["RaceId"] = new SelectList(_ctx.Races, "Id", "Name");
            ViewData["ClassId"] = new SelectList(_ctx.Classes, "Id", "Name");
            return View();
        }

        [HttpPost]
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
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var character = _ctx.Characters.FirstOrDefault(x => id == x.Id);

            if(character == null)
            {
                return NotFound();
            }

            var service = CreateCharacterService();

            var model = new CharacterDetailView
            {
                Name = character.Name,
                Race = service.FindRaceById(character.RaceId).Name,
                Class = service.FindClassById(character.ClassId).Name,
                Level = character.Level,
                Strength = character.Strength,
                Dexterity = character.Dexterity,
                Consitution = character.Consitution,
                Inteligence = character.Inteligence,
                Wisdom = character.Wisdom,
                Charisma = character.Charisma,
                Description = character.Description,
            };

            return View(model);
        }

        //GET
        public ActionResult Edit (int id)
        {
            var service = CreateCharacterService();
            var detail = service.FindCharacterById(id);
            var model = new CharacterEdit
            {
                Name = detail.Name,
                Level = detail.Level,
                Strength = detail.Strength,
                Dexterity = detail.Dexterity,
                Consitution = detail.Consitution,
                Inteligence = detail.Inteligence,
                Wisdom = detail.Wisdom,
                Charisma = detail.Charisma,
                Description = detail.Description
            };
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
