(function () {
    "use strict";
    angular
        .module("expenseManager")
        .controller("ExpenseEditCtrl",
                     ["report","$state","expenseResource",
                         ExpenseEditCtrl]);

    function ExpenseEditCtrl(report, $state, expenseResource) {
        var vm = this;
        vm.messgae = '';
        vm.report = report;
        vm.originalReport = angular.copy(report);

        vm.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.opened = !vm.opened;
        };

        vm.submit = function () {
            vm.message = '';
            if (vm.report.id) {
                vm.report.$update({ reportId: vm.report.id },
                    function (data) {
                        vm.message = "... Save Complete";
                    })
            }
            else {
                vm.report.$save(
                    function (data) {
                        vm.originalReport = angular.copy(data);
                        vm.message = "...Save Complete";
                    })
            }
        };

        vm.cancel = function (editForm) {
            editForm.$setPristine();
            vm.report = angular.copy(vm.originalReport);
            vm.message = '';
        }

    }
}());
