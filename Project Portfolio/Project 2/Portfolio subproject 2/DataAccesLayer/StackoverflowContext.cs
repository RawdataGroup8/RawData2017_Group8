using DataAccesLayer.DBObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccesLayer
{
    internal class StackoverflowContext : DbContext
    {
        //internal readonly object Post1;
       // public DbSet<SimplePost> SimplePost { get; set; }
       // public DbSet<SimpleQuestion> SimpleQuestion { get; set; }

        public DbSet<Answers> Answer { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<History> History { get; set; } 
        public DbSet<LinkedPosts> LinkedPosts { get; set; }
        public DbSet<Marking> Marking { get; set; } 
        public DbSet<Post> Post { get; set; }
        public DbSet<PostTags> PostTags { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<WordIndex> WordIndex { get; set; }
        public DbSet<RankedQuestions> RankedQuestions { get; set; }
        public DbSet<RankedWords> RankedWords { get; set; }
        public DbSet<NewestQuestions> NewestQuestions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql("server=localhost;database=stack_overflow_normalized;uid=root; pwd=toor;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Answers
            modelBuilder.Entity<Answers>().ToTable("answer");
            modelBuilder.Entity<Answers>().HasKey(x => x.PostId);
            modelBuilder.Entity<Answers>().Property(x => x.Parentid).HasColumnName("parent_id");
            modelBuilder.Entity<Answers>().Property(x => x.PostId).HasColumnName("post_id");
          
            // Comment
            modelBuilder.Entity<Comment>().Property(x => x.CommentId).HasColumnName("comment_id");
            modelBuilder.Entity<Comment>().Property(x => x.CommentText).HasColumnName("comment_text");
            modelBuilder.Entity<Comment>().Property(x => x.CommentScore).HasColumnName("comment_score"); 
            modelBuilder.Entity<Comment>().Property(x => x.PostId).HasColumnName("post_id");
            modelBuilder.Entity<Comment>().Property(x => x.CommentCreateDate).HasColumnName("comment_create_date");
             
            //History
            modelBuilder.Entity<History>().Property(x => x.Userid).HasColumnName("user_id");
            modelBuilder.Entity<History>().Property(x => x.LinkPostId).HasColumnName("link_post_id");
            modelBuilder.Entity<History>().Property(x => x.DateTimeAdded).HasColumnName("datetime_added");
            modelBuilder.Entity<History>().HasKey(k => new { k.Userid, DateTime_aded = k.DateTimeAdded });

            //LinkedPosts 
            modelBuilder.Entity<LinkedPosts>().ToTable("linked_posts");
            modelBuilder.Entity<LinkedPosts>().Property(x => x.LinkPostId).HasColumnName("link_post_id");
            modelBuilder.Entity<LinkedPosts>().Property(x => x.PostId).HasColumnName("post_id");
            modelBuilder.Entity<LinkedPosts>().HasKey(k => new { k.LinkPostId, k.PostId});

            //Marking
            modelBuilder.Entity<Marking>().Property(x => x.Userid).HasColumnName("user_id");
            modelBuilder.Entity<Marking>().Property(x => x.Postid).HasColumnName("post_id");
            modelBuilder.Entity<Marking>().Property(x => x.Datetime_added).HasColumnName("datetime_added");
            modelBuilder.Entity<Marking>().Property(x => x.Folder_label).HasColumnName("folder_tag");
            modelBuilder.Entity<Marking>().HasKey(k => new { k.Userid, k.Postid });

            //Post
            modelBuilder.Entity<Post>().Property(x => x.PostId).HasColumnName("post_id");
            modelBuilder.Entity<Post>().Property(x => x.CreationDate).HasColumnName("creation_date");
            modelBuilder.Entity<Post>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<Post>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<Post>().Property(x => x.OwnerUserId).HasColumnName("owner_user_id");
            modelBuilder.Entity<Post>().Property(x => x.TypeId).HasColumnName("type_id");

            //One to many relationship between the user and the post.
            modelBuilder.Entity<Post>().HasOne(c => c.User).WithMany(p => p.Posts).HasForeignKey(c => c.OwnerUserId);

            //Posttags
            modelBuilder.Entity<PostTags>().ToTable("post_tags");
            modelBuilder.Entity<PostTags>().Property(x => x.PostId).HasColumnName("post_id");
            modelBuilder.Entity<PostTags>().Property(x => x.TagName).HasColumnName("tag_name");
            modelBuilder.Entity<PostTags>().HasKey(k => new { k.PostId, k.TagName });

            //Question
            modelBuilder.Entity<Question>().Property(x => x.PostId1).HasColumnName("post_id");
            modelBuilder.Entity<Question>().Property(x => x.AcceptedAnswerId).HasColumnName("accepted_answer_id");
            modelBuilder.Entity<Question>().Property(x => x.ClosedDate).HasColumnName("closed_date");           

            //User
            modelBuilder.Entity<User>().Property(x => x.Userid).HasColumnName("user_id");
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnName("user_name");
            modelBuilder.Entity<User>().Property(x => x.UserLocation).HasColumnName("user_location");
            modelBuilder.Entity<User>().Property(x => x.UserCreationDate).HasColumnName("user_creation_date");
            modelBuilder.Entity<User>().Property(x => x.Userage).HasColumnName("user_age");

            //WordIndex
            modelBuilder.Entity<WordIndex>().ToTable("wi");
            modelBuilder.Entity<WordIndex>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<WordIndex>().Property(x => x.Word).HasColumnName("word");
            modelBuilder.Entity<WordIndex>().Property(x => x.TermFrequency).HasColumnName("tf");
            modelBuilder.Entity<WordIndex>().HasKey(k => new { k.Id, k.Word, k.TermFrequency});

            //Search
            modelBuilder.Entity<RankedQuestions>().Property(x => x.PostId).HasColumnName("id");
            modelBuilder.Entity<RankedQuestions>().Property(x => x.Rank).HasColumnName("rank");
            modelBuilder.Entity<RankedQuestions>().Property(x => x.Title).HasColumnName("title");

            //Ranked Words search
            modelBuilder.Entity<RankedWords>().Property(x => x.Word).HasColumnName("word");
            modelBuilder.Entity<RankedWords>().Property(x => x.Rank).HasColumnName("rank");

            //Newest Questions View
            modelBuilder.Entity<NewestQuestions>().ToTable("newest_question_view");
            modelBuilder.Entity<NewestQuestions>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<NewestQuestions>().Property(x => x.Score).HasColumnName("score");
            modelBuilder.Entity<NewestQuestions>().Property(x => x.PostId).HasColumnName("post_id");
        }



    }
}
