var angular = require("angular");

require("./modulesDefinition");

require("./authorization/signInController");
require("./authorization/signUpController");
require("./layout/layoutController");
require("./finOperationsCrud/addFinOperationController");
require("./history/historyController");

require("./authInterceptor");
require("./spinnerInterceptor");
var configState = require("./configState");

var app = angular.module("app");

app.config(function($httpProvider, $stateProvider, $urlRouterProvider) {
  "ngInject";

  configState($stateProvider, $urlRouterProvider);

  $httpProvider.interceptors.push("authInterceptor");
  $httpProvider.interceptors.push("spinnerInterceptor");

  $httpProvider.defaults.withCredentials = true;
}).run(function($state) {
  "ngInject";

  $state.transitionTo("signIn");
});