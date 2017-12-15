define(['knockout', 'dataservice'], function (ko, dataservice) {
    return function (params) {
        var title = ko.observable("Wordcloud");

        var words = ko.observableArray([
            { text: "Lorem", weight: 13 },
            { text: "Ipsum", weight: 10.5 },
            { text: "Dolor", weight: 9.4 },
            { text: "Sit", weight: 8 },
            { text: "Amet", weight: 6.2 },
            { text: "Consectetur", weight: 5 },
            { text: "Adipiscing", weight: 5 },
            /* ... */
        ]);

        var changeWords = function () {
//            words([
//                { text: "Joe", weight: 13 },
//                { text: "Peter", weight: 10.5 },
//                { text: "Dolor", weight: 9.4 },
//                { text: "Sit", weight: 8 },
//                { text: "Amet", weight: 6.2 },
//                { text: "Consectetur", weight: 5 },
//                { text: "Adipiscing", weight: 5 },
//                /* ... */
//            ]);
            //words(dataservice.GetTfOfWordsInAPost(19));
            forEach(VARIABLE in dataservice.GetTfOfWordsInAPost(19))
            {
                words.add({ text: $parent.text, weight: $parent.weight })
            }
        }


        return {
            title,
            words,
            changeWords
        };
    }
});