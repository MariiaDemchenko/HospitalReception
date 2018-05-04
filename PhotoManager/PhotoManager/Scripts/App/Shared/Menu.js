(function ($) {
    $(function () {
        $(".form-menu").on("click", ".btn-edit",
            function () {
                var attr = $("#menu").data("item");
                var albumId = $("#photoAlbumId").val();
                albumId = albumId !== undefined ? albumId : "";
                if (attr === "album") {
                    $.ajax({
                        url: "/api/albums/" + $(".selected").data("albumId"),
                        error: function () {
                            location.href = "/albums/error";
                        }
                    }).done(function (album) {
                        location.href = "/albums/edit/album?" + $.serialize(album);
                    });
                }
                else if (attr === "photo") {
                    $.ajax({
                        url: "/api/photos/" + $(".selected").data("photoId") + "/album/" + albumId,
                        error: function () {
                            location.href = "/photos/error";
                        }
                    }).done(function (photo) {
                        location.href = "/photos/edit/photo?" + $.serialize(photo);
                    });
                }
            });

        $(".form-menu").on("click", ".btn-add",
            function () {
                var albumId = $("#photoAlbumId").val();
                albumId = albumId !== undefined ? albumId : "";
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

        $(".btn-get-link").on("click", function (event) {
            event.preventDefault();
            var parts = location.toString().split("/");
            var url = "";
            for (var i = 0; i < parts.length - 1; i++) {
                url += parts[i] + "/";
            }

            url = url + document.getElementById("photoAlbumName").innerHTML;
            document.getElementById("albumLink").innerHTML = url;
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
        };

        $.hideMenu = function () {
            $.ajax({
                url: "/api/users/settings",
                error: function () {
                    location.href = "/users/error";
                }
            })
                .done(function (settings) {
                    if (settings.IsAuthorized) {
                        $("#content").addClass("selectable");
                    } else {
                        $("#content").removeClass("selectable");
                        $(".btn-add").attr("visibility", "hidden");
                        return;
                    }

                    var item = $("#menu").data("item");
                    if (item === "photo") {
                        if (settings.CanAddPhotos === false) {
                            $(".btn-add").css("visibility", "hidden");
                        } else {
                            $(".btn-add").css("visibility", "visible");
                        }
                    } else if (item === "album") {
                        if (settings.CanAddAlbums === false) {
                            $(".btn-add").css("visibility", "hidden");
                        } else {
                            $(".btn-add").css("visibility", "visible");
                        }
                    }
                });
        };

        $.stopSpinning = function () {
            document.getElementById("spinner").style.display = "none";
        };

        function deletePhotos(albumId) {
            albumId = albumId !== undefined ? albumId : "";
            var selectedPhotos = document.getElementsByClassName("selected");
            var photosId = [];

            for (var i = 0; i < selectedPhotos.length; i++) {
                photosId.push(selectedPhotos[i].dataset.photoId);
            }

            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                headers: { __RequestVerificationToken: token },
                url: '/api/photos/album/' + albumId,
                contentType: "application/json",
                data: JSON.stringify(photosId),
                type: "DELETE",
                dataType: "json",
                error: function () {
                    location.href = "/albums/error/delete";
                }
            })
                .done(function () {
                    $("#content").empty();
                    $.photosPage.setPageIndex(0);
                    $.photosPage.getData();
                    var selectedCount = document.getElementsByClassName("selected").length;
                    $(".btn-edit").attr("disabled", selectedCount !== 1);
                    $(".btn-remove-confirm").attr("disabled", selectedCount < 1);
                });

            $('#photosDeleteModal').modal('hide');
        }

        function deleteAlbums() {
            var selectedAlbums = document.getElementsByClassName("selected");
            var albumsId = [];

            for (var i = 0; i < selectedAlbums.length; i++) {
                albumsId.push(selectedAlbums[i].dataset.albumId);
            }
            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                headers: { __RequestVerificationToken: token },
                url: '/api/albums',
                contentType: "application/json",
                data: JSON.stringify(albumsId),
                type: "DELETE",
                error: function () {
                    location.href = "/photos/error/delete";
                }
            })
                .done(function () {
                    $("#content").empty();
                    $.albumsPage.setPageIndex(0);
                    $.albumsPage.getData();
                    var selectedCount = document.getElementsByClassName("selected").length;
                    $(".btn-edit").attr("disabled", selectedCount !== 1);
                    $(".btn-remove-confirm").attr("disabled", selectedCount < 1);
                });

            $('#albumsDeleteModal').modal('hide');
        }
    });
})(jQuery);