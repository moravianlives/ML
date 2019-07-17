define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	return Backbone.Model.extend({
		parse: function(data) {
			return data.data;
		}
	});
});