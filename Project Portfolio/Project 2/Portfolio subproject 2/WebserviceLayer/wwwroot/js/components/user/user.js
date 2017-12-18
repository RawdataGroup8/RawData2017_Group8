define(['knockout', 'dataservice', 'store'], (ko, dataservice, store) => {
    return function (params) {
        var title = ko.observable("Users");
        var users = ko.observableArray([]);
        var nextLink = ko.observable();
        var prevLink = ko.observable();
      //var total = data.total;
        

        var getUsers = function (url) {
            dataservice.getUsers(url, data => {
                users(data.data);
                nextLink(data.result.next);
                prevLink(data.result.prev);
                
               
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

        // this will be called for each user, and it will be defined in the dataservice.js
        var showUser = (data) => {
            dataservice.getUser(data.url, user => {
                store.dispatch(store.actions.selectUser(user));
                store.dispatch(store.actions.changeView("show_userpost"));
            });
        };
        
       

          //getPosts();
          getUsers();

          return {
            getUsers,
            //name,
            users,
            next,
            canNext,
            prev,
            canPrev,
            //showPost,
            title
            
            
        };
    }
});