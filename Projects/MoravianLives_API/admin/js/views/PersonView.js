define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');
	require('leaflet');

	var DataView = require('views/DataView');
	var DataModel = require('models/DataModel');

	return DataView.extend({
		initialize: function(options) {
			this.options = options;

			this.model = new DataModel();
			this.model.once('change', this.render, this);
			this.model.on('sync', this.modelSaved, this);
			this.model.url = 'http://moravianlives.org:8001/person/'+this.options.personId;
			this.model.fetch();
		},

		events: {
			'click .save-button': 'saveButtonClick'
		},

		saveButtonClick: function() {
			this.model.url = 'http://moravianlives.org:8001/admin/person/'+this.options.personId;
			this.model.save(null, {
				success: _.bind(function() {
					this.render();
					this.options.app.showMessage('Person entry saved.')
				}, this),
				type: 'POST'
			});
		},

		render: function() {
			window.model = this.model;

			var template = _.template($("#personViewTemplate").html());

			this.$el.html(template({
				model: this.model
			}));

			if (this.$el.find('.map-container.birthplace').length > 0) {
				this.map = L.map(this.$el.find('.map-container.birthplace')[0], {
					scrollWheelZoom: false
				}).setView([this.model.get('birth').place.lat, this.model.get('birth').place.lng], 10);

				L.tileLayer('http://korona.geog.uni-heidelberg.de/tiles/roads/x={x}&y={y}&z={z}', {
					maxZoom: 20,
					attribution: 'Imagery from <a href="http://giscience.uni-hd.de/">GIScience Research Group @ University of Heidelberg</a> &mdash; Map data &copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
				}).addTo(this.map);

				L.marker([this.model.get('birth').place.lat, this.model.get('birth').place.lng]).addTo(this.map)
					.bindPopup(this.model.get('birth').place.name)
					.openPopup();
			}

			if (this.$el.find('.map-container.deathplace').length > 0) {
				this.map = L.map(this.$el.find('.map-container.deathplace')[0], {
					scrollWheelZoom: false
				}).setView([this.model.get('death').place.lat, this.model.get('death').place.lng], 10);

				L.tileLayer('http://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
					attribution: 'Imagery from <a href="http://mapbox.com/about/maps/">MapBox</a> &mdash; Map data &copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
					subdomains: 'abcd',
					id: 'mapbox.outdoors',
					accessToken: 'pk.eyJ1IjoidHJhdXN0aWQiLCJhIjoib0tQVlcxRSJ9.886zIW04YDanKiDXRWG_SA'
				}).addTo(this.map);

				L.marker([this.model.get('death').place.lat, this.model.get('death').place.lng]).addTo(this.map)
					.bindPopup(this.model.get('death').place.name)
					.openPopup();
			}

			this.initBindings();

			return this;
		}
	});
});