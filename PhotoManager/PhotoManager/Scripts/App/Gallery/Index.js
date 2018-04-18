(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Gallery/Index.html";
        var uri = "/api/photos";

        $.loadPhotos = function (isAuthenticated) {
            $.hideMenu(isAuthenticated);
            $.initialize();

            $(window).scroll(function () {
                var scrollTop = $.getScrollTop();
                if (scrollTop == $(document).height() - $(window).height()) {
                    getData();
                }
            });

            var pageIndex = 0;
            var pageSize = 9;

            getData();

            function getData() {
                $.ajax({
                        url: "/gallery",
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
            
        }
    });
})(jQuery);