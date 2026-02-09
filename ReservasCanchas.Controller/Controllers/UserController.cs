using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic;
using ReservasCanchas.BusinessLogic.Dtos;
using ReservasCanchas.BusinessLogic.Dtos.Complex;
using ReservasCanchas.BusinessLogic.Dtos.User;
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

        [HttpGet("withRole")]
        public async Task<ActionResult<IEnumerable<UserResponseWithRoleDTO>>> GetAllUsersWithRole()
        {
            var users = await _usuarioBusinessLogic.GetAllUsersWithRoleAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDTO>> GetUserById([FromRoute] int id)
        {
            var user = await _usuarioBusinessLogic.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDTO>> UpdateUser([FromRoute] int id, [FromBody] UserRequestDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userUpdated = await _usuarioBusinessLogic.UpdateUserAsync(id, userDto);
            if(userUpdated == null)
            {
                return BadRequest("Error al actualizar el usuario (Identity)");
            }
            return Ok(userUpdated);
        }

        [HttpPut("{id}/block")]
        public async Task<ActionResult> BlockUserById([FromRoute] int id)
        {
            await _usuarioBusinessLogic.BlockUserAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/unBlock")]
        public async Task<ActionResult> UnBlockUserById([FromRoute] int id)
        {
            await _usuarioBusinessLogic.UnBlockUserAsync(id);
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
