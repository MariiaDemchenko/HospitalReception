(function ($) {
    $(function () {
        addPhoto();

        function addPhoto() {
            var currentAlbumId;

            $("input[type=file]").change(function () {
                if (event.target.files.length > 0) {
                    var tmppath = URL.createObjectURL(event.target.files[0]);
                    $("#displayedImage").fadeIn("fast").attr("src", tmppath);
                } else {
                    $("#displayedImage").fadeIn("fast").attr("src", "/api/image");
                }
            });

            $("#freePaymentPhotos").on("hidden.bs.modal",
                function () {
                    $.goToAlbum(currentAlbumId);
                });

            $("#formAdd").submit(function (e) {
                e.preventDefault();
                if (!$(this).valid()) {
                    return false;
                }
                var serializedData = new FormData();
                var fileInput = document.getElementById("file");

                if (fileInput.files.length === 0) {
                    $("#selectPhotoModal").modal("show");
                    return false;
                }

                var file = fileInput.files[0];

                if (file.size / 1024 > 500) {
                    $("#fileSizeExceedingModal").modal("show");
                    return false;
                }

                var extension = file.name.split(".").pop();
                if (extension !== "jpg" && extension !== "jpeg") {
                    $("#invalidMimeTypeModal").modal("show");
                    return false;
                }

                serializedData.append("Image", file);
                serializedData.append("ViewModel", $(this).stringifyFormJSON());
                var token = $('input[name="__RequestVerificationToken"]').val();
                $.ajax({
                    headers: { __RequestVerificationToken: token },
                    url: "/api/photos/",
                    type: "POST",
                    data: serializedData,
                    processData: false,
                    contentType: false,
                    error: function () {
                        location.href = "/photos/error/add";
                    }
                }).done(function (albumId) {
                    currentAlbumId = albumId;
                    $.ajax({
                        url: "/api/users/settings",
                        error: function () {
                            location.href = "/users/error";
                        }
                    })
                        .done(function (settings) {
                            if (!settings.CanAddPhotos) {
                                $('#freePaymentPhotos').modal("show");
                            } else {
                                $.goToAlbum(albumId);
                            }
                        });
                });
            });
        }
    });
})(jQuery);