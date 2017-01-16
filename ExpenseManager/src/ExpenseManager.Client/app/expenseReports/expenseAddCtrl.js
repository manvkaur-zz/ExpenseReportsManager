(function () {
    "use strict";
    angular
        .module("expenseManager")
        .controller("ExpenseAddCtrl",
                     ["expenseResource",
                         ExpenseAddCtrl]);

    function ExpenseAddCtrl(expenseResource) {
        var vm = this;
        vm.message = '';
        vm.report = {};
        vm.submit = function () {
            expenseResource.save(function (data) {
                vm.message = "...Save Complete";
            });
        };
    }
}());