(function ($) {
    $.loadPhotos = function (templatePath, searchKey) {
        $.initialize();

        function getUri(keyWord) {
            return keyWord !== "" ? "/api/photos/search/" + keyWord : "/api/photos";
        }
        
        load(getUri(searchKey));

        function load(uri) {
            $.ajax(uri)
                .done(function (photos) {
                    $.get(templatePath,
                        function (templates) {
                            var template = $(templates).filter('#photoAlbumTemplate').html();
                            var data = {};
                            data.photos = photos;
                            data.AlbumId = 0;
                            var output = Mustache.render(template, data);
                            document.getElementById('content').innerHTML = output;
                        });
                });
        }

        $(".btn-search-gallery").on("click",
            function () {
                var keyWord = $("#KeyWord").val();
                load(getUri(keyWord));
            });
    }
})(jQuery);