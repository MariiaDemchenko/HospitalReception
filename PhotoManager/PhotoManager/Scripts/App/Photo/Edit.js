(function ($) {
    $(function () {
        $.editPhoto = function (templatePath, uri) {
            $.ajax(uri)
                .done(function (photo) {
                    $.displayPhoto(templatePath, photo);
                });

            $("#formEdit").submit(function (e) {
                e.preventDefault();
                var serializedData = $(this).serialize();

                $.ajax({
                    url: '/api/photos/',
                    type: "PUT",
                    data: serializedData
                }).done(function (photo) {
                    $.goToAlbum(photo.AlbumId);
                });
            });
        }
    });
})(jQuery);