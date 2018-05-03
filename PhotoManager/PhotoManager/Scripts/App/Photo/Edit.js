(function ($) {
    $(function () {
        $("#formEdit").submit(function (e) {
            e.preventDefault();
            if (!$(this).valid()) {
                return false;
            }
            var serializedData = $(this).serialize();
            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                headers: { __RequestVerificationToken: token },
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