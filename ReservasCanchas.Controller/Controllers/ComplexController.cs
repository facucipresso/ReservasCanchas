using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos.Complex;

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
            var complexesDtos = await _complexBusinessLogic.GetComplexesForAdminComplexAsync(adminComplexId);
            return Ok(complexesDtos);
        }     


    }
}
