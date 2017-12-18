
define(['knockout'], function(ko) {
    return function (params) {
        var title = ko.observable("Hello form KO component");
        //var value = params.value;
        console.log(params);
        return {
            title
        };

    }
});

