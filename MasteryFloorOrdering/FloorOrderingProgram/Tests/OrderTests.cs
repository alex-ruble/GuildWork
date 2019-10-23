using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FloorOrderBLL;
using FloorOrderBLL.Manager;
using FloorOrderBLL.ManagerFactory;
using FloorOrderDAL;
using FloorOrderModels;
using FloorOrderModels.Interfaces;
using FloorOrderModels.Responses;

namespace Tests
{
    [TestFixture]
    public class OrderTests
    {
        OrderManager manager = OrderManagerFactory.Create();
        ProductManager productManager = ProductManagerFactory.Create();
        TaxStateManager stateManager = TaxStateManagerFactory.Create();
        InputRules inputManager = new InputRules();
        public decimal Convert(double x)
        {
            string y = x.ToString();
            decimal z = decimal.Parse(y);
            return z;
        }
        [Test]
        public void LocateOrderFile()
        {
            DateTime orderDate = new DateTime(2013, 06, 01);
            OrderLookupResponse response = manager.AllOrderLookup(orderDate);

            Assert.IsNotNull(response.Orders);
            Assert.IsTrue(response.Success);
        }

        [TestCase(2, 2013, 06, 01, false)]
        [TestCase(1, 2013, 06, 01, true)]
        [TestCase(1, 2013, 06, 02, false)]
        public void LocateOrder(int orderNumber, int y, int m, int d, bool expected)
        {

            DateTime orderDate = new DateTime(y, m, d);

            OrderLookupResponse response = manager.OrderLookup(orderDate, orderNumber);

            Assert.AreEqual(expected, response.Success);
        }

        [TestCase(2013, 06, 01, false)]
        [TestCase(2019, 09, 13, true)]
        [TestCase(2020, 09, 09, true)]
        public void DateCheckTest(int y, int m, int d, bool expected)
        {
            DateTime dateTime = new DateTime(y, m, d);
            DateCheckResponse response = new DateCheckResponse();

            response = inputManager.DateCheck(dateTime);

            Assert.AreEqual(expected, response.Success);

        }

        [TestCase("", false)]
        [TestCase("]", false)]
        [TestCase("***", false)]
        [TestCase("Rockwell***Grant", false)]
        [TestCase("Dwight, Chase", true)]
        [TestCase("Dwight", true)]
        public void NameCheckTest(string nameInput, bool expected)
        {
            NameCheckResponse response = new NameCheckResponse();

            response = inputManager.NameCheck(nameInput);

            Assert.AreEqual(expected, response.Success);
        }

        [TestCase("", false)]
        [TestCase("Minnesota", true)]
        public void StateCheckTest(string input, bool expected)
        {
            StateInputCheckResponse response = new StateInputCheckResponse();

            response = inputManager.StateCheck(input);

            Assert.AreEqual(expected, response.Success);
        }

        [TestCase("", false)]
        [TestCase("Wood", true)]
        public void ProductCheckTest(string input, bool expected)
        {
            ProductInputCheckResponse response = new ProductInputCheckResponse();

            response = inputManager.ProductCheck(input);

            Assert.AreEqual(expected, response.Success);
        }

        [TestCase(20.00, false)]
        [TestCase(-100.00, false)]
        [TestCase(100.00, true)]
        [TestCase(1000.00, true)]
        public void AreaCheckTest(double fakeArea, bool expected)
        {
            decimal area = Convert(fakeArea);
            AreaCheckResponse response = new AreaCheckResponse();

            response = inputManager.AreaCheck(area);

            Assert.AreEqual(expected, response.Success);
        }

        [TestCase("Metal", false)]
        [TestCase("Carpet", true)]
        [TestCase("Laminate", true)]
        [TestCase("Tile", true)]
        [TestCase("Wood", true)]
        public void ProductFindTest(string productType, bool expected)
        {
            ProductLookupResposnse response = productManager.FindProduct(productType);

            Assert.AreEqual(expected, response.Success);
        }

        [TestCase("Ohio", true)]
        [TestCase("Pennsylvania", true)]
        [TestCase("Michigan", true)]
        [TestCase("Indiana", true)]
        [TestCase("Minnesota", false)]
        public void StateFindTest(string state, bool expected)
        {
            TaxStateLookupResponse response = stateManager.FindTaxState(state);

            Assert.AreEqual(expected, response.Success);
        }
    }
}
