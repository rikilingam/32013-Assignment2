﻿var today = new Date();
$(document).ready(function () {
    var dp = $('#<%=txtItemDate.ClientID%>');
    dp.datetimepicker({
        pickTime: false,
        autoclose: true,

    });
});

function ShowExpenseItemModal() {
    $('#ExpenseItemModal').modal('show');
}

function HideExpenseItemModal() {
    $('#ExpenseItemModal').modal('hide');
}


function OpenReceipt(receiptFileName) {
    var path = '<%=ConfigurationManager.AppSettings["ReceiptItemFilePath"].ToString() %>'
    window.open(receiptFileName);
}


function ShowBudgetWarningModal() {
    $('#BudgetWarningModal').modal('show');
}

function HideBudgetWarningModal() {
    $('#BudgetWarningModal').modal('hide');
}
