using System;
using System.Linq;
using DataAccesLayer;
using Microsoft.AspNetCore.Mvc;

namespace WebLayer.Controllers
{
    [Route("api/posts")]
    public class PostsController : Controller
    {
        private readonly IDataService _ds;

        public PostsController(IDataService ds) => _ds = ds;


        [HttpGet(Name = nameof(GetPosts))]
        public IActionResult GetPosts(int page = 0, int pageSize = 10)
        {
            var posts = _ds.GetPosts(page, pageSize).Select(x => new
                {
                    Link = Url.Link(nameof(GetPost), new {x.PostId}),
                    x.Title
                });
            var total = _ds.NumberOfQuestions();
            var pages = Math.Ceiling(total / (double) pageSize);
            var prev = page > 0 ? Url.Link(nameof(GetPosts), new {page = page - 1, pageSize}) : null;
            var next = page < pages - 1 ? Url.Link(nameof(GetPosts), new {page = page + 1, pageSize}) : null;

            var result = new
            {
                total,
                pages,
                prev,
                next,
                items = posts
            };

            return Ok(result);
        }

        [HttpGet("q", Name = nameof(GetNewestQuestions))]
        public IActionResult GetNewestQuestions(int page = 0, int pageSize = 10)
        {
            var posts = _ds.GetNewestQuestions(page, pageSize)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPost), new {x.Post.PostId}),
                    x.Post.Title,
                    x.Post.Score
                });

            var total = _ds.NumberOfQuestions();
            var pages = Math.Ceiling(total / (double) pageSize);
            var prev = page > 0 ? Url.Link(nameof(GetNewestQuestions), new {page = page - 1, pageSize}) : null;
            var next = page < pages - 1 ? Url.Link(nameof(GetNewestQuestions), new {page = page + 1, pageSize}) : null;

            var result = new
            {
                total,
                pages,
                prev,
                next,
                items = posts
            };

            return Ok(result);
        }

        [HttpGet("{PostId}", Name = nameof(GetPost))]
        public IActionResult GetPost(int postId)
        {

            var post = _ds.GetPost(postId);
            if (post == null) return NotFound();
            if (post.TypeId == 1)
            {

                var result = new
                {
                    Link = Url.Link(nameof(GetPost), new {post.PostId}),
                    post.Title,
                    post.CreationDate,
                    post.Score,
                    post.Body,
                    Answers = Url.Link(nameof(GetAnswers), new {post.PostId})
                };

                return Ok(result);
            }

            else
            {
                var answer = _ds.GetAnswer(postId);
                var result = new
                {
                    Link = Url.Link(nameof(GetPost), new {post.PostId}),
                    post.Title,
                    post.CreationDate,
                    post.Score,
                    post.Body,
                    Parent = Url.Link(nameof(GetPost), new {id = answer.Parentid}),
                };

                return Ok(result);
            }


        }

        [HttpGet("{postid}/answers", Name = nameof(GetAnswers))]
        public IActionResult GetAnswers(int id)
        {
            var posts = _ds.GetAnswers(id)
                .Select(x => new
                {
                    Link = Url.Link(nameof(GetPost), new {x.PostId}),
                    Parent = Url.Link(nameof(GetPost), new {id}),
                    x.GetPost().Body,
                    x.GetPost().CreationDate,
                    x.GetPost().Score
                });

            return Ok(posts);
        }

        [HttpPost("add", Name = nameof(AddQuestion))]
        public IActionResult AddQuestion(int id, string body, string title)
        {
            var succes = _ds.AddQuestion(id, body, title);
            return succes == 1 ? (IActionResult) Ok("Succesfully added") : BadRequest();
        }
    }
}
