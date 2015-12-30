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