var angular = require("angular");

angular.module("app").factory("authInterceptor", authInterceptor);

function authInterceptor($location) {
  "ngInject";

  return {
    responseError: function(response) {
      if (response.status === 401) {
        $location.path("/signIn");
      }

      return response;
    }
  }
}