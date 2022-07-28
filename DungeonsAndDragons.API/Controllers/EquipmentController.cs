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

        [HttpGet("ViewItem/{id}")]
        public async Task<IActionResult> ViewById(int id)
        {
            var item = _service.ViewItem(id);
            
            if(item == null) return NotFound();

            return Ok(item);
        }

        

        [HttpGet("ViewAllItems")]
        public async Task<IActionResult> GetAllItems()
        {
            IEnumerable<EquipmentDetail> equipment = _service.GetAllItems();

            if (equipment.Count() > 0 ) return Ok(equipment.ToList());

            return BadRequest("Invalid request.");
        }

    }
}
