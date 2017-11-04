﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.DBObjects;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    class StackoverflowContext : DbContext
    {
        //internal readonly object Post1;
        public DbSet<Post1> Post1 { get; set; }

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

            modelBuilder.Entity<Post1>().ToTable("post");
            modelBuilder.Entity<Post1>().HasKey(x => x.post_id);

            modelBuilder.Entity<Answers1>().ToTable("answers");
            modelBuilder.Entity<Answers1>().HasKey(x => x.post_id);

            // Answers
            modelBuilder.Entity<Answers>().Property(x => x.Parentid).HasColumnName("parent_id");
            modelBuilder.Entity<Answers>().Property(x => x.post_id).HasColumnName("post_id");

           

            // Comment
            modelBuilder.Entity<Comment>().Property(x => x.CommentId).HasColumnName("comment_id");
            //modelBuilder.Entity<Comment>().Property(x => x.CommentUser).HasColumnName("CommentUser");
            modelBuilder.Entity<Comment>().Property(x => x.CommentText).HasColumnName("comment_text");
            modelBuilder.Entity<Comment>().Property(x => x.CommentScore).HasColumnName("comment_score"); 
            modelBuilder.Entity<Comment>().Property(x => x.PostId).HasColumnName("post_id");
            modelBuilder.Entity<Comment>().Property(x => x.CommentCreateDate).HasColumnName("comment_create_date");


            //modelBuilder.Entity<Comment>().HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(c => c.PostId);

             



            //History
            modelBuilder.Entity<History>().Property(x => x.Userid).HasColumnName("user_id");
            modelBuilder.Entity<History>().Property(x => x.Linkpost_id).HasColumnName("link_post_id");
            modelBuilder.Entity<History>().Property(x => x.DateTime_aded).HasColumnName("datetime_added");
            modelBuilder.Entity<History>().HasKey(k => new { k.Userid, k.DateTime_aded });
            
            //LinkedPosts 
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


            //modelBuilder.Entity<Post>(). //fill comments here?

            //One to many relationship between the user and the post.

            modelBuilder.Entity<Post>().HasOne(c => c.User).WithMany(p => p.Posts).HasForeignKey(c => c.OwnerUserId);


            //Posttags
            modelBuilder.Entity<PostTags>().Property(x => x.PostId).HasColumnName("post_id");
            modelBuilder.Entity<PostTags>().Property(x => x.TagName).HasColumnName("tag_name");
            modelBuilder.Entity<PostTags>().HasKey(k => new { k.PostId, k.TagName });

            //modelBuilder.Entity<PostTags>().HasOne(c => c.Post).WithMany(p => PostTags).HasForeignKey(c => c.PostId);


            //Question
            //modelBuilder.Entity<Question>().Property(x => x.PostId).HasColumnName("post_id");
            modelBuilder.Entity<Question>().Property(x => x.AcceptedAnswerId).HasColumnName("accepted_answer_id");
            modelBuilder.Entity<Question>().Property(x => x.ClosedDate).HasColumnName("closed_date");
            

            //User
            modelBuilder.Entity<User>().Property(x => x.Userid).HasColumnName("user_id");
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnName("user_name");
            modelBuilder.Entity<User>().Property(x => x.UserLocation).HasColumnName("user_location");
            modelBuilder.Entity<User>().Property(x => x.UserCreationDate).HasColumnName("user_creation_date");
            modelBuilder.Entity<User>().Property(x => x.Userage).HasColumnName("user_age");
            //modelBuilder.Entity<User>().HasMany(Comment).WithOne();
        }

    }
}
