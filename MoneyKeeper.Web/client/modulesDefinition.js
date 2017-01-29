var angular = require("angular");

require("angular-animate");
require("angular-aria");
require("angular-material");
require("angular-ui-router");

angular.module("authorization", []);
angular.module("home", []);
angular.module("finOperationsCrud", []);
angular.module("history", []);

var libsModules = ["ngAnimate", "ngMaterial", "ngAria", "ui.router"];
var customModules = ["authorization", "home", "history", "finOperationsCrud"];

angular.module("app", libsModules.concat(customModules));