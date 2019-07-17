define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');
	require('markercluster');

	var PersonListCollection = require('collections/PersonListCollection');
	var DataListView = require('views/DataListView');
	var DataCollection = require('collections/DataCollection');

	return DataListView.extend({
		uiTemplateName: 'mapViewTemplate',

		initialize: function(options) {
			console.log('MapView:initialize')
			this.options = options;

			this.collection = new DataCollection();
			this.collection.on('reset', this.render, this);
			this.collection.url = 'http://moravianlives.org:8001/locations';
			this.collection.fetch({
				reset: true
			});

			this.renderUI();

			this.renderMap();
		},

		renderMap: function() {
			this.map = L.map(this.$el.find('.map-container')[0]).setView([54.525961, 15.255119], 4);

			L.tileLayer('http://korona.geog.uni-heidelberg.de/tiles/roads/x={x}&y={y}&z={z}', {
				maxZoom: 20,
				attribution: 'Imagery from <a href="http://giscience.uni-hd.de/">GIScience Research Group @ University of Heidelberg</a> &mdash; Map data &copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
			}).addTo(this.map);

			this.markers = new L.MarkerClusterGroup({
				showCoverageOnHover: false,
				maxClusterRadius: 40
			});
			this.map.addLayer(this.markers);
		},

		render: function() {
			console.log(this.collection.length)
			_.each(this.collection.models, _.bind(function(model) {
				var marker = L.marker([model.get('lat'), model.get('lng')], {
					title: model.get('name')
				}).bindPopup('<strong>'+model.get('name')+'</strong><br/>'+model.get('area')+'<br/><br/><a href="#place/'+model.get('id')+'">More information</a>');

				this.markers.addLayer(marker);
			}, this));
		}
	});
});