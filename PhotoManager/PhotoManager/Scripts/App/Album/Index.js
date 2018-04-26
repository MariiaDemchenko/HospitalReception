(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Index.html";
        var headerTemplateId = "#photoAlbumHeaderTemplate";
        var contentId = "albumHeader";
        var pageSize = 9;
        var pageIndex = 0;
        var url;
        var userId;

        $.photosPage = {
            setPageIndex: function (newPageIndex) {
                $.hideMenu(userId);
                pageIndex = newPageIndex;
            },
            getData: function () {
                $.ajax({
                    url: url,
                    data: {
                        pageIndex: pageIndex,
                        pageSize: pageSize
                    }
                })
                    .done(function (album) {
                        if (album != null && album.Photos.length !== 0) {
                            $.displayPhotoAlbum(templatePath, album.Photos);
                            pageIndex++;
                        } else {
                            if (pageIndex === 0) {
                                var template;
                                var data = {};
                                $.get(templatePath,
                                    function (templates) {
                                        template = $(templates).filter('#photoAlbumEmptyTemplate').html();
                                        var output = Mustache.render(template, data);
                                        document.getElementById('content').innerHTML = output;
                                        $.stopSpinning();
                                    });
                            }
                        }

                        $.get(templatePath,
                            function (templates) {
                                var template = $(templates).filter(headerTemplateId).html();
                                var output = Mustache.render(template, album);
                                document.getElementById(contentId).innerHTML = output;
                            });
                    });
            }
        }

        $.loadPhotoAlbum = function (uri, id) {
            url = uri;
            userId = id;

            $.hideMenu(userId);
            $.initialize();
            $.setScroll($.photosPage.getData);
            $.photosPage.getData();
        };
    });
})(jQuery);