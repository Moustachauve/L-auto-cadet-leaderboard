var app = angular.module('LautoCadet', ['ngRoute', 'ngAnimate', 'ui.bootstrap.showErrors', 'ui.sortable'])



.run(['$rootScope', '$route', function ($rootScope, $route) {
    $rootScope.$on('$routeChangeSuccess', function () {
        $rootScope.pageTitle = $route.current.title;
        $rootScope.isHomePage = $route.current.isHomePage;
    });
}])

.controller('applicationController', ['$scope', '$rootScope', '$route', '$window', applicationController]);

function applicationController($scope, $rootScope, $route, $window) {

    $scope.showLoading = false;
    $scope.errorMessage = null;
    $scope.isFullScreen = false;
    $rootScope.showBackButton = false;

    $scope.enterFullScreen = function () {
        $scope.isFullScreen = true;
        if($rootScope.isBrowser("Going Fullscreen"))
            return;

        clientUtils.enterFullScreen();
    }

    $scope.leaveFullScreen = function () {
        $scope.isFullScreen = false;

        if ($rootScope.isBrowser("Leaving Fullscreen"))
            return;

        clientUtils.leaveFullScreen();
    }

    $scope.showDevTools = function () {
        if ($rootScope.isBrowser("Opening DevTools"))
            return;

        clientUtils.showDevTools();
    }

    $rootScope.startLoading = function () {
        $scope.showLoading = true;
    }

    $rootScope.stopLoading = function () {
        $scope.showLoading = false;
    }

    $rootScope.navigateBack = function () {
        $window.history.back();
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

    $rootScope.isBrowser = function (message) {
        if (typeof clientUtils == "undefined") {
            console.log(message);
            return true;
        }

        return false;
    }
}

app.directive('ngEmptyValue', ['$parse', function ($parse) {
    return {
        require: ['select', 'ngModel'],
        link: function (scope, element, attrs, controllers) {
            var select = controllers[0];
            var readValue = select.readValue;
            var emptyValue = $parse(attrs.ngEmptyValue)(scope);
            select.readValue = function () {
                if (element.val() == '') return emptyValue;
                return readValue();
            };
        }
    };
}]);

// Prevents context menu except in inputs, but will work in regular browser
if (typeof clientUtils != "undefined") {
    document.addEventListener('contextmenu', function (event) {
        if (event.target.nodeName !== 'INPUT' && event.target.type !== 'text' && event.target.nodeName !== 'TEXTAREA') {
            event.preventDefault();
        }
    });
}