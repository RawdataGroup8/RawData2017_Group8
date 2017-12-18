define([], function() {
    var subscribers = [];

    var subscribe = function(event, callback) {
        var subscriber = { event, callback };
        subscribers.push(subscriber);

        return function() {
            subscribers = subscribers.filter(s => {
                if (s !== subscriber) return s;
            });
        }
    }

    var publish = function(event, data) {
        subscribers.forEach(s => {
            if (s.event === event) s.callback(data);
        });
    }

    var remove = function(event) {
        subscribers = subscribers.filter(s => {
            if (s.event !== event) return s;
        });
    }

    var events = {
        changeComponent: 'changeComponent',
        showPost: 'showPost'
    };

    return {
        subscribe,
        publish,
        remove,
        events
    };
});