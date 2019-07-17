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
			this.on('listCheckChanged', _.bind(this.placeCheckClick, this));
		},

		checkedPlaces: [],

		placeCheckClick: function(event) {
			this.checkedPlaces = _.map(this.$el.find('.item-check:checked'), _.bind(function(checkBox) {
				return $(checkBox).data('id');
			}, this));

			if (this.checkedPlaces.length > 1) {
				this.$el.find('.combine-controls').css('display', 'block');
				this.$el.find('.combine-controls .checked-number').text(this.checkedPlaces.length);

				var selectOptions = _.map(this.checkedPlaces, _.bind(function(placeId) {
					return '<option value="'+placeId+'">'+_.find(this.model.get('places'), function(place) {
						return place.id == placeId;
					}).name+' ['+_.find(this.model.get('places'), function(place) {
						return place.id == placeId;
					}).area+']</option>';
				}, this));
				this.$el.find('.combine-controls .combine-places-select').html(selectOptions);
			}
			else {
				this.$el.find('.combine-controls').css('display', 'none');
			}
		},

		combinePlacesButtonClick: function() {
			var finalPlace = this.$el.find('.combine-controls .combine-places-select option:selected').attr('value');

			$.ajax({
				url: 'http://moravianlives.org:8001/admin/places/combine/'+finalPlace,
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
			var template = _.template($("#duplicatePlaceListItemTemplate").html());
			this.$el.html(template({
				model: this.model
			}));
			
			this.map = L.map(this.$el.find('.map-container')[0]).setView([this.model.get('lat'), this.model.get('lng')], 7);

			L.tileLayer('http://korona.geog.uni-heidelberg.de/tiles/roads/x={x}&y={y}&z={z}', {
				maxZoom: 20,
				attribution: 'Imagery from <a href="http://giscience.uni-hd.de/">GIScience Research Group @ University of Heidelberg</a> &mdash; Map data &copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
			}).addTo(this.map);

			this.marker = L.marker([this.model.get('lat'), this.model.get('lng')]).addTo(this.map);

			this.$el.find('.item-check').click(_.bind(this.placeCheckClick, this));
			this.placeCheckClick();			
		}
	});
});