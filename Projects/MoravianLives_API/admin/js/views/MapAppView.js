define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	var TabsView = require('views/TabsView');

	return Backbone.View.extend({
		initialize: function() {
			console.log('MapAppView:initialize');
			this.render();
		},

		render: function() {
			var template = _.template($("#appTemplate").html());

			console.log('renderMapView');

			this.$el.html(template());

			this.initTabs();
			this.renderMap();

			return this;
		},

		initTabs: function() {
			var tabsView = new TabsView({
				el: this.$el.find('.tabs-container')
			});
			tabsView.on('tab', _.bind(function(event) {
	//			this.googleDriveTab();
			}, this));
		},

		renderMap: function() {
			this.map = L.map(this.$el.find('.map-container')[0]).setView([54.525961, 15.255119], 4);

			L.tileLayer('http://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
				attribution: 'Imagery from <a href="http://mapbox.com/about/maps/">MapBox</a> &mdash; Map data &copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
				subdomains: 'abcd',
				id: 'mapbox.outdoors',
				accessToken: 'pk.eyJ1IjoidHJhdXN0aWQiLCJhIjoib0tQVlcxRSJ9.886zIW04YDanKiDXRWG_SA'
			}).addTo(this.map);

			this.markers = new L.MarkerClusterGroup({
				showCoverageOnHover: false,
				maxClusterRadius: 40
			});
			this.map.addLayer(this.markers);
		}
	});
});