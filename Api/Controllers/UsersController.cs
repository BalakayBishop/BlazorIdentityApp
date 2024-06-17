using Domain.IRepository;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;
        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<UsersViewModel> allUsers = await _usersRepository.ReadAllAsync();
            if (allUsers is not null)
                return Ok(allUsers);

            return BadRequest("User list returned null");
        }

        [HttpGet("GetByEmail")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            UsersViewModel user = await _usersRepository.ReadAsync(email);
            if (user is not null)
                return Ok(user);

            return BadRequest("User not found");
        }
    }
}