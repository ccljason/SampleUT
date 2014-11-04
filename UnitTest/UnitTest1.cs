using System;
using System.Collections.Generic;
using DataLayer.Data;
using DataLayer.DataService;
using DomainLogic.LedgerRecs;
using DomainLogic.ListRecMgr;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTest
{
   [TestClass]
   public class UnitTest1
   {
      [TestMethod]
      public void Test_AddData()
      {
         // theory:
         //http://stackoverflow.com/questions/1980108/verifying-a-method-was-called

         // Try this:
         //http://stackoverflow.com/questions/20863180/in-a-unit-test-do-you-verify-and-assert


         // Arrange
         var mockCustListRec = new CustListRec();
         var mockCustomerDataService = new Mock<IDataService>();
         var mockCustListRecMgr = new Mock<IListRecMgr>();

         CustLedgerModel cust_stub = new CustLedgerModel(mockCustomerDataService.Object, mockCustListRecMgr.Object);

         // Act
         // Add a data - solid data added
         CustomerData custData = new CustomerData()
         {
            Id = 1,
            Property = "property 1",
            Email = "1@email.com",
         };
         cust_stub.DataHolder = custData;
         cust_stub.Add();

         // Add another data - not created
         cust_stub = null;
         cust_stub = new CustLedgerModel(mockCustomerDataService.Object, mockCustListRecMgr.Object);
         cust_stub.Add();

         // Assert
         mockCustomerDataService.Verify(x => x.InsertData(It.IsAny<IData>()), Times.Exactly(2));
         mockCustListRecMgr.Verify(x => x.Add(It.IsAny<CustListRec>()), Times.Exactly(2));
      }

      [TestMethod]
      public void Test_DeleteData()
      {
         // Arrange
         var mockCustomerDataService = new Mock<IDataService>();
         var mockCustListRecMgr = new Mock<IListRecMgr>();

         CustLedgerModel cust_stub = new CustLedgerModel(mockCustomerDataService.Object, mockCustListRecMgr.Object);

         // Act
         // Add a data - solid data added
         CustomerData custData = new CustomerData()
         {
            Id = 1,
            Property = "property 1",
            Email = "1@email.com",
         };
         cust_stub.DataHolder = custData;
         cust_stub.Delete();

         // Add another data - not created
         cust_stub = null;
         cust_stub = new CustLedgerModel(mockCustomerDataService.Object, mockCustListRecMgr.Object);
         cust_stub.Delete();

         // Assert
         mockCustomerDataService.Verify(x => x.DeleteData(It.IsAny<IData>()), Times.Exactly(2));
         mockCustListRecMgr.Verify(x => x.Delete(It.IsAny<CustListRec>()), Times.Exactly(2));
      }

      [TestMethod]
      public void Test_LoadData()
      {
         // Arrange
         var mockCustomerDataService = new Mock<IDataService>();
         var mockCustListRecMgr = new Mock<IListRecMgr>();

         CustLedgerModel cust_stub = new CustLedgerModel(mockCustomerDataService.Object, mockCustListRecMgr.Object);

         // Act
         // Add a data - solid data added
         CustomerData custData = new CustomerData()
         {
            Id = 1,
            Property = "property 1",
            Email = "1@email.com",
         };
         cust_stub.DataHolder = custData;
         cust_stub.Load();

         // Add another data - not created
         cust_stub = null;
         cust_stub = new CustLedgerModel(mockCustomerDataService.Object, mockCustListRecMgr.Object);
         cust_stub.Load();

         // Assert
         //var custData2 = It.IsAny<IData>();
         //mockCustomerDataService.Verify(x => x.LoadData(ref custData2), Times.Exactly(2));
         //mockCustListRecMgr.Verify(x => x.(It.IsAny<CustListRec>()), Times.Exactly(2));
      }
   }
}
