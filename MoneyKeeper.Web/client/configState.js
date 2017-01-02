var loginViewTemplate = require("./views/login.html");

module.exports = function($stateProvider, $urlRouterProvider) {
  //$urlRouterProvider.otherwise('/home'); // todo: create not found page

  var loginState = {
    name: "login",
    url: "/login",
    templateUrl: loginViewTemplate,
    controller: "loginController"
  };

  var homeState = {
    name: "home",
    url: "/home",
    template: "<h3>This is home page! Be happy.</h3>"
  }

  $stateProvider.state(loginState);
  $stateProvider.state(homeState);
}