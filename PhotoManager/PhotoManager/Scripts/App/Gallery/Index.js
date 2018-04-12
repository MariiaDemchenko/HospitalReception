(function ($) {
    $(function () {
        $.loadPhotos = function (templatePath, uri) {
            $.initialize();

            $.ajax(uri)
                .done(function (photos) {
                    $.displayPhotos(templatePath, photos);
                });
        }
    });
})(jQuery);