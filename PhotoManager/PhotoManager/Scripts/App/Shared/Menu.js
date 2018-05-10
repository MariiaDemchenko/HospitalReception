(function ($) {
    $(function () {
        var selectedPhotoId;
        var selectedAlbumId;

        $("#content").on("click", ".edit",
            function () {
                var attrPhoto = $(this).data("photoId");
                var attrAlbum = $(this).data("albumId");

                if (attrAlbum !== undefined) {
                    location.href = "/albums/edit/" + attrAlbum;
                }
                else if (attrPhoto !== undefined) {
                    location.href = "/photos/" + attrPhoto + "/album/" + $("#photoAlbumId").val();
                }
            });

        $(".modal-footer").on("click", ".btn-remove-single",
            function () {
                if (selectedAlbumId !== undefined) {
                    deleteAlbums(selectedAlbumId);
                    $('#albumsDeleteSingleModal').modal('hide');
                    $(".markedToDelete").removeClass("markedToDelete");
                } else if (selectedPhotoId !== undefined) {
                    deletePhotos($("#photoAlbumId").val(), selectedPhotoId);
                    $('#photosDeleteSingleModal').modal('hide');
                }
            });

        $("#content").on("click", ".remove",
            function () {
                selectedPhotoId = $(this).data("photoId");
                selectedAlbumId = $(this).data("albumId");
                $(this).addClass("markedToDelete");
                if (selectedPhotoId !== undefined) {
                    $("#photosDeleteSingleModal").modal("show");
                }
                else if (selectedAlbumId !== undefined) {
                    $("#albumsDeleteSingleModal").modal("show");
                }
            });

        $("#content").on("click", ".like.gallery-like .disabled, .dislike.gallery-dislike .disabled",
            function () {
                bootbox.alert("It's a photo gallery. To vote go to the albums page.");
            });

        $(".menu").on("click", ".btn-add",
            function () {
                if (!$(this).hasClass("add-enabled")) {
                    bootbox.alert(
                        "Your access to adding photos is restricted as you have already reached the allowed limit of 30 free photos");
                    return;
                }
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
                    $('#albumsDeleteModal').modal('hide');
                } else {
                    deletePhotos($("#photoAlbumId").val());
                    $('#photosDeleteModal').modal('hide');
                }
            });

        $(".btn-search").on("click", function (event) {
            event.preventDefault();
            var searchKey = $("#KeyWord").val();
            window.location.href = "/gallery/search/" + searchKey;
        });

        $(".btn-back").on("click", function (event) {
            event.preventDefault();
            history.back();
        });

        $(".btn-get-link").on("click", function (event) {
            event.preventDefault();
            var parts = location.toString().split("/");
            var url = "";
            for (var i = 0; i < parts.length - 1; i++) {
                url += parts[i] + "/";
            }

            bootbox.alert(url);
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
                    bootbox.alert("Error getting user access parameters");
                }
            })
                .done(function (settings) {
                    if (settings.IsAuthorized) {
                        $("#content").addClass("selectable");
                    } else {
                        $("#content").removeClass("selectable");
                        return;
                    }

                    var item = $("#menu").data("item");
                    if (item === "photo") {
                        if (settings.CanAddPhotos) {
                            $(".btn-add").addClass("add-enabled");
                        }
                    } else if (item === "album") {
                        if (settings.CanAddAlbums) {
                            $(".btn-add").addClass("add-enabled");
                        }
                    }
                });
        };

        $.stopSpinning = function () {
            document.getElementById("spinner").style.display = "none";
        };

        function deletePhotos(albumId, photoId) {
            albumId = albumId !== undefined ? albumId : "";

            var photosId = [];

            if (photoId === undefined) {
                var selectedPhotos = document.getElementsByClassName("selected");
                for (var i = 0; i < selectedPhotos.length; i++) {
                    photosId.push(selectedPhotos[i].dataset.photoId);
                }
            } else photosId.push(photoId);

            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                headers: { __RequestVerificationToken: token },
                url: '/api/photos',
                contentType: "application/json",
                data: JSON.stringify(photosId),
                type: "DELETE",
                error: function () {
                    bootbox.alert("Error deleting photos");
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

        function deleteAlbums(albumId) {

            var albumsId = [];

            if (albumId === undefined) {
                var selectedAlbums = document.getElementsByClassName("selected");
                for (var i = 0; i < selectedAlbums.length; i++) {
                    albumsId.push(selectedAlbums[i].dataset.albumId);
                }
            } else {
                albumsId.push(albumId);
            }

            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                headers: { __RequestVerificationToken: token },
                url: '/api/albums',
                contentType: "application/json",
                data: JSON.stringify(albumsId),
                type: "DELETE",
                error: function () {
                    bootbox.alert("Error deleting albums");
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
        }
    });
})(jQuery);