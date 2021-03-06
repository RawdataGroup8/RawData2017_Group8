﻿

require.config({

    baseUrl: "js",
    paths: {
        jquery: '../lib/jquery.min',
        knockout: '../lib/knockout/dist/knockout',
        text: '../lib/text/text',
        postman: 'services/postman',
        jqcloud: '../lib/jqcloud2/dist/jqcloud.min',
        dataservice: 'services/dataservice',
        store: 'services/store',
        redux: '../lib/redux'
    },
    shim: {
        jqcloud: {
            deps: ['jquery']
        }
    }
});

require(['knockout'], function (ko) {

    ko.components.register("new_quest", {
        viewModel: { require: "components/newest_questions/newest_questions" },
        template: { require: "text!components/newest_questions/newest_questions_view.html" }
    });

    ko.components.register("show_quest", {
        viewModel: { require: "components/question/question" },
        template: { require: "text!components/question/question_view.html" }
    });
    ko.components.register("show_user", {
        viewModel: { require: "components/userposts/userposts" },
        template: { require: "text!components/userposts/userposts_view.html" }
    });

    ko.components.register("wordcloud", {
        viewModel: { require: "components/wordcloud/wordcloud" },
        template: { require: "text!components/wordcloud/wordcloud.html" }
    });

    ko.components.register("add_quest", {
        viewModel: { require: "components/add_question/add_question" },
        template: { require: "text!components/add_question/add_question_view.html" }
    });

    ko.components.register("all_users", {
        viewModel: { require: "components/user/user" },
        template: { require: "text!components/user/user_view.html" }
    });

    ko.components.register("search", {
        viewModel: { require: "components/search/search" },
        template: { require: "text!components/search/search_view.html" }
    });
});

var states = [];
require(['knockout', 'store'], function (ko, store) {

    // show the state everytime it is updated
    store.subscribe(() => {
        console.log(store.getState());
    });

    var vm = (function () {
        var title = ko.observable();
        var currentView = ko.observable();

        store.subscribe(() => {
            title(store.getState().title);
            currentView(store.getState().view);
        });

        store.dispatch(store.actions.pageListTitle());
        store.dispatch(store.actions.pageListView());

        var wordcloudView = function () {
            store.dispatch(store.actions.changeTitle('Wordcloud'));
            store.dispatch(store.actions.changeView('wordcloud'));
        }
        var addQuestion = function () {
            store.dispatch(store.actions.changeTitle('New Question'));
            store.dispatch(store.actions.changeView('add_quest'));
        }
        // I added this post is related with getting post of each user, I tried to use the show_quest but i couldnot make it!

        /*var user = function () {
            store.dispatch(store.actions.changeTitle('User'));
            store.dispatch(store.actions.changeView('show_user'));
        }*/

        var allUsers = function () {
            store.dispatch(store.actions.changeTitle('Users'));
            store.dispatch(store.actions.changeView('all_users'));
        }
        var search = function () {
            store.dispatch(store.actions.changeTitle('Search'));
            store.dispatch(store.actions.changeView('search'));
        }
        var new_quest = function () {
            store.dispatch(store.actions.changeTitle('Questions'));
            store.dispatch(store.actions.changeView('new_quest'));
        }

        var back = function () {
            console.log(store.getState().history[store.getState().history.length - 1]);
            store.dispatch(store.actions.useHistory(store.getState().history[store.getState().history.length - 1]));
        }

        return {
            title,
            currentView,
            wordcloudView,
            addQuestion,
            new_quest,
            allUsers,
            search,
            //user
            //post,
            back
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
