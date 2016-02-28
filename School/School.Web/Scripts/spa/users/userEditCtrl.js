(function (app) {
    'use strict';

    app.controller('userEditCtrl', userEditCtrl);

    userEditCtrl.$inject = ['$scope', '$modalInstance','$timeout', 'apiService', 'notificationService'];

    function userEditCtrl($scope, $modalInstance, $timeout, apiService, notificationService) {

        $scope.cancelEdit = cancelEdit;
        $scope.updateUser = updateUser;

        $scope.openDatePicker = openDatePicker;
        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.datepicker = {};

        function updateUser()
        {
            console.log($scope.EditedUser);
            apiService.post('/api/users/update/', $scope.EditedUser,
            updateUserCompleted,
            updateUserLoadFailed);
        }

        function updateUserCompleted(response)
        {
            notificationService.displaySuccess($scope.EditedUser.Username + ' has been updated');
            $scope.EditedUser = {};
            $modalInstance.dismiss();
        }

        function updateUserLoadFailed(response)
        {
            notificationService.displayError(response.data);
        }

        function cancelEdit() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

        function openDatePicker($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $timeout(function () {
                $scope.datepicker.opened = true;
            });

            $timeout(function () {
                $('ul[datepicker-popup-wrap]').css('z-index', '10000');
            }, 100);

            
        };

    }

})(angular.module('school'));