var signInViewTemplate = require("./views/sign-in.html");
var signUpViewTemplate = require("./views/sign-up.html");

module.exports = function($stateProvider, $urlRouterProvider) {
  //$urlRouterProvider.otherwise('/not-found'); // todo: create not found page

  var loginState = {
    name: "signIn",
    url: "/sign-in",
    templateUrl: signInViewTemplate,
    controller: "signInController"
  };

  var signUpState = {
    name: "signUp",
    url: "/sign-up",
    templateUrl: signUpViewTemplate,
    controller: "signUpController"
  }

  var homeState = {
    name: "home",
    url: "/home",
    template: "<h3>This is home page! Be happy.</h3>"
  }

  $stateProvider.state(loginState);
  $stateProvider.state(signUpState);
  $stateProvider.state(homeState);
}