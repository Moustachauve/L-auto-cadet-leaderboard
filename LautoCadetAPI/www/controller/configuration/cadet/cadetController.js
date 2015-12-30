angular
    .module('LautoCadet')
    .controller('cadetController', ['$scope', '$rootScope', '$location', '$route', '$routeParams', 'notification', cadetController]);

function cadetController($scope, $rootScope, $location, $route, $routeParams, notification) {

    $scope.getAllCadets = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Cadet/GetAll",
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.cadets = data;
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.addCadet = function () {
        $scope.$broadcast('show-errors-check-validity');
        if (!$scope.cadetAddForm.$valid)
            return;

        $rootScope.startLoading();
        $.ajax({
            method: "POST",
            url: "http://localhost:8080/api/Cadet/Add",
            data: $scope.cadet
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $rootScope.navigateBack();
            notification.showSuccess('Le cadet "' + data.FullName + '" a bien été ajouté');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.cadetEditInit = function () {
        $rootScope.startLoading();
        $scope.cadet = null;

        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Cadet/Get/" + $routeParams.id,
        })
        .done(function (data) {
            $scope.cadet = data;
            $scope.cadetFormInit();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.cadetEdit = function () {
        $scope.$broadcast('show-errors-check-validity');
        if (!$scope.cadetEditForm.$valid)
            return;

        $rootScope.startLoading();
        $.ajax({
            method: "PUT",
            url: "http://localhost:8080/api/Cadet/Edit",
            data: $scope.cadet
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $rootScope.navigateBack();
            notification.showSuccess('Le cadet "' + data.FullName + '" a bien été modifié');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.cadetDelete = function (cadet) {
        if (confirm('Voulez-vous vraiment retirer le cadet "' + cadet.FullName + '"?')) {
            $.ajax({
                type: "DELETE",
                url: "http://localhost:8080/api/Cadet/Delete/" + cadet.CadetID,
            })
			.done(function (data) {
			    $rootScope.stopLoading();
			    $scope.getAllCadets();
			    notification.showSuccess('Le cadet "' + cadet.FullName + '" a bien été retiré');
			    $scope.$apply();
			}).fail(function () {
			    $rootScope.showError();
			    $rootScope.stopLoading();
			    $scope.$apply();
			});
        }
    }

    $scope.cadetFormInit = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Cadet/GetFormInit",
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.cadetFormData = data;
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }
}