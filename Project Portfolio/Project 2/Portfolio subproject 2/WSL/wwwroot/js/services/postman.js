define([], function () {

    var subscribers = [];

    var subscribe = (event, callback) => {
        var subscriber = { event, callback };
        subscribers.push(subscriber);

        return function () {
            var index = subscribers.indexOf(subscriber);
            subscribers.slice(index, 1);
        };

    };

    var publish = (event, payload) => {
        subscribers.forEach(s => {
            if (s.event === event) s.callback(payload);
        });
    }

    var events =
        {
            changeView: "changeView"
        };

    return {
        subscribe,
        publish,
        events
    };

});