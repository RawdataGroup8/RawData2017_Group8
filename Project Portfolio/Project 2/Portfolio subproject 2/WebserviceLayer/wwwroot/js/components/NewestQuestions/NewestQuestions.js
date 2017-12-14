define(['jquery','knockout'], function ($,ko) {
    return function (params) {
        var title = ko.observable("Newest Questions");
        var heading = ko.observable("New Questions");
        var posts = ko.observableArray([]);
        var nextLink = ko.observable();
        var prevLink = ko.observable();

        var next = () => {
            $.getJSON(nextLink(), data => {
                posts(data.items);
                nextLink(data.next);
                prevLink(data.prev);
            });
        };
        var canNext = ko.computed(() => {
            return nextLink() !== null;
        });

        var prev = () => {
            $.getJSON(prevLink(), data => {
                posts(data.items);
                nextLink(data.next);
                prevLink(data.prev);
            });
        };

        var canPrev = ko.computed(() => {
            return prevLink() !== null;
        });

        $.getJSON("api/posts/q", data => {
            posts(data.items);
            nextLink(data.next);
            prevLink(data.prev);
        });

        return {
            title,
            heading,
            next,
            canNext,
            prev,
            canPrev,
            posts
        };

    }
});