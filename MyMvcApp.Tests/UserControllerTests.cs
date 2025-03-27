using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace MyMvcApp.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public void Index_ReturnsViewWithUserList()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" }
            };

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserController.userlist, result.Model);
        }

        [Fact]
        public void Details_ValidId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };

            // Act
            var result = controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserController.userlist.First(), result.Model);
        }

        [Fact]
        public void Details_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Details(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Get_ReturnsView()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Name = "John Doe", Email = "john@example.com" };

            // Act
            var result = controller.Create(user) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Contains(user, UserController.userlist);
        }

        [Fact]
        public void Create_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            var controller = new UserController();
            controller.ModelState.AddModelError("Name", "Required");
            var user = new User { Email = "john@example.com" };

            // Act
            var result = controller.Create(user) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user, result.Model);
        }

        [Fact]
        public void Edit_Get_ValidId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };

            // Act
            var result = controller.Edit(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserController.userlist.First(), result.Model);
        }

        [Fact]
        public void Edit_Get_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Edit(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_ValidModel_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };
            var updatedUser = new User { Id = 1, Name = "John Smith", Email = "johnsmith@example.com" };

            // Act
            var result = controller.Edit(1, updatedUser) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("John Smith", UserController.userlist.First().Name);
        }

        [Fact]
        public void Edit_Post_InvalidModel_ReturnsView()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };
            controller.ModelState.AddModelError("Name", "Required");
            var updatedUser = new User { Id = 1, Email = "johnsmith@example.com" };

            // Act
            var result = controller.Edit(1, updatedUser) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUser, result.Model);
        }

        [Fact]
        public void Delete_Get_ValidId_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };

            // Act
            var result = controller.Delete(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(UserController.userlist.First(), result.Model);
        }

        [Fact]
        public void Delete_Get_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Post_ValidId_RedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" }
            };

            // Act
            var result = controller.Delete(1, null) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Empty(UserController.userlist);
        }

        [Fact]
        public void Delete_Post_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist = new List<User>();

            // Act
            var result = controller.Delete(1, null);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}