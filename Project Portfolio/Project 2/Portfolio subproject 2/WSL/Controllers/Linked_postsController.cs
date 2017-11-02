using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WSL.Controllers
{
    [Produces("application/json")]
    [Route("api/Linked_posts")]
    public class Linked_postsController : Controller
    {
        private readonly IDataService _ds;
        public Linked_postsController(IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}