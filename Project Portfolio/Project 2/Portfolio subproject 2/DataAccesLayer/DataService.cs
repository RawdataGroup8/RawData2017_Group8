using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using DataAccesLayer.DBObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccesLayer
{
    public class DataService : IDataService
    {
        private readonly StackoverflowContext _db;

        public DataService() => _db = new StackoverflowContext();

        // ------------------------ USERS  ------------------------  
        //public List<User> GetUsers() => _db.User.ToList();
        public List<User> GetUsers(int page, int pageSize) => _db.User.OrderBy(x => x.Userid).Skip(page * pageSize).Take(pageSize).ToList();
        //return a full user, including all posts.
        public User GetUser(int id) => _db.User.Include(c => c.Posts).FirstOrDefault(p => p.Userid == id);

        // ------------------------ POSTS (QUESTIONS / ANSWERS) ------------------------         
        //return a full post, including all comments and tags.
        public Post GetPost(int id) => _db.Post.Include(p1 => p1.Comments).Include(p1 => p1.PostTags).FirstOrDefault(p => p.PostId == id);
        //return a Post, including tags related to the post.
        public Post GetPosts_Tags(int id) => _db.Post.Include(c => c.PostTags).FirstOrDefault(p => p.PostId == id);

        //return a full question
        public Question GetQuestion(int id)
        {
            var question = _db.Question.FirstOrDefault(q => q.PostId1 == id);
            question.SetPost(GetPost(id));
            return question;
        }

        //return a full answer
        public Answers GetAnswer(int id)
        {
            var question = _db.Answer.FirstOrDefault(q => q.PostId == id);
            question.SetPost(GetPost(id));
            return question;
        }
        // ------------------------ LINKS ------------------------         

        public List<LinkedPosts> LinkedFromThisPost(int id) => _db.LinkedPosts.Where(lp => lp.PostId == id).ToList();

        public List<LinkedPosts> LinkingToThisPost(int id) => _db.LinkedPosts.Where(lp => lp.PostId == id).ToList();

        // ------------------------ HISTORY & BOOKMARKS ------------------------         
        public List<History> UserHistory(int id) => _db.History.Where(h => h.Userid == id).ToList();

        public List<Marking> UserBookmarks(int id) => _db.Marking.Where(m => m.Postid == id).ToList();

        public int AddMarking(int uid, int pid, string mark) => _db.Database.ExecuteSqlCommand("call add_marking({0},{1},{2})", uid, pid, mark);

        public void DeleteMarking(int uid, int pid)
        {
            _db.Database.ExecuteSqlCommand("delete from marking where user_id = 1 and post_id = 2", uid, pid);
        }

        public void AddQuestionToHistory(int PostID, int UserID) => _db.History.Add(new History { LinkPostId = PostID, Userid = UserID, DateTimeAdded = new System.DateTime() });
        
        // ------------------------ PROCEDURES ------------------------         
        // A procedure that searches
        public bool FulltextSearch()
        {
            // you can also use the string interpolation syntax
            //var str = "What used for java";
            //var id1 = 1;
            var result = _db.Post1.FromSql("call fulltext_search({0},{1})", "What used for java", 1);

            foreach (var text in result)
            {
                if (text.PostId == 25115395) return true;
                //Console.WriteLine($"{category.CategoryId}, {category.CategoryName}");
            }
            return false;
        }
        public List<Answers> Retrieve_Answers_Procedure(string query)
        {
            // you can also use the string interpolation syntax
            var type_id = 1;
            return _db.Answer.FromSql($"call fulltext_search({query},{type_id})").ToList();
        }

        public List<Question> SearchQuestionsByTag(string tag, int limit)
        {
            throw new System.NotImplementedException();
            //var result = _db.Question.FromSql($"call search_questions_by_tag({tag},{limit})");
        }
    }
}