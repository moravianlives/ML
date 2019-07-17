requirejs.config({
	urlArgs: "bust=" + (new Date()).getTime(),
	baseUrl: 'js/',
	paths: {
		jquery: 'lib/jquery-1.11.3.min',				
		backbone: 'lib/backbone-min',
		epoxy: 'lib/backbone.epoxy.min',
		underscore: 'lib/underscore-min',
		leaflet: 'lib/leaflet',
		markercluster: 'lib/leaflet.markercluster'
	},
	shim: {
		'backbone': {
			deps: ['underscore', 'jquery'],
			exports: 'Backbone'
		},

		'underscore': {
			exports: '_'
		},

		'jquery': {
			exports: '$'
		},

		'markercluster': {
			deps: ['leaflet']
		},

		'epoxy': {
			deps: ['backbone']
		}
	}
});

require(['js/views/AppView.js', 'js/views/MapAppView.js'],function(AppView, MapAppView) {
	$(function() {
		if ($(document.body).hasClass('map-app')) {
			window.appView = new MapAppView({
				el: $('#appView')
			});
		}
		else {
			window.appView = new AppView({
				el: $('#appView')
			});
		}
	});
});
