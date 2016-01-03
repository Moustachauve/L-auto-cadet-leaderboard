angular
    .module('LautoCadet')
    .controller('gradeController', ['$scope', '$rootScope', '$location', '$route', '$routeParams', 'notification', gradeController]);

function gradeController($scope, $rootScope, $location, $route, $routeParams, notification) {

    $scope.getAllGrades = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Grade/GetAll",
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.grades = data;
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.gradeAdd = function () {
        $scope.$broadcast('show-errors-check-validity');
        if (!$scope.gradeAddForm.$valid)
            return;

        $rootScope.startLoading();
        $.ajax({
            method: "POST",
            url: "http://localhost:8080/api/Grade/Add",
            data: $scope.grade
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $rootScope.navigateBack();
            notification.showSuccess('Le grade "' + data.Nom + '" a bien été ajouté');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.gradeEditInit = function () {
        $rootScope.startLoading();
        $scope.grade = null;

        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Grade/Edit/" + $routeParams.id,
        })
        .done(function (data) {
            $scope.grade = data;
            $rootScope.stopLoading();
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.gradeEdit = function () {
        $scope.$broadcast('show-errors-check-validity');
        if (!$scope.gradeEditForm.$valid)
            return;

        $rootScope.startLoading();
        $.ajax({
            method: "PUT",
            url: "http://localhost:8080/api/Grade/Edit",
            data: $scope.grade
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $rootScope.navigateBack();
            notification.showSuccess('Le grade "' + data.Nom + '" a bien été modifiée');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.gradeDelete = function (grade) {
        if (confirm('Voulez-vous vraiment retirer le grade "' + grade.Nom + '"?')) {
            $.ajax({
                type: "DELETE",
                url: "http://localhost:8080/api/Grade/Delete/" + grade.GradeID,
            })
			.done(function (data) {
			    $scope.getAllGrades();
			    notification.showSuccess('Le grade "' + grade.Nom + '" a bien été retiré');
			    $scope.$apply();
			}).fail(function () {
			    $rootScope.showError();
			    $rootScope.stopLoading();
			    $scope.$apply();
			});
        }
    }

}