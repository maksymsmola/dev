var angular = require("angular");
var _ = require("underscore");
var finOpTypeEnum = require("../common/finOperationType");
var finOpCrudType = require("../common/finOpCrudType");

angular.module("finOperationsCrud").controller("addFinOperationController", addFinOperationController);

function addFinOperationController($scope, $state, $http, finOpType, categories, tags, crudType, model) {
  "ngInject";

  $scope.finOpTypeEnum = finOpTypeEnum;
  $scope.finOpCrudType = finOpCrudType;

  var self = this;

  this.model = model;
  
  this.crudType = crudType;
  this.finOpType = finOpType;

  this.categories = categories[finOpType];

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

  this.editFinOperation = function() {
    $http.post("/FinOperation/Edit", self.model).then(function() {
      $state.transitionTo("main.history");
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