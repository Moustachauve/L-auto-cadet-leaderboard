angular.module('LautoCadet', ['ngRoute', 'ngAnimate', 'ui.bootstrap.showErrors', 'ui.sortable'])



.run(['$rootScope', '$route', function ($rootScope, $route) {
    $rootScope.$on('$routeChangeSuccess', function () {
        $rootScope.pageTitle = $route.current.title;
        $rootScope.isHomePage = $route.current.isHomePage;
    });
}])

.controller('applicationController', ['$scope', '$rootScope', '$route', '$window', applicationController]);

function applicationController($scope, $rootScope, $route, $window) {

    $scope.showLoading = false;
    $scope.errorMessage = null;
    $scope.isFullScreen = false;
    $rootScope.showBackButton = false;

    $scope.enterFullScreen = function () {
        $scope.isFullScreen = true;
        if($rootScope.isBrowser("Going Fullscreen"))
            return;

        clientUtils.enterFullScreen();
    }

    $scope.leaveFullScreen = function () {
        $scope.isFullScreen = false;

        if ($rootScope.isBrowser("Leaving Fullscreen"))
            return;

        clientUtils.leaveFullScreen();
    }

    $scope.showDevTools = function () {
        if ($rootScope.isBrowser("Opening DevTools"))
            return;

        clientUtils.showDevTools();
    }

    $rootScope.startLoading = function () {
        $scope.showLoading = true;
    }

    $rootScope.stopLoading = function () {
        $scope.showLoading = false;
    }

    $rootScope.navigateBack = function () {
        $window.history.back();
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
        $rootScope.hideError();
        $route.reload();
    }

    $rootScope.isBrowser = function (message) {
        if (typeof clientUtils == "undefined") {
            console.log(message);
            return true;
        }

        return false;
    }
}

// Prevents context menu except in inputs, but will work in regular browser
if (typeof clientUtils != "undefined") {
    document.addEventListener('contextmenu', function (event) {
        if (event.target.nodeName !== 'INPUT' && event.target.type !== 'text' && event.target.nodeName !== 'TEXTAREA') {
            event.preventDefault();
        }
    });
}
angular
    .module('LautoCadet')
    .config(function ($routeProvider, $locationProvider) {

        $routeProvider

         .when('/', {
             title: "Classement",
             isHomePage: true,
             templateUrl: '/controller/leaderboard/mainPage.html',
             controller: 'leaderboardController',
         })

        // == Config ==

        .when('/configuration/', {
            title: "Configuration",
            templateUrl: '/controller/configuration/settings.html',
            controller: 'configurationController'
        })

        .when('/configuration/file/new', {
            title: "Créer un classement",
            templateUrl: '/controller/configuration/fileNew.html',
            controller: 'configurationController'
        })

        // = Cadets =

        .when('/configuration/cadet/', {
            redirectTo: '/configuration/cadet/list'
        })

        .when('/configuration/cadet/list', {
            title: "Liste des cadets",
            templateUrl: '/controller/configuration/cadet/cadetList.html',
            controller: 'cadetController'
        })

        .when('/configuration/cadet/add', {
            title: "Ajouter un cadet",
            templateUrl: '/controller/configuration/cadet/cadetAdd.html',
            controller: 'cadetController'
        })

        .when('/configuration/cadet/edit/:id', {
            title: "Modifier un cadet",
            templateUrl: '/controller/configuration/cadet/cadetEdit.html',
            controller: 'cadetController'
        })

        // = Sections =

        .when('/configuration/section/', {
            redirectTo: '/configuration/section/list'
        })

        .when('/configuration/section/list', {
            title: "Liste des sections",
            templateUrl: "/controller/configuration/section/sectionList.html",
            controller: 'sectionController'
        })

        .when('/configuration/section/details/:id', {
            title: "Détails d'une section",
            templateUrl: "/controller/configuration/section/sectionDetails.html",
            controller: 'sectionController'
        })

        .when('/configuration/section/add', {
            title: "Ajouter une section",
            templateUrl: "/controller/configuration/section/sectionAdd.html",
            controller: 'sectionController'
        })

        .when('/configuration/section/edit/:id', {
            title: "Modifier une section",
            templateUrl: "/controller/configuration/section/sectionEdit.html",
            controller: 'sectionController'
        })

        // = Grades =

        .when('/configuration/grade/', {
            redirectTo: '/configuration/grade/list'
        })

        .when('/configuration/grade/list', {
            title: "Liste des grades",
            templateUrl: "/controller/configuration/grade/gradeList.html",
            controller: 'gradeController'
        })

        .when('/configuration/grade/add', {
            title: "Ajouter un grade",
            templateUrl: "/controller/configuration/grade/gradeAdd.html",
            controller: 'gradeController'
        })

        .when('/configuration/grade/edit/:id', {
            title: "Modifier un grade",
            templateUrl: "/controller/configuration/grade/gradeEdit.html",
            controller: 'gradeController'
        })

        // = Errors =

        .otherwise({
            title: "Page introuvable",
            templateUrl: 'controller/error/404.html',
            controller: 'Error404Controller'
        });

        $locationProvider.html5Mode(true);
    })

