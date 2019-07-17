define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	var DuplicatePlaceListItemView = require('views/DuplicatePlaceListItemView');
	var DuplicatePlaceListCollection = require('collections/DuplicatePlaceListCollection');

	var DataListView = require('views/DataListView');

	return DataListView.extend({
		disableCheckBoxes: true,
		uiTemplateName: 'duplicatePlaceListViewTemplate',

		initialize: function(options) {
			this.options = options;

			this.collection = new DuplicatePlaceListCollection();
			this.collection.on('reset', this.render, this);
			this.collection.metadata.on('change', this.updateMetadata, this);

			this.collection.getPage(this.options.page);

			this.renderUI();
		},

		updateMetadata: function() {
			this.$el.find('.page-info').html((Number(this.collection.metadata.get('page'))+20)+' / '+this.collection.metadata.get('total'));
		},

		render: function() {
			this.options.router.navigate('/places/duplicates/'+this.collection.currentPage);

			this.renderList();

			return this;
		},

		renderList: function() {
			this.$el.find('.list-container').html('');

			_.each(this.collection.models, _.bind(function(model) {
				var newEl = $('<div></div>');
				this.$el.find('.list-container').append(newEl);

				new DuplicatePlaceListItemView({
					el: newEl,
					model: model
				});
			}, this));
		}
	});
});