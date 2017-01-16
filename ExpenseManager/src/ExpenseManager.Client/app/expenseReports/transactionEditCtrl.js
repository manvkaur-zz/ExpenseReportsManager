(function () {
    "use strict";
    angular
        .module("expenseManager")
        .controller("TransactionEditCtrl",
                     ["transaction", "$state", "transactionResource",
                         TransactionEditCtrl]);

    function TransactionEditCtrl(transaction, $state, transactionResource) {
        var vm = this;
        vm.messgae = '';
        vm.transaction = transaction;
        vm.originalTransaction = angular.copy(transaction);

        vm.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.opened = !vm.opened;
        };

        vm.submit = function () {
            vm.message = '';
            if (vm.transaction.id) {
                vm.transaction.$update({ reportId:transaction.expenseReportId, transactionId: vm.transaction.id },
                    function (data) {
                        vm.message = "... Save Complete";
                    })
            }
            else {
                vm.transaction.$save(
                    function (data) {
                        vm.originalTransaction = angular.copy(data);
                        vm.message = "...Save Complete";
                    })
            }
        };

        vm.cancel = function (editForm) {
            editForm.$setPristine();
            vm.transaction = angular.copy(vm.originalTransaction);
            vm.message = '';
        }

    }
}());
