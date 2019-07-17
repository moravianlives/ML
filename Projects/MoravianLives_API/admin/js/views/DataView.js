define(function(require){
	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	return Backbone.View.extend({
		initBindings: function() {
			_.each(this.$el.find('[data-bind]'), _.bind(function(el) {
				var bindProperty = $(el).data('bind');
				var bindPropertyKey = $(el).data('bind-key');

				if ($(el).is('input') || $(el).is('textarea')) {
					$(el).focusout(_.bind(function() {
						if (bindPropertyKey != undefined) {
							var attribute = this.model.get(bindProperty);
							attribute[bindPropertyKey] = $(el).val();
							this.model.set(bindProperty, attribute);
						}
						else {
							this.model.set(bindProperty, $(el).val());
						}
					}, this));
					this.model.on('change:'+bindProperty, _.bind(function() {
						$(el).val(this.model.get(bindProperty));
					}, this));
				}

				if ($(el).is('select')) {
					console.log('is select')
					$(el).change(_.bind(function() {
						console.log('select change')
						if (bindPropertyKey != undefined) {
							var attribute = this.model.get(bindProperty);
							attribute[bindPropertyKey] = $(el).val();
							this.model.set(bindProperty, attribute);
						}
						else {
							this.model.set(bindProperty, $(el).val());
						}
					}, this));
					this.model.on('change:'+bindProperty, _.bind(function() {
						$(el).val(this.model.get(bindProperty));
					}, this));
				}
			}, this));
		},

		destroy: function() {
			this.undelegateEvents();
			this.$el.removeData().unbind(); 
		}
	});
});