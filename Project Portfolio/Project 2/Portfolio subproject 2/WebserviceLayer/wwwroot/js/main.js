

require.config({

    baseUrl: "js",

    paths: {
        jquery: '../lib/jquery.min',
        knockout: '../lib/knockout/dist/knockout',
        text: '../lib/text/text',
        postman: 'services/postman'
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
});

require(['knockout', 'postman'], function(ko, postman) {
    var vm = (function() {
        var currentView = ko.observable('new_quest');
        var currentParams = ko.observable({});
        var switchComponent = function() {
            if (currentView() === "mylist") {
                currentView("my-element");
            } else {
                currentView("post");
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
            switchComponent
        }
    })();

    ko.applyBindings(vm);
});