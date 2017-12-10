

require.config({

    baseUrl: "js",

    paths: {
        jquery: '../lib/jQuery/dist/jquery.min',
        knockout: '../lib/knockout/dist/knockout',
        text: '../lib/text/text',
        jqcloud: '../lib/jqcloud2/dist/jqcloud.min'
    },
    shim: {
        jqcloud: {
            deps: ['jquery']
        }
    }

    
    
});

require(['knockout', 'jquery', 'jqcloud'], function (ko, $) {
    ko.bindingHandlers.cloud = {
        init: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called when the binding is first applied to an element
            // Set up any initial state, event handlers, etc. here
            var words = allBindings.get('cloud').words;
            if(words && ko.isObservable(words)) {
                words.subscribe(function() {
                    $(element).jQCloud('update', ko.unwrap(words));
                });
            }
        },
        update: function(element, valueAccessor, allBindings, viewModel, bindingContext) {
            // This will be called once when the binding is first applied to an element,
            // and again whenever any observables/computeds that are accessed change
            // Update the DOM element based on the supplied values here.
            var words = ko.unwrap(allBindings.get('cloud').words) || [];
            $(element).jQCloud(words);
        }
    };
});

require(['knockout'], function (ko) {
    ko.components.register("c1",
        {
            viewModel: { require: "components/c1/c1" },
            template: { require: "text!components/c1/c1_view.html" }
        });
    ko.components.register("c2",
        {
            viewModel: { require: "components/c2/c2" },
            template: { require: "text!components/c2/c2_view.html" }
        });
    ko.components.register("c3",
        {
            viewModel: { require: "components/c3/c3" },
            template: { require: "text!components/c3/c3_view.html" }
        });
});



require(['knockout'], function (ko) {

    var vm = (function () {
        var links = [
            { name: 'Link 1', comp: 'c1' },
            { name: 'Link 2', comp: 'c2' },
            { name: 'Link 3', comp: 'c3' }
        ];
        var currentComp = ko.observable('c1');

        var isActive = function(menu) {
            if(menu.comp === currentComp()) {
                return 'active';
            }
            return '';
        }

        var change = function(menu) {
            currentComp(menu.comp);
        }

        return {
            links,
            currentComp,
            change,
            isActive
        };
    })();

    ko.applyBindings(vm);
});