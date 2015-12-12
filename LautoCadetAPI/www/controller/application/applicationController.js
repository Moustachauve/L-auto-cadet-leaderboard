﻿angular.module('LautoCadet', ['ngRoute'])


.config(function ($routeProvider, $locationProvider) {
    $routeProvider

     .when('/', {
         title: "Classement",
         isHomePage: true,
         templateUrl: '/controller/leaderboard/topTenSellers.html',
         controller: 'leaderboardController',
     })

    // == Config ==

    // = Cadets =
    .when('/configuration/', {
        title: "Liste des cadets",
        templateUrl: '/controller/configuration/cadetList.html',
        controller: 'configurationController'
    })

    .when('/configuration/cadet/add', {
        title: "Ajouter un cadet",
        templateUrl: '/controller/configuration/cadetAdd.html',
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
    $rootScope.showBackButton = false;

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

    //$scope.pages = [
    //    { name: "index", title: "Classement", url: "/controller/leaderboard/Index.html", isHomePage: true },
    //    { name: "configuration", title: "Configuration", url: "/controller/configuration/index.html" },
    //    { name: "cadetadd", title: "Ajouter un cadet", url: "/controller/configuration/cadetAdd.html" },
    //    { name: "sectionlist", title: "Liste des sections", url: "/controller/configuration/sectionList.html" },
    //    { name: "sectionadd", title: "Ajouter une section", url: "/controller/configuration/sectionAdd.html" },
    //];

    //$scope.currentPage = $scope.pages[0];

    //$rootScope.navigate = function (pageName) {
    //	var page;
    //	pageName = pageName.toLowerCase();

    //	if (!pageName) {
    //		page = $scope.pages[0];
    //	}
    //	else {
    //		for (var i = 0; i < $scope.pages.length; i++) {
    //			if ($scope.pages[i].name == pageName) {
    //				page = $scope.pages[i];
    //				break;
    //			}
    //		}
    //	}
    //	if (page === undefined) {
    //		console.warn('Page "' + pageName + '" not found.');
    //		return;
    //	}

    //	if ($scope.currentPage.name != page.name) {
    //		$scope.currentPage = page;
    //		location.hash = "/" + page.name;
    //		$rootScope.hideError();
    //	}
    //};

    //$scope.$watch(function () {
    //    return location.hash
    //}, function (value) {
    //    $rootScope.navigate(location.hash.substr(2, location.hash.length));
    //});

}

