(function () {
    'use strict';

    app.factory('restaurantService', ['$http', '$q', 'ngUrlSettings', function ($http, $q, ngUrlSettings) {

        var restaurantServiceFactory = {};

        var _getRestaurant = function (criteria) {
            return $http({
                method: 'POST',
                url: 'api/restaurants/getrestaurant',
                data: criteria,
                headers: { 'noblock': true }
            }).then(function (response) {
                return response;
            }, function (response) {
                return $q.reject("An error ocurred while processing your last request. Please try again later.");
            });
        };

        var _getRestaurants = function (criteria) {
            return $http({
                method: 'POST',
                url: 'api/restaurants/getrestaurants',
                data: criteria,
                headers: { 'noblock': true }
            }).then(function (response) {
                return response;
            }, function (response) {
                return $q.reject("An error ocurred while processing your last request. Please try again later.");
            });
        };

        var _rateRestaurant = function (criteria) {
            return $http({
                method: 'POST',
                url: 'api/restaurants/raterestaurant',
                data: criteria,
                headers: { 'noblock': true }
            }).then(function (response) {
                return response;
            }, function (response) {
                return $q.reject("An error ocurred while processing your last request. Please try again later.");
            });
        };

        var _saveRestaurant = function (criteria) {
            return $http({
                method: 'POST',
                url: 'api/restaurants/saverestaurant',
                data: criteria,
                headers: { 'noblock': true }
            }).then(function (response) {
                return response.data;
            }, function (response) {
                return $q.reject("An error ocurred while processing your last request. Please try again later.");
            });
        };

        var _getNewRestaurantStub = function () {
            return $http.get('/app/data/newRestaurant.json').then(function (result) {
                return result.data;
            });
        };

        restaurantServiceFactory.getRestaurant = _getRestaurant;
        restaurantServiceFactory.getRestaurants = _getRestaurants;
        restaurantServiceFactory.rateRestaurant = _rateRestaurant;
        restaurantServiceFactory.saveRestaurant = _saveRestaurant;
        restaurantServiceFactory.getNewRestaurantStub = _getNewRestaurantStub;

        return restaurantServiceFactory;

    }]);

})();