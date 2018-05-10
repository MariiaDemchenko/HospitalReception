(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Edit.html";
        var counterTemplate = "/Content/Templates/Shared/Counter.html";
        var counterId = "#counterTemplate";

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
                        bootbox.alert("Error getting photos");
                    }
                })
                    .done(function (photos) {
                        var template;
                        var data = {};
                        data.Counter = photos.TotalCount === 0 ? "There are no photos available to add" : "Photos available: " + photos.TotalCount;
                        $.get(counterTemplate, function (templates) {
                            template = $(templates).filter(counterId).html();
                            var output = Mustache.render(template, data);
                            $("counter").html(output);
                            $.stopSpinning();
                        });
                        if (photos.Items.length !== 0) {
                            $.displayPhotoAlbum(templatePath, photos.Items);
                            pageIndex++;
                        }
                    });
            }

            $("#freePaymentAlbums").on("hidden.bs.modal",
                function () {
                    location.href = "/albums/manage";
                });

            $("#formAlbumEdit").on("submit", function (e) {
                e.preventDefault();
                if (!$(this).valid()) {
                    return false;
                }
                var serializedData = $(this).serializeFormJSON();
                var selectedPhotos = document.getElementsByClassName("selected");
                serializedData.Photos = {};
                serializedData.Photos.Items = [];
                for (var i = 0; i < selectedPhotos.length; i++) {
                    var photo = { Id: selectedPhotos[i].dataset.photoId };
                    serializedData.Photos.Items.push(photo);
                }
                var token = $('input[name="__RequestVerificationToken"]').val();
                $.ajax({
                    headers: { __RequestVerificationToken: token },
                    url: "/api/albums/",
                    type: "POST",
                    data: serializedData,
                    error: function () {
                        bootbox.alert("Error adding album");
                    }
                }).done(function (result) {
                    if (result) {
                        $.ajax({
                            url: "/api/users/settings",
                            error: function () {
                                bootbox.alert("Error getting user access parameters");
                            }
                        }).done(function (settings) {
                            if (!settings.CanAddAlbums) {
                                $("#freePaymentAlbums").modal("show");
                            } else {
                                location.href = "/albums/manage";
                            }
                        });
                    }
                    else {
                        $("#addAlbumUniqueModal").modal("show");
                    }
                });
            });
        }
    });
})(jQuery);