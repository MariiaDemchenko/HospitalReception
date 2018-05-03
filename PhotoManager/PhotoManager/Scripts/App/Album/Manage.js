(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Manage.html";
        var templateId = "#albumsTemplate";
        var pageSize = 9;
        var pageIndex = 0;

        $.albumsPage = {
            setPageIndex: function(newPageIndex) {
                $.hideMenu();
                pageIndex = newPageIndex;
            },
            getData: function() {
                $.ajax({
                        url: "/api/albums",
                        data: { pageIndex: pageIndex, pageSize: pageSize },
                        error: function() {
                            location.href = "/albums/error";
                        }
                    })
                    .done(function(albums) {
                        var counterTemplate;
                        var counterData = {};
                        counterData.Counter = albums.TotalCount === 0
                            ? "There are no albums yet"
                            : "Total albums count: " + albums.TotalCount;
                        $.get(templatePath,
                            function(templates) {
                                counterTemplate = $(templates).filter('#photoAlbumEmptyTemplate').html();
                                var output = Mustache.render(counterTemplate, counterData);
                                document.getElementById('counter').innerHTML = output;
                            });

                        $.get(templatePath,
                            function(templates) {
                                var template = $(templates).filter(templateId).html();

                                var data = {};
                                data.albums = albums.Items;

                                var output = Mustache.render(template, data);
                                $("#content").append(output);
                                $.stopSpinning();
                            });
                        pageIndex++;
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
            var keycode = e.keyCode || e.charCode || e.which; //for cross browser
            if (keycode === 13) //keyCode for enter key
            {
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