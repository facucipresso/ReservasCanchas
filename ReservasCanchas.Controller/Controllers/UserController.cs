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
        private readonly UserBusinessLogic _usuarioBusinessLogic;

        public UserController(UserBusinessLogic usuarioBusinessLogic)
        {
            _usuarioBusinessLogic = usuarioBusinessLogic;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAllUsers() //aca deberia devolver una list o ienumerable
        {
            var users = await _usuarioBusinessLogic.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserById([FromRoute] int id)
        {
            var user = await _usuarioBusinessLogic.GetUserByIdAsync(id);
            return Ok(user);
        }

        // eliminado, ya no se pueden crear ususrios desde aca, se crean desde AccountController
        /*
        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> CreateUser([FromBody] UserRequestDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCreated = await _usuarioBusinessLogic.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = userCreated.Id }, userCreated);
        }
        */

        [HttpPut("{id}")]
        public async Task<ActionResult<UserRequestDTO>> UpdateUser([FromRoute] int id, [FromBody] UserRequestDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userUpdated = await _usuarioBusinessLogic.UpdateUserAsync(id, userDto);
            return Ok(userUpdated);
        }

        [HttpPut("{id}/block")]
        public async Task<ActionResult> BlockUserById([FromRoute] int id)
        {
            await _usuarioBusinessLogic.BlockUserAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int id)
        {
            await _usuarioBusinessLogic.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
