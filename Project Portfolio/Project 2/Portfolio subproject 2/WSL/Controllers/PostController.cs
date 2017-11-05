using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Post")]
    public class PostController : Controller
    {
        private readonly IDataService _ds;
        public PostController(IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}