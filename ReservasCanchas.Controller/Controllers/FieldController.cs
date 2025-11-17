using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Field;
using ReservasCanchas.Domain.Entities;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/complexes/{complexId}/fields")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly FieldBusinessLogic _fieldBusinessLogic;

        public FieldController(FieldBusinessLogic fieldBusinessLogic)
        {
            _fieldBusinessLogic = fieldBusinessLogic;
        }
        [HttpGet("{fieldId}")]
        public async Task<ActionResult<FieldDetailResponseDTO>> GetFieldById([FromRoute] int complexId,[FromRoute]int fieldId)
        {
            var fieldDTO = await _fieldBusinessLogic.GetById(complexId,fieldId);
            return Ok(fieldDTO);

        }

        [HttpGet]
        public async Task<ActionResult<List<FieldDetailResponseDTO>>> GetFieldsByComplexId([FromRoute] int complexId)
        {
            var fieldsDTOs = await _fieldBusinessLogic.GetAllByComplexId(complexId);
            return Ok(fieldsDTOs);
        }

        // Aca no tendria que venir ya de entrada con la franja horaria seteada?
        [HttpPost]
        public async Task<ActionResult<FieldDetailResponseDTO>> CreateField([FromRoute] int complexId, [FromBody] FieldRequestDTO fieldDTO)
        {
            var createdFieldDTO = await _fieldBusinessLogic.Create(complexId, fieldDTO);
            return CreatedAtAction(nameof(GetFieldById), new {complexId = complexId, id = createdFieldDTO.Id }, createdFieldDTO);
        }

        [HttpPatch("{fieldId}")]
        public async Task<ActionResult<FieldDetailResponseDTO>> UpdateField([FromRoute] int complexId, [FromRoute] int fieldId, [FromBody] FieldUpdateRequestDTO fieldDTO)
        {
            var updatedFieldDTO = await _fieldBusinessLogic.Update(complexId, fieldId, fieldDTO);
            return Ok(updatedFieldDTO);
        }

        [HttpPost("{fieldId}/add-recurring-block")]
        public async Task<ActionResult<FieldDetailResponseDTO>> AddRecurringFieldBlockToField([FromRoute] int complexId, [FromRoute] int fieldId, RecurringFieldBlockRequestDTO recurridBlockDTO)
        {
            var updateFieldDTO = await _fieldBusinessLogic.AddRecurringFieldBlockAsync(complexId, fieldId, recurridBlockDTO);
            return Ok(updateFieldDTO);
        }

        [HttpDelete("{fieldId}/delete-recurring-block/{idRb}")]
        public async Task<ActionResult<FieldDetailResponseDTO>> DeleteRecurridFieldBlockToField([FromRoute] int complexId, [FromRoute] int fieldId, [FromRoute] int idRb)
        {
            await _fieldBusinessLogic.DeleteRecurringFieldBlockAsync(complexId, fieldId, idRb);
            return NoContent();
        }

        [HttpDelete("{fieldId}")]
        public async Task<ActionResult> DeleteField([FromRoute] int complexId, [FromRoute] int fieldId)
        {
            await _fieldBusinessLogic.Delete(complexId,fieldId);
            return NoContent();
        }




    }
}
