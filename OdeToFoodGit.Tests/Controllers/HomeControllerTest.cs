using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OdeToFoodGit;
using OdeToFoodGit.Controllers;
using OdeToFoodGit.Tests.Fakes;
using OdeToFoodGit.Models;

namespace OdeToFoodGit.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var db = new FakeOdeToFoodDb();
            db.AddSet(TestData.Restaurants);
            HomeController controller = new HomeController(db);
            controller.ControllerContext = new FakeControllerContext();

            // Act
            ViewResult result = controller.Index() as ViewResult;
            IEnumerable<RestaurantListViewModel> model = result.Model as IEnumerable<RestaurantListViewModel>;

            // Assert
            Assert.AreEqual(10, model.Count());
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
