define(['jquery', 'knockout'], function ($, ko) {

    var getPosts = (url, callback) => {
        if (url === undefined) {
            url = "api/posts/q";
        }
        $.getJSON(url, callback);
    };


    var searchPosts = (terms, callback) => {
        $.getJSON('api/search/'+terms, callback);
    };


    //for use with the wordcloud
    var getTfWords = (id, callback) => {
        $.getJSON('api/wordtf/' + id, callback);
    }
    
    //getting the users
    var getUsers = (url, callback) => {
        if (url === undefined) {
            url = "api/user";
        }
        $.getJSON(url, callback);
    };

    var addPost = function (data) {
        $.ajax({
            url: '/api/posts/add',
            data: data,
            processData: false,
            contentType: false,
            type: 'POST'
        }).done(function (retdata) {
            alert(retdata);
            //todo: select and show created post
        });
        /*if (url === undefined) {
            url = "api/posts/add";
        }
        $.getJSON(url, callback);*/
    }
    var getPost = function (url, callback) {
        $.getJSON(url, data => {
            var post = {
                title: data.title,
                score: data.score,
                creationDate: data.creationDate,
                body: data.body
            }

            $.getJSON(data.answers, ans => {
                post.answers = ko.observableArray(ans);
                callback(post);
            });
        });
    };

    var getUser = function (url, callback) {
        $.getJSON(url, data => {
            var post = {
                Postid: data.result.id,
                Body: data.data,
                
            }

            $.getJSON(data.linkp, pos => {
                post.linkp = ko.observableArray(pos);
                callback(post);
            });
           
        });
    };

   


    return {
        getUsers,
        getUser,
        getPosts,
        getPost,
        getTfWords,
        searchPosts, 
      //getuserp
    };
});