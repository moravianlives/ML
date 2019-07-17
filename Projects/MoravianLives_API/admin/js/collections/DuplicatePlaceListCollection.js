define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	return Backbone.Collection.extend({
		urlBase: 'http://moravianlives.org:8001/places/duplicates',

		initialize: function() {
			this.metadata = new Backbone.Model();
		},

		parse: function(data) {
			this.metadata.set({
				page: data.metadata.page,
				total: data.metadata.total
			});
			return data.data;
		},

		getPage: function(page) {
			this.currentPage = page;
			this.url = this.urlBase+'/'+((this.currentPage-1)*20)+'/20';
			this.fetch({
				reset: true
			});
		}
	});
});