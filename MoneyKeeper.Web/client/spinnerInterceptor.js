var angular = require("angular");

angular.module("app").factory("spinnerInterceptor", spinnerInterceptor);

function spinnerInterceptor($rootScope, $q) {
  "ngInject";

  return {
    request: function(config) {
      $rootScope.isLoading = true;
      return $q.resolve(config);
    },
    response: function(response) {
      $rootScope.isLoading = false;
      return $q.resolve(response);
    },
    responseError: function(response) {
      //$mdToast.show(
      //  $mdToast.simple()
      //  .textContent("Sorry, error occured while processing your request")
      //  .position({
      //    bottom: false,
      //    top: true,
      //    left: false,
      //    right: true
      //  })
      //  .hideDelay(3000)
      //);

      $rootScope.isLoading = false;
      return $q.reject(response);
    }
  };
}