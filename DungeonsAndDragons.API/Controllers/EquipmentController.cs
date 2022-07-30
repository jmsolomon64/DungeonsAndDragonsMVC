using DungeonsAndDragons.Model.Equipment;
using DungeonsAndDragons.Service;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DungeonsAndDragons.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _service;

        //Dependency Injection
        public EquipmentController(IEquipmentService service)
        {
            _service = service;
        }

        //View Specific Item
        [HttpGet("ViewItem/{id}")]
        public async Task<IActionResult> ViewById(int id)
        {
            var item = _service.ViewItem(id);
            
            if(item == null) return NotFound();

            return Ok(item);
        }

        //View All Items
        [HttpGet("ViewAllItems")]
        public async Task<IActionResult> GetAllItems()
        {
            IEnumerable<EquipmentDetail> equipment = _service.GetAllItems();

            if (equipment.Count() > 0 ) return Ok(equipment.ToList());

            return BadRequest("Invalid request.");
        }

        //View Items In characters inventory
        [HttpGet("ViewCharacterItems/{id}")]
        public async Task<IActionResult> GetCharactersItems(int id)
        {
            List<EquipmentDetail> equipment = _service.GetEquipmentByCharacter(id);

            if (equipment.Count() > 0) return Ok(equipment);

            return BadRequest("Invalid request.");
        }

        //Add Items to Character
        [HttpPut("AddItemToCharacter/{characterId}/{itemId}")]
        public async Task<IActionResult> AddItemToCharacter(int characterId, int itemId)
        {
            bool success = _service.AddItemToCharacter(characterId, itemId);

            if (success) return Ok("Item was added to inventory.");

            return BadRequest();
        }
    }
}
