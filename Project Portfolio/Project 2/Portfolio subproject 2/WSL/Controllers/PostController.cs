using DataAccesLayer;
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
            PostDTO data;
            if (_ds.GetPost(id).PostId == 1)
            {
                data = new PostDTO
                {
                    Url = Url.Link(nameof(GetPost), new {id}),
                    Title = _ds.GetPost(id).Title,
                    Author =
                        _ds.GetUser(_ds.GetPost(id).OwnerUserId)
                            .UserName, // becaus _ds.GetPost(id).User.UserName dosent work... for some reason
                    AuthorUrl = Url.Link(nameof(UserController.GetUser), new {id = _ds.GetPost(id).OwnerUserId}),
                    CreationDate = _ds.GetPost(id).CreationDate,
                    Score = _ds.GetPost(id).Score,
                    Body = _ds.GetPost(id).Body,
                    Comments = _ds.GetPost(id).Comments,
                    PostTags = _ds.GetPost(id).PostTags
                };
            }
            else
            {
                data = new PostDTO
                {
                    Url = Url.Link(nameof(GetPost), new { id }),
                    Title = "This is an answer to the question: " + _ds.GetPost(_ds.GetAnswer(id).Parentid).Title, //maybe we should include a link to it
                    Author =
                        _ds.GetUser(_ds.GetPost(id).OwnerUserId)
                            .UserName, // becaus _ds.GetPost(id).User.UserName dosent work... for some reason
                    AuthorUrl = Url.Link(nameof(UserController.GetUser), new { id = _ds.GetPost(id).OwnerUserId }),
                    CreationDate = _ds.GetPost(id).CreationDate,
                    Score = _ds.GetPost(id).Score,
                    Body = _ds.GetPost(id).Body,
                    Comments = _ds.GetPost(id).Comments,
                    PostTags = _ds.GetPost(id).PostTags
                };
            }

            var result = new
            {
                //maybe add an if statement so it only runes this code if the post has a title
                Data = data
            };
            return Ok(result);
        }
    }
}