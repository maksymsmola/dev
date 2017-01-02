var angular = require("angular");

angular.module("login").controller("loginController", loginController);

function loginController($http, $state) {
  "ngInject";

  var scope = this;

  scope.name = "";
  scope.password = "";

  scope.login = function() {
    $http.post("/Account/SignIn", { name: scope.name, password: scope.password })
    .then(function() {
      $state.go("home");
    });
  }
}