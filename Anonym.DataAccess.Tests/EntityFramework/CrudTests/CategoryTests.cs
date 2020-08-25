using Anonym.DataAccess.Abstract;
using Anonym.DataAccess.Concrete.EntityFramework;
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
    public class CategoryTests
    {
        [TestMethod, Priority(1)]
        public void Create_a_category_in_database_via_anonym_context()
        {
            try
            {

                Category category = new Category
                {
                    CategoryId = Guid.NewGuid().ToString(),
                    Name = "category-unit-testing",
                    Description = "category-unit-testing",
                    Active = false
                };

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    var addedCategory = context.Entry(category);
                    addedCategory.State = EntityState.Added;
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
        public void Read_a_category_in_database_via_anonym_context()
        {
            try
            {
                Category category = new Category();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    category = context.Set<Category>().SingleOrDefault(c => c.Name == "category-unit-testing");
                }

                Assert.AreEqual("category-unit-testing", category.Description);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod, Priority(3)]
        public void Update_a_category_in_database_via_anonym_context()
        {
            try
            {
                Category category = new Category();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    category = context.Set<Category>().SingleOrDefault(c => c.Name == "category-unit-testing");

                    category.Active = true;

                    var updatedCategory = context.Entry(category);
                    updatedCategory.State = EntityState.Modified;
                    context.SaveChanges();
                }

                Assert.AreEqual(true, category.Active);
            }
            catch (Exception exception)
            {
                Assert.Fail(exception.Message);
            }
        }

        [TestMethod, Priority(4)]
        public void Delete_a_category_in_database_via_anonym_context()
        {
            try
            {
                Category category = new Category();

                using (var context = new AnonymContext(DbOptionsFactory.DbContextOptions))
                {
                    category = context.Set<Category>().SingleOrDefault(c => c.Name == "category-unit-testing");

                    var deletedCategory = context.Entry(category);
                    deletedCategory.State = EntityState.Deleted;
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
