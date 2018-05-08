(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Gallery/Index.html";
        var uri = "/api/photos";
        var pageSize = 9;
        var pageIndex = 0;
        var counterTemplate = "/Content/Templates/Shared/Counter.html";
        var counterId = "#counterTemplate";
        var dataLoading;

        $.photosPage = {
            setPageIndex: function (newPageIndex) {
                $.hideMenu();
                pageIndex = newPageIndex;
            },
            getData: function () {
                if (dataLoading) {
                    return;
                }
                dataLoading = true;
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
                        $.each(photos.Items,
                            function(index, value) {
                                value.ShootDate =
                                    value.CreationDate === null ? "" : moment(value.CreationDate).format("DD/MM/YYYY");
                            });
                        if (photos.Items !== null && photos.Items.length !== 0) {
                            $.displayPhotoAlbum(templatePath, photos.Items);
                            pageIndex++;
                        }
                        var template;
                        var data = {};
                        data.Counter = photos.TotalCount === 0 ? "There are no photos in the gallery" : "Total photos count: " + photos.TotalCount;
                        $.get(counterTemplate,
                            function (templates) {
                                template = $(templates).filter(counterId).html();
                                var output = Mustache.render(template, data);
                                document.getElementById("counter").innerHTML = output;
                                $.stopSpinning();
                            });
                        dataLoading = false;
                    });
            }
        };

        $(this).keypress(function (e) {
            var keycode = e.keyCode || e.charCode || e.which;
            if (keycode === 13) {
                $(".btn-search").click();
                return false;
            }
        });

        $.hideMenu();
        $.initialize();
        $.setScroll($.photosPage.getData);
        $.photosPage.getData();
    });
})(jQuery);