(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Gallery/Index.html";
        var uri = "/api/photos";

        $.loadPhotos = function (isAuthenticated) {
            $.hideMenu(isAuthenticated);
            $.initialize();
            
            $.ajax(uri)
                .done(function (photos) {
                    $.displayPhotoAlbum(templatePath, photos);
                });
        }
    });
})(jQuery);