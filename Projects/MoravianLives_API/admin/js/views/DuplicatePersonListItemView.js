define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	var DataListView = require('views/DataListView');

	return DataListView.extend({
		initialize: function(options) {
			this.options = options;
			this.render();

			this.$el.find('.combine-controls .combine-button').click(_.bind(this.combinePlacesButtonClick, this));
			this.on('listCheckChanged', _.bind(this.personCheckClick, this));
		},

		checkedPlaces: [],

		personCheckClick: function(event) {
			this.checkedPlaces = _.map(this.$el.find('.item-check:checked'), _.bind(function(checkBox) {
				return $(checkBox).data('id');
			}, this));

			if (this.checkedPlaces.length > 1) {
				this.$el.find('.combine-controls').css('display', 'block');
				this.$el.find('.combine-controls .checked-number').text(this.checkedPlaces.length);

				var selectOptions = _.map(this.checkedPlaces, _.bind(function(personId) {
					var person = _.find(this.model.get('persons'), function(person) {
						return person.id == personId;
					});
					return '<option value="'+personId+'">'+person.surname+', '+person.firstname+' ('+person.birth.year+'-'+person.death.year+')</option>';
				}, this));
				this.$el.find('.combine-controls .combine-persons-select').html(selectOptions);
			}
			else {
				this.$el.find('.combine-controls').css('display', 'none');
			}
		},

		combinePlacesButtonClick: function() {
			var finalPlace = this.$el.find('.combine-controls .combine-persons-select option:selected').attr('value');

			$.ajax({
				url: 'http://moravianlives.org:8001/admin/persons/combine/'+finalPlace,
				type: 'POST',
				data: {
					ids: this.checkedPlaces
				},
				complete: _.bind(function() {
					this.destroy();
				}, this)
			});
		},

		render: function() {
			var template = _.template($("#duplicatePersonListItemTemplate").html());
			this.$el.html(template({
				model: this.model
			}));

			this.$el.find('.item-check').click(_.bind(this.personCheckClick, this));
			this.personCheckClick();			
		}
	});
});