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
    [Route("api/Marking")]
    public class MarkingController : Controller
    {
        private readonly IDataService _ds;
        public MarkingController(IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}