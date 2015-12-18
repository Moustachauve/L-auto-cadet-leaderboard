angular
    .module('LautoCadet')
    .controller('configurationController', ['$scope', '$rootScope', '$location', '$routeParams', configurationController]);

function configurationController($scope, $rootScope, $location, $routeParams) {

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
            $location.path('configuration/');
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
            $location.path('configuration/');
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
	}

	$scope.cadetDelete = function (cadet) {
		if (confirm('Voulez-vous vraiment retirer le cadet "' + cadet.Prenom + ' ' + cadet.Nom + '"?')) {
			$.ajax({
				type: "DELETE",
				url: "http://localhost:8080/api/Cadet/Delete/" + cadet.CadetID,
			})
			.done(function (data) {
				$rootScope.stopLoading();
				$scope.getAllCadets();
			}).fail(function () {
				$rootScope.showError();
				$rootScope.stopLoading();
				$scope.$apply();
			});
		}
	}

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
        	$scope.$apply();
        	$location.path('configuration/section/list');
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
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
	}

	$scope.sectionDelete = function (section) {
	    if (confirm('Voulez-vous vraiment retirer la section "' + section.Nom + '"? \nTous les cadets qui font partie de cette section seront du même coup retirer!')) {
	        $.ajax({
	            type: "DELETE",
	            url: "http://localhost:8080/api/Section/Delete/" + section.SectionID,
	        })
			.done(function (data) {
			    $rootScope.stopLoading();
			    $scope.getAllSections();
			}).fail(function () {
			    $rootScope.showError();
			    $rootScope.stopLoading();
			    $scope.$apply();
			});
	    }
	}

}