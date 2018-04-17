(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Photo/Edit.html";

        $.addPhoto = function (uri) {
            $.ajax(uri)
                .done(function (photo) {
                    $.displayPhoto(templatePath, photo);
                });

            $('input[type=file]').change(function () {
                if (event.target.files.length > 0) {
                    var tmppath = URL.createObjectURL(event.target.files[0]);
                    $("#displayedImage").fadeIn("fast").attr('src', tmppath);
                } else {
                    $("#displayedImage").fadeIn("fast").attr('src', "/api/image/0");
                }
            });

            $("#formAdd").submit(function (e) {
                e.preventDefault();

                var serializedData = new FormData();
                var fileInput = document.getElementById('file');

                if (fileInput.files.length === 0) {
                    return false;
                }

                var file = fileInput.files[0];

                serializedData.append('Image', file);
                serializedData.append("ViewModel", $(this).serializeFormJSON());

                $.ajax({
                    url: '/api/photos/',
                    type: "POST",
                    data: serializedData,
                    processData: false,
                    contentType: false
                }).done(function (photo) {
                    $.goToAlbum(photo.AlbumId);
                });
            });
        }
    });
})(jQuery);