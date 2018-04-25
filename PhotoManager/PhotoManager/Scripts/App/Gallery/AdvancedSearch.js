(function ($) {
    $(function () {
        var templatePathAdvancedSearch = "/Content/Templates/Gallery/AdvancedSearch.html";
        var templatePathPhotoAlbum = "/Content/Templates/Album/Index.html";
        var templatePathLoadPhotos = "/Content/Templates/Gallery/Index.html";
        var photoTemplateId = "#photoTemplate";
        var photoAlbumEmptyTemplateId = "#photoAlbumEmptyTemplate";
        var searchFormContentId = "searchForm";
        var contentId = "content";

        $.advancedSearchPhotos = function () {

            var pageSize = 9;
            var pageIndex = 0;

            $.setScroll(load);

            load();

            function loadForm() {
                $.ajax("/api/photos/advancedSearchModel")
                    .done(function (photo) {
                        $.get(templatePathAdvancedSearch,
                            function (templates) {
                                var template = $(templates).filter(photoTemplateId).html();
                                var output = Mustache.render(template, photo);
                                document.getElementById(searchFormContentId).innerHTML = output;
                            })
                            .done(function () {
                                load();
                            });
                    });
            }

            function load() {
                var serializedData = $("#formAdvancedSearch").serializeFormJSON();
                var pageViewModel = { pageIndex: pageIndex, pageSize: pageSize };

                $.ajax({
                    url: '/api/photos/advancedSearch',
                    data: { photoViewModel: serializedData, scrollViewModel: pageViewModel }
                }).done(function (photos) {
                    var template;
                    var data = {};
                    if (photos.length > 0) {
                        $.displayPhotoAlbum(templatePathLoadPhotos, photos);
                        pageIndex++;

                    } else {
                        if (pageIndex !== 0) {
                            return;
                        }
                        $.get(templatePathLoadPhotos, function (templates) {
                            template = $(templates).filter(photoAlbumEmptyTemplateId).html();
                            var output = Mustache.render(template, data);
                            document.getElementById(contentId).innerHTML = output;
                            $.stopSpinning();
                        });
                    }
                });
            }

            window.onpopstate = function () {
                location.reload();
            }

            $("#formAdvancedSearch").submit(function (e) {
                e.preventDefault();
                pageIndex = 0;
                $("#content").empty();
                load();
                history.pushState(null, null, "/gallery/advancedSearch?" + $("#formAdvancedSearch").serialize());
            });
        }
    });
})(jQuery);