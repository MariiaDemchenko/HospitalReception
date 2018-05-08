(function ($) {
    $(function () {

        var currentAlbumId;
        $("#freePaymentPhotosModal").on("hidden.bs.modal",
            function () {
                $.goToAlbum(currentAlbumId);
            });

        $("input[type=file]").change(function () {
            if (event.target.files.length > 0) {
                var tmppath = URL.createObjectURL(event.target.files[0]);
                $("#displayedImage").fadeIn("fast").attr("src", tmppath);
            } else {
                $("#displayedImage").fadeIn("fast").attr("src", "/api/image");
            }
        });

        $("#formEdit").submit(function (e) {
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
            var serializedViewModel = $(this).serializeFormJSON();

            var date = new Date(moment($("#datepicker").val(), "DD-MM-YYYY"));
            var newDate = moment(date).format("MM/DD/YYYY");

            if (newDate !== "Invalid date") {
                serializedViewModel["CreationDate"] = newDate;
            }

            serializedData.append("ViewModel", JSON.stringify(serializedViewModel));

            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                headers: { __RequestVerificationToken: token },
                url: "/api/photos/",
                type: "POST",
                data: serializedData,
                processData: false,
                contentType: false,
                error: function () {
                    bootbox.alert("Error adding photo");
                }
            }).done(function (albumId) {
                currentAlbumId = albumId;
                $.ajax({
                    url: "/api/users/settings",
                    error: function () {
                        bootbox.alert("Error getting user access paramters");
                    }
                })
                    .done(function (settings) {
                        if (!settings.CanAddPhotos) {
                            $('#freePaymentPhotosModal').modal("show");
                        } else {
                            $.goToAlbum(albumId);
                        }
                    });
            });
        });
    });
})(jQuery);