(function ($) {
    $(function () {
        $.loadPhotoAlbum = function (templatePath, uri, isAuthenticated) {
            if (isAuthenticated === "True") {
                $("#content").addClass("selectable");
            } else {
                $("#content").removeClass("selectable");
            }

            $.initialize();
            $.ajax(uri)
                .done(function (album) {
                    $.get(templatePath, function (templates) {
                        var template = $(templates).filter('#photoAlbumHeaderTemplate').html();
                        var output = Mustache.render(template, album);
                        document.getElementById('albumHeader').innerHTML = output;
                    });
                    $.displayPhotoAlbum(templatePath, album.Photos);
                });
        };

        $("#content").on("click", ".photo-footer",
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
    });
})(jQuery);