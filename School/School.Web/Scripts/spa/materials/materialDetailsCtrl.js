(function (app) {
    'use strict';

    app.controller('materialDetailsCtrl', materialDetailsCtrl);

    materialDetailsCtrl.$inject = ['$scope', '$location', '$routeParams', '$modal', 'apiService', 'notificationService'];

    function materialDetailsCtrl($scope, $location, $routeParams, $modal, apiService, notificationService) {
        $scope.pageClass = 'page-movies';
        $scope.material = {};
        $scope.loadingMaterials = true;
        //$scope.loadingRentals = true;
        $scope.isReadOnly = true;
        //$scope.openRentDialog = openRentDialog;
        //$scope.returnMaterial = returnMaterial;
        //$scope.rentalHistory = [];
        $scope.getStatusColor = getStatusColor;
        $scope.clearSearch = clearSearch;
        //$scope.isBorrowed = isBorrowed;

        function loadMaterial() {

            $scope.loadingMaterials = true;

            apiService.get('/api/materials/details/' + $routeParams.id, null,
            materialLoadCompleted,
            materialLoadFailed);
        }

        /*
        function loadRentalHistory() {
            $scope.loadingRentals = true;

            apiService.get('/api/rentals/' + $routeParams.id + '/rentalhistory', null,
            rentalHistoryLoadCompleted,
            rentalHistoryLoadFailed);
        }
        */

        function loadMaterialDetails() {
            loadMaterial();
            //loadRentalHistory();
        }

        /*
        function returnMaterial(rentalID) {
            apiService.post('/api/rentals/return/' + rentalID, null,
            returnMaterialSucceeded,
            returnMaterialFailed);
        }
        */

        function isBorrowed(rental)
        {
            return rental.Status == 'Borrowed';
        }

        function getStatusColor(status) {
            if (status == 'Borrowed')
                return 'red'
            else {
                return 'green';
            }
        }

        function clearSearch()
        {
            $scope.filterRentals = '';
        }

        function materialLoadCompleted(result) {
            $scope.material = result.data;
            $scope.loadingMaterials = false;
        }

        function materialLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        /*
        function rentalHistoryLoadCompleted(result) {
            console.log(result);
            $scope.rentalHistory = result.data;
            $scope.loadingRentals = false;
        }

        
        function rentalHistoryLoadFailed(response) {
            notificationService.displayError(response);
        }

        function returnMaterialSucceeded(response) {
            notificationService.displaySuccess('Movie returned to School succeesfully');
            loadMaterialDetails();
        }

        function returnMaterialFailed(response) {
            notificationService.displayError(response.data);
        }*/

        /*
        function openRentDialog() {
            $modal.open({
                templateUrl: 'scripts/spa/rental/rentMovieModal.html',
                controller: 'rentMovieCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                loadMaterialDetails();
            }, function () {
            });
        }
        */
        loadMaterialDetails();
    }
    

})(angular.module('school'));