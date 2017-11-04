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
                    ans => ans.post_id,
                    (post, ans) => new {Post = post, Meta = ans})
                .Where(postAndAns => postAndAns.Post.PostId == id);
            return q;
        }

        //return a full post, including all comments.
        public Post GetPost(int id) => _db.Post.Include(c => c.Comments).FirstOrDefault(p => p.PostId == id);

        //return a full user, including all posts.
        public User GetUser_Posts(int id) => _db.User.Include(c => c.Posts).FirstOrDefault(p => p.Userid == id);

        //return a Post, including tags related to the post.
        public Post GetPosts_Tags(int id) => _db.Post.Include(c => c.PostTags).FirstOrDefault(p => p.PostId == id);












        // A procedure that searches

        public bool Searching_usingtype_String()
        {
           
                // you can also use the string interpolation syntax
                //var str = "What used for java";
                //var id1 = 1;
            var result = _db.Post1.FromSql("call fulltext_search({0},{1})", "What used for java", 1);

            foreach (var text in result)
            {
                if (text.post_id == 22944075) return true;


                //Console.WriteLine($"{category.CategoryId}, {category.CategoryName}");
            }

            return false;
        }
        public List<Answers> Retrieve_Answers_Procedure()
        {

            // you can also use the string interpolation syntax
            var id = 99;
            var limit = 10;
            return _db.Answers.FromSql($"call fulltext_search({id},{limit})").ToList();
        }
    }

}