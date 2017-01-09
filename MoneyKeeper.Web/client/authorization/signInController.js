var angular = require("angular");

angular.module("authorization").controller("signInController", signInController);

function signInController($http, $state) {
  "ngInject";

  var scope = this;

  scope.model = {
    name: "",
    password: ""
  };

  scope.signIn = function() {
    $http.post("/Account/SignIn", scope.model)
    .then(function() {
      $state.go("home");
    });
  }
}