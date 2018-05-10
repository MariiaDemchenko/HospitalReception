(function ($) {
    $(function () {
        $.loadPhoto = function (templatePath, uri) {
            $.ajax({
                url: uri,
                error: function () {
                    bootbox.alert("Error getting photo");
                }
            })
                .done(function (photo) {
                    $.get(templatePath,
                        function (templates) {
                            var template = $(templates).filter("#photoHeaderTemplate").html();
                            var output = Mustache.render(template, photo);
                            photo.CreationDate = photo.CreationDate == null ? "" : moment(new Date(photo.CreationDate)).format("LLLL");
                            $("#photoHeader").html(output);
                        });
                    $.displayPhoto(templatePath, photo);
                });
        };
    });
})(jQuery);