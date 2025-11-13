using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.BusinessLogic.Dtos.Usuario;

namespace ReservasCanchas.Controller.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UsuarioBusinessLogic _usuarioBusinessLogic;

        public UserController(UsuarioBusinessLogic usuarioBusinessLogic)
        {
            _usuarioBusinessLogic = usuarioBusinessLogic;
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioResponseDTO>> GetAllUsers()
        {
            var usersDtos = await _usuarioBusinessLogic.GetAll();

            return Ok(usersDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDTO>> GetUserById([FromRoute] int id)
        {
            var userDto = await _usuarioBusinessLogic.GetById(id);
            return Ok(userDto);
        }

        // En un futuro seria el registro de usuario
        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDTO>> CreateUser([FromBody] UsuarioRequestDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCreated = await _usuarioBusinessLogic.Create(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = userCreated.Id }, userCreated);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioRequestDTO>> UpdateUser([FromRoute] int id, [FromBody] UsuarioRequestDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userUpdated = await _usuarioBusinessLogic.Update(id, userDto);
            return Ok(userUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BlockUserById([FromRoute] int id)
        {
            await _usuarioBusinessLogic.BlockUser(id);
            return NoContent();
        }
    }
}
