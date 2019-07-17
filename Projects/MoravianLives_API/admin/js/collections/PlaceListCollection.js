define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	return Backbone.Collection.extend({
		urlBase: 'http://moravianlives.org:8001/places',

		initialize: function() {
			this.metadata = new Backbone.Model();
		},

		parse: function(data) {
			if (data.metadata) {			
				this.metadata.set({
					page: data.metadata.page,
					total: data.metadata.total
				});
			}
			return data.data;
		},

		search: function(query) {
			this.searchQuery = query;
			this.url = this.urlBase+'/search/'+query;
			this.fetch({
				reset: true
			});
		},

		getPage: function(page, order, orderDir) {
			this.searchQuery = '';
			this.order = order == undefined ? '' : order;
			this.orderDir = orderDir == undefined ? '' : orderDir;
			this.currentPage = page;
			this.url = this.urlBase+'/'+((this.currentPage-1)*200)+'/200'+(this.order != '' ? '/'+order : '')+(this.orderDir != '' ? '/'+this.orderDir : '');
			this.fetch({
				reset: true
			});
		}
	});
});