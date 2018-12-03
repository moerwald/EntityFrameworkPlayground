using EntityTest;
using NUnit.Framework;
using Respawn;
using System.Linq;

namespace Playground.Tests
{

    [TestFixture]
    public class FixtureName
    {
        private static readonly Checkpoint CheckPoint = new Checkpoint();

        [SetUp]
        public void Setup()
        {
            // Add you setup code here
            using (var db = new BloggingContext())
            {
                var connectionString = db.Database.Connection.ConnectionString;
                var t = CheckPoint.Reset(connectionString);
                t.Wait(5000);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Add you teardown code here
        }

        [Test]
        [MaxTime(60000)]
        public void ModifyingOneEntityDoesntChangeEntityStateOfForeignEntities()
        {
            using (var db = new BloggingContext())
            {
                #region Arrange
                var blog = new Blog { Name = "TestBlog"};
                var user = new User { Blog = blog, UserName = "User1" };
                db.Blogs.Add(blog);
                db.Users.Add(user);
                db.SaveChanges();

                #endregion

                #region Act
                var fetchedUser = db.Users.First(u => u.UserName.Equals("User1"));
                fetchedUser.DisplayName = "SomeDisplayName";
                var entries = db.GetModifiedEntries();
                var materializedEntries = entries.ToList();
                #endregion

                #region Assert
                Assert.That(materializedEntries.Count, Is.EqualTo(1));
                #endregion
            }
        }
    }
}
