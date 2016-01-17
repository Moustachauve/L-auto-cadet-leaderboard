angular
    .module('LautoCadet')
    .controller('sectionController', ['$scope', '$rootScope', '$location', '$route', '$routeParams', 'notification', sectionController]);

function sectionController($scope, $rootScope, $location, $route, $routeParams, notification) {

    $scope.getAllSections = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Section/GetAll",
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.sections = data;
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.sectionGetDetails = function () {
        $rootScope.startLoading();
        $scope.sectionDetails = null;

        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Section/Get/" + $routeParams.id,
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.sectionDetails = data;
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.addSection = function (section) {
        $scope.$broadcast('show-errors-check-validity');
        if (!$scope.sectionAddForm.$valid)
            return;

        $rootScope.startLoading();
        $.ajax({
            method: "POST",
            url: "http://localhost:8080/api/Section",
            data: $scope.section
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $rootScope.navigateBack();
            notification.showSuccess('La section "' + data.Nom + '" a bien été ajouté');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.sectionEditInit = function () {
        $rootScope.startLoading();
        $scope.sectionDetails = null;

        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Section/Edit/" + $routeParams.id,
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.sectionDetails = data;
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.sectionEdit = function () {
        $scope.$broadcast('show-errors-check-validity');
        if (!$scope.sectionEditForm.$valid)
            return;

        $rootScope.startLoading();
        $.ajax({
            method: "PUT",
            url: "http://localhost:8080/api/Section/Edit",
            data: $scope.sectionDetails
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $rootScope.navigateBack();
            notification.showSuccess('La section "' + data.Nom + '" a bien été modifiée');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.sectionDelete = function (section) {
        if (confirm('Voulez-vous vraiment retirer la section "' + section.Nom + '"?')) {
            $.ajax({
                type: "DELETE",
                url: "http://localhost:8080/api/Section/Delete/" + section.SectionID,
            })
			.done(function (data) {
			    $rootScope.stopLoading();
			    notification.showSuccess('La section "' + data.Nom + '" a bien été retirée');
			    $scope.getAllSections();
			}).fail(function () {
			    $rootScope.showError();
			    $rootScope.stopLoading();
			    $scope.$apply();
			});
        }
    }
}