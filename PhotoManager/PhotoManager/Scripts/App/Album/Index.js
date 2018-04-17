(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Index.html";
        var headerTemplateId = "#photoAlbumHeaderTemplate";
        var contentId = "albumHeader";

        $.loadPhotoAlbum = function (uri, isAuthenticated) {
            $.hideMenu(isAuthenticated);
            $.initialize();

            $.ajax(uri)
                .done(function (album) {
                    $.get(templatePath, function (templates) {
                        var template = $(templates).filter(headerTemplateId).html();
                        var output = Mustache.render(template, album);
                        document.getElementById(contentId).innerHTML = output;
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