(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Edit.html";
        var headerTemplateId = "#photoAlbumHeaderTemplate";
        var contentId = "albumHeader";
        var uri = "/Content/Templates/Album/Manage";

        $.addAlbum = function (userId) {
            $.setScroll(getData);

            var pageIndex = 0;
            var pageSize = 9;

            $.hideMenu(userId);
            $.initialize();
            getHeader();
            getData();

            function getHeader() {
                $.ajax("/api/albums/add")
                    .done(function (album) {
                        $.get(templatePath, function (templates) {
                            var template = $(templates).filter(headerTemplateId).html();
                            var output = Mustache.render(template, album);
                            document.getElementById("albumEditForm").innerHTML = output;
                        });
                    });
            }

            function getData() {
                $.ajax({
                    url: "/api/photos",
                    data: {
                        pageIndex: pageIndex,
                        pageSize: pageSize
                    }
                })
                    .done(function (photos) {
                        if (photos.length !== 0) {
                            $.displayPhotoAlbum(templatePath, photos);
                            pageIndex++;
                        }
                    });
            }

            $("#formAlbumAdd").submit(function (e) {
                e.preventDefault();
                var serializedData = $(this).serializeFormJSON();
                var selectedPhotos = document.getElementsByClassName("selected");
                serializedData.Photos = [];
                for (var i = 0; i < selectedPhotos.length; i++) {
                    var photo = { Id: selectedPhotos[i].dataset.photoId };
                    serializedData.Photos.push(photo);
                }
                $.ajax({
                    url: '/api/albums/',
                    type: "POST",
                    data: serializedData
                }).done(function () {
                    location.href = "/albums/manage";
                });
            });
        };
    });
})(jQuery);