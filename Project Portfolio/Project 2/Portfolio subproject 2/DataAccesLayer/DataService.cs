using System.Collections.Generic;
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
        //return count of numbers
        public int GetUserCount() => _db.User.Count();
        //return a full user, including all posts.
        public User GetUser(int id) => _db.User.Include(c => c.Posts).FirstOrDefault(p => p.Userid == id);

        // ------------------------ POSTS (QUESTIONS / ANSWERS) ------------------------   

        public List<Post> GetPosts(int page, int pageSize)
        {
            return _db.Post
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public List<Answers> GetAnswers(int id)
        {
            var answers = _db.Answer.Where(x => x.Parentid == id).ToList();
            foreach (var answer in answers)
            {
                answer.SetPost(_db.Post.FirstOrDefault(p => p.PostId == answer.PostId));
            }
            return answers;
        }

        public int NumberOfQuestions()
        {
            using (var db = new StackoverflowContext())
            {
                return _db.Post.Count(x => x.TypeId == 1);
            }
        }

        //return a full post, including all comments and tags. 
        public Post GetPost(int id)
        {
            using (var db = new StackoverflowContext())
            {
                return db.Post.Include(p1 => p1.Comments).Include(p1 => p1.PostTags)
                    .FirstOrDefault(p => p.PostId == id);
            }
        }

        //return a Post, including tags related to the post.
        public Post GetPosts_Tags(int id) => _db.Post.Include(c => c.PostTags).FirstOrDefault(p => p.PostId == id);

        //return a full question
        public Question GetQuestion(int id)
        {
            using (var db = new StackoverflowContext())
            {
                var question = db.Question.FirstOrDefault(q => q.PostId1 == id);
                question?.SetPost(GetPost(id));
                return question;
            }
        }

        public List<Question> GetNewestQuestions(int page, int pageSize)
        {
            using (var db = new StackoverflowContext())
            {
                var posts = db.Post.Where(p => p.TypeId == 1).OrderByDescending(q => q.CreationDate)
                    .Skip(page * pageSize).Take(pageSize).ToList();
                return posts.Select(post => GetQuestion(post.PostId)).ToList();
            }
        }

        //return a full answer
        public Answers GetAnswer(int id)
        {
            var answer = _db.Answer.FirstOrDefault(q => q.PostId == id);
            answer?.SetPost(GetPost(id));
            return answer;
        }
        // ------------------------ LINKS ------------------------         

        public List<LinkedPosts> LinkedFromThisPost(int id) => _db.LinkedPosts.Where(lp => lp.PostId == id).ToList();

        public List<LinkedPosts> LinkingToThisPost(int id) => _db.LinkedPosts.Where(lp => lp.PostId == id).ToList();


        // ------------------------ HISTORY & BOOKMARKS ------------------------         
        public List<History> UserHistory(int id) => _db.History.Where(h => h.Userid == id).ToList();

        public List<Marking> UserBookmarks(int id) => _db.Marking.Where(m => m.Postid == id).ToList();

        public int AddMarking(int uid, int pid, string mark) => _db.Database.ExecuteSqlCommand("call add_marking({0},{1},{2})", uid, pid, mark);

        public void DeleteMarking(int uid, int pid) => _db.Database.ExecuteSqlCommand("delete from marking where user_id = {0} and post_id = {1}", uid, pid);

        public List<History> GetHistory() => _db.History.ToList();

        public History GetHistoryItem(int userId, int linkPostId) => _db.History.FirstOrDefault(h => h.Userid == userId && h.LinkPostId == linkPostId);

        public void AddQuestionToHistory(int userId, int postId)
        {
            _db.History.Add(new History(userId, postId));
            _db.SaveChanges();
        }

        public void RemoveQuestionFromHistory(int userId, int linkPostId)
        {
            _db.History.Remove(GetHistoryItem(userId, linkPostId));
            _db.SaveChanges();
        }
        // ------------------------ PROCEDURES ------------------------         
        // A procedure that searches
        //public List<Post> FulltextSearch(string text, int postType) => _db.Post.FromSql("call fulltext_search({0},{1})", text, postType).ToList();


        public List<Post> BestMatch(string text) => _db.Post.FromSql("call bestmatch({0})").ToList(); // the words in the text needs to be coma seperated and the string needs to be max 5000 in length for this  to work


        public List<Question> SearchQuestionsByTag(string tag, int limit) => _db.Question.FromSql("call search_questions_by_tag({0},{1})", tag, limit).ToList();
    }
}