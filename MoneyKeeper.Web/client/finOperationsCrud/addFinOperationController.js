var angular = require("angular");
var _ = require("underscore");

angular.module("finOperationsCrud").controller("addFinOperationController", addFinOperationController);

function addFinOperationController($state, $http, type, categories, tags) {
  "ngInject";

  var self = this;

  this.model = {
    date: new Date(),
    value: null,
    amount: 1,
    description: "",
    type: type,
    categoryId: null,
    tags: []
  };

  this.categories = categories[type];

  this.selectedTag = null;
  this.searchTagText = null;

  this.cancel = function() {
    $state.transitionTo("main.home");
  };

  this.addFinOperation = function() {
    $http.post("/FinOperation/Add", self.model).then(function() {
      $state.transitionTo("main.home");
    });
  };

  this.transformChip = function(chip) {
    if (angular.isObject(chip)) {
      return chip;
    }

    return { id: null, name: chip };
  };

  this.queryTags = function(query) {
    var queryLowerCase = angular.lowercase(query);
    return _.filter(tags, function(tag) {
        return angular.lowercase(tag.name).indexOf(queryLowerCase) !== -1;
      });
  }
}