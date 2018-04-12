(function ($) {
    $.editPhoto = function (templatePath, uri) {
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

        $("#formEdit").submit(function (e) {
            e.preventDefault();

            var serializedData = $(this).serialize();

            $.ajax({
                url: '/api/photos/',
                type: "PUT",
                data: serializedData
            }).done(function (photo) {
                var url = photo.AlbumId === 0 ? "/gallery" : "/albums/" + photo.AlbumId;
                window.location.href = url;
            });
        });
    }
})(jQuery);