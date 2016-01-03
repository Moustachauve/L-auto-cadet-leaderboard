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
            templateUrl: '/controller/configuration/general.html',
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
