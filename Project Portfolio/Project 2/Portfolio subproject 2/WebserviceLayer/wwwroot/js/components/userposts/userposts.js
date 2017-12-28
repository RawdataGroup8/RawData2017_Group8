
define(['knockout', 'store'], (ko, store) => {
    return function (params) {
        var currentUser = ko.observable(store.getState().selectedUser);
        console.log(currentUser);


        var home = () => {
            store.dispatch(store.actions.pageListTitle());
            store.dispatch(store.actions.pageListView1());
        };

        //store.dispatch(store.actions.changeTitle("Users info"));

        return {
            currentUser,
            home

        };
    }
});