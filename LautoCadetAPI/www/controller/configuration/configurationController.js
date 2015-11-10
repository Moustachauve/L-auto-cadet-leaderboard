angular
    .module('LautoCadet')
    .controller('configurationController', ['$scope', '$rootScope', leaderboardController]);

function configurationController($scope, $rootScope) {


    $scope.addCadet = function (cadet) {

        $rootScope.startLoading();
        $.ajax({
            method: "POST",
            url: "http://localhost:8080/api/Cadet/Add",
            data: cadet
        })
        .done(function (data) {
            $rootScope.stopLoading();
            alert("Bravo!");
            $scope.$apply();
        }).fail(function () {
            $scope.serverError = true;
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.getAllCadet = function () {
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

}