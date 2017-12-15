using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/search")]
    public class SearchController : Controller
    {
        private readonly IDataService _ds;
        public SearchController(IDataService iDataService) => _ds = iDataService;


        //GET: api/search/java%20python
        [HttpGet("{terms}", Name = nameof(RankedPostSearch))] //TODO: use parameters instead
        public IActionResult RankedPostSearch(string terms, int page = 0, int pageSize = 10)
        {
            var data = _ds.RankedPostSearch(terms, page, pageSize);
            return data != null ? (IActionResult)Ok(data) : NotFound();
        }

    }
}
