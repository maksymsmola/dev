var angular = require("angular");

require("angular-animate");
require("angular-aria");
require("angular-material");
require("angular-ui-router");

angular.module("login", []);

angular.module("app", ["ngAnimate", "ngMaterial", "ngAria", "ui.router", "login"]);