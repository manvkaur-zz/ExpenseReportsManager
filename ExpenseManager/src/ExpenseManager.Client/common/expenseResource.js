(function () {
    "use strict";
    var app = angular.module("expenseManager")
                     .factory("expenseResource",
                             ["$resource",
                             "appSettings",
                             expenseResource]);
    function expenseResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/expensereport/:reportId", { reportId: '@reportId' }, {
            'query': { method: 'GET', isArray: true },
            'update': {
                method: 'PUT'
            },            
            'save': {
                method: 'POST'
            },
        })
    }
    app.factory("transactionResource",
                ["$resource",
                "appSettings",
                transactionResource]);
    function transactionResource($resource, appSettings) {
        return $resource(appSettings.serverPath + "/api/expensereport/:reportId/transaction/:transactionId",
            { reportId: '@reportId', transactionId: '@transactionId' },
            {
                'query': { method: 'GET', isArray: true },
                'update': {
                    method: 'PUT'
                },
                'save': {
                    method: 'POST'
                },
            })
    }
}());