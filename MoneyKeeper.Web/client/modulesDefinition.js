var angular = require("angular");

require("angular-animate");
require("angular-aria");
require("angular-material");
require("angular-ui-router");

angular.module("authorization", []);
angular.module("history", []);
angular.module("home", []);

var libsModules = ["ngAnimate", "ngMaterial", "ngAria", "ui.router"];
var customModules = ["authorization", "home", "history"];

angular.module("app", libsModules.concat(customModules));