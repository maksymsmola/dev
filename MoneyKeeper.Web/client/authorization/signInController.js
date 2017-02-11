var angular = require("angular");

angular.module("authorization").controller("signInController", signInController);

function signInController($http, fetchCategories) {
  "ngInject";

  var scope = this;

  scope.model = {
    name: "",
    password: ""
  };

  scope.signIn = function() {
    $http.post("/Account/SignIn", scope.model).then(fetchCategories);
  }
}