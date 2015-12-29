angular
    .module('LautoCadet')
    .controller('leaderboardController', ['$scope', '$rootScope', '$location', '$interval', leaderboardController]);

function leaderboardController($scope, $rootScope, $location, $interval) {

    var nbSecondBetweenPages = 16;

    $scope.topTenSeller = [];
    $scope.pages = [
        '/controller/leaderboard/topTenSellers.html',
    ];
    $scope.currentPageId = 0;
    $scope.currentPage = $scope.pages[$scope.currentPageId];

    $scope.getTopTenSeller = function () {
        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Leaderboard/GetTopTenSeller",
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.topTenSeller = data;
            console.log(data);
            $scope.$apply();
        }).fail(function () {
        	$rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.changePage = function () {
        $interval(function () {
            $scope.currentPageId++;
            if ($scope.currentPageId == $scope.pages.length)
                $scope.currentPageId = 0;

            $scope.currentPage = $scope.pages[$scope.currentPageId];

        }, nbSecondBetweenPages * 1000);
    }

    $scope.changePage();
}