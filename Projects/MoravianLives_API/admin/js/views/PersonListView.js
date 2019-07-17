define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	var PersonListCollection = require('collections/PersonListCollection');
	var DataListView = require('views/DataListView');

	return DataListView.extend({
		uiTemplateName: 'personListViewTemplate',

		initialize: function(options) {
			this.options = options;

			this.collection = new PersonListCollection();
			this.collection.order = this.options.order;
			this.collection.orderDir = this.options.orderDir;
			this.collection.on('reset', this.render, this);
			this.collection.metadata.on('change', this.updateMetadata, this);

			if (this.options.placeId != undefined) {
				this.collection.byPlace(this.options.placeId);
			}
			else if (this.options.searchQuery != '') {
				console.log('search !')
				this.collection.search(this.options.searchQuery);
			}
			else {
				this.collection.getPage(this.options.page, this.options.order);
			}

			if (this.options.renderUI != undefined) {
				if (this.options.renderUI != false) {
					this.renderUI();
				}
			}
			else {
				this.renderUI();
			}

			_.each(this.$el.find('.column-sort'), _.bind(function(sortLink) {
				$(sortLink).click(_.bind(function(event) {
					event.preventDefault();
					$(sortLink).toggleClass('desc');
					this.collection.getPage(this.collection.currentPage, $(sortLink).data('sort'), $(sortLink).hasClass('desc') ? 'desc' : '');
				}, this));
			}, this));

			this.$el.find('.combine-controls .combine-button').click(_.bind(this.combinePersonButtonClick, this));

			this.on('listCheckChanged', _.bind(this.personCheckClick, this));

			this.on('search', _.bind(function(event) {
				this.options.router.navigate('/persons/search/'+event.query);
			}, this));
		},

		updateMetadata: function() {
			if (this.collection.metadata.get('page') != undefined) {
				this.$el.find('.page-info').html((Number(this.collection.metadata.get('page'))+200)+' / '+this.collection.metadata.get('total'));
			}
		},

		personCheckClick: function(event) {
			console.log('personCheckClick')
			this.checkedPersons = _.map(this.$el.find('.item-check:checked'), _.bind(function(checkBox) {
				return $(checkBox).data('id');
			}, this));

			if (this.checkedPersons.length > 1) {
				this.$el.find('.combine-controls').css('display', 'block');
				this.$el.find('.combine-controls .checked-number').text(this.checkedPersons.length);

				var selectOptions = _.map(this.checkedPersons, _.bind(function(personId) {
					return '<option value="'+personId+'">'+(this.collection.get(personId).get('surname') ? this.collection.get(personId).get('surname') : '')+', '+(this.collection.get(personId).get('firstname') ? this.collection.get(personId).get('firstname') : '')+' ('+(this.collection.get(personId).get('birth') && this.collection.get(personId).get('birth').year ? this.collection.get(personId).get('birth').year : '?')+'-'+(this.collection.get(personId).get('death') && this.collection.get(personId).get('death').year ? this.collection.get(personId).get('death').year : '?')+')'+' ['+this.collection.get(personId).get('birth').place.name+' - '+this.collection.get(personId).get('death').place.name+']</option>';
				}, this));
				this.$el.find('.combine-controls .combine-persons-select').html(selectOptions);
			}
			else {
				this.$el.find('.combine-controls').css('display', 'none');
			}
		},

		combinePersonButtonClick: function() {
			var finalPErson = this.$el.find('.combine-controls .combine-persons-select option:selected').attr('value');

			$.ajax({
				url: 'http://moravianlives.org:8001/admin/persons/combine/'+finalPErson,
				type: 'POST',
				data: {
					ids: this.checkedPersons
				},
				complete: _.bind(function() {
					_.each(this.checkedPersons, _.bind(function(checkedpersonId) {
						if (checkedpersonId != finalPErson) {
							this.collection.remove({id: checkedpersonId});
						}
					}, this));
					this.renderList();
				}, this)
			});
		},

		render: function() {
			if (this.options.route) {
				if (this.collection.searchQuery != '') {
					this.options.router.navigate('/persons/search/'+this.collection.searchQuery);
				}
				else {
					this.options.router.navigate('/persons/'+this.collection.currentPage);
				}
			}

			this.renderList();

			return this;
		},

		renderList: function() {

			var template = _.template($("#personListTemplate").html());

			this.$el.find('.list-container').html(template({
				models: this.collection.models,
				showCheckBoxes: this.options.hideCheckBoxes ? false : true
			}));

			this.$el.find('.item-check').click(_.bind(this.personCheckClick, this));
			this.personCheckClick();			
		}
	});
});