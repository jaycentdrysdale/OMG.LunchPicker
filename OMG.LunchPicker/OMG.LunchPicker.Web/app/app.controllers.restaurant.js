(function () {
    'use strict';
    angular
        .module('app')
        .controller('restaurantController', restaurantCtrl);

    restaurantCtrl.$inject = ['_', '$http', '$location', '$state', '$scope', '$filter', 'restaurantAgent', 'cuisineService', '$timeout', '$q', '$log', '$mdDialog'];

    function restaurantCtrl(_, $http, $location, $state, $scope, $filter, restaurantAgent, cuisineService, $timeout, $q, $log, $mdDialog) {

        /* jshint validthis: true */
        var vm = this;
        

        takeRestaurantSnapshot(restaurantAgent.initNewRestaurant());

        vm.onSubmitRestaurant = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            var hasChanges = restaurantAgent.sessionHasPendingChanges() === true;
            var isNewRestaurant = vm.workingRestaurant.id === 0;
            
            if (isNewRestaurant == true)
                restaurantAgent.saveRestaurant();

            if (isNewRestaurant == false && hasChanges == true)
                restaurantAgent.saveRestaurant();

        }

        loadCuisines();

        /* begin helper methods */
        function takeRestaurantSnapshot(restaurant) {
            restaurantAgent.takeSessionSnapshot(restaurant)
            vm.workingRestaurant = restaurantAgent.workingRestaurant;
            vm.masterRestaurant = restaurantAgent.masterRestaurant;
        }

        function loadCuisines()
        {
            vm.selectedCuisines = [];

            var criteria = {
                "skip": 0,
                "take": 100,
                "sortField": null,
                "reverse": false
            }

            cuisineService.getCuisines(criteria)
            .then(function (response) {
                var items = response.data.results;
                vm.cuisines = [];
                _.each(items, function (item) {
                    vm.cuisines.push({ icon: '<i class="fa fa-spoon"></i>', name: item.name, id: item.id, selected: false });
                });
            },
            function (err) {
                //toastr.error(JSON.stringify(err));
            });
        }

        function attachSelectedCuisines()
        {

        }

        /* end helper methods */
    }

})();
