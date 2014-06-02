using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Tests.Mocks;

namespace ThreeAmigos_ExpenseManagement.Tests.BusinessLogicTests
{
    [TestClass]
    public class CurrencyServiceTests
    {
        CurrencyService currencyService;

        [TestInitialize]
        public void InitializeCurrencyTests()
        {
            IConfigurationDAL config = new MockConfigurationDAL();
            currencyService = new CurrencyService(config);

        }

        [TestMethod]
        public void CurrencyService_CalcAud_FromEUR_IsEqual()
        {
            decimal? expectedAudAmount = 149.146M;
            decimal? testEURAmount = 100.00M;

            decimal? result = currencyService.CalcAud(testEURAmount, "EUR");

            Assert.AreEqual(expectedAudAmount,result,"The EUR converstion did not result in correct AUD amount");
        }

        [TestMethod]
        public void CurrencyService_CalcAud_FromCNY_IsEqual()
        {
            decimal? expectedAudAmount = 17.43M;
            decimal? testCNYAmount = 100.00M;

            decimal? result = currencyService.CalcAud(testCNYAmount, "CNY");

            Assert.AreEqual(expectedAudAmount, result, "The CNY converstion did not result in correct AUD amount");
        }

        [TestMethod]
        public void CurrencyService_CalcAud_FromUnknownCurrency_IsZERO()
        {
            decimal? expectedAudAmount = 0;
            decimal? testFJDAmount = 100.00M;

            decimal? result = currencyService.CalcAud(testFJDAmount, "FJD");

            Assert.AreEqual(expectedAudAmount, result, "The Unknown currency converstion did not result in correct AUD amount");
        }
    }
}
