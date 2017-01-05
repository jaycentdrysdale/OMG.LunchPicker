(function () {
    'use strict';

    app.factory('cuisineService', ['$http', '$q', 'ngUrlSettings', function ($http, $q, ngUrlSettings) {

        var cuisineServiceFactory = {};

        var _getCuisines = function (criteria) {
            return $http({
                method: 'POST',
                url: 'api/Cuisine/GetCuisines',
                data: criteria,
                headers: { 'noblock': true }
            }).then(function (response) {
                return response;
            }, function (response) {
                return $q.reject("An error ocurred while processing your last request. Please try again later.");
            });
        };

        cuisineServiceFactory.getCuisines = _getCuisines;
        return cuisineServiceFactory;

    }]);

})();