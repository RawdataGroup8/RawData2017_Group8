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
            optionsBuilder.UseMySql("server=localhost;database=stack_overflow_normalized;uid=root;pwd=toor");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Answers
            modelBuilder.Entity<Answers>().Property(x => x.Parentid).HasColumnName("Parentid");
            modelBuilder.Entity<Answers>().Property(x => x.Postid).HasColumnName("Postid");

            // Comment
            modelBuilder.Entity<Comment>().Property(x => x.CommentId).HasColumnName("CommentId");
            modelBuilder.Entity<Comment>().Property(x => x.CommentUser).HasColumnName("CommentUser");
            modelBuilder.Entity<Comment>().Property(x => x.CommentText).HasColumnName("CommentText");

            // 
            modelBuilder.Entity<LinkedPosts>().Property(x => x.LinkPostId).HasColumnName("LinkPostId");
            modelBuilder.Entity<LinkedPosts>().Property(x => x.PostId).HasColumnName("PostId");

            //Post
            modelBuilder.Entity<Post>().Property(x => x.PostId).HasColumnName("PostId");
            modelBuilder.Entity<Post>().Property(x => x.OwnerUserId).HasColumnName("OwnerUserId");
            modelBuilder.Entity<Post>().Property(x => x.Body).HasColumnName("Body");
            modelBuilder.Entity<Post>().Property(x => x.Title).HasColumnName("Title");
            modelBuilder.Entity<Post>().Property(x => x.PostId).HasColumnName("PostIdt");
            modelBuilder.Entity<Post>().Property(x => x.TypeId).HasColumnName("TypeId");

            //Posttags
            modelBuilder.Entity<PostTags>().Property(x => x.PostId).HasColumnName("PostId");
            modelBuilder.Entity<PostTags>().Property(x => x.LinkPostId).HasColumnName("linkPostId");

            //Question
            modelBuilder.Entity<Question>().Property(x => x.QuestionPost).HasColumnName("QuestionPost");
            modelBuilder.Entity<Question>().Property(x => x.AcceptedAnswerId).HasColumnName("AcceptedAnswerId");
            modelBuilder.Entity<Question>().Property(x => x.ClosedDate).HasColumnName("ClosedDate");

            //User
            modelBuilder.Entity<User>().Property(x => x.Userid).HasColumnName("Userid");
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnName("UserName");
            modelBuilder.Entity<User>().Property(x => x.UserLocation).HasColumnName("UserLocation");
            modelBuilder.Entity<User>().Property(x => x.Userage).HasColumnName("Userage");



        }

    }
}
