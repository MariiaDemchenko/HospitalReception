(function ($) {
    $(function () {
        var templatePathLoadPhotos = "/Content/Templates/Gallery/Index.html";
        var photoAlbumEmptyTemplateId = "#photoAlbumEmptyTemplate";
        advancedSearchPhotos();

        function advancedSearchPhotos() {

            var pageSize = 9;
            var pageIndex = 0;

            $.setScroll(load);

            load();

            function load() {
                var serializedData = $("#formAdvancedSearch").serializeFormJSON();
                var pageViewModel = { pageIndex: pageIndex, pageSize: pageSize };

                $.ajax({
                    url: '/api/photos/advancedSearch',
                    data: { photoViewModel: serializedData, scrollViewModel: pageViewModel },
                    error: function () {
                        location.href = "/gallery/error/index";
                    }
                }).done(function (photos) {
                    var template;
                    var data = {};
                    if (photos.Items.length > 0) {
                        $.displayPhotoAlbum(templatePathLoadPhotos, photos.Items);
                        pageIndex++;
                    }
                    data.Counter = photos.TotalCount === 0 ? "There are no photos matching the given parameters" : "Advanced search result: " + photos.TotalCount + " photos were found";
                    $.get(templatePathLoadPhotos,
                        function (templates) {
                            template = $(templates).filter(photoAlbumEmptyTemplateId).html();
                            var output = Mustache.render(template, data);
                            document.getElementById("counter").innerHTML = output;
                            $.stopSpinning();
                        });
                });
            }

            window.onpopstate = function () {
                location.reload();
            };

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