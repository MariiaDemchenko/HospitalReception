(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Gallery/Index.html";
        var counterTemplate = "/Content/Templates/Shared/Counter.html";
        var counterId = "#counterTemplate";

        $.searchPhotos = function (searchKey) {
            var currentUrl = getUri(searchKey);

            $("#KeyWord").val(searchKey);
            $.initialize();

            $(window).scroll(function () {
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
                    error: function () {
                        bootbox.alert("Error getting photos");
                        $.stopSpinning();
                    }
                })
                    .done(function (photos) {
                        if (photos.Items.length !== 0) {
                            $.each(photos.Items,
                                function (index, value) {
                                    value.ShootDate =
                                        value.CreationDate === null ? "" : moment(value.CreationDate).format("DD/MM/YYYY");
                                });
                            $.displayPhotoAlbum(templatePath, photos.Items);
                            pageIndex++;
                        }
                        var template;
                        var data = {};
                        data.Counter = photos.TotalCount === 0 ? "There are no photos matching the given keyword" : "Search result: " + photos.TotalCount + " photos were found";
                        $.get(counterTemplate,
                            function (templates) {
                                template = $(templates).filter(counterId).html();
                                var output = Mustache.render(template, data);
                                $("#counter").html(output);
                                $.stopSpinning();
                            });
                    });

                window.onpopstate = function () {
                    window.location.reload();
                };
            }

            $(".btn-search-gallery").on("click",
                function () {
                    var keyWord = $("#KeyWord").val();
                    pageIndex = 0;
                    pageSize = 9;
                    currentUrl = getUri(keyWord);
                    currentLocation = "/gallery/search/" + keyWord;
                    $("#content").empty();
                    history.pushState(null, null, currentLocation);
                    load(currentUrl);
                });

            $(this).keypress(function (e) {
                var keycode = e.keyCode || e.charCode || e.which;
                if (keycode === 13) {
                    $(".btn-search-gallery").click();
                    return false;
                }
            });
        };
    });
})(jQuery);