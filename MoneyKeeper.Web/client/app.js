var angular = require("angular");

require("./modulesDefinition");

require("./authorization/signInController");
require("./authorization/signUpController");
require("./home/homeController");
require("./authInterceptor");
var configState = require("./configState");

var app = angular.module("app");

app.config(function($httpProvider, $stateProvider, $urlRouterProvider) {
  "ngInject";

  configState($stateProvider, $urlRouterProvider);

  $httpProvider.interceptors.push("authInterceptor");
  $httpProvider.defaults.withCredentials = true;
}).run(function($state) {
  $state.transitionTo("signIn");
});