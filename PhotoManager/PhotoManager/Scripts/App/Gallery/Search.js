(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Gallery/Index.html";

        $.searchPhotos = function(searchKey) {
            var currentUrl = getUri(searchKey);

            $("#KeyWord").val(searchKey);
            $.initialize();

            $(window).scroll(function() {
                var scrollTop = $.getScrollTop();
                if (scrollTop === $(document).height() - $(window).height()) {
                    load(currentUrl);
                }
            });

            function getUri(keyWord) {
                return "/api/photos/search/" + $.trim(keyWord);
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
                        },
                        error: function() {
                            location.href = "/gallery/error/search";
                        }
                    })
                    .done(function(photos) {
                        if (photos !== null && photos.length !== 0) {
                            $.displayPhotoAlbum(templatePath, photos);
                            pageIndex++;
                        } else {
                            if (pageIndex === 0) {
                                var template;
                                var data = {};
                                $.get(templatePath,
                                    function(templates) {
                                        template = $(templates).filter('#photoAlbumEmptyTemplate').html();
                                        var output = Mustache.render(template, data);
                                        document.getElementById('content').innerHTML = output;
                                        $.stopSpinning();
                                    });
                            }
                        }
                    });

                window.onpopstate = function() {
                    window.location.reload();
                };
            }

            $(".btn-search-gallery").on("click",
                function() {
                    var keyWord = $("#KeyWord").val();
                    pageIndex = 0;
                    pageSize = 9;
                    currentUrl = getUri(keyWord);
                    currentLocation = "/gallery/search/" + keyWord;
                    $("#content").empty();
                    history.pushState(null, null, currentLocation);
                    load(currentUrl);
                });

            $(this).keypress(function(e) {
                var keycode = e.keyCode || e.charCode || e.which; //for cross browser
                if (keycode === 13) //keyCode for enter key
                {
                    $(".btn-search-gallery").click();
                    return false;
                }
            });
        };
    });
})(jQuery);