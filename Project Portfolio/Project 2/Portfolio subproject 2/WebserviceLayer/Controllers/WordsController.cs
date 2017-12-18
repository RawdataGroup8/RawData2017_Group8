using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/words")]
    public class WordsController : Controller
    {
        private readonly IDataService _ds;
        public WordsController(IDataService iDataService) => _ds = iDataService;


        //GET: api/words/java%20python
        [HttpGet("{terms}", Name = nameof(RankedWordsSearch))]       
        public IActionResult RankedWordsSearch(string terms/*, int page = 0, int pageSize = 10*/)
        {
            var data = _ds.RankedWordsSearch(terms/*, page, pageSize*/).Select(x => new
            {
                //Link = Url.Link(nameof(PostsController.GetPost), new { x.PostId }),
                //x.
                text = x.Word,
                weight = x.Rank

            });
            /*var total = 100;//need to find a way to get the length of the IEnumerable here
            var pages = Math.Ceiling(total / (double)pageSize);
            var prev = page > 0 ? Url.Link(nameof(RankedWordsSearch), new { page = page - 1, pageSize }) : null;
            var next = page < pages - 1 ? Url.Link(nameof(RankedWordsSearch), new { page = page + 1, pageSize }) : null;

            var result = new
            {
                total,
                pages,
                prev,
                next,
                items = data
            };*/
            return Ok(data);
        }

    }
}
