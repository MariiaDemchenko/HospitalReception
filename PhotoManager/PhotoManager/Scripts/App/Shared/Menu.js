(function ($) {
    $(function () {
        $(".form-menu").on("click", ".btn-edit",
            function () {
                var attr = $("#menu").data("item");
                var albumId = $("#photoAlbumId").val();
                var url = attr === "album" ? "/albums/edit/" + $(".selected").data("albumId") :
                    "/photos/edit/" + $(".selected").data("photoId") + "/album/" + albumId;
                window.location.href = url;
            });

        $(".form-menu").on("click", ".btn-add",
            function () {
                var albumId = $("#photoAlbumId").val();
                var attr = $("#menu").data("item");
                var url = attr === "album" ? "/albums/add" : "/photos/add/" + albumId;
                location.href = url;
            });

        $(".modal-footer").on("click", ".btn-remove",
            function () {
                var attr = $("#menu").data("item");
                if (attr === "album") {
                    deleteAlbums();
                } else {
                    deletePhotos($("#photoAlbumId").val());
                }
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

        $(".btn-manage-albums").on("click", function (event) {
            event.preventDefault();
            window.location.href = "/albums/manage/";
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
            $.ajax("/api/users/settings/" + userId)
                .done(function (settings) {
                    if (settings.IsAuthorized) {
                        $("#content").addClass("selectable");
                    } else {
                        $("#content").removeClass("selectable");
                    }

                    var item = $("#menu").data("item");
                    if (item === "photo") {
                        if (settings.CanAddPhotos === false) {
                            $(".btn-add").css("visibility", "hidden");
                        } else {
                            $(".btn-add").css("visibility", "visible");
                        }
                    }
                    else if (item === "album") {
                        if (settings.CanAddAlbums === false) {
                            $(".btn-add").css("visibility", "hidden");
                        } else {
                            $(".btn-add").css("visibility", "visible");
                        }
                    }

                });
        }

        $.stopSpinning = function () {
            document.getElementById("spinner").style.display = "none";
        }

        function deletePhotos(albumId) {
            var selectedPhotos = document.getElementsByClassName("selected");
            var photosId = [];

            for (var i = 0; i < selectedPhotos.length; i++) {
                photosId.push(selectedPhotos[i].dataset.photoId);
            }

            $.ajax({
                url: '/api/photos/album/' + albumId,
                contentType: "application/json",
                data: JSON.stringify(photosId),
                type: "DELETE",
                dataType: "json"
            })
                .done(function () {
                    $("#content").empty();
                    $.photosPage.setPageIndex(0);
                    $.photosPage.getData();
                });

            $('#exampleModal').modal('hide');
        }

        function deleteAlbums() {
            var selectedAlbums = document.getElementsByClassName("selected");
            var albumsId = [];

            for (var i = 0; i < selectedAlbums.length; i++) {
                albumsId.push(selectedAlbums[i].dataset.albumId);
            }

            $.ajax({
                url: '/api/albums',
                contentType: "application/json",
                data: JSON.stringify(albumsId),
                type: "DELETE"
            })
                .done(function () {
                    $("#content").empty();
                    $.albumsPage.setPageIndex(0);
                    $.albumsPage.getData();
                });

            $('#exampleModal').modal('hide');
        }
    });
})(jQuery);