(function () {
    'use strict';

    angular.module('school', ['common.core', 'common.ui'])
        .config(config)
        .run(run);

    config.$inject = ['$routeProvider'];
    function config($routeProvider) {
        $routeProvider


            .when("/", {
                templateUrl: "scripts/spa/home/index.html",
                controller: "indexCtrl"
            })
            .when("/login", {
                templateUrl: "scripts/spa/account/login.html",
                controller: "loginCtrl"
            })
            .when("/register", {
                templateUrl: "scripts/spa/account/register.html",
                controller: "registerCtrl"
            })
            .when("/users", {
                templateUrl: "scripts/spa/users/users.html",
                controller: "usersCtrl"
            })
            .when("/users/register", {
                templateUrl: "scripts/spa/users/register.html",
                controller: "usersRegCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/users/:id", {
                templateUrl: "scripts/spa/users/details.html",
                controller: "usersDetailsCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/materials", {
                templateUrl: "scripts/spa/materials/materials.html",
                controller: "materialsCtrl"
            })
            .when("/materials/add", {
                templateUrl: "scripts/spa/materials/add.html",
                controller: "materialAddCtrl",
                //resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/materials/:id", {
                templateUrl: "scripts/spa/materials/details.html",
                controller: "materialDetailsCtrl",
                resolve: { isAuthenticated: isAuthenticated }
            })
            .when("/materials/edit/:id", {
                templateUrl: "scripts/spa/materials/edit.html",
                controller: "materialEditCtrl"
            });
    }

    run.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];

    function run($rootScope, $location, $cookieStore, $http) {
        // handle page refreshes
        $rootScope.repository = $cookieStore.get('repository') || {};
        if ($rootScope.repository.loggedUser) {
            $http.defaults.headers.common['Authorization'] = $rootScope.repository.loggedUser.authdata;
        }

        $(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });

            $('.fancybox-media').fancybox({
                openEffect: 'none',
                closeEffect: 'none',
                helpers: {
                    media: {}
                }
            });

            $('[data-toggle=offcanvas]').click(function () {
                $('.row-offcanvas').toggleClass('active');
            });
        });
    }

    isAuthenticated.$inject = ['membershipService', '$rootScope', '$location'];

    function isAuthenticated(membershipService, $rootScope, $location) {
        if (!membershipService.isUserLoggedIn()) {
            $rootScope.previousState = $location.path();
            $location.path('/login');
        }
    }

})();