using Anonym.Entities.Concrete;
using Core.DataAccess.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anonym.DataAccess.Tests.EntityFramework.CrudTests
{
    [TestClass]
    public class ChatRoomTests
    {
        [TestMethod, Priority(1)]
        public void Create_a_chatroom_in_database_via_anonym_context()
        {
            try
            {
                Category category = new Category
                {
                    CategoryId = Guid.NewGuid().ToString(),
                    Name = "chatroom-unit-testing-relationship",
                    Description = "chatroom-unit-testing-relationship",
                    Active = false
                };

                Post post = new Post
                {
                    PostId = Guid.NewGuid().ToString(),
                    UserId = "",
                    CategoryId = category.CategoryId,
                    Title = "chatroom-unit-testing-relationship",
                    Text = "chatroom-unit-testing-relationship",
                    CreatePostDate = DateTime.Now,
                    IsActive = false
                };

                ChatRoom chatRoom = new ChatRoom
                {
                    ChatRoomId = Guid.NewGuid().ToString(),
                    ReviewerUserId = "",
                    PostId = post.PostId,
                    PublisherName = "chatroom-unit-testing",
                    ReviewerName = "chatroom-unit-testing",
                    PublisherCommented = false,
                    Active = false,
                    CreateDate = DateTime.Now,
                };

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    var addedCategory = context.Entry(category);
                    addedCategory.State = EntityState.Added;

                    var addedPost = context.Entry(post);
                    addedPost.State = EntityState.Added;

                    var addedChatRoom = context.Entry(chatRoom);
                    addedChatRoom.State = EntityState.Added;
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
        public void Read_a_chatroom_in_database_via_anonym_context()
        {
            try
            {
                ChatRoom chatRoom = new ChatRoom();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    chatRoom = context.Set<ChatRoom>().SingleOrDefault(c => c.ReviewerName == "chatroom-unit-testing");
                }

                Assert.AreEqual("chatroom-unit-testing", chatRoom.PublisherName);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod, Priority(3)]
        public void Update_a_chatroom_in_database_via_anonym_context()
        {
            try
            {
                ChatRoom chatRoom = new ChatRoom();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    chatRoom = context.Set<ChatRoom>().SingleOrDefault(c => c.ReviewerName == "chatroom-unit-testing");

                    chatRoom.Active = true;

                    var updatedChatRoom = context.Entry(chatRoom);
                    updatedChatRoom.State = EntityState.Modified;
                    context.SaveChanges();
                }

                Assert.AreEqual(true, chatRoom.Active);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod, Priority(4)]
        public void Delete_a_chatroom_in_database_via_anonym_context()
        {
            try
            {
                ChatRoom chatRoom = new ChatRoom();
                Post post = new Post();
                Category category = new Category();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    chatRoom = context.Set<ChatRoom>().SingleOrDefault(c => c.ReviewerName == "chatroom-unit-testing");

                    post = context.Set<Post>().SingleOrDefault(c => c.Title == "chatroom-unit-testing-relationship");

                    category = context.Set<Category>().SingleOrDefault(c => c.Name == "chatroom-unit-testing-relationship");

                    var deletedCategory = context.Entry(category);
                    deletedCategory.State = EntityState.Deleted;

                    var deletedPost = context.Entry(post);
                    deletedPost.State = EntityState.Deleted;

                    var deletedChatRoom = context.Entry(chatRoom);
                    deletedChatRoom.State = EntityState.Deleted;
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
