var angular = require("angular");

angular.module("history").controller("historyController", historyController);

function historyController($http) {
  "ngInject";

  var vm = this;

  this.operations = [];

  loadOperations();

  function loadOperations() {
    $http.get("/FinOperation/GetForCurrentUser").then(function(response) {
      vm.operations = response.data;
    });
  }
}