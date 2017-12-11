define(['knockout'], function(ko) {
    return function (params) {
        var title = ko.observable("Component 3"); // the listing should be done like in 4.2
        return {
            title
       };
   } 
});