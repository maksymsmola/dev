var dateService = require("./services/dateService");

module.exports = function($mdDateLocaleProvider) {
  $mdDateLocaleProvider.months = [
    "Январь",
    "Февраль",
    "Март",
    "Апрель",
    "Май",
    "Июнь",
    "Июль",
    "Август",
    "Сентябрь",
    "Октябрь",
    "Ноябрь",
    "Декабрь"
  ];

  $mdDateLocaleProvider.shortMonths = [
    "Янв",
    "Фев",
    "Мрт",
    "Апр",
    "Май",
    "Июнь",
    "Июль",
    "Авг",
    "Сент",
    "Окт",
    "Нояб",
    "Дек"
  ];

  $mdDateLocaleProvider.days = [
    "Воскресенье",
    "Понедельник",
    "Вторник",
    "Среда",
    "Четверг",
    "Пятница",
    "Суббота"
  ];

  $mdDateLocaleProvider.shortDays = [
    "Вс",
    "Пн",
    "Вт",
    "Ср",
    "Чт",
    "Пт",
    "Сб"
  ];

  $mdDateLocaleProvider.firstDayOfWeek = 1;

  $mdDateLocaleProvider.formatDate = function(date) {
      return dateService.toDisplayFormat(date);
    };
}