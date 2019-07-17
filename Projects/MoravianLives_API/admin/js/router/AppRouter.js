define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	return Backbone.Router.extend({
		routes: {
			"": "default",
			"places/duplicates(/:page)": "placesDuplicates",
			"places/search/:query": "placesearch",
			"places(/:page)(/:order)(/:orderdir)": "places",
			"place/:id": "place",
			"persons/duplicates(/:page)": "personsDuplicates",
			"persons/search/:query": "personsearch",
			"persons(/:page)(/:order)(/:orderdir)": "persons",
			"person/:id": "person",
			"map": "map"
		}
	});
});