﻿define(['knockout', 'dataservice', 'store'], (ko, dataservice, store) => {
    return function (params) {
        var title = ko.observable("Users");
        var users = ko.observableArray([]);
        var nextLink = ko.observable();
        var prevLink = ko.observable();
        var name = ko.observable();
        var total = ko.observable();

        var getUsers = function (url) {
            dataservice.getUsers(url, data => {
                users(data.items);
                nextLink(data.next);
                prevLink(data.prev);
                name(data.items.name);
                total(data.pages);
            });
        };

        var next = () => {
            getUsers(nextLink());
        };
        var canNext = ko.computed(() => {
            return nextLink() !== null;
        });

        var prev = () => {
            getUsers(prevLink());
        };

        var canPrev = ko.computed(() => {
            return prevLink() !== null;
        });

        var showPost = (data) => {
            dataservice.getPost(data.url, post => {
                store.dispatch(store.actions.selectPost(post));
                store.dispatch(store.actions.changeView("all_users"));
            });
        };
       

          //getPosts();
          getUsers();

          return {
            getUsers,
            name,
            users,
            next,
            canNext,
            prev,
            canPrev,
            showPost,
            title,
            total
        };
    }
});