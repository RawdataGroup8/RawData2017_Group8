using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/history")]
    public class HistoryController : Controller
    {
        private readonly IDataService _ds;
        public HistoryController(IDataService iDataService) => _ds = iDataService;

       
        //GET: api/History/14
        [HttpGet("{id}", Name = nameof(UserHistory))]
        public IActionResult UserHistory(int id)
        {
            var data = _ds.UserHistory(id);
            return data != null ? (IActionResult)Ok(data) : NotFound(data);
        }

    }
}
