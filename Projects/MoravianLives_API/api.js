var ApiMethod = Backbone.Model.extend({});

var Api = Backbone.Collection.extend({
	model: ApiMethod,
	url: 'api.json',

	initialize: function() {
		this.fetch({
			dataType: 'json'
		});
	},

	parse: function(response) {
		return response.api;
	},

	fetch: function(options) {
		options.reset = true;
		return Backbone.Collection.prototype.fetch.call(this, options);
	}
});

var ApiMethodView = Backbone.View.extend({
	initialize: function() {
		_.bindAll(this, "render")
		this.listenTo(this.model, 'change', this.render);
		this.render();
	},

	events: {
		'click .results-demo-link': 'toggleResultsDemo'
	},

	toggleResultsDemo: function() {
		this.$('.results-demo').slideToggle();
		var _this = this;
		if (!this.$('.results-container').hasClass('loaded')) {
			this.$('.results-container').addClass('loaded')
			$.getJSON('../api'+this.model.get('demoquery'), function(response) {
				console.log(response);
				_this.$('.results-container').text(JSON.stringify(response, null, 5));
			});
		}
	},

	render: function() {
		var template = _.template($("#methodViewTemplate").html());

		this.$el.html(template({
			model: this.model
		}));
		this.$el.find('.results-demo').hide();

		return this;
	}
});

var ApiView = Backbone.View.extend({
	initialize: function() {
		this.listenTo(this.collection, 'reset', this.render);
	},

	render: function() {
		var _this = this;
		this.$el.empty();
		this.collection.each(function(model) {
			var renderedEl = (new ApiMethodView({model: model})).render().$el;
      		_this.$el.append(renderedEl);
    	});
	}
});

var ApiMenuView = Backbone.View.extend({
	initialize: function() {
		this.listenTo(this.collection, 'reset', this.render);
	},

	render: function() {
		var template = _.template($("#methodsMenuTemplate").html());
		this.$el.html(template({
			models: this.collection.models
		}));
	}
});

$(document).ready(function() {
	var api = new Api();

	var apiView = new ApiView({
		el: $("#apiContainer"),
		collection: api
	});

	var apiMenuView = new ApiMenuView({
		el: $("#menu"),
		collection: api
	});
});