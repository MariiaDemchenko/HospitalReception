(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Edit.html";

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

                        if (album !== null && album.Photos.length !== 0) {
                            $.each(album.Photos,
                                function (index, value) {
                                    var className = value.Selected === true ? "selected" : "";
                                    value.Selected = className;
                                });
                            $.displayPhotoAlbum(templatePath, album.Photos);
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

                $.ajax({
                    url: '/api/albums/',
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