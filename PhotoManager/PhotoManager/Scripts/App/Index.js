function loadAlbums() {
    $.ajax("/api/Albums")
        .done(function (albums) {
            $.get('/Content/Templates/albumsTemplate.html', function (templates) {
                var template = $(templates).filter('#albumsTemplate').html();

                var data = {};
                data.albums = albums;

                var output = Mustache.render(template, data);
                document.getElementById('content').innerHTML = output;
            });
        });
}

$(function () {
    $(document).on("click", ".album .photo-frame",
        function () {
            var photoId = $(this).attr("albumId");
            window.location.href = "/Album/Index/" + photoId;
        });
});