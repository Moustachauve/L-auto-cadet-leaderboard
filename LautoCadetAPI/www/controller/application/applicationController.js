angular.module('LautoCadet', [])

angular
    .module('LautoCadet')
    .controller('applicationController', ['$scope', '$rootScope', applicationController]);

function applicationController($scope, $rootScope) {

    $scope.showLoading = false;
    $rootScope.showBackButton = false;

    $scope.pages = [
        { name: "index", title: "Classement", url: "/controller/leaderboard/Index.html" },
        { name: "configuration", title: "Configuration", url: "/controller/configuration/index.html", backPage: "index" },
        { name: "cadetadd", title: "Ajouter un cadet", url: "/controller/configuration/cadetAdd.html", backPage: "configuration" },
        { name: "sectionlist", title: "Liste des sections", url: "/controller/configuration/sectionList.html", backPage: "configuration" },
        { name: "sectionadd", title: "Ajouter une section", url: "/controller/configuration/sectionAdd.html", backPage: "sectionlist" },
    ];

    $scope.currentPage = $scope.pages[0];

    $rootScope.startLoading = function () {
        $scope.showLoading = true;
    }

    $rootScope.stopLoading = function () {
        $scope.showLoading = false;
    }

    $rootScope.navigate = function (pageName) {
        var page;
        pageName = pageName.toLowerCase();

        if (!pageName) {
            page = $scope.pages[0];
        }
        else {
            for (var i = 0; i < $scope.pages.length; i++) {
                if ($scope.pages[i].name == pageName) {
                    page = $scope.pages[i];
                    break;
                }
            }
        }
        if (page === undefined) {
            console.warn('Page "' + pageName + '" not found.');
            return;
        }

        if ($scope.currentPage.name != page.name) {
            $scope.currentPage = page;
            location.hash = "/" + page.name;
        }
    };

    $rootScope.navigateBack = function () {
        window.history.back();
    }

    $scope.$watch(function () {
        return location.hash
    }, function (value) {
        $rootScope.navigate(location.hash.substr(2, location.hash.length));
    });
}

