﻿using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/PostTags")]
    public class PostTagsController : AbstractController
    {
        private readonly IDataService _ds;
        public PostTagsController(IDataService iDataService)
        {
            _ds = iDataService;
        }
    }
}