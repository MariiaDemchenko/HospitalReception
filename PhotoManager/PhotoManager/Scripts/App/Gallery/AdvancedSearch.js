(function ($) {
    $(function () {
        var templatePathAdvancedSearch = "/Content/Templates/Gallery/AdvancedSearch.html";
        var templatePathPhotoAlbum = "/Content/Templates/Album/Index.html";
        var templatePathLoadPhotos = "/Content/Templates/Gallery/Index.html";
        var photoTemplateId = "#photoTemplate";
        var photoAlbumEmptyTemplateId = "#photoAlbumEmptyTemplate";
        var searchFormContentId = "searchForm";
        var contentId = "content";

        $.advancedSearchPhotos = function () {
            $.ajax("/api/photos/advancedSearch")
                .done(function (photo) {
                    $.get(templatePathAdvancedSearch,
                        function (templates) {
                            var template = $(templates).filter(photoTemplateId).html();
                            var output = Mustache.render(template, photo);
                            document.getElementById(searchFormContentId).innerHTML = output;
                        });
                });

            $.ajax("/api/photos")
                .done(function (photos) {
                    var template;
                    var data = {};
                    if (photos.length > 0) {
                        $.displayPhotoAlbum(templatePathLoadPhotos, photos);
                    } else {
                        $.get(templatePathLoadPhotos, function (templates) {
                            template = $(templates).filter(photoAlbumEmptyTemplateId).html();
                            var output = Mustache.render(template, data);
                            document.getElementById(contentId).innerHTML = output;
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
                        $.displayPhotoAlbum(templatePathPhotoAlbum, photos);
                    } else {
                        $.get(templatePathPhotoAlbum, function (templates) {
                            template = $(templates).filter(photoAlbumEmptyTemplateId).html();
                            var output = Mustache.render(template, data);
                            document.getElementById(contentId).innerHTML = output;
                            $.stopSpinning();
                        });
                    }
                });
            });
        }
    });
})(jQuery);