(function (app) {
    'use strict';

    app.controller('materialsCtrl', materialsCtrl);

    materialsCtrl.$inject = ['$scope', 'apiService', 'notificationService'];

    function materialsCtrl($scope, apiService, notificationService) {
        $scope.pageClass = 'page-movies';
        $scope.loadingMaterials = true;
        $scope.page = 0;
        $scope.pagesCount = 0;
        
        $scope.Materials = [];

        $scope.search = search;
        $scope.clearSearch = clearSearch;

        function search(page) {
            page = page || 0;

            $scope.loadingMaterials = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 6,
                    filter: $scope.filterMaterials
                }
            };

            apiService.get('/api/materials/', config,
            materialsLoadCompleted,
            materialsLoadFailed);
        }

        function materialsLoadCompleted(result) {
            $scope.Materials = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingMaterials = false;

            if ($scope.filterMaterials && $scope.filterMaterials.length)
            {
                notificationService.displayInfo(result.data.Items.length + ' materials found');
            }
            
        }

        function materialsLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function clearSearch() {
            $scope.filterMaterials = '';
            search();
        }

        $scope.search();
    }

})(angular.module('school'));