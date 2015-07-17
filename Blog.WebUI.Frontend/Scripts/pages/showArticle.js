(function ($) {

    var IndexPage = function () {

        var that = this;

        this.$finalTitle = undefined;

        this.initialize = function () {
            this.$finalTitle = $("#divText");
        };

        this.loadFormattedText = function () {

            var articleId = this.$finalTitle.data('article-id');
            var xhr = $.ajax({
                url: "/Home/GetFormattedText",
                dataType: "json",
                type: "POST",
                data: {
                    id: articleId
                }
            });

            xhr.done(function (data) {
                var decodedText = unescape(data.formattedText);
                var replacedText = decodedText.split('+').join(' ');

                var htmlText = $.parseHTML(replacedText);
                that.$finalTitle.append(htmlText);
            });
        };

    };

    $(function () {
        var page = new IndexPage();
        page.initialize();
        page.loadFormattedText();
    });


})(jQuery);