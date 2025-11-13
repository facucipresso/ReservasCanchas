using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Field;

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


    }
}
