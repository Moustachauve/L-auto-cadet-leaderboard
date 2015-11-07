angular
    .module('LautoCadet')
    .controller('leaderboardController', ['$scope', '$rootScope', leaderboardController]);

function leaderboardController($scope, $rootScope) {

    $scope.topTenSeller = [];

    $scope.getTopTenSeller = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Leaderboard/GetTopTenSeller",
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.topTenSeller = data;
            $scope.$apply();
        }).fail(function () {
            $scope.serverError = true;
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.getTopTenSeller();
}