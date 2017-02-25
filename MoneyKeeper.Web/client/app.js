var angular = require("angular");

require("./modulesDefinition");

require("./authorization/signInController");
require("./authorization/signUpController");
require("./layout/layoutController");
require("./finOperationsCrud/addFinOperationController");
require("./history/historyController");
require("./common/fetchCategories");

require("./authInterceptor");
require("./spinnerInterceptor");

var configState = require("./configState");
var configCalendar = require("./configCalendar");

var app = angular.module("app");

app.value("categories", {});

app.config(function($httpProvider, $stateProvider, $urlRouterProvider, $mdDateLocaleProvider) {
  "ngInject";

  configState($stateProvider, $urlRouterProvider);
  configCalendar($mdDateLocaleProvider);

  $httpProvider.interceptors.push("authInterceptor");
  $httpProvider.interceptors.push("spinnerInterceptor");
  $httpProvider.defaults.withCredentials = true;
}).run(function($state) {
  "ngInject";

  $state.transitionTo("signIn");
});