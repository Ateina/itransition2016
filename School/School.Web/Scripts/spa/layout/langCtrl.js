var translations = {
    "en": {
        "hp1": "Swipe to the right to reveal the left menu.",
        "hp2": "(On desktop click and drag from left to right)",
        "ht": "Welcome"
    },
    "de": {
        "hp1": "Streichen Sie nach rechts, um die linke Menü offenbaren.",
        "hp2": "(Auf dem Desktop klicken und ziehen Sie von links nach rechts)",
        "ht": "willkommen"
    }
}

angular.module('ionicApp', ['ionic', 'pascalprecht.translate'])

.config(function ($stateProvider, $urlRouterProvider, $translateProvider) {

    for (lang in translations) {
        $translateProvider.translations(lang, translations[lang]);
    }

    $translateProvider.preferredLanguage('en');

    $stateProvider
      .state('eventmenu', {
          url: "/event",
          abstract: true,
          templateUrl: "event-menu.html"
      })
      .state('eventmenu.home', {
          url: "/home",
          views: {
              'menuContent': {
                  templateUrl: "home.html"
              }
          }
      })
      .state('eventmenu.checkin', {
          url: "/check-in",
          views: {
              'menuContent': {
                  templateUrl: "check-in.html",
                  controller: "CheckinCtrl"
              }
          }
      })
      .state('eventmenu.attendees', {
          url: "/attendees",
          views: {
              'menuContent': {
                  templateUrl: "attendees.html",
                  controller: "AttendeesCtrl"
              }
          }
      })

    $urlRouterProvider.otherwise("/event/home");
})

.controller('MainCtrl', function ($scope, $ionicSideMenuDelegate, $translate) {

    $scope.toggleLeft = function () {
        $ionicSideMenuDelegate.toggleLeft();
    };

    $scope.ChangeLanguage = function (lang) {
        $translate.use(lang);
    }
})

    /*
.controller('CheckinCtrl', function ($scope) {
    $scope.showForm = true;

    $scope.shirtSizes = [
      { text: 'Large', value: 'L' },
      { text: 'Medium', value: 'M' },
      { text: 'Small', value: 'S' }
    ];

    $scope.attendee = {};
    $scope.submit = function () {
        if (!$scope.attendee.firstname) {
            alert('Info required');
            return;
        }
        $scope.showForm = false;
        $scope.attendees.push($scope.attendee);
    };

})

.controller('AttendeesCtrl', function ($scope) {

    $scope.activity = [];
    $scope.arrivedChange = function (attendee) {
        var msg = attendee.firstname + ' ' + attendee.lastname;
        msg += (!attendee.arrived ? ' has arrived, ' : ' just left, ');
        msg += new Date().getMilliseconds();
        $scope.activity.push(msg);
        if ($scope.activity.length > 3) {
            $scope.activity.splice(0, 1);
        }
    };

});
*/