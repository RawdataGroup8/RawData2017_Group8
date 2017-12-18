using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccesLayer;
using DataAccesLayer.DBObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServiceLayer.DataTransferObjects;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Search")]
    public class SearchController : AbstractController
    {
        private readonly IDataService _ds;
        public SearchController(IDataService iDataService) => _ds = iDataService;


        [HttpGet("text", Name = nameof(Search))]
        public IActionResult Search(string text, int page = 0, int pageSize = 10)
        {
            var procesedText = text.Replace(' ', ',');

            try
            {
                CheckPageSize(ref pageSize);

                var total = _ds.BestMatch(procesedText).Count;
                var totalPages = GetTotalPages(pageSize, total);


                var searchResult = _ds.BestMatch(procesedText)
                    .Select(x => new ListingDTO
                    {
                        Url = Url.Link(GetNameOfMethod(x), new { id = x.PostId }),
                        Name = x.Title
                    });

                var metaData = new
                {
                    Number_Of_Posts = total,
                    Number_Of_Pages = totalPages,
                    PageSize = pageSize,
                    Page = page,
                    Prev = Link(nameof(Search), page, pageSize, -1, () => page > 0),
                    Next = Link(nameof(Search), page, pageSize, +1, () => page < totalPages - 1),
                    Url = Link(nameof(Search), page, pageSize),
                };
                return Ok(new { metaData, searchResult });
            }
            catch
            {
                return NotFound();
            }
        }

        private string GetNameOfMethod(Post post) => post.TypeId == 1 ? nameof(QuestionController.GetQuestion) : nameof(AnswerController.GetAnswer);

    }
}