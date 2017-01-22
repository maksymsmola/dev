var signInViewTemplate = require("./views/sign-in.html");
var signUpViewTemplate = require("./views/sign-up.html");
var homeViewTemplate = require("./views/home.html");
var mainViewTemplate = require("./views/main.html");
var historyViewTemplate = require("./views/history.html");

module.exports = function($stateProvider, $urlRouterProvider) {
  //$urlRouterProvider.otherwise('/not-found'); // todo: create not found page

  var loginState = {
    name: "signIn",
    url: "/sign-in",
    templateUrl: signInViewTemplate
  };

  var signUpState = {
    name: "signUp",
    url: "/sign-up",
    templateUrl: signUpViewTemplate
  }

  var homeState = {
    name: "home",
    abstract: true,
    templateUrl: homeViewTemplate
  }

  var mainState = {
    name: "home.main",
    parent: "home",
    url: "/main",
    templateUrl: mainViewTemplate
  }

  var historyState = {
    name: "home.history",
    parent: "home",
    url: "/history",
    templateUrl: historyViewTemplate
  }

  $stateProvider.state(loginState);
  $stateProvider.state(signUpState);
  $stateProvider.state(homeState);
  $stateProvider.state(mainState);
  $stateProvider.state(historyState);
}