(function (app) {
    'use strict';

    app.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', 'apiService', 'notificationService'];

    function indexCtrl($scope, apiService, notificationService) {
        $scope.pageClass = 'page-home';
        $scope.loadingMaterials = true;
        $scope.loadingCategories = true;
        $scope.isReadOnly = true;

        $scope.latestMaterials = [];
        $scope.loadData = loadData;

        function loadData() {
            apiService.get('/api/materials/latest', null,
                        materialsLoadCompleted,
                        materialsLoadFailed);

            apiService.get("/api/categories", null,
                categoriesLoadCompleted,
                categoriesLoadFailed);
        }

        function materialsLoadCompleted(result) {
            $scope.latestMaterials = result.data;
            $scope.loadingMaterials = false;
        }

        function categoriesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function materialsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function categoriesLoadCompleted(result) {
            var categories = result.data;
            Morris.Bar({
                element: "genres-bar",
                data: categories,
                xkey: "Name",
                ykeys: ["NumberOfMaterials"],
                labels: ["Number Of Materials"],
                barRatio: 0.4,
                xLabelAngle: 55,
                hideHover: "auto",
                resize: 'true'
            });

            $scope.loadingCategories = false;
        }

        loadData();
    }

})(angular.module('school'));