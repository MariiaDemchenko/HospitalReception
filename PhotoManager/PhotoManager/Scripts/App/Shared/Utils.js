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
            var url = albumId === 0 ? "/gallery" : "/albums/album?id=" + albumId;
            window.location.href = url;
        };

        $("#content").on("click", ".selected-icon.select",
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
        $("#content").on("click", ".selected-icon.like",
            function () {
                var photo = $(this);
                $.ajax("/api/users").done(function (userId) {
                    if (userId == null || photo.hasClass("disabled")) {
                        return;
                    }
                    if (photo.hasClass("liked")) {
                        photo.removeClass("liked");
                    } else {
                        if (photo.parent().find(".selected-icon.dislike").hasClass("disliked")) {
                            photo.parent().find(".selected-icon.dislike").removeClass("disliked");
                        }
                        photo.addClass("liked");
                    }
                    var photoId = photo.data("photoId");
                    var likesCounter = photo.find("#likesCount");
                    var dislikesCounter = photo.parent().find(".selected-icon.dislike").find("#dislikesCount");
                    $.ajax({
                        url: "/api/photos/like?Id=" + photoId + "&AlbumId=" + $("#photoAlbumId").val() + "&IsPositive=true",
                        type: "post"
                    })
                        .done(function (likesModel) {
                            likesCounter.text(likesModel.LikesCount);
                            dislikesCounter.text(likesModel.DislikesCount);
                        });
                });
            });
        $("#content").on("click", ".selected-icon.dislike",
            function () {
                var photo = $(this);
                $.ajax("/api/users").done(function (userId) {
                    if (userId == null || photo.hasClass("disabled")) {
                        return;
                    }
                    if (photo.hasClass("disliked")) {
                        photo.removeClass("disliked");
                    } else {
                        if (photo.parent().find(".selected-icon.like").hasClass("liked")) {
                            photo.parent().find(".selected-icon.like").removeClass("liked");
                        }
                        photo.addClass("disliked");
                    }
                    var photoId = photo.data("photoId");
                    var likesCounter = photo.parent().find(".selected-icon.like").find("#likesCount");
                    var dislikesCounter = photo.find("#dislikesCount");
                    $.ajax({
                        url: "/api/photos/like?Id=" + photoId + "&AlbumId=" + $("#photoAlbumId").val() + "&IsPositive=false",
                        type: "post"
                    })
                        .done(function (likesModel) {
                            likesCounter.text(likesModel.LikesCount);
                            dislikesCounter.text(likesModel.DislikesCount);
                        });
                });
            });
    });
})(jQuery);