using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using WebServiceLayer.DataTransferObjects;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/answers")]
    public class AnswerController : AbstractController
    {
        private readonly IDataService _ds;
        public AnswerController(IDataService iDataService) => _ds = iDataService;

        // GET: api/answers/5
        [HttpGet("{id}", Name = nameof(GetAnswer))]
        public IActionResult GetAnswer(int id)
        {
            var data = new AnswerDTO(_ds.GetAnswer(id), Url.Link(nameof(GetAnswer), new { id }));
            return data.Body != null ? (IActionResult)Ok(data) : NotFound(data);
        }
    }
}

