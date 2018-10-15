using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApiStudentData;
using WebApiStudentData.Controllers;

namespace WebApiStudentData.Tests.Controllers
{
    [TestClass]
    public class UserInfoControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            UserInfoController controller = new UserInfoController();

            // Act
            //IEnumerable<string> result = controller.Get();

            List<Object> Object_List = controller.Get();

            // Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(2, result.Count());
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            UserInfoController controller = new UserInfoController();

            // Act
            //string result = controller.Get(5);

            // Assert
            //Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            UserInfoController controller = new UserInfoController();

            // Act
            controller.Post("value");

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            UserInfoController controller = new UserInfoController();

            // Act
            controller.Put(5, "value");

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            UserInfoController controller = new UserInfoController();

            // Act
            controller.Delete(5);

            // Assert
        }
    }
}
