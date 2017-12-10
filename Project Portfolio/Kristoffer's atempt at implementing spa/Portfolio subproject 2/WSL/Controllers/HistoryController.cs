using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/History")]
    public class HistoryController : AbstractController
    {
        private readonly IDataService _ds;
        public HistoryController(IDataService iDataService) => _ds = iDataService;
    }
}