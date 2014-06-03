using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.Tests.Mocks;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;

namespace ThreeAmigos_ExpenseManagement.Tests.BusinessLogicTests
{
    [TestClass]
    public class CompanyBudgetServiceTests
    {
        IBudgetService companyBudgetService;
        Department department;

        [TestInitialize]
        public void IntializeCompanyBudgetService()
        {
            IBudgetDAL budgetDAL = new MockBudgetDAL();

            department = new Department();
            department.DepartmentId = 1;
            department.DepartmentName = "Test Department";
            department.MonthlyBudget = 10000;

            companyBudgetService = new CompanyBudgetService(30000, budgetDAL); 
        }


        [TestMethod]
        public void CompanyBudget_IsBudgetExceeded_False()
        {
            bool result = companyBudgetService.IsBudgetExceeded(100);

            Assert.IsFalse(result, "The budget is not exceeed when the amount is greater than budget amount");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void CompanyBudget_IsBudgetExceeded_ThrowException()
        {
            var expenseAmount = "TwoThousand";

            Assert.IsTrue(companyBudgetService.IsBudgetExceeded(decimal.Parse(expenseAmount)),"This should throw an exception");
        }

    }
}
