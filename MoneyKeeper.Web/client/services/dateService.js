var moment = require("moment");

var dateService = {
    toDisplayFormat: function(date) {
        return moment(date).format("DD.MM.YYYY");
    }
}

module.exports = dateService;