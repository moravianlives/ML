define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	return Backbone.View.extend({
		
		initialize: function(options) {
			this.render();
		},

		events: {
			'click .tabs a': 'tabClick'
		},

		tabClick: function(event) {
			event.preventDefault();

			this.$el.find('.tabs a').removeClass('active');
			$(event.currentTarget).addClass('active');

			this.$el.find('.tabs-content .tab').removeClass('active');
			this.$el.find('.tabs-content .tab#'+$(event.currentTarget).data('tab')).addClass('active');

			this.currentTab = $(event.currentTarget).data('tab');
			this.trigger('tab', {
				tab: $(event.currentTarget).data('tab')
			});
		},

		render: function() {
			this.$el.find('.tabs-content .tab:first-child').addClass('active');
			this.$el.find('.tabs li:first-child a').addClass('active');
			this.currentTab = this.$el.find('.tabs a:first-child').data('tab');
		}
	});
});