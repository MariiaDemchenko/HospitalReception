(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Home/Index.html";
        var templateId = "#albumsTemplate";
        var contentId = "content";

        $.loadAlbums = function () {
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
                    url: "/api/albums",
                    data: { pageIndex: pageIndex, pageSize: pageSize }
                })
                    .done(function (albums) {

                        if (albums == null || albums.length === 0) {
                            return;
                        }
                        $.get(templatePath, function (templates) {
                                var template = $(templates).filter(templateId).html();

                                var data = {};
                                data.albums = albums;

                                var output = Mustache.render(template, data);
                                $("#content").append(output);
                                $.stopSpinning();
                            });
                        pageIndex++;
                    });
            }
        }
    });
})(jQuery);