(function (app) {
    'use strict';

    app.controller('materialEditCtrl', materialEditCtrl);

    materialEditCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService', 'fileUploadService'];

    function materialEditCtrl($scope, $location, $routeParams, apiService, notificationService, fileUploadService) {
        $scope.pageClass = 'page-movies';
        $scope.material = {};
        $scope.categories = [];
        $scope.loadingMaterials = true;
        $scope.isReadOnly = false;
        $scope.UpdateMaterial = UpdateMaterial;
        $scope.prepareFiles = prepareFiles;
        $scope.openDatePicker = openDatePicker;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        var materialImage = null;

        function loadMaterial() {

            $scope.loadingMaterials = true;

            apiService.get('/api/materials/details/' + $routeParams.id, null,
            materialLoadCompleted,
            materialLoadFailed);
        }

        function materialLoadCompleted(result) {
            $scope.material = result.data;
            $scope.loadingmaterials = false;

            loadCategories();
        }

        function materialLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function categoriesLoadCompleted(response) {
            $scope.categories = response.data;
        }

        function categoriesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function loadCategories() {
            apiService.get('/api/categories/', null,
            categoriesLoadCompleted,
            categoriesLoadFailed);
        }

        function UpdateMaterial() {
            if (materialImage) {
                fileUploadService.uploadImage(materialImage, $scope.material.ID, UpdateMaterialModel);
            }
            else
                UpdateMaterialModel();
        }

        function UpdateMaterialModel() {
            apiService.post('/api/materials/update', $scope.material,
            updateMaterialSucceded,
            updateMaterialFailed);
        }

        function prepareFiles($files) {
            materialImage = $files;
        }

        function updateMaterialSucceded(response) {
            console.log(response);
            notificationService.displaySuccess($scope.material.Title + ' has been updated');
            $scope.material = response.data;
            materialImage = null;
        }

        function updateMaterialFailed(response) {
            notificationService.displayError(response);
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.datepicker.opened = true;
        };

        loadMaterial();
    }

})(angular.module('school'));