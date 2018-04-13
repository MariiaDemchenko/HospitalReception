(function ($) {
    $(function () {
        $.loadPhoto = function (templatePath, uri) {
            $.ajax(uri)
                .done(function (photo) {
                    $.get(templatePath, function (templates) {
                        var template = $(templates).filter('#photoHeaderTemplate').html();
                        var output = Mustache.render(template, photo);
                        photo.CreationDate = moment(new Date(photo.CreationDate)).format("LLLL");
                        document.getElementById('photoHeader').innerHTML = output;

                    });
                    $.displayPhoto(templatePath, photo);
                });
        }
    });
})(jQuery);