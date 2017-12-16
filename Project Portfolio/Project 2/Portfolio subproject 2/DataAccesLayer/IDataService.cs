using System.Collections.Generic;
using DataAccesLayer.DBObjects;

namespace DataAccesLayer
{
    public interface IDataService
    {
        List<User> GetUsers(int page = 0, int pageSize = 10);
        int GetUserCount();
        Post GetPost(int id);
        Post GetPosts_Tags(int id);
        Question GetQuestion(int id);
        List<Question> GetNewestQuestions(int page, int pageSize);
        Answers GetAnswer(int id);
        User GetUser(int id);
        List<LinkedPosts> LinkingToThisPost(int id);
        List<LinkedPosts> LinkedFromThisPost(int id);
        void DeleteMarking(int uid, int pid);
        List<History> UserHistory(int id);
        List<Marking> UserBookmarks(int id);
        int AddMarking(int uid, int pid, string mark);
        int AddQuestion(int uid, string body, string title);
        void AddQuestionToHistory(int userId, int postId);
        void RemoveQuestionFromHistory(int userId, int linkedPostId);
        List<Post> BestMatch(string text);
        int NumberOfQuestions();
        List<Post> GetPosts(int page, int pageSize);
        List<Answers> GetAnswers(int id);
        List<WordIndex> GetTfOfWordsInAPost(int id);
        List<Post> GetPostsUser(int id);
        List<RankedQuestions> RankedPostSearch(string terms, int page, int pageSize);

    }
}
