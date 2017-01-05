(function () {
    'use strict';

    app.factory('dashboardService', ['$http', '$q', 'ngUrlSettings', function ($http, $q, ngUrlSettings) {

        var dashboardServiceFactory = {};

        var _getDashboard = function () {
            return $http({
                method: 'GET',
                url: 'api/Dashboard/GetDashboard',
                headers: { 'noblock': true }
            }).then(function (response) {
                return response;
            }, function (response) {
                return $q.reject("An error ocurred while processing your last request. Please try again later.");
            });

        };

        dashboardServiceFactory.getDashboard = _getDashboard;
        return dashboardServiceFactory;

    }]);

})();