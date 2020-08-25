using Anonym.DataAccess.Concrete.EntityFramework.Contexts;
using Anonym.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anonym.DataAccess.Tests.EntityFramework.CrudTests
{
    [TestClass]
    public class ChatMessageTests
    {
        [TestMethod, Priority(1)]
        public void Create_a_chatmessage_in_database_via_anonym_context()
        {
            try
            {
                Category category = new Category
                {
                    CategoryId = Guid.NewGuid().ToString(),
                    Name = "chatmessage-unit-testing-relationship",
                    Description = "chatmessage-unit-testing-relationship",
                    Active = false
                };

                Post post = new Post
                {
                    PostId = Guid.NewGuid().ToString(),
                    UserId = "",
                    CategoryId = category.CategoryId,
                    Title = "chatmessage-unit-testing-relationship",
                    Text = "chatmessage-unit-testing-relationship",
                    CreatePostDate = DateTime.Now,
                    IsActive = false
                };

                ChatRoom chatRoom = new ChatRoom
                {
                    ChatRoomId = Guid.NewGuid().ToString(),
                    ReviewerUserId = "",
                    PostId = post.PostId,
                    PublisherName = "chatmessage-unit-testing-relationship",
                    ReviewerName = "chatmessage-unit-testing-relationship",
                    PublisherCommented = false,
                    Active = false,
                    CreateDate = DateTime.Now,
                };

                ChatMessage chatMessage = new ChatMessage
                {
                    ChatMessageId = Guid.NewGuid().ToString(),
                    ChatRoomId = chatRoom.ChatRoomId,
                    DisplayUserName = "chatmessage-unit-testing",
                    Message = "chatmessage-unit-testing",
                    IsPublisherMessage = false,
                    CreateDate = DateTime.Now
                };

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
                }

                Assert.IsTrue(true);

            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod, Priority(2)]
        public void Read_a_chatmessage_in_database_via_anonym_context()
        {
            try
            {
                ChatMessage chatMessage = new ChatMessage();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    chatMessage = context.Set<ChatMessage>().SingleOrDefault(c => c.DisplayUserName == "chatmessage-unit-testing");
                }

                Assert.AreEqual("chatmessage-unit-testing", chatMessage.Message);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod, Priority(3)]
        public void Update_a_chatmessage_in_database_via_anonym_context()
        {
            try
            {
                ChatMessage chatMessage = new ChatMessage();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    chatMessage = context.Set<ChatMessage>().SingleOrDefault(c => c.DisplayUserName == "chatmessage-unit-testing");

                    chatMessage.IsPublisherMessage = true;

                    var updatedChatMessage= context.Entry(chatMessage);
                    updatedChatMessage.State = EntityState.Modified;
                    context.SaveChanges();
                }

                Assert.AreEqual(true, chatMessage.IsPublisherMessage);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod, Priority(4)]
        public void Delete_a_chatmessage_in_database_via_anonym_context()
        {
            try
            {
                ChatMessage chatMessage = new ChatMessage();
                ChatRoom chatRoom = new ChatRoom();
                Post post = new Post();
                Category category = new Category();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    chatMessage = context.Set<ChatMessage>().SingleOrDefault(c => c.DisplayUserName == "chatmessage-unit-testing");

                    chatRoom = context.Set<ChatRoom>().SingleOrDefault(c => c.ReviewerName == "chatmessage-unit-testing-relationship");

                    post = context.Set<Post>().SingleOrDefault(c => c.Title == "chatmessage-unit-testing-relationship");

                    category = context.Set<Category>().SingleOrDefault(c => c.Name == "chatmessage-unit-testing-relationship");

                    var deletedCategory = context.Entry(category);
                    deletedCategory.State = EntityState.Deleted;

                    var deletedPost = context.Entry(post);
                    deletedPost.State = EntityState.Deleted;

                    var deletedChatRoom = context.Entry(chatRoom);
                    deletedChatRoom.State = EntityState.Deleted;

                    var deletedChatMessage = context.Entry(chatMessage);
                    deletedChatMessage.State = EntityState.Deleted;
                    context.SaveChanges();
                }

                Assert.IsTrue(true);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }
    }
}
