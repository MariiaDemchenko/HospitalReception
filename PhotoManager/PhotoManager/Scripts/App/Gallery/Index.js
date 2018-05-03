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
                        if (photos.Items !== null && photos.Items.length !== 0) {
                            $.displayPhotoAlbum(templatePath, photos.Items);
                            pageIndex++;
                        }
                        var template;
                        var data = {};
                        data.Counter = photos.TotalCount === 0 ? "There are no photos in the gallery" : "Total photos count: " + photos.TotalCount;
                        $.get(templatePath,
                            function (templates) {
                                template = $(templates).filter('#photoAlbumEmptyTemplate').html();
                                var output = Mustache.render(template, data);
                                document.getElementById('counter').innerHTML = output;
                                $.stopSpinning();
                            });
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