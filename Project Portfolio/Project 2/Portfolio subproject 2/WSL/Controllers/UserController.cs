using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using DAL;

namespace WSL.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        public UserController(IDataService iDataService)
        {
            _ds = iDataService;
        }
        private readonly IDataService _ds;


        [HttpGet(Name = "GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(_ds.GetUsers());
        }







    }
}