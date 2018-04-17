(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Home/Index.html";
        var templateId = "#albumsTemplate";
        var contentId = "content";

        $.loadAlbums = function () {
            $.ajax("/api/albums")
                .done(function (albums) {
                    $.get(templatePath,
                        function (templates) {
                            var template = $(templates).filter(templateId).html();

                            var data = {};
                            data.albums = albums;

                            var output = Mustache.render(template, data);
                            document.getElementById(contentId).innerHTML = output;
                            $.stopSpinning();
                        });
                });
        };
    });
})(jQuery);