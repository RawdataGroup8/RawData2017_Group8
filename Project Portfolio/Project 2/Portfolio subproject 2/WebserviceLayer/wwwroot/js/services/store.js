/*

This is a simplified version of Redux https://redux.js.org/

In Redux the idea is to create a store by givining it the state and reducer/reducers it shound use,
here it is merged into a single fixed object :-)

*/

define(['redux'], function (redux) {
    const CHANGE_TITLE = "CHANGE_TITLE";
    const CHANGE_VIEW = "CHANGE_VIEW";
    const SELECT_POST = "SELECT_POST";

    // this is the state we want to handle
    var currentState = {

    };

    // keep track of subscribers
    var currentListeners = [];

    // The reducer used in this application
    // The main idea is not to modify the state, but create a new copy
    // every time it is changed
    var reducer = function (state, action) {
        switch (action.type) {
            case CHANGE_TITLE:
                return Object.assign({}, state, { title: action.title });
            case CHANGE_VIEW:
                return Object.assign({}, state, { view: action.componentName });
            case SELECT_POST:
                return Object.assign({}, state, { selectedPost: action.post });
            default:
                return state;
        }
    };
    
    // here we change the state by calling the reducer
    // and the notify all subscribers
    var dispatch = function (action) {
        currentState = reducer(currentState, action);
        currentListeners.forEach(listener => {
            listener();
        });
    };

    // subscribe to the state
    var subscribe = function (listener) {
        currentListeners.push(listener);

        return function () {
            var index = currentListeners.indexOf(listener);
            currentListeners.splice(index, 1);
        }
    };

    // get the current state
    var getState = () => currentState;
    
    // this is just helpers that create the actions we need
    var actions = {
        changeTitle: function (title) {
            return {
                type: CHANGE_TITLE,
                title
            }
        },
        pageListTitle: function (title) {
            return {
                type: CHANGE_TITLE,
                title: title
            }
        },
        changeView: function (componentName) {
            return {
                type: CHANGE_VIEW,
                componentName
            }
        },
        pageListView: function () {
            return {
                type: CHANGE_VIEW,
                componentName: 'new_quest'
            }
        },
        selectPost: function (post) {
            return {
                type: SELECT_POST,
                post
            }
        }
    };

    /*return {
        getState,
        dispatch,
        subscribe,
        actions
    };*/


    // uncomment above return
    // and use this instead to use the real Redux :-)
    var reduxstore = redux.createStore(reducer);

    return {
        getState: reduxstore.getState,
        dispatch: reduxstore.dispatch,
        subscribe: reduxstore.subscribe,
        actions
    };
});