using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Database;
using AvansFestivals.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace AvansFestivals.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Mock<IFestivalRepo> mock = new Mock<IFestivalRepo>();
            mock.Setup(m => m.getQuerableAllFestivals()).Returns(new Festival[] {
                new Festival {Id = 1, Name = "F1"},
                new Festival {Id = 2, Name = "F2"},
                new Festival {Id = 3, Name = "F3"},
            }.AsQueryable());

            //// Arrange - create a controller
            //HomeController target = new HomeController(mock.Object);

            //// Action
            //Festival[] result = ((IEnumerable<Festival>)target.Events().
            //    ViewData.Model).ToArray();

            //// Assert
            //Assert.AreEqual(result.Length, 3);
            //Assert.AreEqual("F1", result[0].Name);
            //Assert.AreEqual("F2", result[1].Name);
            //Assert.AreEqual("F3", result[2].Name);
        }
    }
}
