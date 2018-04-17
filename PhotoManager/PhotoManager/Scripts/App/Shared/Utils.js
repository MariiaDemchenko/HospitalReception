(function ($) {
    $(function () {
        var photoAlbumTemplate = "/Content/Templates/Album/Index.html";
        var photoAlbumTemplateId = "#photoAlbumTemplate";
        var contentId = "content";
        
        $.fn.serializeFormJSON = function () {
            var o = {};
            var a = this.serializeArray();
            $.each(a,
                function () {
                    if (o[this.name]) {
                        if (!o[this.name].push) {
                            o[this.name] = [o[this.name]];
                        }
                        o[this.name].push(this.value || '');
                    } else {
                        o[this.name] = this.value || '';
                    }
                });
            return JSON.stringify(o);
        };

        $.displayPhotoAlbum = function (templatePath, photos) {
            $.get(photoAlbumTemplate,
                function (templates) {
                    var template = $(templates).filter(photoAlbumTemplateId).html();
                    var data = {};
                    data.photos = photos;
                    $.each(data.photos,
                        function (key, value) {
                            if (value.CreationDate !== null) {
                                var date = moment(new Date(value.CreationDate)).format("LLLL");
                                value.CreationDate = date;
                            }
                        });
                    data.AlbumId = 0;
                    var output = Mustache.render(template, data);
                    document.getElementById(contentId).innerHTML = output;
                    $.stopSpinning();
                });
        };

        $.displayPhoto = function (templatePath, photo) {
            $.get(templatePath,
                function (templates) {
                    var template = $(templates).filter('#photoTemplate').html();
                    var output = Mustache.render(template, photo);
                    document.getElementById(contentId).innerHTML = output;
                    $.stopSpinning();
                });
        };

        $.goToAlbum = function (albumId) {
            var url = albumId === 0 ? "/gallery" : "/albums/" + albumId;
            window.location.href = url;
        };
    });
})(jQuery);