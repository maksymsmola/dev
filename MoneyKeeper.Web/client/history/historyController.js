var angular = require("angular");
var dateService = require("./../services/dateService");
var _ = require("underscore");

angular.module("history").controller("historyController", historyController);

function historyController($http, $scope) {
  "ngInject";

  $scope.toDisplayFormat = dateService.toDisplayFormat;

  var vm = this;

  this.operations = [];

  this.formatTags = function(tags) {
    var tagsNams =  _.map(tags, function(tag) {
      return tag.name;
    });

    return tagsNams.join(", ");
  };

  loadOperations();

  function loadOperations() {
    $http.get("/FinOperation/GetForCurrentUser").then(function(response) {
      vm.operations = response.data;
    });
  }
}