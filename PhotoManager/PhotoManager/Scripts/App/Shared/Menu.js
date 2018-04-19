(function ($) {
    $(function () {
        $(".form-menu").on("click", ".btn-edit",
            function () {
                var photoId = $(".selected").attr("photoId");
                var albumId = $("#photoAlbumId").val();
                window.location.href = "/photos/edit/" + photoId + "/album/" + albumId;
            });

        $(".form-menu").on("click", ".btn-add",
            function () {
                var albumId = $("#photoAlbumId").val();
                location.href = "/photos/add/" + albumId;
            });

        $(".modal-footer").on("click", ".btn-remove",
            function () {
                var albumId = $("#photoAlbumId").val();
                var selectedPhotos = document.getElementsByClassName("selected");
                var photosId = [];

                for (var i = 0; i < selectedPhotos.length; i++) {
                    photosId.push(selectedPhotos[i].getAttribute("photoId"));
                }

                $.ajax({
                    url: '/api/photos/album/' + albumId,
                    contentType: "application/json",
                    data: JSON.stringify(photosId),
                    type: "DELETE",
                    dataType: "json"
                })
                    .done(function (photos) {
                        $.displayPhotoAlbum('/Content/Templates/Album/Index.html', photos);
                    });

                $('#exampleModal').modal('hide');
            });

        $(".btn-search").on("click", function (event) {
            event.preventDefault();
            var searchKey = $("#KeyWord").val();
            window.location.href = "/gallery/search/" + searchKey;
        });

        $(".btn-advanced-search").on("click", function (event) {
            event.preventDefault();
            window.location.href = "/gallery/advancedSearch/";
        });

        $.initialize = function () {
            $(".btn-edit").attr("disabled", true);
            $(".btn-remove-confirm").attr("disabled", true);
        }

        $.hideMenu = function (userId) {
            if (userId == undefined) {
                $("#content").removeClass("selectable");
                $(".btn-add").attr("visibility", "hidden");
                return;
            }
            $.ajax("/api/users/settings/"+userId)
                .done(function(settings) {
                    if (settings.IsAuthorized) {
                        $("#content").addClass("selectable");
                    } else {
                        $("#content").removeClass("selectable");
                    }
                    if (settings.CanAddPhotos === false) {
                        $(".btn-add").css("visibility", "hidden");
                    } else {
                        $(".btn-add").css("visibility", "visible");
                    }
                });
        }

        $.stopSpinning = function () {
            document.getElementById("spinner").style.display = "none";
        }
    });
})(jQuery);