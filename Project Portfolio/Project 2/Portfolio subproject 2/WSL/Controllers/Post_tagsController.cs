using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Post_tags")]
    public class Post_tagsController : Controller
    {
        private readonly IDataService _ds;
        public Post_tagsController(IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}