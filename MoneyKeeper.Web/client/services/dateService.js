var moment = require("moment");

var dateService = {
  toDisplayFormat: function(date) {
    var momentDate = moment(date);
    return momentDate.isValid() ? momentDate.format("DD.MM.YYYY") : "";
    }
}

module.exports = dateService;