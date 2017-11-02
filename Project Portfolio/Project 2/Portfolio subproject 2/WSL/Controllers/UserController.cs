
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using DAL;
using DAL.DBObjects;

namespace WSL.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IDataService _ds;
        public UserController(IDataService iDataService)
        {
            _ds = iDataService;
        }

        [HttpGet(Name = nameof(GetUsers))] 
        public IActionResult GetUsers()
        {
            return Ok(_ds.GetUsers());
        }

        //like: "api/User/1"
        [HttpGet("{id}", Name = nameof(GetUser))]
        public IActionResult GetUser(int id)
        {
            return Ok(_ds.GetUser(id));
        }







    }
}