(function ($) {
    $(function () {
        $("#formEdit").submit(function (e) {
            e.preventDefault();
            if (!$(this).valid()) {
                return false;
            }
            
            var date = new Date(moment($("#datepicker").val(), "DD-MM-YYYY"));
            var newDate = moment(date).format("MM/DD/YYYY");
            var serializedData = $(this).serialize();
            serializedData = serializedData + "&CreationDate=" + newDate;
            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                headers: { __RequestVerificationToken: token },
                url: "/api/photos",
                type: "PUT",
                data: serializedData,
                error: function () {
                    bootbox.alert("Error editing photo");
                }
            }).done(function (albumId) {
                $.goToAlbum(albumId);
            });
        });
    });
})(jQuery);