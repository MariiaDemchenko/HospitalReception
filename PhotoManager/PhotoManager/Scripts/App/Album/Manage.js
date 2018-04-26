(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Manage.html";
        var templateId = "#albumsTemplate";
        var contentId = "content";
        var pageSize = 9;
        var pageIndex = 0;
        var userId;

        $.albumsPage = {
            setPageIndex: function (newPageIndex) {
                $.hideMenu(userId);
                pageIndex = newPageIndex;
            },
            getData: function () {
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

        $.loadAlbumsManage = function (id) {
            userId = id;
            $.hideMenu(userId);
            $.initialize();
            $.setScroll($.albumsPage.getData);
            $.albumsPage.getData();
        }

        $("#content").on("click", ".album-footer", function () {
            if ($(this).hasClass("selected")) {
                $(this).removeClass("selected");
            } else {
                $(this).addClass("selected");
            }
            var selectedCount = document.getElementsByClassName("selected").length;
            $(".btn-edit").attr("disabled", selectedCount !== 1);
            $(".btn-remove-confirm").attr("disabled", selectedCount < 1);
        });
    });
})(jQuery);