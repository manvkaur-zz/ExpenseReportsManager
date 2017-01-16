(function () {
    "use strict";
    angular
        .module("expenseManager")
        .controller("TransactionDeleteCtrl",
                     ["transactionResource","$state", "$stateParams",
                         TransactionDeleteCtrl]);

    function TransactionDeleteCtrl(transactionResource,$state, $stateParams) {
        var vm = this;
        transactionResource.delete({
            reportId: $stateParams.reportId,
            transactionId: $stateParams.transactionId
        })
        $state.go("transactionList", { reportId: $stateParams.reportId });
        window.location.reload();
    }
}());