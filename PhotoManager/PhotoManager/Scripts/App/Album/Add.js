(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Edit.html";

        addAlbum();

        function addAlbum() {
            $.setScroll(getData);

            var pageIndex = 0;
            var pageSize = 9;

            $.hideMenu();
            $.initialize();
            getData();

            function getData() {
                $.ajax({
                    url: "/api/photos",
                    data: {
                        pageIndex: pageIndex,
                        pageSize: pageSize
                    },
                    error: function () {
                        location.href = "/photos/error";
                    }
                })
                    .done(function (photos) {
                        if (photos.length !== 0) {
                            $.displayPhotoAlbum(templatePath, photos);
                            pageIndex++;
                        }
                    });
            }

            $("#formAlbumAdd").submit(function (e) {
                e.preventDefault();
                if (!$(this).valid()) {
                    return false;
                }
                var serializedData = $(this).serializeFormJSON();
                var selectedPhotos = document.getElementsByClassName("selected");
                serializedData.Photos = [];
                for (var i = 0; i < selectedPhotos.length; i++) {
                    var photo = { Id: selectedPhotos[i].dataset.photoId };
                    serializedData.Photos.push(photo);
                }
                $.ajax({
                    url: '/api/albums/',
                    type: "POST",
                    data: serializedData,
                    error: function () {
                        location.href = "/albums/error/add";
                    }
                }).done(function (result) {
                    if (result) {
                        location.href = "/albums/manage";
                    } else {
                        $('#addAlbumUniqueModal').modal('show');
                    }
                });
            });
        }
    });
})(jQuery);