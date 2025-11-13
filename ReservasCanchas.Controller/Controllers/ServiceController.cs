using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Service;
using ReservasCanchas.BusinessLogic.Mappers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

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
        public async Task<ActionResult<List<ServiceResponseDTO>>> getAllServices()
        {
            var servicesDtos = await _serviceBusinessLogic.GetAll();

            return Ok(servicesDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponseDTO>> getServiceById([FromRoute] int id)
        {
            var serviceDto = await _serviceBusinessLogic.GetById(id);
            return Ok(serviceDto);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponseDTO>> createService([FromBody] ServiceRequestDTO serviceDto)
        {
            var serviceCreated = await _serviceBusinessLogic.Create(serviceDto);
            return CreatedAtAction(nameof(getServiceById), new { id = serviceCreated.Id }, serviceCreated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponseDTO>> updateSerice([FromRoute] int id, [FromBody] ServiceRequestDTO serviceDto)
        {
            var serviceUpdated = await _serviceBusinessLogic.Update(id, serviceDto);
            return Ok(serviceUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteService([FromRoute] int id)
        {
            await _serviceBusinessLogic.Delete(id);
            return NoContent();
        }

    }
}
