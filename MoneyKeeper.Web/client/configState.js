var finOpType = require("./common/finOperationType");

var signInViewTemplate = require("./views/sign-in.html");
var signUpViewTemplate = require("./views/sign-up.html");
var layoutViewTemplate = require("./views/layout.html");
var homeViewTemplate = require("./views/home.html");
var mainViewTemplate = require("./views/main.html");
var historyViewTemplate = require("./views/history.html");
var addFinOperationViewTemplate = require("./views/add-fin-operation.html");

module.exports = function($stateProvider, $urlRouterProvider) {
  //$urlRouterProvider.otherwise('/not-found'); // todo: create not found page

  var loginState = {
    name: "signIn",
    url: "/sign-in",
    templateUrl: signInViewTemplate,
    resolve: {
      $title: function() {
        return "Вход";
      }
    }
  };

  var signUpState = {
    name: "signUp",
    url: "/sign-up",
    controller: "signUpController",
    controllerAs: "signUpCtrl",
    templateUrl: signUpViewTemplate,
    resolve: {
      $title: function() {
        return "Регистрация";
      }
    }
  };

  var layoutState = {
    name: "layout",
    abstract: true,
    controller: "layoutController",
    controllerAs: "layoutCtrl",
    templateUrl: layoutViewTemplate
  };

  var mainState = {
    name: "main",
    abstract: true,
    parent: "layout",
    templateUrl: mainViewTemplate
  };

  var homeState = {
    name: "main.home",
    parent: "main",
    url: "/home",
    templateUrl: homeViewTemplate,
    resolve: {
      $title: function() {
        return "Домашняя страница";
      }
    }
  };

  var historyState = {
    name: "main.history",
    parent: "main",
    controller: "historyController",
    controllerAs: "historyCtrl",
    url: "/history",
    templateUrl: historyViewTemplate,
    resolve: {
      $title: function() {
        return "История";
      }
    }
  };

  var addExpenseState = {
    name: "main.addexpense",
    parent: "layout",
    url: "/add-expense",
    templateUrl: addFinOperationViewTemplate,
    controller: "addFinOperationController",
    controllerAs: "addFinOpCtrl",
    resolve: {
      $title: function() {
        return "Добавить расход";
      },
      type: function() {
        return finOpType.expense;
      },
      tags: function($http) {
        "ngInject";

        return $http.get("/Tags/GetAllForUser").then(function(result) { return result.data; });
      }
    }
  };

  var addIncomeState = {
    name: "main.addincome",
    parent: "layout",
    url: "/add-income",
    templateUrl: addFinOperationViewTemplate,
    controller: "addFinOperationController",
    controllerAs: "addFinOpCtrl",
    resolve: {
      $title: function() {
        return "Добавить доход";
      },
      type: function() {
        return finOpType.income;
      },
      tags: function($http) {
        "ngInject";

        return $http.get("/Tags/GetAllForUser").then(function(result) { return result.data; });
      }
    }
  };

  $stateProvider.state(loginState);
  $stateProvider.state(signUpState);
  $stateProvider.state(layoutState);
  $stateProvider.state(mainState);
  $stateProvider.state(homeState);
  $stateProvider.state(historyState);
  $stateProvider.state(addExpenseState);
  $stateProvider.state(addIncomeState);
}