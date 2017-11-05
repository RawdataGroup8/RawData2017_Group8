using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Linked_posts")]
    public class Linked_postsController : Controller
    {
        private readonly IDataService _ds;
        public Linked_postsController(IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}