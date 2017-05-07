var angular = require("angular");
var _ = require("underscore");
var moment = require("moment");

angular.module("statistic").controller("statisticController", statisticController);

function statisticController($scope, $http) {
  "ngInject";

  $http.get("/Statistic/GetGeneralMonthlyReport")
    .then(function (response) {
      var rows = _.map(response.data, function (item) {
        return {
          c: [{
            v: moment(item.date).format("DD.MM")
          }, {
            v: item.expenseValue
          }, {
            v: item.incomeValue
          }]
        }
      });
      $scope.monthlyReport = {
        type: "LineChart",
        displayed: false,
        options: {
          title: "Всего за месяц",
          colors: ['#ff4d00', '#009900'],
          isStacked: "true",
          fill: 20,
          displayExactValues: true,
          vAxis: {
            title: "Значение",
            gridlines: {
              count: 10
            }
          },
          hAxis: {
            title: "День"
          }
        },
        data: {
          cols: [{
            id: "date",
            label: "Дата",
            type: "string"
          }, {
            id: "value-expense",
            label: "Расходы",
            type: "number"
          }, {
            id: "value-incomes",
            label: "Доходы",
            type: "number"
          }],
          rows: rows
        },
        view: {
          columns: [0, 1, 2]
        }
      };
    });

  $scope.isColumnChart = true;
  $scope.expensesCategoriesChart = {};
  $scope.incomesCategoriesChart = {};
  $scope.$watch("isColumnChart", function(newValue) {
    if(newValue) {
      $scope.expensesCategoriesChart.type = "ColumnChart";
      $scope.incomesCategoriesChart.type = "ColumnChart";
    } else {
      $scope.expensesCategoriesChart.type = "PieChart";
      $scope.incomesCategoriesChart.type = "PieChart";
    }
  });
  $http.get("/Statistic/GetReportByCategories")
    .then(function (response) {
      $scope.expensesCategoriesChart = {
        type: "ColumnChart",
        options: {
          title: "Всего расходов по категориям"
        },
        data: {
          cols: [
            {id: "category-name", label: "Категория", type: "string"},
            {id: "value", label: "Расходы", type: "number"}
          ],
          rows: _getRows(response.data.expenses)
        }
      };

      $scope.incomesCategoriesChart = {
        type: "ColumnChart",
        options: {
          title: "Всего доходов по категориям"
        },
        data: {
          cols: [
            {id: "category-name", label: "Категория", type: "string"},
            {id: "value", label: "Доходы", type: "number"}
          ],
          rows: _getRows(response.data.incomes)
        }
      }
    });

  function _getRows(collection) {
    var sortHandler = function(item) {
      return -item.amount;
    };

    return _.map(_.sortBy(collection, sortHandler), function (item) {
      return {
        c: [{
          v: item.categoryName
        }, {
          v: item.amount,
          f: item.amount + ", " + item.percent + "%"
        }]
      }
    });
  }
}