using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.Domain.Enums;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/complexes")]
    [ApiController]
    public class ComplexController : ControllerBase
    {
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        public ComplexController(ComplexBusinessLogic complexBusinessLogic)
        {
            _complexBusinessLogic = complexBusinessLogic;
        }

        [HttpGet("my")]
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetMyComplexes()
        {
            //Recuperariamos el id del admin con _authService.GetUserId();
            int adminComplexId = 1; //Valor para probar
            var complexes = await _complexBusinessLogic.GetComplexesForAdminComplexIdAsync(adminComplexId);
            return Ok(complexes);
        }

        // Devuelvo las cards de complejos
        // El usuario ingresa Provincia, Ciudad, dia y hora por otro lado
        // Yo deberia filtrar por ciudad, de esos complejos que me da, que se fije si el complejo tiene en ese horario abierto (que habria que fijarse en el TimeSlotComplex)
        // Y despues ver si en ese complejo que paso el filtro anterior no tiene reservas de canchar a esa hora
        // Y si tampoco tiene RecurridFieldBlocks en ese horario
        [HttpGet("filters")]
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetComplexesWithFilters([FromQuery] ComplexSearchRequestDTO filters)
        {
            var complexes = await _complexBusinessLogic.SearchAvailableComplexes(filters);
            return Ok(complexes);
        }
        [HttpGet("super-admin")]
        public async Task<ActionResult<List<ComplexSuperAdminResponseDTO>>> GetAllComplexesForSuperAdmin()
        {
            //Chequeo de rol superadmin.
            var complexes = await _complexBusinessLogic.GetAllComplexesBySuperAdminAsync();
            return Ok(complexes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> GetComplexById([FromRoute] int id)
        {
            var complex = await _complexBusinessLogic.GetComplexByIdAsync(id);
            return Ok(complex);
        }

        [HttpPost]
        public async Task<ActionResult<ComplexDetailResponseDTO>> CreateComplex([FromBody] CreateComplexRequestDTO requestCreateDTO)
        {
            var created = await _complexBusinessLogic.CreateComplexAsync(requestCreateDTO);
            return CreatedAtAction(nameof(CreateComplex), new { id = created.Id }, created);
        }

        [HttpPatch("{id}/basic-info")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexBasicInfo([FromRoute] int id,[FromBody] UpdateComplexBasicInfoRequestDTO requestUpdateDTO)
        {
            int adminComplexId = 1;
            var updatedComplexDTO = await _complexBusinessLogic.UpdateComplexAsync(id, requestUpdateDTO);
            return Ok(updatedComplexDTO);
        }

        [HttpPut("{id}/time-slots")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexTimeSlots([FromRoute] int id, [FromBody] UpdateTimeSlotComplexRequestDTO requestUpdateDTO)
        {
            int adminComplexId = 1;
            var updated = await _complexBusinessLogic.UpdateTimeSlotsAsync(id, requestUpdateDTO);
            return Ok(updated);
        }

        [HttpPut("{id}/services")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexServices([FromRoute] int id, [FromBody] UpdateComplexServiceRequestDTO requestUpdateDTO)
        {
            //simulo el id del usuario sacado del token
            int adminComplexId = 1;
            var updated = await _complexBusinessLogic.UpdateServicesAsync(id, requestUpdateDTO.ServicesIds);
            return Ok(updated);
        }

        [HttpPatch("{id}/state")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplexState([FromRoute] int id, [FromBody] UpdateComplexStateDTO requestUpdateDTO)
        {
            //simulo el id del usuario sacado del token
            int superAdminId = 1;
            var updated = await _complexBusinessLogic.ChangeStateComplexAsync(id, requestUpdateDTO.State);

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComplex([FromRoute] int id)
        {
            //Chequeamos rol de admin complejo.
            //Obtenemos id del admin complejo desde el token
            int adminComplexId = 1;
            await _complexBusinessLogic.DeleteComplexAsync(id);
            return NoContent();
        }

    }
}
