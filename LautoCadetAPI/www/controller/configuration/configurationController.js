angular
    .module('LautoCadet')
    .controller('configurationController', ['$scope', '$rootScope', configurationController]);

function configurationController($scope, $rootScope) {


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
	    $rootScope.startLoading();
	    $.ajax({
	        method: "POST",
	        url: "http://localhost:8080/api/Cadet/Add",
	        data: $scope.cadet
	    })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.$apply();
            $rootScope.navigate("configuration");
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
        	$rootScope.navigate("sectionList");
        }).fail(function () {
        	$rootScope.showError();
        	$rootScope.stopLoading();
        	$scope.$apply();
        });
	}
}