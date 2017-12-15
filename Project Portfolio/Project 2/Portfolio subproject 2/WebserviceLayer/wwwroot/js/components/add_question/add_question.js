define(['knockout'], function (ko) {
    return function (params) {
        var viewModel = {
            doSomething: function (formElement) {
                // ... now do something
            }
        };
        var doSomething = (formElement) => {
            
        }
        var showPost = (data) => {
            dataservice.getPost(data.link, post => {
                store.dispatch(store.actions.selectPost(post));
                store.dispatch(store.actions.changeView("show_quest"));
            });
        };

        var addQuestion = function (formElement) {
            //var data = ko.toJSON(this);
            $.ajax({
                url: "/api/posts/add",
                type: "POST",
                data: formElement,
                datatype: "json",
                processData: false,
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    alert(result);
                }
            });
        };

        return {
            addQuestion
        };

    }
});