using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyBlog_API.Domain;

namespace MyBlog_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersRepo _usersrepo;
        public UsersController(ILogger<UsersController> logger, IUsersRepo usersrepo)
        {
            _logger = logger;
            _usersrepo = usersrepo;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AppUser>> GetUsers()
        {
            var users = await _usersrepo.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            var user = await _usersrepo.GetUserById(id);
            return Ok(user);
        }

        //Post: api/<UsersController>
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(AppUser user)
        {
            try
            {
                return await _usersrepo.Register(user);

            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("login")]
        public async Task<ActionResult<AppUser>> Login(AppUser user)
        {
            try
            {
                return await _usersrepo.Login(user);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}