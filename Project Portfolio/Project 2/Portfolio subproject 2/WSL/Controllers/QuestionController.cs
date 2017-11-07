using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Question")]
    public class QuestionController : AbstractController
    {
        private readonly IDataService _ds;
        public QuestionController(IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}