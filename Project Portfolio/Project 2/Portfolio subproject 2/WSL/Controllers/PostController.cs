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
            try
            {
                PostDTO data;
                if (_ds.GetPost(id).PostId == 1) //This should be typeid right?
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
                        //Comments = _ds.GetPost(id).Comments, //needs dto
                        //PostTags = _ds.GetPost(id).PostTags  //needs dto
                    };
                }
                else
                {
                    data = new PostDTO
                    {
                        Url = Url.Link(nameof(GetPost), new {id}),
                        Title =
                            "This is an answer to the question: " +
                            _ds.GetPost(_ds.GetAnswer(id).Parentid)
                                .Title, //maybe we should include a link to it, or just use the answer controller I guess
                        Author =
                            _ds.GetUser(_ds.GetPost(id).OwnerUserId)
                                .UserName, // becaus _ds.GetPost(id).User.UserName dosent work... for some reason
                        AuthorUrl = Url.Link(nameof(UserController.GetUser), new {id = _ds.GetPost(id).OwnerUserId}),
                        CreationDate = _ds.GetPost(id).CreationDate,
                        Score = _ds.GetPost(id).Score,
                        Body = _ds.GetPost(id).Body,
                        //Comments = _ds.GetPost(id).Comments, //needs dto
                        //PostTags = _ds.GetPost(id).PostTags  //needs dto
                    };
                }

                var result = new
                {
                    Data = data
                };
                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}