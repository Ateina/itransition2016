(function (app) {
    'use strict';

    app.controller('materialAddCtrl', materialAddCtrl);

    materialAddCtrl.$inject = ['$scope', '$location', '$routeParams', 'apiService', 'notificationService', 'fileUploadService'];

    function materialAddCtrl($scope, $location, $routeParams, apiService, notificationService, fileUploadService) {

        $scope.pageClass = 'page-movies';
        $scope.material = { CategoryId: 1, Rating: 1, NumberOfTags: 1 };

        $scope.categories = [];
        $scope.isReadOnly = false;
        $scope.AddMaterial = AddMaterial;
        $scope.prepareFiles = prepareFiles;
        $scope.openDatePicker = openDatePicker;
        $scope.changeNumberOfTags = changeNumberOfTags;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        var materialImage = null;

        function loadCategories() {
            apiService.get('/api/categories/', null,
            categoriesLoadCompleted,
            categoriesLoadFailed);
        }

        function categoriesLoadCompleted(response) {
            $scope.categories = response.data;
        }

        function categoriesLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function AddMaterial() {
            AddMaterialModel();
        }

        function AddMaterialModel() {
            apiService.post('/api/materials/add', $scope.material,
            addMaterialSucceded,
            addMaterialFailed);
        }

        function prepareFiles($files) {
            materialImage = $files;
        }

        function addMaterialSucceded(response) {
            notificationService.displaySuccess($scope.material.Title + ' has been submitted to School');
            $scope.material = response.data;

            if (materialImage) {
                fileUploadService.uploadImage(materialImage, $scope.material.ID, redirectToEdit);
            }
            else
                redirectToEdit();
        }

        function addMaterialFailed(response) {
            console.log(response);
            notificationService.displayError(response.statusText);
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.datepicker.opened = true;
        };

        function redirectToEdit() {
            $location.url('materials/edit/' + $scope.material.ID);
        }

        function changeNumberOfTags($vent)
        {
            var btn = $('#btnSetStocks'),
            oldValue = $('#inputStocks').val().trim(),
            newVal = 0;

            if (btn.attr('data-dir') == 'up') {
                newVal = parseInt(oldValue) + 1;
            } else {
                if (oldValue > 1) {
                    newVal = parseInt(oldValue) - 1;
                } else {
                    newVal = 1;
                }
            }
            $('#inputStocks').val(newVal);
            $scope.material.NumberOfTags = newVal;
            console.log($scope.material);
        }

        loadCategories();
    }

})(angular.module('school'));