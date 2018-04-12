(function ($) {
    $(function () {
        $.addPhoto = function (templatePath, uri) {
            $.ajax(uri)
                .done(function (photo) {
                    $.get(templatePath,
                        function (templates) {
                            var template = $(templates).filter('#photoEditTemplate').html();
                            var output = Mustache.render(template, photo);
                            document.getElementById('content').innerHTML = output;
                            $.stopSpinning();
                        });
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
                    var url = photo.AlbumId === 0 ? "/gallery" : "/albums/" + photo.AlbumId;
                    window.location.href = url;
                });
            });
        }
    });
})(jQuery);