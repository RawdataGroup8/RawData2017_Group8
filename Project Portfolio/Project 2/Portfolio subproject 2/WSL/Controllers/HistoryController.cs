using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/History")]
    public class HistoryController : Controller
    {
        private readonly IDataService _ds;
        public HistoryController(IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}