using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/answer")]
    public class AnswerController : AbstractController
    {
        private readonly IDataService _ds;
        public AnswerController (IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}