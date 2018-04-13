(function ($) {
    $(function () {
        $.advancedSearchPhotos = function (templatePath, uri) {
            $.ajax("/api/photos/advancedSearch")
                .done(function (photo) {
                    $.get(templatePath,
                        function (templates) {
                            var template = $(templates).filter('#photoTemplate').html();
                            var output = Mustache.render(template, photo);
                            document.getElementById('searchForm').innerHTML = output;
                        });
                });

            $.ajax(uri)
                .done(function (photos) {
                    var template;
                    var data = {};
                    if (photos.length > 0) {
                        $.displayPhotoAlbum("/Content/Templates/photoSearchTemplate.html", photos);
                    } else {
                        $.get("/Content/Templates/photoSearchTemplate.html", function (templates) {
                            template = $(templates).filter('#photoAlbumEmptyTemplate').html();
                            var output = Mustache.render(template, data);
                            document.getElementById('content').innerHTML = output;
                            $.stopSpinning();
                        });
                    }
                });

            $("#formAdvancedSearch").submit(function (e) {
                e.preventDefault();

                var serializedData = $(this).serialize();

                $.ajax({
                    url: '/api/photos/advancedSearch',
                    type: "POST",
                    data: serializedData
                }).done(function (photos) {
                    var template;
                    var data = {};
                    if (photos.length > 0) {
                        $.displayPhotoAlbum("/Content/Templates/photoAlbumTemplate.html", photos);
                    } else {
                        $.get("/Content/Templates/photoAlbumTemplate.html", function (templates) {
                            template = $(templates).filter('#photoAlbumEmptyTemplate').html();
                            var output = Mustache.render(template, data);
                            document.getElementById('content').innerHTML = output;
                            $.stopSpinning();
                        });
                    }
                });
            });
        }
    });
})(jQuery);