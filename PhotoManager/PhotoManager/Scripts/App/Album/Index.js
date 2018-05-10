(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Index.html";
        var headerTemplateId = "#photoAlbumHeaderTemplate";
        var contentId = "#albumHeader";
        var pageSize = 9;
        var pageIndex = 0;
        var url;
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
                    url: url,
                    data: {
                        pageIndex: pageIndex,
                        pageSize: pageSize
                    },
                    error: function () {
                        bootbox.alert("error getting album");
                        $.stopSpinning();
                    }
                })
                    .done(function (album) {
                        $.ajax({
                            url: "/api/users",
                            error: function () {
                                bootbox.alert("Error getting user access parameters");
                                $.stopSpinning();
                            }
                        }).done(function (userId) {
                            if (album.Photos.Items.length !== 0) {
                                if (!userId || album.OwnerId === userId) {
                                    $.each(album.Photos.Items,
                                        function (index, value) {
                                            value.ShootDate = value.CreationDate === null ? "" :
                                                moment(value.CreationDate).format("DD/MM/YYYY");
                                            value.Liked = "disabled";
                                            value.Disliked = "disabled";
                                        });
                                } else {
                                    $.each(album.Photos.Items,
                                        function (index, value) {
                                            value.ShootDate = value.CreationDate === null ? "" :
                                                moment(value.CreationDate).format("DD/MM/YYYY");
                                            var className = value.Liked === true ? "liked" : "";
                                            value.Liked = className;
                                            className = value.Disliked === true ? "disliked" : "";
                                            value.Disliked = className;
                                        });
                                }
                            }
                            $.displayPhotoAlbum(templatePath, album.Photos.Items);
                            var template;
                            var data = {};
                            data.Counter = album.Photos.TotalCount === 0 ? "There are no photos yet" : "Total photos count: " + album.Photos.TotalCount;
                            $.get(counterTemplate,
                                function (templates) {
                                    template = $(templates).filter(counterId).html();
                                    var output = Mustache.render(template, data);
                                    $("#counter").html(output);
                                    $.stopSpinning();
                                });

                            pageIndex++;
                            album.CurrentUserId = userId;
                            $.get(templatePath,
                                function (templates) {
                                    var template = $(templates).filter(headerTemplateId).html();
                                    var output = Mustache.render(template, album);
                                    $(contentId).html(output);
                                });
                        });
                        dataLoading = false;
                    });
            }
        };

        $.loadPhotoAlbum = function (uri) {
            url = uri;
            $.hideMenu();
            $.initialize();
            $.setScroll($.photosPage.getData);
            $.photosPage.getData();
        };

        $(this).keypress(function (e) {
            var keycode = e.keyCode || e.charCode || e.which;
            if (keycode === 13) {
                $(".btn-search").click();
                return false;
            }
        });
    });
})(jQuery);