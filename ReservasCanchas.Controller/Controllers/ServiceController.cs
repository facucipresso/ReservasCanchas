using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.BusinessLogic.Mappers;

namespace ReservasCanchas.Controller.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {

        private readonly ServiceBusinessLogic _serviceBusinessLogic;

        public ServiceController(ServiceBusinessLogic serviceBusinessLogic)
        {
            _serviceBusinessLogic = serviceBusinessLogic;
        }

        [HttpGet]
        public async Task<IActionResult> getAllServices()
        {
            var servicesDtos = await _serviceBusinessLogic.getAll();

            return Ok(servicesDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getServiceById([FromRoute] int id)
        {
            var serviceDto = await _serviceBusinessLogic.GetById(id);
            if(serviceDto == null)
            {
                return NotFound("Servicio no encontrado");
            }

            return Ok(serviceDto);
        }

        [HttpPost]
        public async Task<IActionResult> createService([FromBody] ServiceDto serviceDto)
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
