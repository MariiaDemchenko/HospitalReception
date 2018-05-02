(function ($) {
    $(function () {
        $("#formEdit").submit(function (e) {
            e.preventDefault();
            var serializedData = $(this).serialize();

            $.ajax({
                url: '/api/photos/',
                type: "PUT",
                data: serializedData,
                error: function () {
                    location.href = "/photos/error/edit";
                }
            }).done(function (albumId) {
                $.goToAlbum(albumId);
            });
        });
    });
})(jQuery);