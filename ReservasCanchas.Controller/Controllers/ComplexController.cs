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

        [HttpGet("/my-complexes")]
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetMyComplexes()
        {
            //Recuperariamos el id del admin con _authService.GetUserId();
            int adminComplexId = 1; //Valor para probar
            var complexesDtos = await _complexBusinessLogic.GetComplexesForAdminComplexIdAsync(adminComplexId);
            return Ok(complexesDtos);
        }

        // Devuelvo las cards de complejos
        // El usuario ingresa Ciudad, dia y hora por otro lado
        // Yo deberia filtrar por ciudad, de esos complejos que me da, que se fije si el complejo tiene en ese horario abierto (que habria que fijarse en el TimeSlotComplex)
        // Y despues ver si en ese complejo que paso el filtro anterior no tiene reservas de canchar a esa hora
        // Y si tampoco tiene RecurridFieldBlocks en ese horario
        [HttpGet]
        public async Task<ActionResult<List<ComplexCardResponseDTO>>> GetAllComplex([FromQuery] string locality, [FromQuery] DateOnly date, [FromQuery] TimeOnly hour)
        {
            var complejosCardDto = await _complexBusinessLogic.SearchAvailableComplexes(locality, date, hour);
            return Ok(complejosCardDto);
        }

        [HttpPost]
        public async Task<ActionResult<ComplexDetailResponseDTO>> CreateComplex([FromBody] CreateComplexRequestDTO complexDTO)
        {
            var createdComplexDto = await _complexBusinessLogic.CreateComplexAsync(complexDTO);
            return CreatedAtAction(nameof(CreateComplex), new { id = createdComplexDto.Id }, createdComplexDto);
        }

        [HttpPut("{id}/basicinfo")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateComplex([FromRoute] int id,[FromBody] UpdateComplexRequestDTO updateComplexRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int adminComplexId = 1;
            var updatedComplexDto = await _complexBusinessLogic.UpdateComplexAsinc(adminComplexId, id, updateComplexRequestDTO);
            return Ok(updatedComplexDto);
        }

        [HttpPut("{id}/timeslots")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> UpdateTimeSlotsComplex([FromRoute] int id, [FromBody] UpdateTimeSlotComplexRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int adminComplexId = 1;
            var updatedComplex = await _complexBusinessLogic.UpdateTimeSlotsAsync(adminComplexId, id, request);
            return Ok(updatedComplex);
        }

        [HttpPut("{id}/state")]
        public async Task<ActionResult<ComplexDetailResponseDTO>> ChangeStateComplex([FromRoute] int id, [FromBody] ComplexState newState)
        {
            //simulo el id del usuario sacado del token
            int superUserId = 1;
            var updatedComplexDTO = await _complexBusinessLogic.ChageStateComplexAsync(superUserId, id, newState);

            return Ok(updatedComplexDTO);
        }

    }
}
