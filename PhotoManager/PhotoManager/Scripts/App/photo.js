function loadPhoto(uri) {
    $.ajax(uri)
        .done(function (photo) {
            $.get('/Content/Templates/photoTemplate.html', function (templates) {
                var template = $(templates).filter('#photoTemplate').html();
                var output = Mustache.render(template, photo);
                document.getElementById('content').innerHTML = output;
            });
        });
}

function editPhoto(uri) {
    $.ajax(uri)
        .done(function (photo) {
            $.get('/Content/Templates/photoEditTemplate.html', function (templates) {
                var template = $(templates).filter('#photoEditTemplate').html();
                var output = Mustache.render(template, photo);
                document.getElementById('content').innerHTML = output;
            });
        });
}

function addPhoto() {
    $.get('/Content/Templates/photoEditTemplate.html', function (templates) {
        var template = $(templates).filter('#photoEditTemplate').html();
        var photo = {}
        var output = Mustache.render(template, photo);
        document.getElementById('content').innerHTML = output;
    });

    $('input[type=file]').change(function () {
        var tmppath = URL.createObjectURL(event.target.files[0]);
        $("#displayedImage").fadeIn("fast").attr('src', tmppath);
    });
}