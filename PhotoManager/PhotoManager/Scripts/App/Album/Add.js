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
                        var template;
                        var data = {};
                        data.Counter = photos.TotalCount === 0 ? "There are no photos available to add" : "Photos available: " + photos.TotalCount;
                        $.get(templatePath,
                            function (templates) {
                                template = $(templates).filter('#photoAlbumEmptyTemplate').html();
                                var output = Mustache.render(template, data);
                                document.getElementById('counter').innerHTML = output;
                                $.stopSpinning();
                            });
                        if (photos.Items.length !== 0) {
                            $.displayPhotoAlbum(templatePath, photos.Items);
                            pageIndex++;
                        }
                    });
            }

            $('#freePaymentAlbums').on('hidden.bs.modal',
                function () {
                    location.href = "/albums/manage";
                });

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
                        $.ajax({
                            url: "/api/users/settings",
                            error: function () {
                                location.href = "/users/error";
                            }
                        }).done(function (settings) {
                            if (!settings.CanAddAlbums) {
                                $('#freePaymentAlbums').modal('show');
                            } else {
                                location.href = "/albums/manage";
                            }
                        });
                    }
                    else {
                        $('#addAlbumUniqueModal').modal('show');
                    }
                });
            });
        }
    });
})(jQuery);