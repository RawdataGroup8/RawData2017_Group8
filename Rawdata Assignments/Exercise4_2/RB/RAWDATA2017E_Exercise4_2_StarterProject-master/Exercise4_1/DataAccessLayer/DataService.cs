using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise4_1.DomainModel;

namespace Exercise4_1.DataAccessLayer
{
    public class DataService : IDataService
    {
        List<Post> _posts = new List<Post>();

        public DataService()
        {
            var csv = File.OpenText("posts.csv");

            if (!csv.EndOfStream)
            {
                // skip column names
                csv.ReadLine();

                while (!csv.EndOfStream)
                {
                    var line = csv.ReadLine();

                    var post = Parse(line);
                    if (post != null)
                    {
                        _posts.Add(post);
                    }
                }
            }
        }

        public List<Post> GetPosts(int page, int pageSize)
        {
            return _posts
                .Where(x => x.PostType == 1)
                .Skip(page*pageSize)
                .Take(pageSize)
                .ToList();
        }


        public Post GetPost(int id) => _posts.FirstOrDefault(x => x.Id == id);

        public List<Post> GetAnswers(int id) => _posts.Where(x => x.ParentId == id).ToList();

        public int NumberOfQuestions() => _posts.Count(x => x.PostType == 1);


        /***************************************************
         * 
         * Helpers 
         * 
         **************************************************/


        private static Post Parse(string line)
        {
            var post = new Post();
            var pos = 0;
            var data = "";

            (data, pos) = ParseField(pos, line);
            post.Id = int.Parse(data, NumberStyles.Integer);

            (data, pos) = ParseField(pos, line);
            post.PostType = int.Parse(data, NumberStyles.Integer);

            (data, pos) = ParseField(pos, line);
            if (data == "NULL") post.ParentId = null;
            else post.ParentId = int.Parse(data, NumberStyles.Integer);

            (data, pos) = ParseField(pos, line);
            post.CreationDate = data;

            (data, pos) = ParseField(pos, line);
            post.Score = int.Parse(data, NumberStyles.Integer);

            (data, pos) = ParseField(pos, line);
            if (data == "NULL") post.Title = null;
            else post.Title = data;

            (data, pos) = ParseField(pos, line);
            post.Body = data;
            
            return post;
        }

        static (string, int) ParseField(int pos, string line)
        {
            while (pos < line.Length && char.IsWhiteSpace(line[pos])) pos++;

            var data = "";

            // parse qouted string
            if (line[pos] == '"')
            {
                pos++;
                while (pos < line.Length)
                {
                    if (line[pos] == '\\' && pos + 1 < line.Length && line[pos + 1] == '"')
                    {
                        data += "\\\"";
                        pos += 2;
                    }
                    else if (line[pos] == '"') break;
                    else data += line[pos++];
                }
                pos++;
            }
            else
            {
                while (line[pos] != ',')
                    data += line[pos++];
            }
            
            // skip ending '
            pos++;
            return (data, pos);
        }


    }
}
