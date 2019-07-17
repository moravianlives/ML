define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	return Backbone.View.extend({
		events: {
			'click .footer-toolbar .prev': function() {
				if (this.collection.currentPage > 1) {
					this.collection.getPage(Number(this.collection.currentPage)-1, this.collection.order, this.collection.orderDir);
				}
			},
			'click .footer-toolbar .next': function() {
				this.collection.getPage(Number(this.collection.currentPage)+1, this.collection.order, this.collection.orderDir);
			},
			'keydown .footer-toolbar .search-input': function(event) {
				if (event.keyCode == 13 && $(event.currentTarget).val().length > 3) {
					this.collection.search($(event.currentTarget).val());
					this.trigger('search', {
						query: $(event.currentTarget).val()
					});
				}
			},
			'click .check-all': 'checkAllClick'
		},

		checkAllClick: function(event) {
			if (!this.disableCheckBoxes) {
				console.log($(event.currentTarget).closest('table').find('.item-check').length)
				if ($(event.currentTarget).closest('table').find('.item-check').length == $(event.currentTarget).closest('table').find('.item-check:checked').length) {
					$(event.currentTarget).closest('table').find('.item-check').prop('checked', false);
				}
				else {
					$(event.currentTarget).closest('table').find('.item-check').prop('checked', true);
				}
				this.trigger('listCheckChanged');
			}
		},

		destroy: function() {
			this.undelegateEvents();
			this.$el.removeData().unbind();
			this.$el.html('');
		},

		updateMetadata: function() {
			console.log('updateMetadata');

			this.$el.find('.page-info').html((Number(this.collection.metadata.get('page'))+200)+' / '+this.collection.metadata.get('total'));
		},

		renderUI: function() {
			var template = _.template($("#"+this.uiTemplateName).html());

			this.$el.html(template());
			return this;
		}
	});
});