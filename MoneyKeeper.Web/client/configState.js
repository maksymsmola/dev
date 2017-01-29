var finOpType = require("./common/finOperationType");

var signInViewTemplate = require("./views/sign-in.html");
var signUpViewTemplate = require("./views/sign-up.html");
var homeViewTemplate = require("./views/home.html");
var mainViewTemplate = require("./views/main.html");
var historyViewTemplate = require("./views/history.html");
var addFinOperationViewTemplate = require("./views/add-fin-operation.html");

module.exports = function($stateProvider, $urlRouterProvider) {
  //$urlRouterProvider.otherwise('/not-found'); // todo: create not found page

  var loginState = {
    name: "signIn",
    url: "/sign-in",
    templateUrl: signInViewTemplate
  };

  var signUpState = {
    name: "signUp",
    url: "/sign-up",
    templateUrl: signUpViewTemplate
  };

  var homeState = {
    name: "home",
    abstract: true,
    templateUrl: homeViewTemplate
  };

  var mainState = {
    name: "home.main",
    parent: "home",
    url: "/main",
    templateUrl: mainViewTemplate
  };

  var historyState = {
    name: "home.history",
    parent: "home",
    url: "/history",
    templateUrl: historyViewTemplate
  };

  var addExpenseState = {
    name: "addexpense",
    url: "/add-expense",
    templateUrl: addFinOperationViewTemplate,
    controller: "addFinOperationController",
    controllerAs: "addFinOpCtrl",
    resolve: {
      type: function() {
        return finOpType.expense;
      }
    }
  };

  var addIncomeState = {
    name: "addincome",
    url: "/add-income",
    templateUrl: addFinOperationViewTemplate,
    controller: "addFinOperationController",
    controllerAs: "addFinOpCtrl",
    resolve: {
      type: function() {
        return finOpType.income;
      }
    }
  };

  $stateProvider.state(loginState);
  $stateProvider.state(signUpState);
  $stateProvider.state(addExpenseState);
  $stateProvider.state(addIncomeState);
  $stateProvider.state(homeState);
  $stateProvider.state(mainState);
  $stateProvider.state(historyState);
}