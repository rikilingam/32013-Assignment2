var today = new Date();
$(document).ready(function () {
    var toEndDate = new Date();
    var dp = $('#expenseDate');
    dp.datetimepicker({
        pickTime: false,
        autoclose: true,
    });
});


// This script was adapted from http://itzonesl.blogspot.com.au/2014/01/how-to-validate-ddmmyyyy-date-format-in.html
$(document).ready(function () {
    $.culture = Globalize.culture("en-AU");
    $.validator.methods.date = function (value, element) {
        //This is not ideal but Chrome passes dates through in ISO1901 format regardless of locale
        //and despite displaying in the specified format.

        return this.optional(element)
            || Globalize.parseDate(value, "dd/MM/yyyy", "en-AU")
            || Globalize.parseDate(value, "yyyy-mm-dd");
    }
});

function ShowExpenseItemModal() {
    $('#ExpenseItemModal').modal('show');
}

function HideExpenseItemModal() {
    $('#ExpenseItemModal').modal('hide');
}


function OpenReceipt(receiptFileName) {
    var path = '<%=ConfigurationManager.AppSettings["ReceiptItemFilePath"].ToString() %>';
    window.open(receiptFileName);
}


function ShowBudgetWarningModal() {
    $('#BudgetWarningModal').modal('show');
}

function HideBudgetWarningModal() {
    $('#BudgetWarningModal').modal('hide');
}

function CheckDeptBudgetExceeded(isExceeded) {
    if (isExceeded == true) {
        $('#BudgetWarningModal').modal('show');
    }
}

//Check if the expense form has items
function CheckExpenseItems(itemCount)
{
    if (itemCount > 0)
    {
        return true;
    }
    else
    {
        return false;
    }
}


