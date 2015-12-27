angular
    .module('LautoCadet')
    .controller('configurationController', ['$scope', '$rootScope', '$location', '$route', '$routeParams', 'notification', configurationController]);

function configurationController($scope, $rootScope, $location, $route, $routeParams, notification) {

    // ===================================================================================
    // Save
    // ===================================================================================

    $scope.getSaveDetails = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Save/Details",
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.saveDetails = data;
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
            $location.path('configuration/');
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

    $scope.saveSaveDetails = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "POST",
            url: "http://localhost:8080/api/Save/Save",
            data: "="+$scope.saveDetails.Nom
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

    // ===================================================================================
    // Cadets
    // ===================================================================================

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

    $scope.cadetGetDetails = function () {
        $rootScope.startLoading();
        $scope.cadetDetails = null;

        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Cadet/Get/" + $routeParams.id,
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.cadetDetails = data;
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.addCadet = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "POST",
            url: "http://localhost:8080/api/Cadet/Add",
            data: $scope.cadet
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $location.path('configuration/cadet/list');
            notification.showSuccess('Le cadet "' + data.DisplayName + '" a bien été ajouté');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.cadetEdit = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "PUT",
            url: "http://localhost:8080/api/Cadet/Edit",
            data: $scope.cadetDetails
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $location.path('configuration/cadet/list');
            notification.showSuccess('Le cadet "' + data.DisplayName + '" a bien été modifié');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.cadetDelete = function (cadet) {
        if (confirm('Voulez-vous vraiment retirer le cadet "' + cadet.DisplayName + '"?')) {
            $.ajax({
                type: "DELETE",
                url: "http://localhost:8080/api/Cadet/Delete/" + cadet.CadetID,
            })
			.done(function (data) {
			    $rootScope.stopLoading();
			    $scope.getAllCadets();
			    notification.showSuccess('Le cadet "' + cadet.DisplayName + '" a bien été retiré');
			    $scope.$apply();
			}).fail(function () {
			    $rootScope.showError();
			    $rootScope.stopLoading();
			    $scope.$apply();
			});
        }
    }

    // ===================================================================================
    // Sections
    // ===================================================================================

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

        $rootScope.startLoading();
        $.ajax({
            method: "POST",
            url: "http://localhost:8080/api/Section",
            data: $scope.section
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $location.path('configuration/section/list');
            notification.showSuccess('La section "' + data.Nom + '" a bien été ajouté');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.sectionEdit = function () {
        $rootScope.startLoading();
        $.ajax({
            method: "PUT",
            url: "http://localhost:8080/api/Section/Edit",
            data: $scope.sectionDetails
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $location.path('configuration/section/list');
            notification.showSuccess('La section "' + data.Nom + '" a bien été modifiée');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.sectionDelete = function (section) {
        if (confirm('Voulez-vous vraiment retirer la section "' + section.Nom + '"? \nTous les cadets qui font partie de cette section seront du même coup retiré!')) {
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