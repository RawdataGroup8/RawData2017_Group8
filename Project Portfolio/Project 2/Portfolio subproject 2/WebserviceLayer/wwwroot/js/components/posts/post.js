define(['knockout', 'store'], (ko, store) => {
    return function (params) {
        var currentPost = ko.observable(store.getState().selectedPost);

        var home1= () => {
            store.dispatch(store.actions.pageListTitle());
            store.dispatch(store.actions.pageListView1());
        };

        store.dispatch(store.actions.changeTitle("post"));

        return {
            currentPost,
            home
        };
    }
});