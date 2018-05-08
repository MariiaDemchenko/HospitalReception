(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Manage.html";
        var templateId = "#albumsTemplate";
        var pageSize = 9;
        var pageIndex = 0;
        var counterTemplate = "/Content/Templates/Shared/Counter.html";
        var counterId = "#counterTemplate";
        var dataLoading;

        $.albumsPage = {

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
                    url: "/api/albums",
                    data: { pageIndex: pageIndex, pageSize: pageSize },
                    error: function () {
                        bootbox.alert("error getting albums");
                        $.stopSpinning();
                    }
                })
                    .done(function (albums) {
                        var template;
                        var counterData = {};
                        counterData.Counter = albums.TotalCount === 0
                            ? "There are no albums yet"
                            : "Total albums count: " + albums.TotalCount;
                        $.get(counterTemplate,
                            function (templates) {
                                template = $(templates).filter(counterId).html();
                                var output = Mustache.render(template, counterData);
                                document.getElementById("counter").innerHTML = output;
                            });

                        $.get(templatePath,
                            function (templates) {
                                var template = $(templates).filter(templateId).html();

                                var data = {};
                                data.albums = albums.Items;

                                var output = Mustache.render(template, data);
                                $("#content").append(output);
                                $.stopSpinning();
                            });
                        pageIndex++;
                        dataLoading = false;
                    });
            }
        };

        loadAlbumsManage();

        function loadAlbumsManage() {
            $.hideMenu();
            $.initialize();
            $.setScroll($.albumsPage.getData);
            $.albumsPage.getData();
        }

        $(this).keypress(function (e) {
            var keycode = e.keyCode || e.charCode || e.which;
            if (keycode === 13) {
                $(".btn-search").click();
                return false;
            }
        });

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