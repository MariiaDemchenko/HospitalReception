function loadPhotoAlbum(uri) {
    $(".btn-edit").attr("disabled", true);

    $.ajax(uri)
        .done(function (album) {
            $.get('/Content/Templates/photoAlbumTemplate.html',
                function (templates) {
                    var template = $(templates).filter('#photoAlbumTemplate').html();
                    var data = {};
                    data.photos = album.Photos;
                    var output = Mustache.render(template, data);
                    document.getElementById('content').innerHTML = output;
                });
        });
}

function loadPhotos(searchKey) {
    $(".btn-edit").attr("disabled", true);
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
                    var output = Mustache.render(template, data);
                    document.getElementById('content').innerHTML = output;
                });
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
    });

    $(document).on("click", ".btn-edit", function () {
        var photoId = $(".selected").attr("photoId");
        window.location.href = "/Photo/Edit/" + photoId;
    });

    $(document).on("click", ".btn-add", function () {
        window.location.href = "/Photo/Add";
    });
});