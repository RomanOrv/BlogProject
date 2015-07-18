(function ($) {

    var IndexPage = function () {

        var that = this;

        this.$finalTitle = undefined;
        this.$imgProf = undefined;
       var articleId = 0;

        this.initialize = function () {
            this.$finalTitle = $("#divText");
            this.$imgProf = $("#imgProfile");
            articleId = this.$finalTitle.data('article-id');
        };

        this.setImageSrc = function () {

            var xhr = $.ajax({
                url: "/Account/GetProfileImagePath",
                dataType: "json",
                type: "GET",
                data: {
                    id: articleId
                }
            });

            xhr.done(function (data) {
                that.$imgProf.attr("src", "/Images/" + data.imgSrc);
            });
        }

        this.loadFormattedText = function () {

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
        page.setImageSrc();
        page.loadFormattedText();
    });


})(jQuery);