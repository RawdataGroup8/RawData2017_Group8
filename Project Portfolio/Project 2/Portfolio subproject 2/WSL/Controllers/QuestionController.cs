using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebServiceLayer.DataTransferObjects;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/questions")]
    public class QuestionController : AbstractController
    {
        private readonly IDataService _ds;
        public QuestionController(IDataService iDataService) => _ds = iDataService;

        [HttpGet("{id}", Name = "GetQuestion")] // GET: api/questions/5
        public IActionResult GetQuestion(int id)
        {
            var question = new QuestionDTO(_ds.GetQuestion(id), Url.Link(nameof(GetQuestion), id));
            return question.Body != null ? (IActionResult)Ok(new { question }) : NotFound(question);
        }
    }
}