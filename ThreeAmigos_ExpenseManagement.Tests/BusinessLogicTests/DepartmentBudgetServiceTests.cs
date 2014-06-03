using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeAmigos_ExpenseManagement.BusinessLogic;
using ThreeAmigos_ExpenseManagement.DataAccess;
using ThreeAmigos_ExpenseManagement.Models;
using ThreeAmigos_ExpenseManagement.Tests.Mocks;

namespace ThreeAmigos_ExpenseManagement.Tests.BusinessLogicTests
{
    /// <summary>
    /// Test for Department Budget
    /// </summary>
    [TestClass]
    public class DepartmentBudgetServiceTests
    {
        IBudgetService deptBudgetService;
        Department department;

        [TestInitialize]
        public void IntializeDepartmentService()
        {
            IBudgetDAL budgetDAL = new MockBudgetDAL();
            
            department = new Department();
            department.DepartmentId = 1;
            department.DepartmentName = "Test Department";
            department.MonthlyBudget = 10000;

            deptBudgetService = new DepartmentBudgetService(department,budgetDAL);
        }

        [TestMethod]
        public void SetBudgetSpent_GetTotalSpentFromMockDb_IsTrue()
        {
            decimal expectedAmount = 9900;
            deptBudgetService.SetBudgetSpent(06,2014);

            Assert.AreEqual(expectedAmount, deptBudgetService.Budget.Spent, "Expected amount does not equal the amount spent");
        }

    }
}
