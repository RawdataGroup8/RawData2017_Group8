define(['knockout', 'dataservice'], function (ko, dataservice) {
    return function (params) {
        var title = ko.observable("Wordcloud");
        var terms = ko.observable("test");

        var words = ko.observableArray([
            {text: "wordcloud", weight: 10}
            /*{ text: "Lorem", weight: 13 },
            { text: "Ipsum", weight: 10.5 },
            { text: "Dolor", weight: 9.4 },
            { text: "Sit", weight: 8 },
            { text: "Amet", weight: 6.2 },
            { text: "Consectetur", weight: 5 },
            { text: "Adipiscing", weight: 5 },
            /* ... */
        ]);

        var getRankingForWords = function(url) {
            dataservice.RankedWordsSearch(url, data => {
                    console.log(data);
                    words(data);
                });
        };

        var shearchWords = function() {
            getRankingForWords(terms());
        };

//        var getWordsTf = function (url) {
//            dataservice.getTfWords(url, data => {
//                console.log(data);
//                words(data);
//            });
//        };
//
//        var changeWords = function() {
//            getWordsTf(19);
//        };


        return {
            title,
            words,
            shearchWords,
            terms
        };
    }
});