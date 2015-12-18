angular.module('LautoCadet', ['ngRoute'])


.config(function ($routeProvider, $locationProvider) {
    $routeProvider

     .when('/', {
         title: "Classement",
         isHomePage: true,
         templateUrl: '/controller/leaderboard/topTenSellers.html',
         controller: 'leaderboardController',
     })

    // == Config ==

    .when('/configuration/', {
        redirectTo: '/configuration/cadet/list'
    })

    // = Cadets =

    .when('/configuration/cadet/list', {
        title: "Liste des cadets",
        templateUrl: '/controller/configuration/cadetList.html',
        controller: 'configurationController'
    })

    .when('/configuration/cadet/add', {
        title: "Ajouter un cadet",
        templateUrl: '/controller/configuration/cadetAdd.html',
        controller: 'configurationController'
    })

    .when('/configuration/cadet/edit/:id', {
        title: "Modifier un cadet",
        templateUrl: '/controller/configuration/cadetEdit.html',
        controller: 'configurationController'
    })

    // = Sections =

    .when('/configuration/section/list', {
        title: "Liste des sections",
        templateUrl: "/controller/configuration/sectionList.html",
        controller: 'configurationController'
    })

    .when('/configuration/section/add', {
        title: "Ajouter une section",
        templateUrl: "/controller/configuration/sectionAdd.html",
        controller: 'configurationController'
    })

    .when('/configuration/section/edit/:id', {
        title: "Modifier une section",
        templateUrl: "/controller/configuration/sectionEdit.html",
        controller: 'configurationController'
    })

    .otherwise({
        title: "Page introuvable",
        templateUrl: 'controller/error/404.html',
        controller: 'Error404Controller'
    });

    $locationProvider.html5Mode(true);
})

.run(['$rootScope', '$route', function ($rootScope, $route) {
    $rootScope.$on('$routeChangeSuccess', function () {
        $rootScope.pageTitle = $route.current.title;
        $rootScope.isHomePage = $route.current.isHomePage;
    });
}])

.controller('applicationController', ['$scope', '$rootScope', '$route', applicationController]);

function applicationController($scope, $rootScope, $route) {

    $scope.showLoading = false;
    $scope.errorMessage = null;
    $scope.isFullScreen = false;
    $rootScope.showBackButton = false;

    $scope.enterFullScreen = function () {
        $scope.isFullScreen = true;
        if (typeof clientUtils != "undefined") {
            clientUtils.enterFullScreen();
        }
        else {
            console.log("Going Fullscreen");
        }
    }

    $scope.leaveFullScreen = function () {
        $scope.isFullScreen = false;
        if (typeof clientUtils != "undefined") {
            clientUtils.leaveFullScreen();
        }
        else {
            console.log("Leaving Fullscreen");
        }
    }

    $scope.showDevTools = function () {
        if (typeof clientUtils != "undefined") {
            clientUtils.showDevTools();
        }
        else {
            console.log("Opening DevTools");
        }
    }

    $rootScope.startLoading = function () {
        $scope.showLoading = true;
    }

    $rootScope.stopLoading = function () {
        $scope.showLoading = false;
    }

    $rootScope.navigateBack = function () {
        window.history.back();
    }

    $rootScope.showError = function (text) {
        if (text)
            $scope.errorMessage = text;
        else
            $scope.errorMessage = "Une erreur de communication est survenue.";
    }

    $rootScope.hideError = function () {
        $scope.errorMessage = null;
    }

    $rootScope.refreshPage = function () {
        $rootScope.hideError();
        $route.reload();
    }
}

