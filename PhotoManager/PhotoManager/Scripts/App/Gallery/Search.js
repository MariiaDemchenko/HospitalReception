(function ($) {
    $(function () {
        $.searchPhotos = function (templatePath, searchKey) {
            $.initialize();

            function getUri(keyWord) {
                return keyWord !== "" ? "/api/photos/search/" + keyWord : "/api/photos";
            }

            load(getUri(searchKey));

            function load(uri) {
                $.ajax(uri)
                    .done(function (photos) {
                        var template;
                        var data = {};
                        if (photos.length > 0) {
                            $.displayPhotos(templatePath, photos);
                        } else {
                            $.get(templatePath, function (templates) {
                                template = $(templates).filter('#photoAlbumEmptyTemplate').html();
                                var output = Mustache.render(template, data);
                                document.getElementById('content').innerHTML = output;
                                $.stopSpinning();
                            });
                        }
                    });
            }

            $(".btn-search-gallery").on("click", function () {
                var keyWord = $("#KeyWord").val();
                load(getUri(keyWord));
            });
        }
    });
})(jQuery);