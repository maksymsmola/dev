var angular = require("angular");

angular.module("finOperationsCrud").controller("addFinOperationController", addFinOperationController);

function addFinOperationController($state, $http, type) {
  "ngInject";

  var self = this;

  this.model = {
    date: new Date(),
    value: 0.0,
    description: "",
    type: type
  };

  this.addFinOperation = function() {
    $http.post("/FinOperation/Add", self.model).then(function() {
      $state.transitionTo("main.home");
    });
  };
}