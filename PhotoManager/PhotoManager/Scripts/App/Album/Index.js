(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Index.html";
        var headerTemplateId = "#photoAlbumHeaderTemplate";
        var contentId = "albumHeader";
        
        $.loadPhotoAlbum = function (uri, userId) {
            $.setScroll(getData);
            
            var pageIndex = 0;
            var pageSize = 9;

            $.hideMenu(userId);
            $.initialize();
            getData();

            function getData() {
                $.ajax({
                    url: uri,
                    data: {
                        pageIndex: pageIndex,
                        pageSize: pageSize
                    }
                })
                    .done(function (album) {
                        if (album != null && album.Photos.length !== 0) {
                            $.displayPhotoAlbum(templatePath, album.Photos);
                            pageIndex++;
                        }

                        $.get(templatePath, function (templates) {
                            var template = $(templates).filter(headerTemplateId).html();
                            var output = Mustache.render(template, album);
                            document.getElementById(contentId).innerHTML = output;
                        });
                    });
            }
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