angular
    .module('LautoCadet')
    .directive('cgPie', [cgPie])

function cgPie() {

    return {
        restrict: 'E',
        template: '<div class="pie" style="animation-delay: -{{percentage()}}s"><span>{{value}} / {{max}}</span></div>',
        scope: {
            max: '=',
            value: '='
        },
        link: function (scope, element, attrs) {
            scope.percentage = function () {
                return (scope.value / scope.max) * 100 - 0.000000000001;
            }
        },
    };
}
/// <reference path="notification.html" />
angular
    .module('LautoCadet')
    .service('notification', ['$rootScope', '$timeout', notification]);

angular
    .module('LautoCadet')
    .directive('cgNotification', ['notification', cgNotification])


// Service

function notification($rootScope, $timeout) {

    var notifications = [];
    var $this = this;

    this.showError = function (errorMsg) {
        addNotification('danger', errorMsg);
    }

    this.showSuccess = function (errorMsg) {
        addNotification('success', errorMsg);
    }

    this.getNotifications = function () {
        return notifications;
    }
     
    this.remove = function (notification) {
        for (var i = 0; i < notifications.length; i++) {
            if (notifications[i] == notification) {
                $timeout.cancel(notification.timeout);
                notifications.splice(i, 1);
            }
        }
    }

    function addNotification(type, errorMsg) {
        var notif = {
            type: type,
            errorMsg: errorMsg,
        }
        notif.timeout = $timeout(function () {
            $this.remove(notif);
        }, 5000);

        notifications.push(notif);
    }
}


// Directive

function cgNotification(notification) {

    return {
        restrict: 'E',
        templateUrl: '/controller/application/notification.html',
        replace: true,
        controller: function controllerConstructor($scope, notification) {

            $scope.removeNotification = function(notifToRemove) {
                notification.remove(notifToRemove);
            }
        },
        //scope: boolOrObject,

        link: function (scope, element, attrs) {
            scope.cgNotifications = notification.getNotifications();
        },
    };
}
angular
    .module('LautoCadet')
    .directive('lowerThan', [
  function () {

      var link = function ($scope, $element, $attrs, ctrl) {

          var validate = function (viewValue) {
              var comparisonModel = $attrs.lowerThan;

              if (!viewValue || !comparisonModel) {
                  ctrl.$setValidity('lowerThan', true);
              }
              else {
                  ctrl.$setValidity('lowerThan', parseInt(viewValue, 10) <= parseInt(comparisonModel, 10));
              }
              return viewValue;
          };

          ctrl.$parsers.unshift(validate);
          ctrl.$formatters.push(validate);

          $attrs.$observe('lowerThan', function (comparisonModel) {
              return validate(ctrl.$viewValue);
          });

      };

      return {
          require: 'ngModel',
          link: link
      };

  }
]);
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

        if ($scope.cadet.Section.SectionID == null)
            $scope.cadet.Section = null;

        if ($scope.cadet.Grade.GradeID == null)
            $scope.cadet.Grade = null;

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

    $scope.sortableOptions = {
        helper: function (e, tr) {
            var $originals = tr.children();
            var $helper = tr.clone();
            $helper.children().each(function (index) {
                $(this).width($originals.eq(index).outerWidth());
            });
            return $helper;
        },
        placeholder: 'sortable-placeholder',
        axis: 'y',
        stop: function (e, ui) {
            for (var index in $scope.grades) {
                $scope.grades[index].Ordre = index;
            }

            $.ajax({
                type: "PUT",
                url: "http://localhost:8080/api/Grade/EditOrder/",
                data: { '': $scope.grades }
            })
			.done(function (data) {
			    $scope.grades = data;
			    $scope.$apply();
			}).fail(function () {
			    $rootScope.showError();
			    $rootScope.stopLoading();
			    $scope.$apply();
			});
        }
    };
}
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
			    notification.showSuccess('La section "' + section.Nom + '" a bien été retirée');
			    $scope.getAllSections();
			}).fail(function () {
			    $rootScope.showError();
			    $rootScope.stopLoading();
			    $scope.$apply();
			});
        }
    }
}
angular
    .module('LautoCadet')
    .controller('errorController', ['$scope', '$rootScope', errorController]);

