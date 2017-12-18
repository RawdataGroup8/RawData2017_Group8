define(['knockout', 'store'], (ko, store) => {
    return function (params) {
        var currentPost = ko.observable(store.getState().selectedPost);
        //var showWordCloud = ko.observable(false);
        var test = ko.observable(false);

        /*var toggleVisibility = function () {
            test = !test();
            alert('test is now ' + test());
        };*/

        var home = () => {
            store.dispatch(store.actions.pageListTitle());
            store.dispatch(store.actions.pageListView());
        };

        store.dispatch(store.actions.changeTitle("Post"));

        return {
            currentPost,
            home,
            test
            //toggleVisibility
        };
    }
});

/*require(['jquery', 'knockout'], ($, ko) => {


        var title = ko.observable("Show Question");
      
        var currentPost = ko.observable();

        var showPost = (data) => {
            $.getJSON(data.link, postData => {
                var post = {
                    title: postData.title,
                    score: postData.score,
                    creationDate: postData.creationDate,
                    body: postData.body
                }

                $.getJSON(postData.answers, ans => {
                    post.answers = ko.observableArray(ans);
                    currentPost(post);
                });
            });
            title("Post");
            //currentView('postview');
        };

        var home = () => {
            title("Show Posts");
            //currentView('postlist');
        };

        return {
            title,
            //currentView,
            showPost,
            currentPost,
            home
        };
});*/