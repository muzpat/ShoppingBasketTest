using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingBasketTest.Models;

namespace ShoppingBasketTest.Tests.Models
{
    [TestClass]
    public class Models
    {
        [TestMethod]
        public void CreateStockTest()
        {
            // Arrange  
            Stock myStock = new Stock();
            // Act
            myStock = myStock.CreateStock();

            // Assert
            Assert.IsNotNull(myStock, "Unable to create myStock object");
        }

        [TestMethod]
        public void CreateOffersTest()
        {
            // Arrange  
            Offers mySpecials = new Offers();
            // Act
            mySpecials = mySpecials.CreateOffers();

            // Assert
            Assert.IsNotNull(mySpecials, "Unable to create mySpecials object");
        }
    }
}
