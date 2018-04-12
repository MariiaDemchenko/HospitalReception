(function ($) {
    $(function() {
        $.fn.serializeFormJSON = function() {
            var o = {};
            var a = this.serializeArray();
            $.each(a,
                function() {
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

        $.displayPhotos = function (templatePath, photos) {
            $.get(templatePath, function (templates) {
                var template = $(templates).filter('#photoAlbumTemplate').html();
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
                document.getElementById('content').innerHTML = output;
                $.stopSpinning();
            });
        }
    });
})(jQuery);