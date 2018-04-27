(function ($) {
    $(function () {
        $.editPhoto = function() {
            $("#formEdit").submit(function(e) {
                e.preventDefault();
                var serializedData = $(this).serialize();

                $.ajax({
                    url: '/api/photos/',
                    type: "PUT",
                    data: serializedData
                }).done(function(albumId) {
                    $.goToAlbum(albumId);
                });
            });
        };
    });
})(jQuery);