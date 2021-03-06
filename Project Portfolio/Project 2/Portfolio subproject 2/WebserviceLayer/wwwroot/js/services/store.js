﻿/*

This is a simplified version of Redux https://redux.js.org/

In Redux the idea is to create a store by givining it the state and reducer/reducers it shound use,
here it is merged into a single fixed object :-)

*/

define(['redux'], function (redux) {
    const CHANGE_TITLE = "CHANGE_TITLE";
    const CHANGE_VIEW = "CHANGE_VIEW";
    const SELECT_POST = "SELECT_POST";
    const SELECT_USER = "SELECT_USER";
    const USE_HISTORY = "USE_HISTORY";

    // this is the state we want to handle
    var currentState = {

    };

    // keep track of subscribers
    var currentListeners = [];

    // The reducer used in this application
    // The main idea is not to modify the state, but create a new copy
    // every time it is changed
    var reducer = function(state, action) {

        var nextState = Object.assign({}, state, { history: [...(state.history ? state.history : []), state] });

        switch (action.type) {
            case CHANGE_TITLE:
                return Object.assign({}, nextState, { title: action.title });
            case CHANGE_VIEW:
                return Object.assign({}, nextState, { view: action.componentName });
            case SELECT_POST:
                return Object.assign({}, nextState, { selectedPost: action.post });
            case SELECT_USER:
                return Object.assign({}, nextState, { selectedUser: action.user });
            case USE_HISTORY:
                return  action.state;

                //[...(history ? history : []), state]
                //(hiatory ? history.map(e => e) : []), push(state);

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

        // page list view for user

        pageListView1: function () {
            return {
                type: CHANGE_VIEW,
                componentName: 'all_users'
            }
        },

        selectPost: function (post) {
            return {
                type: SELECT_POST,
                post
            }
        },

        selectUser: function (user) {
            return {
                type: SELECT_USER,
                user
            }
        }, 

        saveHistory: function () {
            return {
                type: SAVE_HISTORY
            }
        },
        useHistory: function (state) {
            return {
                type: USE_HISTORY,
                state
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
    var reduxstore = redux.createStore(reducer, {});

    return {
        getState: reduxstore.getState,
        dispatch: reduxstore.dispatch,
        subscribe: reduxstore.subscribe,
        actions
    };
});