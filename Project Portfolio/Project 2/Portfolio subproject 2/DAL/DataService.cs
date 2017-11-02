using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.DBObjects;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataService : IDataService
    {
        private readonly StackoverflowContext _db;

        public DataService()
        {
            _db = new StackoverflowContext();
        }

        public List<User> GetUsers() => _db.User.ToList();

 
        public IQueryable GetQuestion(int id) //NOT WORKING/DONE
        {
            /*var question = from post in _db.Post.ToList()
                           join q in _db.Question.ToList() on post.PostId == q.PostId;*/
            var q = _db.Post.Join(_db.Answers,
                    post => post.PostId,
                    ans => ans.Postid1,
                    (post, ans) => new {Post = post, Meta = ans})
                .Where(postAndAns => postAndAns.Post.PostId == id);
            return q;
        }

        // returns a post and include the comments
        public Post GetPost(int id)
        {
            /*var question = from post in _db.Post.ToList()
                           join q in _db.Question.ToList() on post.PostId == q.PostId;*/
            return _db.Post.Include(c => c.Comments).FirstOrDefault(p => p.PostId == id);
        }
        //retuns a user with their posts
        public User GetUser(int id)
        {
            return _db.User.Include(c => c.Posts).FirstOrDefault(p => p.Userid == id);
        }

        public List<Question> SeachQuestionsByTag(string tagname, int limit)
        {
            List<Question> result = _db.Question.FromSql("call search_questions_by_tag({0},{1})", tagname, limit).ToList();
            return result;
        }

    }

}