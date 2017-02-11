var angular = require("angular");
var _ = require("underscore");
var finOpTypes = require("./finOperationType");

angular.module("app").factory("fetchCategories", fetchCategories);

function fetchCategories($http, $state, categories) {
  "ngInject";

  return function() {
    $http
      .get("/Categories/GetAll")
      .then(function(response) {
        categories[finOpTypes.income] = _.filter(response.data, function(item) {
          return item.type === finOpTypes.income;
        });
        categories[finOpTypes.expense] = _.filter(response.data, function(item) {
          return item.type === finOpTypes.expense;
        });
      })
      .then(function() {
        $state.transitionTo("main.home");
      });
  }
}