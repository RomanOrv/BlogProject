(function ($) {

    var IndexPage = function () {

        var that = this;

        this.$finalTitle = undefined;
        this.$imgProf = undefined;
        this.$userPlace = undefined;

        this.initialize = function () {
            this.$finalTitle = $("#divText");
            this.$imgProf = $("#imgProfile");
            this.$userPlace = $("#userPlace");
            
           
        };

        //this.setImageSrc = function () {
        //   var authorId = this.$finalTitle.data('author-id');

        //    var xhr = $.ajax({
        //        url: "/Account/GetProfileImagePath",
        //        dataType: "json",
        //        type: "GET",
        //        data: {
        //            id: authorId
        //        }
        //    });

        //    xhr.done(function (data) {
        //        that.$imgProf.attr("src", "/Images/" + data.imgSrc);
        //        that.$userPlace.html("<b>" + data.userName + "</b>");
        //    });
        //}

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
       // page.setImageSrc();
        page.loadFormattedText();
    });


})(jQuery);