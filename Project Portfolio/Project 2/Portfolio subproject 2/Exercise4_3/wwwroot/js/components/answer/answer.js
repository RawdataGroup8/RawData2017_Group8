define(['knockout', 'store'], (ko, store) => {
    return function (params) {
        var body = params.body;
        var home = () => {
            store.dispatch(store.actions.pageListTitle());
            store.dispatch(store.actions.pageListView());
        };

        return {
            home,
            body
        };
    }
});