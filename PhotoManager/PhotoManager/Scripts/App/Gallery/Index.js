(function ($) {
    $(function () {
        $.loadPhotos = function (templatePath, uri, isAuthenticated) {
            if (isAuthenticated === "True") {
                $("#content").addClass("selectable");
            } else {
                $("#content").removeClass("selectable");
            }
            $.initialize();

            $.ajax(uri)
                .done(function (photos) {
                    $.displayPhotoAlbum(templatePath, photos);
                });
        }
    });
})(jQuery);