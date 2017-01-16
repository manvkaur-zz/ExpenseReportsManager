(function () {
    "use strict";
    angular
        .module("expenseManager")
        .controller("TransactionAddCtrl",
                     ["transactionResource",
                         TransactionAddCtrl]);

    function TransactionAddCtrl(transactionResource) {
        var vm = this;
        vm.message = '';
        
        vm.transaction = {};

        vm.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.opened = !vm.opened;
        };

        vm.submit = function () {
            vm.message = '';
            transactionResource.save(
                    function (data) {
                        vm.transaction = angular.copy(data);
                        vm.message = "...Save Complete";
                    })
        };

        vm.cancel = function (editForm) {
            $state.go("expenseReportsList");
        }

    }
}());
