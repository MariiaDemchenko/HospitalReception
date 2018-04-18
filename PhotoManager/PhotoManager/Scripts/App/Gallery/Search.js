(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Gallery/Index.html";

        $.searchPhotos = function (searchKey) {
            var currentUrl = getUri(searchKey);
            $.initialize();

            $(window).scroll(function () {
                var scrollTop = $.getScrollTop();
                if (scrollTop == $(document).height() - $(window).height()) {
                    load(currentUrl);
                }
            });

            function getUri(keyWord) {
                return "/api/photos/search/" + keyWord;
            }

            var pageIndex = 0;
            var pageSize = 9;

            load(currentUrl);

            function load(uri) {
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
                        } else {
                            if (pageIndex === 0) {
                                var template;
                                var data = {};
                                $.get(templatePath, function (templates) {
                                    template = $(templates).filter('#photoAlbumEmptyTemplate').html();
                                    var output = Mustache.render(template, data);
                                    document.getElementById('content').innerHTML = output;
                                    $.stopSpinning();
                                });
                            }
                        }
                    });
            }

            $(".btn-search-gallery").on("click", function () {
                var keyWord = $("#KeyWord").val();
                pageIndex = 0;
                pageSize = 9;
                currentUrl = getUri(keyWord);
                $("#content").empty();
                load(currentUrl);
            });
        }
    });
})(jQuery);