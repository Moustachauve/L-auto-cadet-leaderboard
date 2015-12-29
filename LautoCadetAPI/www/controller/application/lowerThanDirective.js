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