(function ($) {
    $(function () {
        var templatePath = "/Content/Templates/Album/Edit.html";
        var headerTemplateId = "#photoAlbumHeaderTemplate";
        var contentId = "albumHeader";
        var uri = "/Content/Templates/Album/Manage";

        $.editAlbum = function (id, userId) {
            $.setScroll(getData);

            var pageIndex = 0;
            var pageSize = 9;

            $.hideMenu(userId);
            $.initialize();
            getHeader();
            getData();

            function getHeader() {
                $.ajax("/api/albums/" + id)
                    .done(function (album) {
                        $.get(templatePath,
                            function (templates) {
                                var template = $(templates).filter(headerTemplateId).html();
                                var output = Mustache.render(template, album);
                                document.getElementById("albumEditForm").innerHTML = output;
                            });
                    });
            }

            function getData() {
                $.ajax({
                    url: "/api/albums/edit/" + id,
                    data: {
                        pageIndex: pageIndex,
                        pageSize: pageSize
                    }
                })
                    .done(function (album) {

                        if (album != null && album.Photos.length !== 0) {
                            $.each(album.Photos,
                                function (index, value) {
                                    var className = value.Selected === true ? "selected" : "";
                                    value.Selected = className;
                                });
                            $.displayPhotoAlbum(templatePath, album.Photos);
                            pageIndex++;
                        }
                    });
            }

            $("#formAlbumEdit").submit(function (e) {
                e.preventDefault();
                var serializedData = $(this).serializeFormJSON();
                serializedData.Photos = [];
                var selectedPhotos = document.getElementsByClassName("selected");

                for (var i = 0; i < selectedPhotos.length; i++) {
                    var photo = { Id: selectedPhotos[i].dataset.photoId }
                    serializedData.Photos.push(photo);
                };

                $.ajax({
                    url: '/api/albums/',
                    type: "PUT",
                    data: serializedData
                }).done(function () {
                    location.href = "/albums/manage";
                });
            });
        }
    });
})(jQuery);