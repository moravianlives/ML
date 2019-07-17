define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');
	require('leaflet');

	var DataView = require('views/DataView');
	var PersonListView = require('views/PersonListView');
	var DataModel = require('models/DataModel');

	return DataView.extend({
		initialize: function(options) {
			this.options = options;

			this.model = new DataModel();
			this.model.once('change', this.render, this);
			this.model.url = 'http://moravianlives.org:8001/place/'+this.options.placeId;
			this.model.fetch();
		},

		events: {
			'click .save-button': 'saveButtonClick',
			'click .add-location-button': 'addLocationButtonClick',
			'keyup .latlng-input': 'updateMap',
			'click .geo-lookup-button': 'geoLookupButtonClick',
			'keyup .geo-lookup-input': 'geoLookupInputKeyUp',
			'click .geo-lookup-clear-button': 'geoLookupClearButtonClick'
		},

		saveButtonClick: function() {
			this.model.url = 'http://moravianlives.org:8001/admin/place/'+this.options.placeId;
			this.model.save(null, {
				success: _.bind(function() {
					this.render();
					this.options.app.showMessage('Place entry saved.')
				}, this),
				type: 'POST'
			});
		},

		updateMap: function() {
			if (this.map != undefined) {
				this.marker.setLatLng([this.model.get('lat'), this.model.get('lng')]);
				this.map.setView([this.model.get('lat'), this.model.get('lng')]);
			}
		},

		addLocationButtonClick: function() {
			this.model.set({
				lat: 51.165691,
				lng: 10.451526
			});
			this.render();
		},

		geoLookupClearButtonClick: function() {
			this.lookupMarkers.clearLayers();
		},

		searchMarkerIcon: L.icon({
			iconUrl: 'js/lib/images/marker-icon-green.png',
			iconRetinaUrl: 'js/lib/images/marker-icon-green-2x.png',
			iconSize: [25, 41],
			iconAnchor: [12, 41],
			popupAnchor: [1, -34],
			shadowSize: [41, 41],
			shadowUrl: 'js/lib/images/marker-shadow.png'
		}),

		geoLookupInputKeyUp: function(event) {
			if (event.keyCode == 13) {
				this.geoLookupButtonClick();
			}
		},

		geoLookupButtonClick: function() {
			$.ajax('https://maps.googleapis.com/maps/api/geocode/json?language=en&address='+$('#geoLookupInput').val(), {
				dataType: 'json',
				method: 'get',
				success: _.bind(function(data) {
					this.lookupMarkers.clearLayers();

					_.each(data.results, _.bind(function(resultItem) {
						var marker = L.marker(resultItem.geometry.location, {
							title: resultItem.formatted_address,
							icon: this.searchMarkerIcon
						}).bindPopup(
							'<h2>'+resultItem.formatted_address+'</h2>'+
							'<ul>'+_.map(resultItem.address_components, function(addressComponent) {
								return '<li>'+addressComponent.long_name+'</li>';
							}).join('')+'</ul>'+
							'<p><a href="#" class="lookup-marker-set-location" data-lat="'+resultItem.geometry.location.lat+'" data-lng="'+resultItem.geometry.location.lng+'">Set location</a></p>'
						);
						marker.on('popupopen', _.bind(function(event) {
							$('.lookup-marker-set-location').click(_.bind(function(event) {
								event.preventDefault();

								this.model.set({
									lat: resultItem.geometry.location.lat,
									lng: resultItem.geometry.location.lng,
									google_id: resultItem.place_id,
									google_name: resultItem.address_components[0].long_name,
									google_location_type: resultItem.geometry.location_type,
									google_address: resultItem.address_components
								});

								this.marker.setLatLng([
									resultItem.geometry.location.lat,
									resultItem.geometry.location.lng
								]);
							}, this));
						}, this));
						this.lookupMarkers.addLayer(marker);
					}, this));
				}, this)
			});
		},

		render: function() {
			var template = _.template($("#placeViewTemplate").html());

			this.$el.html(template({
				model: this.model
			}));

			if (this.$el.find('.map-container').length > 0) {
				this.map = L.map(this.$el.find('.map-container')[0]).setView([this.model.get('lat'), this.model.get('lng')], 7);

				L.tileLayer('http://korona.geog.uni-heidelberg.de/tiles/roads/x={x}&y={y}&z={z}', {
					maxZoom: 20,
					attribution: 'Imagery from <a href="http://giscience.uni-hd.de/">GIScience Research Group @ University of Heidelberg</a> &mdash; Map data &copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
				}).addTo(this.map);

				this.marker = L.marker([this.model.get('lat'), this.model.get('lng')]).addTo(this.map);

				this.lookupMarkers = L.featureGroup().addTo(this.map);

				this.map.addEventListener('click', _.bind(function(event) {
					this.marker.setLatLng(event.latlng);
					this.model.set({
						lat: event.latlng.lat,
						lng: event.latlng.lng
					});
				}, this));
			}

			this.personList = new PersonListView({
				el: this.$el.find('.person-list-container'),
				placeId: this.model.get('id'),
				renderUI: false,
				hideCheckBoxes: true
			});

			this.initBindings();
			return this;
		},

		destroy: function() {
			this.personList.destroy();

			DataView.prototype.destroy.call(this);
		}
	});
});