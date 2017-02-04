var angular = require("angular");

angular.module("home").controller("homeController", homeController);

function homeController($scope, $http, $state) {
  "ngInject";

  this.logOut = function() {
    $http.get("/Account/SignOut").then(function() {
      $state.transitionTo("signIn");
    });
  };
}