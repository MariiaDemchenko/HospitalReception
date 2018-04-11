(function ($) {
    $(document).on("click", ".btn-edit", function () {
        var photoId = $(".selected").attr("photoId");
        var albumId = $("#photoAlbumId").val();
        window.location.href = "/photos/edit/" + photoId + "/album/" + albumId;
    });

    $(document).on("click", ".btn-add", function () {
        var albumId = $("#photoAlbumId").val();
        location.href = "/photos/add/" + albumId;
    });

    $(document).on("click", ".btn-remove", function () {
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
                $.get('/Content/Templates/photoAlbumTemplate.html',
                    function (templates) {
                        var template = $(templates).filter('#photoAlbumTemplate').html();
                        var data = {};
                        data.photos = photos;
                        data.AlbumId = albumId;
                        var output = Mustache.render(template, data);
                        document.getElementById('content').innerHTML = output;
                    });
            });

        $('#exampleModal').modal('hide');
    });

    $.initialize = function () {
        $(".btn-edit").attr("disabled", true);
        $(".btn-remove-confirm").attr("disabled", true);
    }
})(jQuery);