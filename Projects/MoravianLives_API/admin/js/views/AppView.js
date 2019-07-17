define(function(require){

	var Backbone = require('backbone');
	var _ = require('underscore');
	var $ = require('jquery');

	var AppRouter = require('router/AppRouter');

	return Backbone.View.extend({
		initialize: function() {
			this.render();

			this.router = new AppRouter();
			this.router.on('route:default', _.bind(function() {
				this.showPlaceListView();
			}, this));
			this.router.on('route:personsearch', _.bind(function(searchQuery) {
				console.log('searchQuery: '+searchQuery)
				this.showPersonListView(undefined, undefined, undefined, searchQuery);
			}, this));
			this.router.on('route:persons', _.bind(function(page, order, orderDir) {
				this.showPersonListView(page, order, orderDir);
			}, this));
			this.router.on('route:person', _.bind(function(personId) {
				this.showPersonView(personId);
			}, this));
			this.router.on('route:places', _.bind(function(page, order, orderDir) {
				this.showPlaceListView(page, order, orderDir);
			}, this));
			this.router.on('route:placesDuplicates', _.bind(function(page) {
				this.showDuplicatePlaceListView(page);
			}, this));
			this.router.on('route:personsDuplicates', _.bind(function(page) {
				this.showDuplicatePersonListView(page);
			}, this));
			this.router.on('route:placesearch', _.bind(function(searchQuery) {
				this.showPlaceListView(undefined, undefined, undefined, searchQuery);
			}, this));
			this.router.on('route:place', _.bind(function(placeId) {
				this.showPlaceView(placeId);
			}, this));
			this.router.on('route:map', _.bind(function() {
				this.showMapView();
			}, this));
			Backbone.history.start();
		},

		showMessage: function(msg) {
			this.$el.find('.overlay-container').html('<div class="message">'+msg+'</div>');
			setTimeout(_.bind(function() {
				this.$el.find('.overlay-container').html('');
			}, this), 1500);
		},

		showPersonListView: function(page, order, orderDir, searchQuery) {
			if (this.currentView != 'PersonListView') {
				this.currentView = 'PersonListView';

				var PersonListView = require('views/PersonListView');
				if (this.mainView != undefined) {
					this.mainView.destroy();
				}
				console.log('searchQuery: '+searchQuery)
				this.mainView = new PersonListView({
					el: this.$el.find('.view-container'),
					router: this.router,
					page: page == undefined ? 1 : page,
					order: order == undefined ? '' : order,
					orderDir: orderDir == undefined ? '' : orderDir,
					searchQuery: searchQuery == undefined ? '' : searchQuery,
					app: this
				});
			}
			else {
				this.mainView.collection.getPage(page == undefined ? 1 : page);
			}
		},

		showPlaceListView: function(page, order, orderDir, searchQuery) {
			if (this.currentView != 'PlaceListView') {
				this.currentView = 'PlaceListView';

				var PlaceListView = require('views/PlaceListView');
				if (this.mainView != undefined) {
					this.mainView.destroy();
				}

				this.mainView = new PlaceListView({
					el: this.$el.find('.view-container'),
					router: this.router,
					page: page == undefined ? 1 : page,
					order: order == undefined ? '' : order,
					orderDir: orderDir == undefined ? '' : orderDir,
					searchQuery: searchQuery == undefined ? '' : searchQuery,
					app: this
				});
			}
			else {
				this.mainView.collection.getPage(page == undefined ? 1 : page);
			}
		},

		showDuplicatePlaceListView: function(page) {
			if (this.currentView != 'DuplicatePlaceListView') {
				this.currentView = 'DuplicatePlaceListView';

				var DuplicatePlaceListView = require('views/DuplicatePlaceListView');
				if (this.mainView != undefined) {
					this.mainView.destroy();
				}

				this.mainView = new DuplicatePlaceListView({
					el: this.$el.find('.view-container'),
					router: this.router,
					page: page == undefined ? 1 : page,
					app: this
				});
			}
			else {
				this.mainView.collection.getPage(page == undefined ? 1 : page);
			}
		},

		showDuplicatePersonListView: function(page) {
			if (this.currentView != 'DuplicatePersonListView') {
				this.currentView = 'DuplicatePersonListView';

				var DuplicatePersonListView = require('views/DuplicatePersonListView');
				if (this.mainView != undefined) {
					this.mainView.destroy();
				}

				this.mainView = new DuplicatePersonListView({
					el: this.$el.find('.view-container'),
					router: this.router,
					page: page == undefined ? 1 : page,
					app: this
				});
			}
			else {
				this.mainView.collection.getPage(page == undefined ? 1 : page);
			}
		},

		showPlaceView: function(placeId) {
			if (this.currentView != 'PlaceView') {
				this.currentView = 'PlaceView';

				var PlaceView = require('views/PlaceView');
				if (this.mainView != undefined) {
					this.mainView.destroy();
				}
				this.mainView = new PlaceView({
					el: this.$el.find('.view-container'),
					placeId: placeId,
					router: this.router,
					app: this
				});
			}
		},

		showPersonView: function(personId) {
			if (this.currentView != 'PersonView') {
				this.currentView = 'PersonView';

				var PersonView = require('views/PersonView');
				if (this.mainView != undefined) {
					this.mainView.destroy();
				}
				this.mainView = new PersonView({
					el: this.$el.find('.view-container'),
					personId: personId,
					router: this.router,
					app: this
				});
			}
		},

		showMapView: function(personId) {
			if (this.currentView != 'MapView') {
				this.currentView = 'MapView';

				var MapView = require('views/MapView');
				if (this.mainView != undefined) {
					this.mainView.destroy();
				}
				this.mainView = new MapView({
					el: this.$el.find('.view-container'),
					personId: personId,
					router: this.router
				});
			}
		},

		render: function() {
			var template = _.template($("#appTemplate").html());

			this.$el.html(template());
			return this;
		}
	});
});