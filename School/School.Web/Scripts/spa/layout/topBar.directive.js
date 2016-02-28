(function (app) {
    'use strict';

    app.directive('topBar', topBar);

    function topBar() {
        return {
            restrict: 'E',
            replace: false,
            templateUrl: '/scripts/spa/layout/topBar.html'
        }
    }

})(angular.module('common.ui'));