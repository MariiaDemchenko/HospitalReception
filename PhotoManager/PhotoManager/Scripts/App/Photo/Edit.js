(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Photo/Edit.html";
        
        $.editPhoto = function (uri) {
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
                }).done(function (albumId) {
                    $.goToAlbum(albumId);
                });
            });
        }
    });
})(jQuery);