using ORM_Fundamentals;
using ORM_Fundamentals.Models;
using ORM_Fundamentals.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ORM.Tests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        [Test]
        public void Create_Product_InsertsProduct()
        {
            //Arrange
            using var context = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("ApplicationDbContext1")
                    .Options);
            context.Database.EnsureDeleted();

            var expected = new Product
            {
                Id = 1,
                Name = "ProductName1",
                Description = "ProductDescription",
                Height = 100,
                Length = 200,
                Weight = 101,
                Width = 200
            };

            var productRepository = new ProductRepository(context);

            //Act
            productRepository.Create(new Product
            {
                Name = "ProductName1",
                Description = "ProductDescription",
                Height = 100,
                Length = 200,
                Weight = 101,
                Width = 200
            });
            var product = context.Products.Single();

            //Assert
            Assert.AreEqual(expected, product);
        }

        [Test]
        public void Delete_Product_DeletesProduct()
        {
            //Arrange
            using var context = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("ApplicationDbContext2")
                    .Options);
            context.Database.EnsureDeleted();

            var expected = new Product
            {
                Id = 1,
                Name = "ProductName1",
                Description = "ProductDescription",
                Height = 100,
                Length = 200,
                Weight = 101,
                Width = 200
            };

            var expectedCount = 0;

            //Act
            context.Products.Add(expected);
            context.SaveChanges();

            var productRepository = new ProductRepository(context);

            productRepository.Delete(expected);

            var actualCount = context.Products.Count();

            //Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Update_Product_UpdatesProduct()
        {
            //Arrange
            using var context = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("ApplicationDbContext3")
                    .Options);
            context.Database.EnsureDeleted();

            var product = new Product
            {
                Id = 1,
                Name = "ProductName1",
                Description = "ProductDescription",
                Height = 100,
                Length = 200,
                Weight = 101,
                Width = 200
            };

            //Act
            context.Products.Add(product);
            context.SaveChanges();

            var expected = context.Products.Single();
            expected.Name = "new name";
            expected.Description = "new desc";

            var productRepository = new ProductRepository(context);
            productRepository.Update(expected);

            var actualProduct = context.Products.Single();

            //Assert
            Assert.AreEqual(expected, actualProduct);
        }

        [Test]
        public void Read_Product_ReturnsProduct()
        {
            //Arrange
            using var context = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("ApplicationDbContext4")
                    .Options);
            context.Database.EnsureDeleted();

            var expected = new Product
            {
                Id = 1,
                Name = "ProductName1",
                Description = "ProductDescription",
                Height = 100,
                Length = 200,
                Weight = 101,
                Width = 200
            };

            //Act
            context.Products.Add(expected);
            context.SaveChanges();

            var productRepository = new ProductRepository(context);

            var actualOrder = productRepository.Read(1);

            //Assert
            Assert.AreEqual(expected, actualOrder);
        }
    }
}