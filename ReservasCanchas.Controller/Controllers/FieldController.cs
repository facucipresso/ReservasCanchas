using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Field;
using ReservasCanchas.Domain.Entities;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/fields")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly FieldBusinessLogic _fieldBusinessLogic;

        public FieldController(FieldBusinessLogic fieldBusinessLogic)
        {
            _fieldBusinessLogic = fieldBusinessLogic;
        }
        [HttpGet("{fieldId}")]
        public async Task<ActionResult<FieldDetailResponseDTO>> GetFieldById([FromRoute]int fieldId)
        {
            var fieldDTO = await _fieldBusinessLogic.GetFieldByIdAsync(fieldId);
            return Ok(fieldDTO);

        }

        [HttpGet("by-complex/{complexId}")]
        public async Task<ActionResult<List<FieldDetailResponseDTO>>> GetFieldsByComplexId([FromRoute] int complexId)
        {
            var fieldsDTOs = await _fieldBusinessLogic.GetAllFieldsByComplexIdAsync(complexId);
            return Ok(fieldsDTOs);
        }

        [HttpPost]
        public async Task<ActionResult<FieldDetailResponseDTO>> CreateField([FromBody] CreateFieldRequestDTO fieldDTO)
        {
            var createdFieldDTO = await _fieldBusinessLogic.CreateFieldAsync(fieldDTO);
            return CreatedAtAction(nameof(GetFieldById), createdFieldDTO);
        }

        [HttpPatch("{fieldId}")]
        public async Task<ActionResult<FieldDetailResponseDTO>> UpdateField([FromRoute] int fieldId, [FromBody] UpdateFieldRequestDTO fieldDTO)
        {
            var updatedFieldDTO = await _fieldBusinessLogic.UpdateFieldAsync(fieldId, fieldDTO);
            return Ok(updatedFieldDTO);
        }

        [HttpPut("{fieldId}/time-slots")]
        public async Task<ActionResult<FieldDetailResponseDTO>> UpdateFieldTimeSlots([FromRoute] int fieldId, UpdateTimeSlotFieldRequestDTO timeSlotUpdateDTO)
        {
            var updatedFieldDTO = await _fieldBusinessLogic.UpdateTimeSlotsFieldAsync(fieldId, timeSlotUpdateDTO);
            return Ok(updatedFieldDTO);
        }

        [HttpDelete("{fieldId}")]
        public async Task<ActionResult> DeleteField([FromRoute] int fieldId)
        {
            await _fieldBusinessLogic.DeleteFieldAsync(fieldId);
            return NoContent();
        }

        [HttpPost("{fieldId}/recurring-block")]
        public async Task<ActionResult<FieldDetailResponseDTO>> AddRecurringFieldBlockToField([FromRoute] int fieldId, RecurringFieldBlockRequestDTO recurridBlockDTO)
        {
            var updateFieldDTO = await _fieldBusinessLogic.AddRecurringFieldBlockAsync(fieldId, recurridBlockDTO);
            return Ok(updateFieldDTO);
        }

        [HttpDelete("{fieldId}/recurring-block/{idRb}")]
        public async Task<ActionResult<FieldDetailResponseDTO>> DeleteRecurridFieldBlockToField([FromRoute] int fieldId, [FromRoute] int idRb)
        {
            await _fieldBusinessLogic.DeleteRecurringFieldBlockAsync(fieldId, idRb);
            return NoContent();
        }
    }
}
