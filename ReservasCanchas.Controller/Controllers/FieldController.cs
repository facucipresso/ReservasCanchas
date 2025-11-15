using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Field;
using ReservasCanchas.Domain.Entities;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("/api/fields")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly FieldBusinessLogic _fieldBusinessLogic;

        public FieldController(FieldBusinessLogic fieldBusinessLogic)
        {
            _fieldBusinessLogic = fieldBusinessLogic;
        }
        [HttpGet("/{id}")]
        public async Task<ActionResult<FieldResponseDTO>> GetFieldById([FromRoute]int id)
        {
            FieldResponseDTO fieldDTO = await _fieldBusinessLogic.GetById(id);
            return Ok(fieldDTO);

        }

        [HttpGet]
        public async Task<ActionResult<List<FieldResponseDTO>>> GetFieldsByComplexId([FromQuery] int complexId)
        {
            var fieldsDTOs = await _fieldBusinessLogic.GetAllByComplexId(complexId);
            return Ok(fieldsDTOs);
        }

        // Aca no tendria que venir ya de entrada con la franja horaria seteada?
        [HttpPost]
        public async Task<ActionResult<FieldResponseDTO>> CreateField([FromQuery] int complexId, [FromBody] FieldRequestDTO fieldDTO)
        {
            var createdFieldDTO = await _fieldBusinessLogic.Create(complexId, fieldDTO);
            return CreatedAtAction(nameof(GetFieldById), new { id = createdFieldDTO.Id }, createdFieldDTO);
        }

        [HttpPatch("/{id}")]
        public async Task<ActionResult<FieldResponseDTO>> UpdateField([FromRoute] int id, [FromBody] FieldUpdateRequestDTO fieldDTO)
        {
            var updatedFieldDTO = await _fieldBusinessLogic.Update(id, fieldDTO);
            return Ok(updatedFieldDTO);
        }

        [HttpPut("{id}/addRecurridBlock")]
        public async Task<ActionResult<FieldResponseDTO>> AddRecurridFieldBlockToField([FromRoute] int id, RecurringFieldBlockRequestDTO recurridBlockDTO)
        {
            // ver como tomo en la realidad el id del usuario que hace la request y el id del complejo, ahora los simulo
            int AdminComplexId = 1;
            int ComplexId = 1;
            var updateFieldDTO = await _fieldBusinessLogic.AddRecurridFieldBlockAsinc(AdminComplexId, ComplexId, id, recurridBlockDTO);
            return Ok(updateFieldDTO);
        }

        [HttpPut("{id}/deleteRecurridBloc/{idrb}")]
        public async Task<ActionResult<FieldResponseDTO>> DeleteRecurridFieldBlockToField([FromRoute] int id, [FromRoute] int idrb)
        {
            int AdminComplexId = 1; 
            int ComplexId = 1;
            await _fieldBusinessLogic.DeleteRecurridFieldBlockAsinc(AdminComplexId, ComplexId, id, idrb);
            return NoContent();
        }

        [HttpDelete("/{id}")]
        public async Task<ActionResult> DeleteField([FromRoute] int id)
        {
            await _fieldBusinessLogic.Delete(id);
            return NoContent();
        }


    }
}
