using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Usuario")]
        public async Task<ActionResult<List<ServiceResponseDTO>>> GetAllServices()
        {
            var servicesDtos = await _serviceBusinessLogic.GetAllServicesAsync();

            return Ok(servicesDtos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Usuario")]
        public async Task<ActionResult<ServiceResponseDTO>> GetServiceById([FromRoute] int id)
        {
            var serviceDto = await _serviceBusinessLogic.GetServiceByIdAsync(id);
            return Ok(serviceDto);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponseDTO>> CreateService([FromBody] ServiceRequestDTO serviceDto)
        {
            var serviceCreated = await _serviceBusinessLogic.CreateServiceAsync(serviceDto);
            return CreatedAtAction(nameof(GetServiceById), new { id = serviceCreated.Id }, serviceCreated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponseDTO>> UpdateService([FromRoute] int id, [FromBody] ServiceRequestDTO serviceDto)
        {
            var serviceUpdated = await _serviceBusinessLogic.UpdateServiceAsync(id, serviceDto);
            return Ok(serviceUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteService([FromRoute] int id)
        {
            await _serviceBusinessLogic.DeleteServiceAsync(id);
            return NoContent();
        }

    }
}
