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
            console.log("JE M'ENLEVE !!!!");
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