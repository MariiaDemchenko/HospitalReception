(function ($) {
    $.loadAlbums = function (templatePath) {
        $.ajax("/api/albums")
            .done(function (albums) {
                $.get(templatePath,
                    function (templates) {
                        var template = $(templates).filter('#albumsTemplate').html();

                        var data = {};
                        data.albums = albums;

                        var output = Mustache.render(template, data);
                        document.getElementById('content').innerHTML = output;
                        $.stopSpinning();
                    });
            });
    };


    $(document).on("click", ".btn-search",
        function () {
            var searchKey = $("#KeyWord").val();
            var uri = searchKey !== "" ? "gallery/search/" + searchKey : "gallery";
            window.location.href = uri;
        });
})(jQuery);