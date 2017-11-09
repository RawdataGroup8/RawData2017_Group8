using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/LinkedPosts")]
    public class LinkedPostsController : AbstractController
    {
        private readonly IDataService _ds;
        public LinkedPostsController(IDataService iDataService) => _ds = iDataService;
    }
}