var angular = require("angular");

angular.module("app").controller("layoutController", layoutController);

function layoutController($scope, $http, $state) {
  "ngInject";

  this.logOut = function() {
    $http.get("/Account/SignOut").then(function() {
      $state.transitionTo("signIn");
    });
  };
}