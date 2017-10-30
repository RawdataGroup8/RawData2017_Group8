using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.DBObjects;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    class StackoverflowContext : DbContext
    {
        public DbSet<Answers> Answers { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<History> History { get; set; } // Not yet created.
        public DbSet<LinkedPosts> LinkedPosts { get; set; }
        public DbSet<Marking> Marking { get; set; } // Not yet
        public DbSet<Post> Post { get; set; }
        public DbSet<PostTags> PostTags { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql("server=localhost;database=stack_overflow_normalized;uid=root; pwd=toor;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Answers
            modelBuilder.Entity<Answers>().Property(x => x.Parentid).HasColumnName("parent_id");
            modelBuilder.Entity<Answers>().Property(x => x.Postid1).HasColumnName("post_id");

            // Comment
            modelBuilder.Entity<Comment>().Property(x => x.CommentId).HasColumnName("comment_id");
            //modelBuilder.Entity<Comment>().Property(x => x.CommentUser).HasColumnName("CommentUser");
            modelBuilder.Entity<Comment>().Property(x => x.CommentText).HasColumnName("comment_text");

            //LinkedPosts 
            modelBuilder.Entity<LinkedPosts>().Property(x => x.LinkPostId).HasColumnName("link_post_id");
            modelBuilder.Entity<LinkedPosts>().Property(x => x.PostId).HasColumnName("post_id");

            //Post
            modelBuilder.Entity<Post>().Property(x => x.PostId).HasColumnName("post_id");
            //modelBuilder.Entity<Post>().Property(x => x.OwnerUserId).HasColumnName("OwnerUserId");
            modelBuilder.Entity<Post>().Property(x => x.Body).HasColumnName("body");
            modelBuilder.Entity<Post>().Property(x => x.Title).HasColumnName("title");
            //modelBuilder.Entity<Post>().Property(x => x.TypeId).HasColumnName("TypeId");

            //Posttags
            modelBuilder.Entity<PostTags>().Property(x => x.PostId).HasColumnName("post_id");
            modelBuilder.Entity<PostTags>().Property(x => x.TagName).HasColumnName("tag_name");

            //Question
            //modelBuilder.Entity<Question>().Property(x => x.PostId).HasColumnName("post_id");
            modelBuilder.Entity<Question>().Property(x => x.AcceptedAnswerId).HasColumnName("accepted_answer_id");
            modelBuilder.Entity<Question>().Property(x => x.ClosedDate).HasColumnName("closed_date");

            //User
            modelBuilder.Entity<User>().Property(x => x.Userid).HasColumnName("user_id");
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnName("user_name");
            modelBuilder.Entity<User>().Property(x => x.UserLocation).HasColumnName("user_location");
            //modelBuilder.Entity<User>().Property(x => x.Userage).HasColumnName("Userage");



        }

    }
}
