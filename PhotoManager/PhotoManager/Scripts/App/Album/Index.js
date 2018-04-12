(function ($) {
    $.loadPhotoAlbum = function (templatePath, uri) {
        $.initialize();
        $.ajax(uri)
            .done(function (album) {
                $.get(templatePath,
                    function (templates) {
                        var template = $(templates).filter('#photoAlbumHeaderTemplate').html();
                        var output = Mustache.render(template, album);
                        document.getElementById('albumHeader').innerHTML = output;
                        $.stopSpinning();
                    });
                $.get(templatePath, function (templates) {
                    var template = $(templates).filter('#photoAlbumTemplate').html();
                    var data = {};
                    data.photos = album.Photos;
                    $.each(data.photos,
                        function (key, value) {
                            var date = moment(new Date(value.CreationDate)).format("LLLL");
                            value.CreationDate = date;
                        });
                    var output = Mustache.render(template, data);
                    document.getElementById('content').innerHTML = output;
                    $.stopSpinning();
                });
            });
    };

    $(document).on("click", ".photo-frame",
        function () {
            var photoId = $(this).attr("photoId");
            window.open("/photos/" + photoId);
        });

    $(document).on("click", ".photo-footer",
        function () {
            if ($(this).hasClass("selected")) {
                $(this).removeClass("selected");
            } else {
                $(this).addClass("selected");
            }
            var selectedCount = document.getElementsByClassName("selected").length;
            $(".btn-edit").attr("disabled", selectedCount !== 1);
            $(".btn-remove-confirm").attr("disabled", selectedCount < 1);
        });
})(jQuery);