define(['knockout', 'store'], (ko, store) => {
    return function (params) {
        var currentUser = ko.observable(store.getState().selectedUser);
        //var showWordCloud = ko.observable(false);
        var home = () => {
            store.dispatch(store.actions.pageListTitle());
            store.dispatch(store.actions.pageListView());
        };

        store.dispatch(store.actions.changeTitle("user"));

        return {
            currentUser,
            home,
            //showWordCloud
        };
    }
});
