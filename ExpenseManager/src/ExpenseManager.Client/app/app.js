(function () {
    "use strict";

    var app = angular.module("expenseManager",
                            ["common.services",
                            "ui.router",
                            "ui.bootstrap"]);

    app.config(["$stateProvider",
                "$urlRouterProvider",
        function ($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise("/");

            $stateProvider
                .state("home", {
                    url: "/",
                    templateUrl: "app/welcomeView.html"
                })
                // Expense Reports
                .state("expenseReportsList", {
                    url: "/expensereport",
                    templateUrl: "app/expenseReports/expenseListView.html",
                    controller: "ExpenseListCtrl as vm"
                })
                .state("expenseReportEdit", {
                    url: "/expensereport/edit/:reportId",
                    templateUrl: "app/expenseReports/expenseEditView.html",
                    controller: "ExpenseEditCtrl as vm",
                    resolve: {
                        expenseResource: "expenseResource",
                        report: function (expenseResource, $stateParams) {
                            var reportId = $stateParams.reportId;
                            return expenseResource.get({ reportId: reportId }).$promise;
                        }
                    }
                })
                .state("expenseReportAdd", {
                    url: "/expensereport/add",
                    templateUrl: "app/expenseReports/expenseAddView.html",
                    controller: "ExpenseAddCtrl as vm",
                })
                .state("transactionList", {
                    url: "/expensereport/:reportId/transaction",
                    templateUrl: "app/expenseReports/transactionListView.html",
                    controller: "TransactionListCtrl as vm",
                    reload:true,
                })
                .state("transactionEdit", {
                    url: "/expensereport/:reportId/transactionedit/:transactionId",
                    templateUrl: "app/expenseReports/transactionEditView.html",
                    controller: "TransactionEditCtrl as vm",
                    resolve: {
                        transactionResource: "transactionResource",
                        transaction: function (transactionResource, $stateParams) {
                            var reportId = $stateParams.reportId;
                            var transactionId = $stateParams.transactionId;
                            return transactionResource.get({ reportId:reportId, transactionId:transactionId}).$promise;
                        }
                    }
                })
                .state("transactionDelete", {
                    url: "/expensereport/:reportId/transactiondelete/:transactionId",
                    controller: "TransactionDeleteCtrl as vm",
                })
                .state("transactionAdd", {
                    url: "/expensereport/:reportId/transactionadd/",
                    templateUrl: "app/expenseReports/transactionEditView.html",
                    controller: "TransactionAddCtrl as vm",
                })

        }]);

}());