(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Gallery/Index.html";
        var uri = "/api/photos";
        var pageSize = 9;
        var pageIndex = 0;

        $.photosPage = {
            setPageIndex: function (newPageIndex) {
                $.hideMenu();
                pageIndex = newPageIndex;
            },
            getData: function () {
                $.ajax({
                    url: uri,
                    data: {
                        pageIndex: pageIndex,
                        pageSize: pageSize
                    },
                    error: function () {
                        location.href = "/gallery/error/index";
                    }
                })
                    .done(function (photos) {
                        if (photos !== null && photos.length !== 0) {
                            $.displayPhotoAlbum(templatePath, photos);
                            pageIndex++;
                        }
                    });
            }
        };

        $(this).keypress(function (e) {
            var keycode = e.keyCode || e.charCode || e.which; //for cross browser
            if (keycode === 13) //keyCode for enter key
            {
                $(".btn-search").click();
                return false;
            }
        });

        loadPhotos();

        function loadPhotos() {
            $.hideMenu();
            $.initialize();
            $.setScroll($.photosPage.getData);
            $.photosPage.getData();
        }
    });
})(jQuery);