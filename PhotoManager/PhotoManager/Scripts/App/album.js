function loadPhotoAlbum(uri) {
    $(".btn-edit").attr("disabled", true);
    $(".btn-remove-confirm").attr("disabled", true);

    $.ajax(uri)
        .done(function (album) {
            $.get('/Content/Templates/photoAlbumTemplate.html',
                function (templates) {
                    var template = $(templates).filter('#photoAlbumTemplate').html();
                    var data = {};
                    data.photos = album.Photos;
                    data.AlbumId = album.Id;
                    var output = Mustache.render(template, data);
                    document.getElementById('content').innerHTML = output;
                });
        });
}

function loadPhotos(searchKey) {
    $(".btn-edit").attr("disabled", true);
    $(".btn-remove-confirm").attr("disabled", true);

    load(searchKey);

    function load(searchKey) {
        $.ajax({
            url: '/api/photos/search',
            type: "POST",
            data: { "KeyWord": searchKey },
            dataType: "json"
        })
            .done(function (photos) {
                $.get('/Content/Templates/photoAlbumTemplate.html',
                    function (templates) {
                        var template = $(templates).filter('#photoAlbumTemplate').html();
                        var data = {};
                        data.photos = photos;
                        data.AlbumId = 0;
                        var output = Mustache.render(template, data);
                        document.getElementById('content').innerHTML = output;
                    });
            });
    }

    $(".btn-search-gallery").on("click",
        function () {
            var searchKey = $("#KeyWord").val();
            load(searchKey);
        });
}

$(function () {
    $(document).on("click", ".photo .photo-frame", function () {
        var photoId = $(this).attr("photoId");
        window.open("/Photo/Index/" + photoId);
    });

    $(document).on("click", ".photo-footer", function () {
        if ($(this).hasClass("selected")) {
            $(this).removeClass("selected");
        } else {
            $(this).addClass("selected");
        }
        var selectedCount = document.getElementsByClassName("selected").length;
        $(".btn-edit").attr("disabled", selectedCount !== 1);
        $(".btn-remove-confirm").attr("disabled", selectedCount < 1);
    });

    $(document).on("click", ".btn-edit", function () {
        var photoId = $(".selected").attr("photoId");
        window.location.href = "/Photo/Edit/" + photoId;
    });

    $(document).on("click", ".btn-add", function () {
        window.location.href = "/Photo/Add";
    });

    $(document).on("click", ".btn-remove", function (e) {
        var albumId = $("#photoAlbumId").val();
        var selectedPhotos = document.getElementsByClassName("selected");
        var photosId = [];

        for (var i = 0; i < selectedPhotos.length; i++) {
            photosId[i] = selectedPhotos[i].getAttribute("photoId");
        }

        $.ajax({
            url: '/api/photos/delete',
            type: "POST",
            data: { "AlbumId": albumId, "PhotosId": photosId },
            dataType: "json"
        })
            .done(function (photos) {
                $.get('/Content/Templates/photoAlbumTemplate.html',
                    function (templates) {
                        var template = $(templates).filter('#photoAlbumTemplate').html();
                        var data = {};
                        data.photos = photos;
                        data.AlbumId = 0;
                        var output = Mustache.render(template, data);
                        document.getElementById('content').innerHTML = output;
                    });
            });

        $('#exampleModal').modal('hide');
    });
});