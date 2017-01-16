(function () {
    "use strict";
    angular
        .module("expenseManager")
        .controller("ExpenseListCtrl",
                     ["expenseResource",
                         ExpenseListCtrl]);

    function ExpenseListCtrl(expenseResource) {
        var vm = this;
        
        expenseResource.query(function (data) {
            vm.expenseReports = data;
        });
    }
}());