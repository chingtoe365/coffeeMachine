using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoffeeMachine.Controllers;
using CoffeeMachine.Models.Exceptions;
using CoffeeMachine.Models.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoffeeMachineUnitTest
{
    [TestClass]
    public class MachineResponseTest : IMachineResponseTest
    {

        [TestMethod]
        public void InsertOrder_CorrectFormat_ReturnCreatedStatusCode()
        {
            // Arrage - Mock
            var orderDetail = new OrderDetails("coffee", "10", 1);
            var controller = new OrderController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            // Act
            var result = controller.Post(orderDetail);
            // Assert
            Assert.IsTrue(result.StatusCode.Equals(HttpStatusCode.Created));
        }

        [TestMethod]
        public void InsertOrder_WrongDrinkFormat_ReturnBadRequest()
        {
            // Arrage - Mock
            var orderDetail = new OrderDetails("apple juice", "10", 1);
            var controller = new OrderController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            // Act
            var result = controller.Post(orderDetail);
            Assert.IsTrue(result.StatusCode.Equals(HttpStatusCode.BadRequest));
        }

        [TestMethod]
        public void InsertOrder_WrongSugarAmountFormat_ReturnBadRequest()
        {
            // Arrage - Mock
            var orderDetail = new OrderDetails("tea", "qty not int", 1);
            var controller = new OrderController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            // Act
            var result = controller.Post(orderDetail);
            Assert.IsTrue(result.StatusCode.Equals(HttpStatusCode.BadRequest));
        }

        [TestMethod]
        public void InsertOrder_WrongOwnMugFormat_ReturnBadRequest()
        {
            // Arrage - Mock
            var orderDetail = new OrderDetails("chocolate", "20", 4);
            var controller = new OrderController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            // Act
            var result = controller.Post(orderDetail);
            Assert.IsTrue(result.StatusCode.Equals(HttpStatusCode.BadRequest));
        }

        [TestMethod]
        public void GetLastOrder_CompareMockInsert_ReturnTrue()
        {
            // Arrage - Mock
            var orderDetail = new OrderDetails("chocolate", "20", 1);
            var controller = new OrderController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
            // Act
            var newOrderResult = controller.Post(orderDetail);
            var getLastOrderResult = controller.Get();
            var lastOrderDetail = getLastOrderResult.Content.ReadAsAsync<OrderDetails>().Result;
            // Assert
            Assert.IsTrue(getLastOrderResult.StatusCode.Equals(HttpStatusCode.OK));
            Assert.IsTrue(lastOrderDetail.Drink.Equals(orderDetail.Drink));
            Assert.IsTrue(lastOrderDetail.SugarAmount.Equals(orderDetail.SugarAmount));
            Assert.IsTrue(lastOrderDetail.OwnMug.Equals(orderDetail.OwnMug));
        }
    }
}
