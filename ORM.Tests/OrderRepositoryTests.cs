using ORM_Fundamentals;
using ORM_Fundamentals.Models;
using ORM_Fundamentals.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ORM.Tests
{
    [TestFixture]
    public class OrderRepositoryTests
    {
        [Test]
        public void Create_Order_InsertsOrder()
        {
            //Arrange
            using var context = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("ApplicationDbContext1")
                    .Options);

            context.Database.EnsureDeleted();

            var orderRepo = new OrderRepository(context);
            var expected = new Order
            {
                Id = 1,
                CreatedDate = new DateTime(2023, 1, 13),
                Status = OrderStatus.Arrived,
                UpdatedDate = new DateTime(2023, 2, 7)
            };

            //Act
            orderRepo.Create(new Order
            {
                CreatedDate = new DateTime(2023, 1, 13),
                Status = OrderStatus.Arrived,
                UpdatedDate = new DateTime(2023, 2, 7)
            });
            var order = context.Orders.Single();

            //Assert
            Assert.AreEqual(expected, order);
        }

        [Test]
        public void Delete_Order_DeletesOrder()
        {
            //Arrange
            using var context = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("ApplicationDbContext2")
                    .Options);
            context.Database.EnsureDeleted();

            var expected = new Order
            {
                Id = 1,
                CreatedDate = new DateTime(2023, 1, 13),
                Status = OrderStatus.Arrived,
                UpdatedDate = new DateTime(2023, 2, 7)
            };

            var expectedCount = 0;

            //Act
            context.Orders.Add(expected);
            context.SaveChanges();

            var orderRepo = new OrderRepository(context);

            orderRepo.Delete(expected);

            var actualCount = context.Orders.Count();

            //Assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void Update_Order_UpdatesOrder()
        {
            //Arrnge
            using var context = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("ApplicationDbContext3")
                    .Options);
            context.Database.EnsureDeleted();

            var order = new Order
            {
                CreatedDate = new DateTime(2023, 1, 13),
                Status = OrderStatus.Arrived,
                UpdatedDate = new DateTime(2023, 2, 7)
            };

            //Act
            context.Orders.Add(order);
            context.SaveChanges();

            var orderRepo = new OrderRepository(context);
            var expected = context.Orders.Single();
            expected.Status = OrderStatus.Loading;

            orderRepo.Update(expected);

            var actualOrder = context.Orders.Single();

            //Assert
            Assert.AreEqual(expected, actualOrder);
        }

        [Test]
        public void Read_Order_ReturnsOrder()
        {
            //Arrange
            using var context = new ApplicationDbContext(
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase("ApplicationDbContext4")
                    .Options);
            context.Database.EnsureDeleted();

            var expected = new Order
            {
                Id = 1,
                CreatedDate = new DateTime(2023, 1, 13),
                Status = OrderStatus.Arrived,
                UpdatedDate = new DateTime(2023, 2, 7)
            };

            //Act
            context.Orders.Add(expected);
            context.SaveChanges();

            var orderRepo = new OrderRepository(context);

            var actualOrder = orderRepo.Read(1);

            //Assert
            Assert.AreEqual(expected, actualOrder);
        }
    }
}