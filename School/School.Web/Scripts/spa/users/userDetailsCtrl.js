(function (app) {
    'use strict';

    app.controller('userDetailsCtrl', userDetailsCtrl);

    userDetailsCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function userDetailsCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-movies';
        $scope.user = {};
        $scope.loadingUsers = true;
        $scope.isReadOnly = true;
        $scope.getStatusColor = getStatusColor;
        $scope.clearSearch = clearSearch;

        function loadUser() {

            $scope.loadingUsers = true;

            apiService.get('/api/users/details/' + $routeParams.id, null,
            userLoadCompleted,
            userLoadFailed);
        }

        function loadUserDetails() {
            loadUser();
        }

        function clearSearch() {
            $scope.filterRentals = '';
        }

        function userLoadCompleted(result) {
            $scope.user = result.data;
            $scope.loadingUsers = false;
        }

        function userLoadFailed(response) {
            notificationService.displayError(response.data);
        }
        loadMaterialDetails();
    }

})(angular.module('school'));