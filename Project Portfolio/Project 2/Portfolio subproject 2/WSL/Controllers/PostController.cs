﻿using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;
using WebServiceLayer.DataTransferObjects;

namespace WebServiceLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Post")]
    public class PostController : AbstractController
    {
        private readonly IDataService _ds;
        public PostController(IDataService iDataService) => _ds = iDataService;

        [HttpGet("{id}", Name = nameof(GetPost))]
        public IActionResult GetPost(int id)
        {
            var result = new
            {
                Data = new PostDTO
                {
                    Url = Url.Link(nameof(GetPost), new { id }),
                    Title = _ds.GetPost(id).Title,
                    Author = _ds.GetPost(id).User.UserName,
                    AuthorUrl = Url.Link(nameof(UserController.GetUser), new { id =_ds.GetPost(id).OwnerUserId }),
                    CreationDate = _ds.GetPost(id).CreationDate,
                    Score = _ds.GetPost(id).Score,
                    Body = _ds.GetPost(id).Body,
                    Comments = _ds.GetPost(id).Comments,
                    PostTags = _ds.GetPost(id).PostTags
                }
            };
            return Ok(result);
        }
    }
}