define(function(require){
	var _ = require('underscore');

	return {
		formatDate: function(day, month, year) {
			var dateArr = [];
			if (day != undefined) {
				dateArr.push(day);
			}
			if (month != undefined) {
				dateArr.push(month);
			}
			if (year != undefined) {
				dateArr.push(year);
			}

			return dateArr.join('-');
		}
	};
});