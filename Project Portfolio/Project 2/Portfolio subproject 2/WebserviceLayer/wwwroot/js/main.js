

require.config({

    baseUrl: "js",

    paths: {
        jquery: '../lib/jquery.min',
        knockout: '../lib/knockout/dist/knockout',
        text: '../lib/text/text',
        postman: 'services/postman',
        jqcloud: '../lib/jqcloud2/dist/jqcloud.min'
    },
    shim: {
        jqcloud: {
            deps: ['jquery']
        }
    }
});

require(['knockout'], function (ko) {

    ko.components.register("mylist", {
        viewModel: { require: "components/mylist/mylist" },
        template: { require: "text!components/mylist/mylist_view.html"}
    });

    ko.components.register("my-element", {
        viewModel: { require: "components/element/element" },
        template: { require: "text!components/element/element_view.html" }
    });

    ko.components.register("new_quest", {
        viewModel: { require: "components/NewestQuestions/NewestQuestions" },
        template: { require: "text!components/NewestQuestions/NewestQuestions_view.html" }
    });

    ko.components.register("show_quest", {
        viewModel: { require: "components/Question/Question" },
        template: { require: "text!components/Question/Question_view.html" }
    });

    ko.components.register("wordcloud", {
        viewModel: { require: "components/wordcloud/wordcloud" },
        template: { require: "text!components/wordcloud/wordcloud.html" }
    });
});

require(['knockout', 'postman'], function(ko, postman) {
    var vm = (function() {
        var currentView = ko.observable('new_quest');
        var currentParams = ko.observable({});
        var switchComponent = function() {
            if (currentView() === "mylist") {
                currentView("my-element");
            } else if (currentView() === "wordcloud") {
                currentView("mylist");
            } else {
                //currentView("post"); //comented out for testing of the wordcloud
                currentView("wordcloud");
            }

        }

        var WordcloudView = function() {
            if (currentView() !== "wordcloud") {
                currentView("wordcloud");
            }
        }

        postman.subscribe(postman.events.changeView,
            viewName => {
                currentParams({ name: "hello"});
                currentView(viewName);
            });
        
        return {
            currentView,
            currentParams,
            switchComponent,
            WordcloudView
        }
    })();

    ko.applyBindings(vm);
});

require(['knockout', 'jquery', 'jqcloud'], function (ko, $) {
    ko.bindingHandlers.cloud = {
        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called when the binding is first applied to an element
            // Set up any initial state, event handlers, etc. here
            var words = allBindings.get('cloud').words;
            if (words && ko.isObservable(words)) {
                words.subscribe(function () {
                    $(element).jQCloud('update', ko.unwrap(words));
                });
            }
        },
        update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called once when the binding is first applied to an element,
            // and again whenever any observables/computeds that are accessed change
            // Update the DOM element based on the supplied values here.
            var words = ko.unwrap(allBindings.get('cloud').words) || [];
            $(element).jQCloud(words);
        }
    };
});
