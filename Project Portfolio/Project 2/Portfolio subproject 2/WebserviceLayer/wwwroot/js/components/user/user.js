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
        var showPost = (data) => {
            dataservice.getPost(data.url, post => {
                store.dispatch(store.actions.selectPost(post));
                store.dispatch(store.actions.changeView("show_quest"));
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