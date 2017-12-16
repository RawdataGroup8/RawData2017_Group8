define(['knockout'], function (ko) {
    return function (params) {
        var id = ko.observable(1234);
        var body = ko.observable("");
        var title = ko.observable("");


        var addQuestion = function () {
            var data = new FormData();
            data.append("id", id());
            data.append("body", body());
            data.append("title", title());
            console.log(data.entries());

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
        };

        return {
            id, body, title,
            addQuestion
        };

    }
});