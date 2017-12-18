define(['knockout', 'postman'], function (ko, postman) {
    return function (params) {
        var title = ko.observable("Hello from element");
        //console.log(params.value);

        var back = function() {
            postman.publish(postman.events.changeView, "mylist");
        };


        return {
            title,
            back
        };

    }
});