(function () {
    "use strict";
    angular
        .module("expenseManager")
        .controller("TransactionListCtrl",
                     ["transactionResource","$stateParams",
                         TransactionListCtrl]);

    function TransactionListCtrl(transactionResource, $stateParams) {
        var vm = this;
        vm.reportId = $stateParams.reportId;
        vm.transactions = transactionResource.query({ reportId: $stateParams.reportId }); 
    }
}());