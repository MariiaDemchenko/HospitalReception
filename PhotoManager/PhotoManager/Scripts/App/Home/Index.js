(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Home/Index.html";
        var templateId = "#albumsTemplate";

        $.setScroll(getData);
        var pageIndex = 0;
        var pageSize = 9;

        getData();

        $(this).keypress(function (e) {
            var keycode = e.keyCode || e.charCode || e.which; //for cross browser
            if (keycode === 13)    //keyCode for enter key
            {
                $(".btn-search").click();
                return false;
            }
        });

        function getData() {
            $.ajax({
                url: "/api/albums",
                data: { pageIndex: pageIndex, pageSize: pageSize },
                error: function () {
                    location.href = "/home/index/error";
                }
            })
                .done(function (albums) {
                    var template;
                    var data = {};
                    data.Counter = albums.TotalCount === 0? "There are no albums yet": "Total albums count: "+albums.TotalCount;
                    $.get(templatePath,
                        function (templates) {
                            template = $(templates).filter('#photoAlbumEmptyTemplate').html();
                            var output = Mustache.render(template, data);
                            document.getElementById('counter').innerHTML = output;
                            $.stopSpinning();
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
                });
        }
    });
})(jQuery);