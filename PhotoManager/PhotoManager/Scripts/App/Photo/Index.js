(function ($) {
    $.loadPhoto = function (templatePath, uri) {
        $.ajax(uri)
            .done(function (photo) {
                $.get(templatePath,
                    function (templates) {
                        var template = $(templates).filter('#photoTemplate').html();
                        var output = Mustache.render(template, photo);
                        document.getElementById('content').innerHTML = output;
                    });
            });
    }
})(jQuery);