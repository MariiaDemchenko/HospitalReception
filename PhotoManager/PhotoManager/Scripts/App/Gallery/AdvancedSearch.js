(function ($) {
    $(function () {
        var templatePathLoadPhotos = "/Content/Templates/Gallery/Index.html";
        advancedSearchPhotos();
        var counterTemplate = "/Content/Templates/Shared/Counter.html";
        var counterId = "#counterTemplate";

        function advancedSearchPhotos() {

            var pageSize = 9;
            var pageIndex = 0;

            $.setScroll(load);

            load();

            function load() {
                var serializedData = $("#formAdvancedSearch").serializeFormJSON();
                var pageViewModel = { pageIndex: pageIndex, pageSize: pageSize };

                $.ajax({
                    url: "/api/photos/advancedSearch",
                    data: { photoViewModel: serializedData, scrollViewModel: pageViewModel },
                    error: function () {
                        bootbox.alert("Error getting photos");
                        $.stopSpinning();
                    }
                }).done(function (photos) {
                    var template;
                    var data = {};
                    if (photos.Items.length > 0) {
                        $.each(photos.Items,
                            function (index, value) {
                                value.ShootDate =
                                    value.CreationDate === null ? "" : moment(value.CreationDate).format("DD/MM/YYYY");
                            });
                        $.displayPhotoAlbum(templatePathLoadPhotos, photos.Items);
                        pageIndex++;
                    }
                    data.Counter = photos.TotalCount === 0 ? "There are no photos matching the given parameters" : "Advanced search result: " + photos.TotalCount + " photos were found";
                    $.get(counterTemplate,
                        function (templates) {
                            template = $(templates).filter(counterId).html();
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