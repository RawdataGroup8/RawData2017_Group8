define(['knockout', 'dataservice', 'store'], (ko, dataservice, store) => {
    return function (params) {
        var title = ko.observable("Search Results");
        var posts = ko.observableArray([]);
        var nextLink = ko.observable();
        var prevLink = ko.observable();
        var terms = ko.observable("test");

        var getPosts = function () {
            dataservice.searchPosts(terms(), data => {
                posts(data.items);
                nextLink(data.next);
                prevLink(data.prev);
            });
        };

        var next = () => {
            getPosts(nextLink());
        };
        var canNext = ko.computed(() => {
            return nextLink() !== null;
        });

        var prev = () => {
            getPosts(prevLink());
        };

        var canPrev = ko.computed(() => {
            return prevLink() !== null;
        });

        var showPost = (data) => {
            dataservice.getPost(data.link, post => {
                store.dispatch(store.actions.selectPost(post));
                store.dispatch(store.actions.changeView("show_quest"));
            });
        };

        //getPosts("");

        return {
            posts,
            next,
            canNext,
            prev,
            canPrev,
            showPost,
            getPosts,
            title,
            terms
        };
    }
});

