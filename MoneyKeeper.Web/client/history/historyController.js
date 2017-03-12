var angular = require("angular");
var dateService = require("./../services/dateService");
var _ = require("underscore");

angular.module("history").controller("historyController", historyController);

function historyController($http, $scope, categories, tags) {
  "ngInject";

  $scope.toDisplayFormat = dateService.toDisplayFormat;

  var vm = this;

  this.categories = categories;
  this.tags = tags;
  this.filter = initFilter();
  this.showFilters = false;

  $scope.$watch("historyCtrl.filter.date.exactValue",
    function(newVal, oldVal) {
      if (newVal) {
        vm.filter.date.to = null;
      }
    });

  $scope.$watch("historyCtrl.filter.value.exactValue",
      function(newVal, oldVal) {
        if (newVal) {
          vm.filter.value.to = null;
        }
      });

  this.operations = [];

  loadOperations();

  this.formatTags = function(tags) {
    var tagsNams = _.map(tags, function(tag) {
      return tag.name;
    });

    return tagsNams.join(", ");
  };

  this.delete = function(id) {
    $http.post("/FinOperation/Delete", { id: id })
      .then(function(response) {
        if (response.data.success) {
          //todo: add immutable to remove deleted item from client instead of fetching all items again
          loadOperations();
        }
      });
  };

  this.getByFilters = function() {
    $http.post("/FinOperation/GetByFilter", vm.filter)
      .then(function(response) {
        vm.operations = response.data;
      });
  };

  this.clearFilters = function() {
    this.filter = initFilter();

    loadOperations();
  };

  function loadOperations() {
    $http.get("/FinOperation/GetForCurrentUser").then(function(response) {
      vm.operations = response.data;
    });
  }

  function initFilter() {
    return {
      type: 0,
      date: {
        from: null,
        to: null,
        exactValue: false
      },
      value: {
        from: null,
        to: null,
        exactValue: false
      },
      description: "",
      categoriesIds: [],
      tagsIds: []
    };
  };
}