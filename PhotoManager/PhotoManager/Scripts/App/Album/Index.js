(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Index.html";
        var headerTemplateId = "#photoAlbumHeaderTemplate";
        var contentId = "albumHeader";
        var pageSize = 9;
        var pageIndex = 0;
        var url;

        $.photosPage = {
            setPageIndex: function (newPageIndex) {
                $.hideMenu();
                pageIndex = newPageIndex;
            },
            getData: function () {

                $.ajax({
                    url: url,
                    data: {
                        pageIndex: pageIndex,
                        pageSize: pageSize
                    },
                    error: function () {
                        location.href = "/albums/error";
                    }
                })
                    .done(function (album) {
                        $.ajax({
                            url: "/api/users",
                            error: function () {
                                location.href = "/users/error";
                            }
                        }).done(function (userId) {
                            if (album.Photos.Items.length !== 0) {
                                if (!userId || album.OwnerId === userId) {
                                    $.each(album.Photos.Items,
                                        function (index, value) {
                                            value.Liked = "disabled";
                                            value.Disliked = "disabled";
                                        });
                                } else {
                                    $.each(album.Photos.Items,
                                        function (index, value) {
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
                            $.get(templatePath,
                                function (templates) {
                                    template = $(templates).filter('#photoAlbumEmptyTemplate').html();
                                    var output = Mustache.render(template, data);
                                    document.getElementById('counter').innerHTML = output;
                                    $.stopSpinning();
                                });

                            pageIndex++;
                            album.CurrentUserId = userId;
                            $.get(templatePath,
                                function (templates) {
                                    var template = $(templates).filter(headerTemplateId).html();
                                    var output = Mustache.render(template, album);
                                    document.getElementById(contentId).innerHTML = output;
                                });
                        });
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
            var keycode = e.keyCode || e.charCode || e.which; //for cross browser
            if (keycode === 13)    //keyCode for enter key
            {
                $(".btn-search").click();
                return false;
            }
        });
    });
})(jQuery);