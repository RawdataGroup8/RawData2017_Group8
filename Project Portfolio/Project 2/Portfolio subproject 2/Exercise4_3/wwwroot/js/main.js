
require.config({
    baseUrl: "js",
    paths: {
        jquery: "../lib/jquery/dist/jquery.min",
        knockout: "../lib/knockout/dist/knockout",
        text: "../lib/text/text",
        dataservice: 'services/dataservice',
        postman: 'services/postman',
        store: 'services/store',
        redux: '../lib/redux'
    }
});

require(['knockout'], (ko) => {

    ko.components.register('post-list',
        {
            viewModel: { require: 'components/postlist/postlist' },
            template: { require: 'text!components/postlist/postlist_view.html' }
        });

    ko.components.register('post',
        {
            viewModel: { require: 'components/post/post' },
            template: { require: 'text!components/post/post_view.html' }
        });

    ko.components.register('answer',
        {
            viewModel: { require: 'components/answer/answer' },
            template: { require: 'text!components/answer/answer_view.html' }
        });
});


require(['knockout', 'store'],
    (ko, store) => {

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


            return {
                title,
                currentView
            };
        })();

        ko.applyBindings(vm);
    });

