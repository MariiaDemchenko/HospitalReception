(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Gallery/Index.html";
        var uri = "/api/photos";
        var pageSize = 9;
        var pageIndex = 0;
        var userId;

        $.photosPage = {
            setPageIndex: function (newPageIndex) {
                $.hideMenu(userId);
                pageIndex = newPageIndex;
            },
            getData: function () {
                $.ajax({
                    url: uri,
                    data: {
                        pageIndex: pageIndex,
                        pageSize: pageSize
                    }
                })
                    .done(function (photos) {
                        if (photos != null && photos.length !== 0) {
                            $.displayPhotoAlbum(templatePath, photos);
                            pageIndex++;
                        }
                    });
            }
        };

        $.loadPhotos = function (id) {
            userId = id;
            $.hideMenu(userId);
            $.initialize();
            $.setScroll($.photosPage.getData);
            $.photosPage.getData();
        }
    });
})(jQuery);