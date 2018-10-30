(function () {
    'use strict';
    angular.module('app')
    .factory('restaurantAgent', ['_', '$http', '$q', '$mdDialog', '$state', '$log', 'toastr', 'restaurantService', 'cuisineService', restaurantAgent]);

    function restaurantAgent(_, $http, $q, $mdDialog, $state, $log, toastr, restaurantService, cuisineService) {

        var exports = {};

        var _takeSessionSnapshot = function (restaurant) {
            exports.workingRestaurant = restaurant;
            exports.masterRestaurant = angular.copy(restaurant);
        }

        var _clearSession = function () {
            toastr.info('Pending changes discarded', 'Discard Changes');
            exports.workingRestaurant = null;
            exports.masterRestaurant = null;
        }

        var _sessionHasPendingChanges = function () {

            if (exports.masterRestaurant && exports.workingRestaurant) {
                var clean = angular.equals(exports.masterRestaurant, exports.workingRestaurant);
                return (clean === false);
            }

            return false;
        }

        var _initNewRestaurant = function () {
            return {
                "id": 0,
                "name": "",
                "street": "",
                "city": "",
                "state": "",
                "zip": "",
                "cuisineIds": []
            }
        }

        var _saveRestaurant = function () {
            var workingRestaurant = exports.workingRestaurant;
            var wasNewRestaurant = workingRestaurant.id === 0;

            var ids = [];
            _.each(workingRestaurant.cuisineIds, function (item) {
                ids.push(item.id);
            });
            workingRestaurant.cuisineIds = ids;

            restaurantService.saveRestaurant(workingRestaurant)
            .then(function (response) {
                if (response.isSuccess === true) {
                    _takeSessionSnapshot(exports.workingRestaurant);
                    toastr.success('Restaurant Saved successfully', 'Save Changes');
                    if (wasNewRestaurant==true)
                        $state.go("dashboard");
                }
                else {
                    _.each(response.messages, function (message) {
                        toastr.error(message);
                    });
                }
            },
            function (err) {
                toastr.error(JSON.stringify(err));
            });
        }

        var getErrors = function (items) {
            var errs = ""
            _.each(items, function (item) {
                errs = errs + item + "<br>";
            });
            errs = errs + "</ul>";
            return errs;
        }

        var _redirectToRestaurantRoute = function () {
            var routeName = 'createrestaurant';

            if ($state.$current.name === routeName)
                $state.reload();
            else
                $state.go(routeName)
        }

        var _confirmAndSaveRestaurant = function ($event, doRedirectAfterSave) {
            var confirm = $mdDialog.confirm()
            .title('Would you like to save your changes ?')
            .textContent('The current Restaurant has unsaved changes...')
            .ariaLabel('Pending changes')
            .targetEvent($event)
            .ok('Save Changes')
            .cancel('Discard Changes');
            $mdDialog.show(confirm).then(function () {
                _saveRestaurant();
                if (doRedirectAfterSave === true) {
                    _redirectToRestaurantRoute();
                }
            }, function () {
                _clearSession();
                _redirectToRestaurantRoute();
            });
        }

        exports.workingRestaurant = null;
        exports.masterRestaurant = null;
        exports.takeSessionSnapshot = _takeSessionSnapshot;
        exports.sessionHasPendingChanges = _sessionHasPendingChanges;
        exports.clearSession = _clearSession;
        exports.saveRestaurant = _saveRestaurant;
        exports.redirectToRestaurantRoute = _redirectToRestaurantRoute;
        exports.confirmAndSaveRestaurant = _confirmAndSaveRestaurant;
        exports.initNewRestaurant = _initNewRestaurant;
        
        return exports;
    }
})();