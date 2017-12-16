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
            var data = _ds.RankedPostSearch(terms, page, pageSize).Select(x => new
            {
                Link = Url.Link(nameof(PostsController.GetPost), new { x.PostId }),
                x.Title
            }).ToList();
            var total = data.Count;
            var pages = Math.Ceiling(total / (double)pageSize);
            var prev = page > 0 ? Url.Link(nameof(RankedPostSearch), new {terms, page = page - 1, pageSize }) : null;
            var next = page < pages - 1 ? Url.Link(nameof(RankedPostSearch), new {terms, page = page + 1, pageSize }) : null;

            var result = new
            {
                total,
                pages,
                prev,
                next,
                items = data
            };
            return Ok(result);
        }
        
    }
}
