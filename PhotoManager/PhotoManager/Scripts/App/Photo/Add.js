(function ($) {
    $.addPhoto = function (templatePath, uri) {
        (function ($) {
            $.fn.serializeFormJSON = function () {
                var o = {};
                var a = this.serializeArray();
                $.each(a,
                    function () {
                        if (o[this.name]) {
                            if (!o[this.name].push) {
                                o[this.name] = [o[this.name]];
                            }
                            o[this.name].push(this.value || '');
                        } else {
                            o[this.name] = this.value || '';
                        }
                    });
                return JSON.stringify(o);
            };
        })(jQuery);

        $.ajax(uri)
            .done(function (photo) {
                $.get(templatePath,
                    function (templates) {
                        var template = $(templates).filter('#photoEditTemplate').html();
                        var output = Mustache.render(template, photo);
                        document.getElementById('content').innerHTML = output;
                    });
            });

        $('input[type=file]').change(function () {
            var tmppath = URL.createObjectURL(event.target.files[0]);
            $("#displayedImage").fadeIn("fast").attr('src', tmppath);
        });


        $("#formAdd").submit(function (e) {
            e.preventDefault();

            var serializedData = new FormData();
            var fileInput = document.getElementById('file');
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
})(jQuery);