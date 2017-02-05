var angular = require("angular");

require("angular-animate");
require("angular-aria");
require("angular-material");
require("angular-ui-router");
require("angular-material-data-table");

angular.module("authorization", []);
angular.module("mainState", []);
angular.module("finOperationsCrud", []);
angular.module("history", ["md.data.table"]);

var libsModules = ["ngAnimate", "ngMaterial", "ngAria", "ui.router"];
var customModules = ["authorization", "mainState", "history", "finOperationsCrud"];

angular.module("app", libsModules.concat(customModules));