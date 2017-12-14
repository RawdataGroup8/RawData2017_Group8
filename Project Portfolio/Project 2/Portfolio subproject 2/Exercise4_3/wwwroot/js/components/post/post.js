define(['knockout', 'store'], (ko, store) => {
    return function (params) {
        var currentPost = ko.observable(store.getState().selectedPost);
        
        var home = () => {
            store.dispatch(store.actions.pageListTitle());
            store.dispatch(store.actions.pageListView());
        };

        store.dispatch(store.actions.changeTitle("Post"));

        return {
            currentPost,
            home
        };
    }
});