(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Edit.html";
        var counterTemplate = "/Content/Templates/Shared/Counter.html";
        var counterId = "#counterTemplate";

        $.editAlbum = function (id) {
            $.setScroll(getData);

            var pageIndex = 0;
            var pageSize = 9;

            $.hideMenu();
            $.initialize();
            getData();

            function getData() {
                $.ajax({
                    url: "/api/albums/edit/" + id,
                    data: {
                        pageIndex: pageIndex,
                        pageSize: pageSize
                    },
                    error: function () {
                        location.href = "/albums/error";
                    }
                })
                    .done(function (album) {
                        var template;
                        var data = {};
                        data.Counter = album.Photos.TotalCount === 0 ? "There are no photos available to add" : "Photos available: " + album.Photos.TotalCount;
                        $.get(counterTemplate,
                            function (templates) {
                                template = $(templates).filter(counterId).html();
                                var output = Mustache.render(template, data);
                                document.getElementById("counter").innerHTML = output;
                                $.stopSpinning();
                            });
                        if (album.Photos.Items !== null && album.Photos.Items.length !== 0) {
                            $.each(album.Photos.Items,
                                function (index, value) {
                                    var className = value.Selected === true ? "selected" : "";
                                    value.Selected = className;
                                });
                            $.displayPhotoAlbum(templatePath, album.Photos.Items);
                            pageIndex++;
                        }
                    });
            }

            $("#formAlbumEdit").submit(function (e) {
                e.preventDefault();
                if (!$(this).valid()) {
                    return false;
                }
                var serializedData = $(this).serializeFormJSON();
                serializedData.Photos = [];
                var selectedPhotos = document.getElementsByClassName("selected");

                for (var i = 0; i < selectedPhotos.length; i++) {
                    var photo = { Id: selectedPhotos[i].dataset.photoId };
                    serializedData.Photos.push(photo);
                }
                var token = $('input[name="__RequestVerificationToken"]').val();
                $.ajax({
                    headers: { __RequestVerificationToken: token },
                    url: "/api/albums/",
                    type: "PUT",
                    data: serializedData,
                    error: function () {
                        location.href = "/albums/error/edit";
                    }
                }).done(function () {
                    location.href = "/albums/manage";
                });
            });
        };
    });
})(jQuery);