function errorController($scope, $rootScope) {
}
angular
    .module('LautoCadet')
    .controller('leaderboardController', ['$scope', '$rootScope', '$location', '$interval', leaderboardController]);

function leaderboardController($scope, $rootScope, $location, $interval) {

    var nbSecondBetweenPages = 6;

    $scope.topTenSeller = [];
    $scope.pages = [
        '/controller/leaderboard/topTenSellers.html',
        '/controller/leaderboard/sectionLeaderboard.html',
    ];
    $scope.currentPageId = 0;
    $scope.currentPage = $scope.pages[$scope.currentPageId];

    $scope.getTopTenSeller = function () {
        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Leaderboard/GetTopTenSeller",
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.topTenSeller = data;
            $scope.$apply();
        }).fail(function () {
        	$rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.getSectionLeaderboard = function () {
        $.ajax({
            method: "GET",
            url: "http://localhost:8080/api/Leaderboard/GetSectionLeaderboard",
        })
        .done(function (data) {
            $rootScope.stopLoading();
            $scope.sectionLeaderboard = data;
            $scope.$apply();
        }).fail(function () {
            $rootScope.showError();
            $rootScope.stopLoading();
            $scope.$apply();
        });
    }

    $scope.changePage = function () {
        $interval(function () {
            $scope.currentPageId++;
            if ($scope.currentPageId == $scope.pages.length)
                $scope.currentPageId = 0;

            $scope.currentPage = $scope.pages[$scope.currentPageId];

        }, nbSecondBetweenPages * 1000);
    }

    $scope.changePage();
}
/*
 jQuery UI Sortable plugin wrapper

 @param [ui-sortable] {object} Options to pass to $.fn.sortable() merged onto ui.config
 */
angular.module('ui.sortable', [])
  .value('uiSortableConfig',{})
  .directive('uiSortable', [
    'uiSortableConfig', '$timeout', '$log',
    function(uiSortableConfig, $timeout, $log) {
      return {
        require: '?ngModel',
        scope: {
          ngModel: '=',
          uiSortable: '='
        },
        link: function(scope, element, attrs, ngModel) {
          var savedNodes;

          function combineCallbacks(first,second){
            if(second && (typeof second === 'function')) {
              return function() {
                first.apply(this, arguments);
                second.apply(this, arguments);
              };
            }
            return first;
          }

          function getSortableWidgetInstance(element) {
            // this is a fix to support jquery-ui prior to v1.11.x
            // otherwise we should be using `element.sortable('instance')`
            var data = element.data('ui-sortable');
            if (data && typeof data === 'object' && data.widgetFullName === 'ui-sortable') {
              return data;
            }
            return null;
          }

          function hasSortingHelper (element, ui) {
            var helperOption = element.sortable('option','helper');
            return helperOption === 'clone' || (typeof helperOption === 'function' && ui.item.sortable.isCustomHelperUsed());
          }

          // thanks jquery-ui
          function isFloating (item) {
            return (/left|right/).test(item.css('float')) || (/inline|table-cell/).test(item.css('display'));
          }

          function getElementScope(elementScopes, element) {
            var result = null;
            for (var i = 0; i < elementScopes.length; i++) {
              var x = elementScopes[i];
              if (x.element[0] === element[0]) {
                result = x.scope;
                break;
              }
            }
            return result;
          }

          function afterStop(e, ui) {
            ui.item.sortable._destroy();
          }

          var opts = {};

          // directive specific options
          var directiveOpts = {
            'ui-floating': undefined
          };

          var callbacks = {
            receive: null,
            remove:null,
            start:null,
            stop:null,
            update:null
          };

          var wrappers = {
            helper: null
          };

          angular.extend(opts, directiveOpts, uiSortableConfig, scope.uiSortable);

          if (!angular.element.fn || !angular.element.fn.jquery) {
            $log.error('ui.sortable: jQuery should be included before AngularJS!');
            return;
          }

          if (ngModel) {

            // When we add or remove elements, we need the sortable to 'refresh'
            // so it can find the new/removed elements.
            scope.$watch('ngModel.length', function() {
              // Timeout to let ng-repeat modify the DOM
              $timeout(function() {
                // ensure that the jquery-ui-sortable widget instance
                // is still bound to the directive's element
                if (!!getSortableWidgetInstance(element)) {
                  element.sortable('refresh');
                }
              }, 0, false);
            });

            callbacks.start = function(e, ui) {
              if (opts['ui-floating'] === 'auto') {
                // since the drag has started, the element will be
                // absolutely positioned, so we check its siblings
                var siblings = ui.item.siblings();
                var sortableWidgetInstance = getSortableWidgetInstance(angular.element(e.target));
                sortableWidgetInstance.floating = isFloating(siblings);
              }

              // Save the starting position of dragged item
              ui.item.sortable = {
                model: ngModel.$modelValue[ui.item.index()],
                index: ui.item.index(),
                source: ui.item.parent(),
                sourceModel: ngModel.$modelValue,
                cancel: function () {
                  ui.item.sortable._isCanceled = true;
                },
                isCanceled: function () {
                  return ui.item.sortable._isCanceled;
                },
                isCustomHelperUsed: function () {
                  return !!ui.item.sortable._isCustomHelperUsed;
                },
                _isCanceled: false,
                _isCustomHelperUsed: ui.item.sortable._isCustomHelperUsed,
                _destroy: function () {
                  angular.forEach(ui.item.sortable, function(value, key) {
                    ui.item.sortable[key] = undefined;
                  });
                }
              };
            };

            callbacks.activate = function(e, ui) {
              // We need to make a copy of the current element's contents so
              // we can restore it after sortable has messed it up.
              // This is inside activate (instead of start) in order to save
              // both lists when dragging between connected lists.
              savedNodes = element.contents();

              // If this list has a placeholder (the connected lists won't),
              // don't inlcude it in saved nodes.
              var placeholder = element.sortable('option','placeholder');

              // placeholder.element will be a function if the placeholder, has
              // been created (placeholder will be an object).  If it hasn't
              // been created, either placeholder will be false if no
              // placeholder class was given or placeholder.element will be
              // undefined if a class was given (placeholder will be a string)
              if (placeholder && placeholder.element && typeof placeholder.element === 'function') {
                var phElement = placeholder.element();
                // workaround for jquery ui 1.9.x,
                // not returning jquery collection
                phElement = angular.element(phElement);

                // exact match with the placeholder's class attribute to handle
                // the case that multiple connected sortables exist and
                // the placehoilder option equals the class of sortable items
                var excludes = element.find('[class="' + phElement.attr('class') + '"]:not([ng-repeat], [data-ng-repeat])');

                savedNodes = savedNodes.not(excludes);
              }

              // save the directive's scope so that it is accessible from ui.item.sortable
              var connectedSortables = ui.item.sortable._connectedSortables || [];

              connectedSortables.push({
                element: element,
                scope: scope
              });

              ui.item.sortable._connectedSortables = connectedSortables;
            };

            callbacks.update = function(e, ui) {
              // Save current drop position but only if this is not a second
              // update that happens when moving between lists because then
              // the value will be overwritten with the old value
              if(!ui.item.sortable.received) {
                ui.item.sortable.dropindex = ui.item.index();
                var droptarget = ui.item.parent();
                ui.item.sortable.droptarget = droptarget;

                var droptargetScope = getElementScope(ui.item.sortable._connectedSortables, droptarget);
                ui.item.sortable.droptargetModel = droptargetScope.ngModel;

                // Cancel the sort (let ng-repeat do the sort for us)
                // Don't cancel if this is the received list because it has
                // already been canceled in the other list, and trying to cancel
                // here will mess up the DOM.
                element.sortable('cancel');
              }

              // Put the nodes back exactly the way they started (this is very
              // important because ng-repeat uses comment elements to delineate
              // the start and stop of repeat sections and sortable doesn't
              // respect their order (even if we cancel, the order of the
              // comments are still messed up).
              if (hasSortingHelper(element, ui) && !ui.item.sortable.received &&
                  element.sortable( 'option', 'appendTo' ) === 'parent') {
                // restore all the savedNodes except .ui-sortable-helper element
                // (which is placed last). That way it will be garbage collected.
                savedNodes = savedNodes.not(savedNodes.last());
              }
              savedNodes.appendTo(element);

              // If this is the target connected list then
              // it's safe to clear the restored nodes since:
              // update is currently running and
              // stop is not called for the target list.
              if(ui.item.sortable.received) {
                savedNodes = null;
              }

              // If received is true (an item was dropped in from another list)
              // then we add the new item to this list otherwise wait until the
              // stop event where we will know if it was a sort or item was
              // moved here from another list
              if(ui.item.sortable.received && !ui.item.sortable.isCanceled()) {
                scope.$apply(function () {
                  ngModel.$modelValue.splice(ui.item.sortable.dropindex, 0,
                                             ui.item.sortable.moved);
                });
              }
            };

            callbacks.stop = function(e, ui) {
              // If the received flag hasn't be set on the item, this is a
              // normal sort, if dropindex is set, the item was moved, so move
              // the items in the list.
              if(!ui.item.sortable.received &&
                 ('dropindex' in ui.item.sortable) &&
                 !ui.item.sortable.isCanceled()) {

                scope.$apply(function () {
                  ngModel.$modelValue.splice(
                    ui.item.sortable.dropindex, 0,
                    ngModel.$modelValue.splice(ui.item.sortable.index, 1)[0]);
                });
              } else {
                // if the item was not moved, then restore the elements
                // so that the ngRepeat's comment are correct.
                if ((!('dropindex' in ui.item.sortable) || ui.item.sortable.isCanceled()) &&
                    !hasSortingHelper(element, ui)) {
                  savedNodes.appendTo(element);
                }
              }

              // It's now safe to clear the savedNodes
              // since stop is the last callback.
              savedNodes = null;
            };

            callbacks.receive = function(e, ui) {
              // An item was dropped here from another list, set a flag on the
              // item.
              ui.item.sortable.received = true;
            };

            callbacks.remove = function(e, ui) {
              // Workaround for a problem observed in nested connected lists.
              // There should be an 'update' event before 'remove' when moving
              // elements. If the event did not fire, cancel sorting.
              if (!('dropindex' in ui.item.sortable)) {
                element.sortable('cancel');
                ui.item.sortable.cancel();
              }

              // Remove the item from this list's model and copy data into item,
              // so the next list can retrive it
              if (!ui.item.sortable.isCanceled()) {
                scope.$apply(function () {
                  ui.item.sortable.moved = ngModel.$modelValue.splice(
                    ui.item.sortable.index, 1)[0];
                });
              }
            };

            wrappers.helper = function (inner) {
              if (inner && typeof inner === 'function') {
                return function (e, item) {
                  var innerResult = inner.apply(this, arguments);
                  item.sortable._isCustomHelperUsed = item !== innerResult;
                  return innerResult;
                };
              }
              return inner;
            };

            scope.$watch('uiSortable', function(newVal /*, oldVal*/) {
              // ensure that the jquery-ui-sortable widget instance
              // is still bound to the directive's element
              var sortableWidgetInstance = getSortableWidgetInstance(element);
              if (!!sortableWidgetInstance) {
                angular.forEach(newVal, function(value, key) {
                  // if it's a custom option of the directive,
                  // handle it approprietly
                  if (key in directiveOpts) {
                    if (key === 'ui-floating' && (value === false || value === true)) {
                      sortableWidgetInstance.floating = value;
                    }

                    opts[key] = value;
                    return;
                  }

                  if (callbacks[key]) {
                    if( key === 'stop' ){
                      // call apply after stop
                      value = combineCallbacks(
                        value, function() { scope.$apply(); });

                      value = combineCallbacks(value, afterStop);
                    }
                    // wrap the callback
                    value = combineCallbacks(callbacks[key], value);
                  } else if (wrappers[key]) {
                    value = wrappers[key](value);
                  }

                  opts[key] = value;
                  element.sortable('option', key, value);
                });
              }
            }, true);

            angular.forEach(callbacks, function(value, key) {
              opts[key] = combineCallbacks(value, opts[key]);
              if( key === 'stop' ){
                opts[key] = combineCallbacks(opts[key], afterStop);
              }
            });

          } else {
            $log.info('ui.sortable: ngModel not provided!', element);
          }

          // Create sortable
          element.sortable(opts);
        }
      };
    }
  ]);