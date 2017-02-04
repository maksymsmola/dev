var angular = require("angular");
var dateService = require("./../services/dateService");

angular.module("history").controller("historyController", historyController);

function historyController($http, $scope) {
  "ngInject";

  $scope.toDisplayFormat = dateService.toDisplayFormat;

  var vm = this;

  this.operations = [];

  loadOperations();

  function loadOperations() {
    $http.get("/FinOperation/GetForCurrentUser").then(function(response) {
      vm.operations = response.data;
    });
  }
}