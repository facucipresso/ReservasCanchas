using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Dtos.Notification;
using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/complexes")]
    [ApiController]
    public class ComplexController : ControllerBase
    {
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly IWebHostEnvironment _env; // para WebRootPath
        public ComplexController(ComplexBusinessLogic complexBusinessLogic, IWebHostEnvironment env)
        {
            _complexBusinessLogic = complexBusinessLogic;
            _env = env;
        }

        [HttpGet("my")]
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetMyComplexes()
        {//Admin de Complejo obtiene sus complejos

            var complexes = await _complexBusinessLogic.GetComplexesForAdminComplexIdAsync();
            return Ok(complexes);
        }

        [HttpGet("filters")]
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetComplexesWithFilters([FromQuery] ComplexFiltersRequestDTO filters)
        {//Busqueda principal de complejos disponibles para usuarios comunes usando filtros
            var complexes = await _complexBusinessLogic.SearchAvailableComplexes(filters);
            return Ok(complexes);
        }

        [HttpGet("super-admin")]
        public async Task<ActionResult<List<ComplexSuperAdminResponseDTO>>> GetAllComplexesForSuperAdmin()
        {//Super Admin obtiene todos los complejos
            var complexes = await _complexBusinessLogic.GetAllComplexesBySuperAdminAsync();
            return Ok(complexes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> GetComplexById([FromRoute] int id)
        {//Obtener detalle de complejo por id
            var complex = await _complexBusinessLogic.GetComplexByIdAsync(id);
            return Ok(complex);
        }

        [HttpPost]
        public async Task<ActionResult<ComplexDetailResponseDTO>> CreateComplex([FromForm] CreateComplexRequestDTO requestCreateDTO)
        {//Creacion de complejo
            var uploadPath = Path.Combine(_env.WebRootPath, "uploads", "complexes");
            var created = await _complexBusinessLogic.CreateComplexAsync(requestCreateDTO, uploadPath);
            return CreatedAtAction(nameof(CreateComplex), new { id = created.Id }, created);
        }

        [HttpPatch("{id}/basic-info")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexBasicInfo([FromRoute] int id,[FromBody] UpdateComplexBasicInfoRequestDTO requestUpdateDTO)
        {//Edicion de info basica del complejo (Solo admin del complejo)
            var updatedComplexDTO = await _complexBusinessLogic.UpdateComplexAsync(id, requestUpdateDTO);
            return Ok(updatedComplexDTO);
        }

        [HttpPut("{id}/time-slots")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexTimeSlots([FromRoute] int id, [FromBody] UpdateTimeSlotComplexRequestDTO requestUpdateDTO)
        {//Edicion de timeslots
            var updated = await _complexBusinessLogic.UpdateTimeSlotsAsync(id, requestUpdateDTO);
            return Ok(updated);
        }

        [HttpPut("{id}/services")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexServices([FromRoute] int id, [FromBody] UpdateComplexServiceRequestDTO requestUpdateDTO)
        {//Edicion de servicios del complejo (Solo admin del complejo)
            var updated = await _complexBusinessLogic.UpdateServicesAsync(id, requestUpdateDTO.ServicesIds);
            return Ok(updated);
        }

        [HttpPatch("{id}/state")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexState([FromRoute] int id, [FromBody] UpdateComplexStateDTO requestUpdateDTO)
        {//Cambio de estado del complejo (Super Admin o Admin del complejo, dependiendo de la transición)
            var updated = await _complexBusinessLogic.ChangeStateComplexAsync(id, requestUpdateDTO.State);

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComplex([FromRoute] int id)
        {//Eliminacion de complejo (Solo Admin del complejo)
            await _complexBusinessLogic.DeleteComplexAsync(id);
            return NoContent();
        }

        //notifico la aprobacion del complejo
        [HttpPatch("/approve")]
        public async Task<IActionResult> ApproveComplex([FromBody] AproveComplexRequestDTO request)
        {
            await _complexBusinessLogic.ApproveComplexAsync(request);
            return NoContent();
        }

        //notifico el rechazo del complejo
        [HttpPatch("reject")]
        public async Task<IActionResult> RejectComplex([FromBody] RejectComplexRequestDTO request)
        {
            await _complexBusinessLogic.RejectComplexAsync(request); 
            return NoContent();
        }

    }
}
