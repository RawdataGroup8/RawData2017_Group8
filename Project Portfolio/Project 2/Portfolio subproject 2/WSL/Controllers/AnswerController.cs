using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccesLayer;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WSL.Controllers
{
    [Produces("application/json")]
    [Route("api/Answer")]
    public class AnswerController : Controller
    {
        private readonly IDataService _ds;
        public AnswerController (IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}