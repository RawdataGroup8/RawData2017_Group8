using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccesLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/WordTf")]
    public class WordTfController : Controller
    {
        private readonly IDataService _ds;

        public WordTfController(IDataService ds) => _ds = ds;

        [HttpGet("{id}", Name = nameof(WordTf))]
        public IActionResult WordTf(int id)
        {
            var tf = _ds.GetTfOfWordsInAPost(id)
                .Select(x => new
                {
                    text = x.Word,
                    weight = x.TermFrequency
                });

            if (tf.First() == null)
            {
                return NotFound();
            }

            return Ok(tf);
        }
    }
}