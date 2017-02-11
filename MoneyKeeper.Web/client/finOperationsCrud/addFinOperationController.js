var angular = require("angular");

angular.module("finOperationsCrud").controller("addFinOperationController", addFinOperationController);

function addFinOperationController($state, $http, type, categories) {
  "ngInject";

  var self = this;

  this.model = {
    date: new Date(),
    value: null,
    description: "",
    type: type,
    categoryId: null
  };

  this.categories = categories[type];

  this.addFinOperation = function() {
    $http.post("/FinOperation/Add", self.model).then(function() {
      $state.transitionTo("main.home");
    });
  };
}