angular.module('LautoCadet', [])

angular
    .module('LautoCadet')
    .controller('applicationController', ['$scope', '$rootScope', applicationController]);

function applicationController($scope, $rootScope) {

	$scope.showLoading = false;
	$scope.errorMessage = null;
	$rootScope.showBackButton = false;

	$scope.pages = [
        { name: "index", title: "Classement", url: "/controller/leaderboard/Index.html", isMainPage: true },
        { name: "configuration", title: "Configuration", url: "/controller/configuration/index.html" },
        { name: "cadetadd", title: "Ajouter un cadet", url: "/controller/configuration/cadetAdd.html" },
        { name: "sectionlist", title: "Liste des sections", url: "/controller/configuration/sectionList.html" },
        { name: "sectionadd", title: "Ajouter une section", url: "/controller/configuration/sectionAdd.html" },
	];

	$scope.currentPage = $scope.pages[0];

	$rootScope.startLoading = function () {
		$scope.showLoading = true;
	}

	$rootScope.stopLoading = function () {
		$scope.showLoading = false;
	}

	$rootScope.navigate = function (pageName) {
		var page;
		pageName = pageName.toLowerCase();

		if (!pageName) {
			page = $scope.pages[0];
		}
		else {
			for (var i = 0; i < $scope.pages.length; i++) {
				if ($scope.pages[i].name == pageName) {
					page = $scope.pages[i];
					break;
				}
			}
		}
		if (page === undefined) {
			console.warn('Page "' + pageName + '" not found.');
			return;
		}

		if ($scope.currentPage.name != page.name) {
			$scope.currentPage = page;
			location.hash = "/" + page.name;
			$rootScope.hideError();
		}
	};

	$rootScope.navigateBack = function () {
		window.history.back();
	}

	$rootScope.showError = function (text) {
		if (text)
			$scope.errorMessage = text;
		else
			$scope.errorMessage = "Une erreur de communication est survenue.";
	}

	$rootScope.hideError = function () {
		$scope.errorMessage = null;
	}

	$rootScope.refreshPage = function () {
		window.location.reload();
		$rootScope.hideError();
	}


	$scope.$watch(function () {
		return location.hash
	}, function (value) {
		$rootScope.navigate(location.hash.substr(2, location.hash.length));
	});

}

