angular
    .module('LautoCadet')
    .controller('configurationController', ['$scope', '$rootScope', '$location', '$route', '$routeParams', 'notification', configurationController]);

function configurationController($scope, $rootScope, $location, $route, $routeParams, notification) {

    // ===================================================================================
    // Save
    // ===================================================================================

    $scope.getSettings = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Save/Details",
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.settings = data.Settings;
            $scope.recentFiles = data.FichiersRecents;
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.fileNew = function () {
        $scope.$broadcast('show-errors-check-validity');

        if (!$scope.fileNewForm.$valid)
            return;

        if ($rootScope.isBrowser("Opening save location selector..."))
            return;

        var chemin = clientUtils.selectNewFile($scope.file.NomSauvegarde);

        if (!chemin)
            return;

        var data = {
            NomSauvegarde: $scope.file.NomSauvegarde,
            CheminFichier: chemin
        }

        $rootScope.startLoading();
        $.ajax({
            method: "POST",
            url: "http://localhost:8080/api/Save/Create",
            data: data
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $rootScope.navigateBack();
            notification.showSuccess('Le classement nommé "' + data.NomSauvegarde + '" a bien été créer');
            console.log(data);
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.openSelectFile = function () {
        if ($rootScope.isBrowser("Opening open file selector..."))
            return;

        $scope.openFile(clientUtils.openFile());
    }

    $scope.openFile = function (chemin) {
        if (!chemin)
            return;

        $rootScope.startLoading();
        $.ajax({
            method: "POST",
            url: "http://localhost:8080/api/Save/Open",
            data: { CheminFichier: chemin }
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $route.reload();
            notification.showSuccess('Le classement nommé "' + data.NomSauvegarde + '" a bien été ouvert');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.saveSettings = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "POST",
            url: "http://localhost:8080/api/Save/Save",
            data: $scope.settings
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $route.reload();
            notification.showSuccess('Le classement a bien été sauvegarder');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });

    }
}