var angular = require("angular");

angular.module("authorization").controller("signUpController", signUpController);

function signUpController($http, $state) {
  "ngInject";

  var scope = this;

  scope.model = {
    name: "",
    firstName: "",
    lastName: "",
    password: "",
    confirmPassword: ""
  };

  scope.signUp = function() {
    $http.post("/Account/SignUp", scope.model)
      .then(function() {
        $state.go("home.main");
      });
  }
}