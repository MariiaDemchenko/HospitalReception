(function ($) {
    $(function () {
        var photoAlbumTemplate = "/Content/Templates/Album/Index.html";
        var photoAlbumTemplateId = "#photoAlbumTemplate";
        var contentId = "content";

        function getJSON(form) {
            var o = {};
            var a = form.serializeArray();
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
            return o;
        }

        $.fn.Page = function () {
            this.pageIndex = 0;
            this.getData = 1;

            alert(this.getData);
        };

        $.fn.serializeFormJSON = function () {
            return getJSON(this);
        }

        $.fn.stringifyFormJSON = function () {
            return JSON.stringify(getJSON(this));
        }

        $.getScrollTop = function () {
            if (typeof pageYOffset != 'undefined') {
                return pageYOffset;
            }
            else {
                var B = document.body;
                var D = document.documentElement;
                D = (D.clientHeight) ? D : B;
                return D.scrollTop;
            }
        }

        $.setScroll = function (callback, data) {
            $(window).scroll(function () {
                var scrollTop = $.getScrollTop();
                if (scrollTop == $(document).height() - $(window).height()) {
                    callback.call(this, data);
                }
            });
        }

        $.displayPhotoAlbum = function (templatePath, photos) {
            $.get(templatePath,
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
                    $("#content").append(output);
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

        $("#content").on("click", ".photo-footer",
            function () {
                if ($(this).hasClass("selected")) {
                    $(this).removeClass("selected");
                } else {
                    $(this).addClass("selected");
                }
                var selectedCount = document.getElementsByClassName("selected").length;
                $(".btn-edit").attr("disabled", selectedCount !== 1);
                $(".btn-remove-confirm").attr("disabled", selectedCount < 1);
            });
    });
})(jQuery);