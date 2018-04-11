(function ($) {
    $.loadAlbums = function (templatePath) {
        $.ajax("/api/Albums")
            .done(function (albums) {
                $.get(templatePath,
                    function (templates) {
                        var template = $(templates).filter('#albumsTemplate').html();

                        var data = {};
                        data.albums = albums;

                        var output = Mustache.render(template, data);
                        document.getElementById('content').innerHTML = output;
                    });
            });
    };

    $(document).on("click", ".album .photo-frame",
        function () {
            var albumId = $(this).attr("albumId");
            window.location.href = "albums/" + albumId;
        });

    $(document).on("click", ".btn-search",
        function () {
            var searchKey = $("#KeyWord").val();
            var uri = searchKey !== "" ? "gallery/search/" + searchKey : "gallery";
            window.location.href = uri;
        });
})(jQuery);