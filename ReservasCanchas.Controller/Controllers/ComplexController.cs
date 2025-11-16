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

        [HttpGet("my-complexes")]
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetMyComplexes()
        {
            //Recuperariamos el id del admin con _authService.GetUserId();
            int adminComplexId = 1; //Valor para probar
            var complexesDtos = await _complexBusinessLogic.GetComplexesForAdminComplexIdAsync(adminComplexId);
            return Ok(complexesDtos);
        }

        // Devuelvo las cards de complejos
        // El usuario ingresa Provincia, Ciudad, dia y hora por otro lado
        // Yo deberia filtrar por ciudad, de esos complejos que me da, que se fije si el complejo tiene en ese horario abierto (que habria que fijarse en el TimeSlotComplex)
        // Y despues ver si en ese complejo que paso el filtro anterior no tiene reservas de canchar a esa hora
        // Y si tampoco tiene RecurridFieldBlocks en ese horario
        [HttpGet]
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetAllComplexWithFilters([FromQuery] ComplexSearchRequestDTO complexSearchDTO)
        {
            var complejosCardDto = await _complexBusinessLogic.SearchAvailableComplexes(complexSearchDTO);
            return Ok(complejosCardDto);
        }
        [HttpGet("super-admin")]
        public async Task<ActionResult<List<ComplexSuperAdminResponseDTO>>> GetAllComplexesForSuperAdmin()
        {
            //Chequeo de rol superadmin.
            var complexesDtos = await _complexBusinessLogic.GetAllComplexesBySuperAdminAsync();
            return Ok(complexesDtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> GetComplexById([FromRoute] int id)
        {
            var complexDto = await _complexBusinessLogic.GetComplexByIdAsync(id);
            return Ok(complexDto);
        }

        [HttpPost]
        public async Task<ActionResult<ComplexDetailResponseDTO>> CreateComplex([FromBody] CreateComplexRequestDTO complexDTO)
        {
            var createdComplexDto = await _complexBusinessLogic.CreateComplexAsync(complexDTO);
            return CreatedAtAction(nameof(CreateComplex), new { id = createdComplexDto.Id }, createdComplexDto);
        }

        [HttpPatch("{id}/basic-info")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateBasicInfoComplex([FromRoute] int id,[FromBody] UpdateComplexBasicInfoRequestDTO updateComplexRequestDTO)
        {
            int adminComplexId = 1;
            var updatedComplexDto = await _complexBusinessLogic.UpdateComplexAsync(adminComplexId, id, updateComplexRequestDTO);
            return Ok(updatedComplexDto);
        }

        [HttpPut("{id}/time-slots")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateTimeSlotsComplex([FromRoute] int id, [FromBody] UpdateTimeSlotComplexRequestDTO request)
        {
            int adminComplexId = 1;
            var updatedComplex = await _complexBusinessLogic.UpdateTimeSlotsAsync(adminComplexId, id, request);
            return Ok(updatedComplex);
        }

        [HttpPut("{id}/services")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateServicesComplex([FromRoute] int id, [FromBody] UpdateComplexServiceRequestDTO request)
        {
            //simulo el id del usuario sacado del token
            int adminComplexId = 1;
            var updatedComplex = await _complexBusinessLogic.UpdateServicesAsync(adminComplexId, id, request.ServicesIds);
            return Ok(updatedComplex);
        }

        [HttpPatch("{id}/state")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateStateComplex([FromRoute] int id, [FromBody] UpdateComplexStateDTO newStateDTO)
        {
            //simulo el id del usuario sacado del token
            int superAdminId = 1;
            var updatedComplexDTO = await _complexBusinessLogic.ChangeStateComplexAsync(superAdminId, id, newStateDTO.State);

            return Ok(updatedComplexDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComplex([FromRoute] int id)
        {
            //Chequeamos rol de admin complejo.
            //Obtenemos id del admin complejo desde el token
            int adminComplexId = 1;
            await _complexBusinessLogic.DeleteComplexAsync(adminComplexId, id);
            return NoContent();
        }

    }
}
