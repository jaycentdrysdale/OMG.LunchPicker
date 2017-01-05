(function () {
    'use strict';
    angular
        .module('app')
        .controller('dashboardController', dashboardCtlr);

    dashboardCtlr.$inject = ['$scope', 'dashboardService', '$log'];

    function dashboardCtlr($scope, dashboardService, $log) {

        /* jshint validthis: true */
        var vm = this;

        $scope.getStars = function (stars) {
            var result = [];
            for (var i = 0 ; i < stars ; i++) {
                result.push(stars);
            }
            return result;
        }

        dashboardService.getDashboard()
        .then(function (response) {
            loadDashboard(response.data, vm);
        },
        function (err) {
            console.log(JSON.stringify(err));
        });

    }

    var loadDashboard = function (data, vm) {
        vm.dashboard = {
            restaurants: data.restaurants,
            totalRestaurants: data.restaurants.length,
            averageRatings: data.averageRating,
            totalUsers: data.users.length,
            totalCuisines: data.cuisines.length,

        };
    }

})();