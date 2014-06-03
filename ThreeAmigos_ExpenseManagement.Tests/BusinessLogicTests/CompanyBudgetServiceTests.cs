using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.Tests.Mocks;
using ThreeAmigos_ExpenseManagement.DataAccess;

namespace ThreeAmigos_ExpenseManagement.Tests.BusinessLogicTests
{
    [TestClass]
    public class CompanyBudgetServiceTests
    {
        IBudgetService companyBudgetService;

        [TestInitialize]
        public void IntializeCompanyBudgetService()
        {
            IBudgetDAL budgetDAL = new MockBudgetDAL();

            companyBudgetService = new CompanyBudgetService(30000, budgetDAL); 
        }
    }
}
