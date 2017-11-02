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
    [Route("api/History")]
    public class HistoryController : Controller
    {
        private readonly IDataService _ds;
        public HistoryController(IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}