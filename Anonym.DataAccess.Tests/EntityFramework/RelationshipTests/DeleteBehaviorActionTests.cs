using Anonym.Entities.Concrete;
using Core.DataAccess.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anonym.DataAccess.Tests.EntityFramework.RelationshipTests
{
    [TestClass]
    public class DeleteBehaviorActionTests
    {
        [TestMethod, Description("When the category is deleted for the category and post relationship, the foreign key value in the post should fail.")]
        [ExpectedException(typeof(DbUpdateException))]
        public void Get_error_if_delete_a_category_when_post_has_a_foreign_key_in_database_via_anonym_context()
        {
            Category category = new Category
            {
                CategoryId = Guid.NewGuid().ToString(),
                Name = "post-category-unit-testing-relationship",
                Description = "post-category-unit-testing-relationship",
                Active = false
            };

            Post post = new Post
            {
                PostId = Guid.NewGuid().ToString(),
                UserId = "",
                CategoryId = category.CategoryId,
                Title = "post-category-unit-testing",
                Text = "post-category-unit-testing",
                CreatePostDate = DateTime.Now,
                IsActive = false
            };

            try
            {
                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    var addedCategory = context.Entry(category);
                    addedCategory.State = EntityState.Added;

                    var addedPost = context.Entry(post);
                    addedPost.State = EntityState.Added;
                    context.SaveChanges();

                    var deletedCategory = context.Entry(category);
                    deletedCategory.State = EntityState.Deleted;
                    context.SaveChanges();
                }

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                // Rollback
                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    var deletedPost = context.Entry(post);
                    deletedPost.State = EntityState.Deleted;
                    context.SaveChanges();

                    var deletedCategory = context.Entry(category);
                    deletedCategory.State = EntityState.Deleted;
                    context.SaveChanges();
                }

                throw;
            }
        }

        [TestMethod, Description("When the post is deleted for the post, chatroom and chatmessage relationship, posts with foreign key should be deleted.")]
        public void Delete_as_cascade_if_delete_a_post_when_chatroom_and_chatmessage_has_foreign_key_in_database_via_anonym_context()
        {
            Category category = new Category
            {
                CategoryId = Guid.NewGuid().ToString(),
                Name = "post-chatroom-unit-testing-relationship",
                Description = "post-chatroom-unit-testing-relationship",
                Active = false
            };

            Post post = new Post
            {
                PostId = Guid.NewGuid().ToString(),
                UserId = "",
                CategoryId = category.CategoryId,
                Title = "post-chatroom-unit-testing-relationship",
                Text = "post-chatroom-unit-testing-relationship",
                CreatePostDate = DateTime.Now,
                IsActive = false
            };

            ChatRoom chatRoom = new ChatRoom
            {
                ChatRoomId = Guid.NewGuid().ToString(),
                ReviewerUserId = "",
                PostId = post.PostId,
                PublisherName = "post-chatroom-unit-testing",
                ReviewerName = "post-chatroom-unit-testing",
                PublisherCommented = false,
                Active = false,
                CreateDate = DateTime.Now,
            };

            ChatMessage chatMessage = new ChatMessage
            {
                ChatMessageId = Guid.NewGuid().ToString(),
                ChatRoomId = chatRoom.ChatRoomId,
                DisplayUserName = "chatmessage-chatroom-unit-testing",
                Message = "chatmessage-chatroom-unit-testing",
                IsPublisherMessage = false,
                CreateDate = DateTime.Now
            };

            ChatMessage chatMessageExpected = new ChatMessage();

            using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
            {
                var addedCategory = context.Entry(category);
                addedCategory.State = EntityState.Added;

                var addedPost = context.Entry(post);
                addedPost.State = EntityState.Added;

                var addedChatRoom = context.Entry(chatRoom);
                addedChatRoom.State = EntityState.Added;

                var addedChatMessage = context.Entry(chatMessage);
                addedChatMessage.State = EntityState.Added;
                context.SaveChanges();

                var deletedPost = context.Entry(post);
                deletedPost.State = EntityState.Deleted;

                var deletedCategory = context.Entry(category);
                deletedCategory.State = EntityState.Deleted;
                context.SaveChanges();

                chatMessageExpected = context.Set<ChatMessage>().SingleOrDefault(c => c.DisplayUserName == "chatmessage-chatroom-unit-testing");
            }

            Assert.IsNull(chatMessageExpected);
        }

        [TestMethod, Description("When the chatroom is deleted for the chatmessage and chatroom relationship, chatmessages with foreign key should be deleted.")]
        public void Delete_as_cascade_if_delete_a_chatroom_when_chatmessage_has_foreign_key_in_database_via_anonym_context()
        {
            Category category = new Category
            {
                CategoryId = Guid.NewGuid().ToString(),
                Name = "chatmessage-chatroom-unit-testing-relationship",
                Description = "chatmessage-chatroom-unit-testing-relationship",
                Active = false
            };

            Post post = new Post
            {
                PostId = Guid.NewGuid().ToString(),
                UserId = "",
                CategoryId = category.CategoryId,
                Title = "chatmessage-chatroom-unit-testing-relationship",
                Text = "chatmessage-chatroom-unit-testing-relationship",
                CreatePostDate = DateTime.Now,
                IsActive = false
            };

            ChatRoom chatRoom = new ChatRoom
            {
                ChatRoomId = Guid.NewGuid().ToString(),
                ReviewerUserId = "",
                PostId = post.PostId,
                PublisherName = "chatmessage-chatroom-unit-testing-relationship",
                ReviewerName = "chatmessage-chatroom-unit-testing-relationship",
                PublisherCommented = false,
                Active = false,
                CreateDate = DateTime.Now,
            };

            ChatMessage chatMessage = new ChatMessage
            {
                ChatMessageId = Guid.NewGuid().ToString(),
                ChatRoomId = chatRoom.ChatRoomId,
                DisplayUserName = "chatmessage-chatroom-unit-testing",
                Message = "chatmessage-chatroom-unit-testing",
                IsPublisherMessage = false,
                CreateDate = DateTime.Now
            };

            ChatMessage chatMessageExpected = new ChatMessage();

            using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
            {
                var addedCategory = context.Entry(category);
                addedCategory.State = EntityState.Added;

                var addedPost = context.Entry(post);
                addedPost.State = EntityState.Added;

                var addedChatRoom = context.Entry(chatRoom);
                addedChatRoom.State = EntityState.Added;

                var addedChatMessage = context.Entry(chatMessage);
                addedChatMessage.State = EntityState.Added;
                context.SaveChanges();

                var deletedChatRoom = context.Entry(chatRoom);
                deletedChatRoom.State = EntityState.Deleted;
                context.SaveChanges();

                var deletedPost = context.Entry(post);
                deletedPost.State = EntityState.Deleted;

                var deletedCategory = context.Entry(category);
                deletedCategory.State = EntityState.Deleted;
                context.SaveChanges();

                chatMessageExpected = context.Set<ChatMessage>().SingleOrDefault(c => c.DisplayUserName == "chatmessage-chatroom-unit-testing");
            }

            Assert.IsNull(chatMessageExpected);
        }
    }
}
