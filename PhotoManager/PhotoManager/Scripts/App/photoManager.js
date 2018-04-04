$(function () {
    var uri = 'api/Albums';

    $.ajax(uri)
        .done(function (albums) {
            loadAlbums(albums);
        });

    function prepareAlbums(albums) {
        for (var i = 0; i < albums.length; i++) {
            albums[i].MainImage = "data:image;base64," + albums[i].Photos[i].Image;
        }
    }

    function preparePhotoAlbum(album) {
        for (var i = 0; i < album.Photos.length; i++) {
            album.Photos[i].Image = "data:image;base64," + album.Photos[i].Image;
        }
    }

    function preparePhotos(photos) {
        for (var i = 0; i < photos.length; i++) {
            photos[i].Image = "data:image;base64," + photos[i].Image;
        }
    }

    function preparePhoto(photo) {
        photo.Image = "data:image;base64," + photo.Image;
    }

    function loadAlbums(albums) {
        prepareAlbums(albums);
        $.get('/Templates/templates.html', function (templates) {
            var template = $(templates).filter('#albumsTemplate').html();

            var data = {};
            data.albums = albums;

            var output = Mustache.render(template, data);
            document.getElementById('content').innerHTML = output;
        });
    }

    function loadPhotoAlbum(album) {
        preparePhotoAlbum(album);
        $.get('/Templates/templates.html',
            function (templates) {
                var template = $(templates).filter('#photoAlbumTemplate').html();
                var data = {};
                data.photos = album.Photos;
                var output = Mustache.render(template, data);
                document.getElementById('content').innerHTML = output;
            });
    }

    function loadPhoto(photo) {
        preparePhoto(photo);
        $.get('/Templates/templates.html',
            function (templates) {
                var template = $(templates).filter('#photoTemplate').html();
                var output = Mustache.render(template, photo);
                document.getElementById('content').innerHTML = output;
            });
    }

    function loadPhotos(photos) {
        preparePhotos(photos);
        $.get('/Templates/templates.html',
            function (templates) {
                var template = $(templates).filter('#photoAlbumTemplate').html();
                var data = {};
                data.photos = photos;
                var output = Mustache.render(template, data);
                document.getElementById('content').innerHTML = output;
            });
    }

    $(document).on("click", ".album",
        function () {
            var albumId = $(this).attr("albumId");
            $.ajax({ url: 'api/Albums/', data: { id: albumId } }).done(function (album) {
                loadPhotoAlbum(album);
            });
        });

    $(document).on("click", ".photo", function () {
        var photoId = $(this).attr("photoId");
        $.ajax({ url: 'api/Photos/', data: { id: photoId } })
            .done(function (photo) {
                loadPhoto(photo);
            });
    });

    $(document).on("click", ".btn-search", function (e) {
        e.preventDefault();
        var searchKey = $("#searchField").val();
        $.ajax({ url: 'api/Photos/', data: { keyWord: searchKey } })
            .done(function (photos) {
                loadPhotos(photos);
            });
    });

    //$(".btn-search").on("click", function (){
    //    var searchKey = $("#searchField").val();
    //    $.ajax({ url: 'api/Photos/', data: { keyWord: searchKey } })
    //        .done(function (photos) {
    //            loadPhotos(photos);
    //        });
    //});
});