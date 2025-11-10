using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.BusinessLogic.Mappers;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServiceController : ControllerBase
    {

        private readonly ServiceBusinessLogic _serviceBusinessLogic;

        public ServiceController(ServiceBusinessLogic serviceBusinessLogic)
        {
            _serviceBusinessLogic = serviceBusinessLogic;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponseDTO>> getAllServices()
        {
            var servicesDtos = await _serviceBusinessLogic.getAll();

            return Ok(servicesDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponseDTO>> getServiceById([FromRoute] int id)
        {
            var serviceDto = await _serviceBusinessLogic.GetById(id);
            if(serviceDto == null)
            {
                return NotFound("Servicio con id " + id + " no encontrado");
            }

            return Ok(serviceDto);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponseDTO>> createService([FromBody] ServiceRequestDTO serviceDto)
        {
            var serviceCreated = await _serviceBusinessLogic.create(serviceDto);

            if(serviceCreated == null)
            {
                return BadRequest("No se pudo crear el servicio");
            }
            return CreatedAtAction(nameof(getServiceById), new { id = serviceCreated.Id }, serviceCreated);
        }
    }
}
