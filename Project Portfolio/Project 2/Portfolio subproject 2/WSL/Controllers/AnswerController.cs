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

        // GET: api/questions/5
        [HttpGet("{id}", Name = nameof(GetAnswer))]
        public IActionResult GetAnswer(int id)
        {
            var data = new AnswerDTO(_ds.GetAnswer(id), Url.Link(nameof(GetAnswer), new { id }));
            return data.Body != null ? (IActionResult)Ok(data) : NotFound(data);
        }
    }
}

/*
  public class QuestionController : AbstractController
    {
        private readonly IDataService _ds;
        public QuestionController(IDataService iDataService)
        {
            _ds = iDataService;
        }

        // GET: api/questions/5
        [HttpGet("{id}", Name = nameof(GetQuestion))]
        public IActionResult GetQuestion(int id)
        {
            var data = new QuestionDTO(_ds.GetQuestion(id), Url.Link(nameof(GetQuestion), new{id}));
            return data.Body != null ? (IActionResult)Ok(data) : NotFound(data);
        }

        // GET: api/questions
        [HttpGet(Name = nameof(GetNewestQuestions))]
        public IActionResult GetNewestQuestions(int limit = 50, int page = 0, int pageSize = 10)
        {
            var questions = _ds.GetNewestQuestions(limit, page, pageSize);
            var output = questions.Select(question => new QuestionDTO(question, 
                Url.Link(nameof(GetNewestQuestions), new{question.PostId1})));
            return Ok(output);
        }
    }
     */
