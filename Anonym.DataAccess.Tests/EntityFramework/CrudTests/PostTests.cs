using Anonym.DataAccess.Abstract;
using Anonym.DataAccess.Concrete.EntityFramework;
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
    public class PostTests
    {
        [TestMethod, Priority(1)]
        public void Create_a_post_in_database_via_anonym_context()
        {
            try
            {
                Category category = new Category
                {
                    CategoryId = Guid.NewGuid().ToString(),
                    Name = "post-unit-testing-relationship",
                    Description = "post-unit-testing-relationship",
                    Active = false
                };

                Post post = new Post
                {
                    PostId = Guid.NewGuid().ToString(),
                    UserId = "",
                    CategoryId = category.CategoryId,
                    Title = "post-unit-testing",
                    Text = "post-unit-testing",
                    CreatePostDate = DateTime.Now,
                    IsActive = false
                };

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    var addedCategory = context.Entry(category);
                    addedCategory.State = EntityState.Added;

                    var addedPost = context.Entry(post);
                    addedPost.State = EntityState.Added;
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
        public void Read_a_post_in_database_via_anonym_context()
        {
            try
            {
                Post post = new Post();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    post = context.Set<Post>().SingleOrDefault(c => c.Title == "post-unit-testing");
                }

                Assert.AreEqual("post-unit-testing", post.Text);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod, Priority(3)]
        public void Update_a_post_in_database_via_anonym_context()
        {
            try
            {
                Post post = new Post();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    post = context.Set<Post>().SingleOrDefault(c => c.Title == "post-unit-testing");

                    post.IsActive = true;

                    var updatedPost = context.Entry(post);
                    updatedPost.State = EntityState.Modified;
                    context.SaveChanges();
                }

                Assert.AreEqual(true, post.IsActive);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod, Priority(4)]
        public void Delete_a_post_in_database_via_anonym_context()
        {
            try
            {
                Post post = new Post();
                Category category = new Category();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    post = context.Set<Post>().SingleOrDefault(c => c.Title == "post-unit-testing");

                    category = context.Set<Category>().SingleOrDefault(c => c.Name == "post-unit-testing-relationship");

                    var deletedCategory = context.Entry(category);
                    deletedCategory.State = EntityState.Deleted;

                    var deletedPost = context.Entry(post);
                    deletedPost.State = EntityState.Deleted;
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
