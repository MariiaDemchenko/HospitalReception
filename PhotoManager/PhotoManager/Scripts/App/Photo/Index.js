(function ($) {
    $.loadPhoto = function (templatePath, uri) {
        $.ajax(uri)
            .done(function (photo) {
                $.get(templatePath,
                    function (templates) {
                        var template = $(templates).filter('#photoHeaderTemplate').html();
                        var output = Mustache.render(template, photo);
                        document.getElementById('photoHeader').innerHTML = output;
                    });
                $.get(templatePath,
                    function (templates) {
                        var template = $(templates).filter('#photoTemplate').html();
                        var output = Mustache.render(template, photo);
                        document.getElementById('content').innerHTML = output;
                        $.stopSpinning();
                    });
                
            });
    }
})(jQuery);