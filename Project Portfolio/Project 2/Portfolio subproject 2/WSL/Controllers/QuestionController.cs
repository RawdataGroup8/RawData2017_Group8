using System.Collections.Generic;
using System.Linq;
using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using WebServiceLayer.DataTransferObjects;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/questions")]
    public class QuestionController : AbstractController
    {
        private readonly IDataService _ds;
        public QuestionController(IDataService iDataService) => _ds = iDataService;

        // GET: api/questions/5
        [HttpGet("{id}", Name = nameof(GetQuestion))]
        public IActionResult GetQuestion(int id)
        {
            var data = new QuestionDTO(_ds.GetQuestion(id), Url.Link(nameof(GetQuestion), new{id}));
            return data.Body != null ? (IActionResult)Ok(new { data }) : NotFound(data);
        }

        // GET: api/questions
        [HttpGet(Name = nameof(GetNewestQuestions))]
        public IActionResult GetNewestQuestions()
        {
            var questions = _ds.GetNewestQuestions(20, 1, 10);
            var output = questions.Select(question => new QuestionDTO(question, Url.Link(nameof(GetNewestQuestions), new{question.PostId1}))).ToList();
            return Ok(output);
        }
    }
}