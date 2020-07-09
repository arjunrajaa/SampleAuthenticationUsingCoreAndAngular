using Accessors.Concrete;
using Common.Dto;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using UserAuth.Models;

namespace UserAuth.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserAccessor _userAccessor;
        public UserController(IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
        }
        //[HttpGet]
        //[Route("GetUserData")]
        //[Authorize(Policy = Policies.User)]
        //public IActionResult GetUserData()
        //{
        //    return Ok("This is an normal user");
        //}

        //[HttpGet]
        //[Route("GetAdminData")]
        //[Authorize(Policy = Policies.Admin)]
        //public IActionResult GetAdminData()
        //{
        //    return Ok("This is an Admin user");
        //}
        
        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(Policy = Policies.Admin)]

        public IActionResult GetAllUsers()
        {
            var users = _userAccessor.GetAllUser();
            return Ok(users);
        }
        
        [HttpGet]
        [Route("GetAllUsersByRole")]
        [Authorize(Policy = Policies.User)]

        public IActionResult GetAllUsersByRole(string role)
        {
            Role roleEnum = (Role)Enum.Parse(typeof(Role), role);
            var users = _userAccessor.GetAllUserByRole(roleEnum);
            return Ok(users);
        }

        [HttpPost]
        [Route("CreateUser")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult CreateUser([FromBody]UserDto user)
        {
            var users = _userAccessor.CreateUser(user);
            return Ok(users);
        }
    }
}
