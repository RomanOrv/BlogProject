(function ($) {

    var IndexPage = function () {

        var that = this;

        this.$htmlEditor = undefined;
        this.$saveButton = undefined;

        this.initialize = function () {

            this.$htmlEditor = $('#id_cazary_full');
            this.$saveButton = $("#save-button");
            this.$htmlEditor.cazary({
                commands: "FULL"
            });

            this.$saveButton.on("click", this.onSaveButton);
        };

        this.onSaveButton = function () {
            var formattedText = that.$htmlEditor.val();
            var articleId = $(this).data('article-id');
            var articleTitle = $(this).data('article-title');
            var authorId = $(this).data('author-id');

            var xhr = $.ajax({
                url: "/Home/SaveFormattedText",
                dataType: "json",
                type: "POST",
                data: {
                    id: articleId,
                    formattedText: escape(formattedText)
                }
            });

            xhr.done(function (data) {
                console.log("saveButton_Click - done", arguments);
                window.location.href = "/Home/ShowArticle?title=" + articleTitle + "&id=" + data.Id + "&authorId=" + authorId;
            });
        };

    };

    $(function () {
        var page = new IndexPage();
        page.initialize();
    });


})(jQuery);