///<reference path="~/freshpin.js" />
var _refresh = true, _forcepinclose = false;

function getVal(o, p) {
    return p ? o[p] : o;
}
function concat() {
    return arguments.join();
}
function getAvatar(o, p) {
    return (p ? o[p] : o) || (FreshPin.constants.cdn + FreshPin.constants.userBlankImg);
}
function _pinshown() {
    return $('#pin').css('display') != 'none';
}
function getUplUImg(o) {
    return o.Avatar ? FreshPin.constants.upl + o.Avatar : FreshPin.constants.cdn + FreshPin.constants.userBlankImg;
}
function cssCenterY(el) {
    return { left: $(window).width() / 2 - $(el).width() / 2 };
}
function cssCenterX(el) {
    return { top: $(window).height() / 2 - $(el).height() / 2 };
}
function cssCenterXY(el) {
    return { left: $(window).width() / 2 - $(el).width() / 2, top: $(window).height() / 2 - $(el).height() / 2 };
}
function cssCenterAbsY(el) {
    return { left: $(document).width() / 2 - $(el).width() / 2 };
}
function cssCenterAbsX(el) {
    return { top: $(document).height() / 2 - $(el).height() / 2 };
}
function cssCenterAbsXY(el) {
    return { left: $(document).width() / 2 - $(el).width() / 2, top: $(document).height() / 2 - $(el).height() / 2 };
}
function getSpecialityImg(o) {
    switch (o.Speciality) {
        case 'M':
            return 'img/M-Icon.png';
            break
        case 'C':
            return 'img/C-Icon.png';
            break;
        case 'D':
            return 'img/D-Icon.png';
            break;
        default:
            return '';
            break;
    }
}
function getWidth(add) {
    var size;
    switch ((h('size') || '').toUpperCase()) {
        case 'S':
            size = 190;
            break;
        case 'M':
            size = 350;
            break;
        case 'L':
            size = 550;
            break;
        default:
            size = 190;
    }
    return size + (add || 0);
}
function getHeight(w) {
    return Math.round(w || getWidth() * height / width);
}
function getUrl(o, t) {
    var pin = String.format('{0}#{1}', $(location).attr('href'), o.BIMID);
    switch (t) {
        case 'w':
            return String.format('http://service.weibo.com/share/share.php?url={0}&appkey={1}&title={2}&pic={3}&ralateUid={4}', $(location).attr('href'), '', escape(o.title), escape(o.url), '');
            break;
        case 'r':
            return String.format('http://widget.renren.com/dialog/share?resourceUrl={0}&pic={1}&title={2}&description={2}&charset=utf-8', $(location).attr('href'), escape(o.url), escape(o.title));
            break;
        case 't':
            return String.format('http://share.v.t.qq.com/index.php?c=share&a=index&url={0}&appkey={1}&title={2}&pic={3}', $(location).attr('href'), '', escape(o.title), escape(o.url));
    }
}
function isgamify(o) {
    return o.gamify;
}
function rcat(v) {
    params = ['cat=' + escape(v)];
    $(location).attr('href', '#' + params.join('&'));
}
function zoomClose(noEffects) {
    var o = $('#PinZoomImage').prop('zoomout');
    if (o) {
        if (noEffects === true) {
            $(o.img).show();
            $('#PinZoomBox').hide();
            $('#PinZoomImage').attr('src', '');
        } else
            $('#PinZoomBox').transition(o.to, 'fast', function () { $('#PinZoomImage').attr('src', ''); $(o.img).css({visibility:'visible'}); $('#PinZoomBox').hide(); });
    }
}
function set() {
    FreshPin.constants.s = 50, FreshPin.constants.p = 0;
    FreshPin.constants.loading = false
    $("#content").empty().hide();
    $("#gallery").empty().hide();
    $("#articlesLayout").empty().hide();
    $("#boardsCont").empty().hide();
    $("#users").empty().hide();
    $(document).scrollTop(0);
    FreshPin.closeall();
    zoomClose(true);
    FreshPin.emit('set');
}
function setsizeEl() {
    $('li[name="sizeEl"]').removeClass('selected');
    var size = h('size');
    if (size)
        $('#' + size).addClass('selected');
}
function GetFImage(type) {
    if (type.toUpperCase() == "FOLLOW")
        return FreshPin.constants.cdn + "img/pinjimu/follow-all-small.png";
    else if (type.toUpperCase() == "UNFOLLOW")
        return FreshPin.constants.cdn + "img/pinjimu/Unfollow-Icon-Small.png";
}
function scrollTo(v) {
    //$(window).scrollTop(v);
    $('html').animate({ scrollTop: v }, 'slow'); //IE, FF
    $('body').animate({ scrollTop: v }, 'slow'); //chrome, don't know if safary works
}
(function ($s) {
    var masonryOpts = {
        itemSelector: '.box',
        gutterWidth: 15,
        isAnimated: false,
        isFitWidth: true
    }, masonryOptsArticles = {
        itemSelector: '.containerBox',
        gutterWidth: 30,
        isAnimated: false,
        isFitWidth: true
    }, rl, rl1, _his = [location.hash], templates = {
        picTemplate: null,
        articles: null,
        boards: null,
        stores: null
    }, masonryOptsBoards = {
        itemSelector: '.pinBoard',
        columnWidth: 220,
        gutterWidth: 15,
        isAnimated: false,
        isFitWidth: true
    };
    var scrolldelay, scrolling;
    function pageScroll() {
        window.scrollBy(0, 2);
        scrolldelay = setTimeout('pageScroll()', 1);
        scrolling = true;
    }
    function stopScroll() {
        clearTimeout(scrolldelay);
        scrolling = false;
    }
    function togglePageScroll() {
        if (scrolling) stopScroll();
        else pageScroll();
    }
    function cb(dt) {
        $("#gallery").show();
        var tmpl = $.tmpl(templates.picTemplate, dt);
        tmpl.appendTo("#gallery");
        if (rl) {
            $("#gallery").masonry('reload');
        } else {
            if ($("#gallery").hasClass('masonry'))
                $("#gallery").masonry('destroy').masonry($.extend({ columnWidth: getWidth(30) }, masonryOpts));
            else
                $("#gallery").masonry($.extend({ columnWidth: getWidth(30) }, masonryOpts));
            rl = true;
        }
        tmpl.find('.pinit').click(function () {
            var url = $(this).attr('href');
            if (url) {
                var w = window.open(url, 'pinit', 'width=800,height=600,resizable=yes,menubar=no,location=no,left=100px,top=100px,scrollbars=no', false);
                w.focus();
            }
        });
        tmpl.hover(function () {
            $(this).find('.buttons').css({ display: 'inline', opacity: 0 }).transition({ opacity: 1 }, 'fast');
            $(this).find('.boardsPinBoards').css({ display: 'inline', opacity: 0 }).transition({ opacity: 1 }, 'fast');
        }, function () {
            $(this).find('.buttons').transition({ opacity: 0 }, 'fast').css({ display: 'none' });
            $(this).find('.boardsPinBoards').transition({ opacity: 0 }, 'fast').css({ display: 'none' });
        });
        tmpl.find('img[name="social"]').click(function () {
            var url = $(this).attr('href');
            if (url) {
                var w = window.open(url, 'pinit', 'width=800,height=600,resizable=yes,menubar=no,location=no,left=100px,top=100px,scrollbars=no', false);
                w.focus();
            }
        });
        tmpl.find('a[name="buttons"]').click(function () {
            FreshPin.closeall();
            $('#signUpWraper').jqmShow();
        });
        tmpl.find('img[name="gmfht"]').click(function () {
            var bimid = $(this).attr('bimid');
            FreshPin.constants.selectedRec = bimid;
            FreshPin.closeall();
            $('#fbd').jqmShow();
        });
        FreshPin.emit('bindimagetmpl', {
            tmpl: tmpl
        });
    }
    function cbqboards(dt) {
        $("#boardsCont").show();
        var tmpl = $.tmpl(templates.boards, dt);
        $("#boardsCont").empty().append(tmpl);
        if (rl1) {
            $("#boardsCont").masonry('reload');
        } else {
            $("#boardsCont").masonry(masonryOptsBoards);
            rl1 = true;
        }
        FreshPin.emit('bindboardstmpl', {
            tmpl: tmpl
        });
    }
    function cbusers(dt) {
        var tmpl = $.tmpl(templates.users, dt);
        tmpl.find('button[name="logint"]').click(function (e) {
            FreshPin.closeall();
            $("#login").jqmShow();
        });
        $("#users").html(tmpl).show();
    }
    function get(sc) {
        if (sc) {
            set();
            cb(FreshPin.constants.data.slice(0, FreshPin.constants.s));
        } else cb(FreshPin.constants.data.slice(FreshPin.constants.s, FreshPin.constants.s += FreshPin.constants.ps));
    }
    function getqboards(sc) {
        set();
        cbqboards(FreshPin.constants.data);
    }
    function getusers(sc) {
        set();
        cbusers(FreshPin.constants.data);
    }
    function setpin() {
        var o = FreshPin.constants.pin;
        var html = templates.pin(o);     
        $('#pin').empty().html(html).attr('bimid', o.BIMID).css(cssCenterY('#pin')).jqmShow();
        $('#pinCloseupImage').click(function () {
            var me = $(this);
            var winwr = $(window);
            var width = winwr.width();
            var height = winwr.height();
            var pos = me.offset();
            $('#PinZoomImage').attr('src', o.url).prop('zoomout', { to: { top: pos.top - $(document).scrollTop(), left: pos.left, width: me.width(), height: me.height() }, img: '#pinCloseupImage' });
            me.css({visibility:'hidden'});
            $('#PinZoomBox').css({ top: pos.top - $(document).scrollTop(), left: pos.left, width: me.width(), height: me.height(), display: 'block'}).transition({
                left: width / 2 - o.width / 2,
                top: height / 2 - o.height / 2,
                height: o.height,
                width: o.width
            }, 'fast', function () { });
        });
    }
    function reload(e) {
        _his.push(location.hash);
        FreshPin.closeall();
        FreshPin.trackGACPV();
        setsizeEl();
        FreshPin.emit('reload');
        var param;
        if (h('pin')) {
            var pin = h('pin');
            FreshPin.trackGACEvents('pin', 'Load', String.format('Pin-{0}', pin));
            $.getJSON('GET?t=getpin', { pin: pin }, function (dt) {
                FreshPin.constants.pin = dt;
                setpin();
            });
            return;
        }
        if (h('q') && h('qt') && h('qt') == 'boards') {
            FreshPin.trackGACEvents('nails', 'Load', String.format('Query-{0},Cat-{1},Board-{2},Filter-{3},Page-{4}', h('q'), h('cat'), h('board'), h('filter'), FreshPin.constants.p));
            FreshPin.constants.data = FreshPin.getState();
            if (FreshPin.constants.data && FreshPin.constants.preventReload) {
                get(true);
            } else {
                $.getJSON('GET?t=getqboards', { name: h('q') }, function (dt) {
                    $.each(dt, function (ind, item) {
                        var l = item.images.length;
                        while (l++ < 5) item.images.push({ ID: null, url: FreshPin.constants.cdn + 'img/paper.jpg' });
                    });
                    FreshPin.constants.data = dt;
                    FreshPin.pushState(dt);
                    FreshPin.constants.dataType = 'boards';
                    getqboards(true);
                });
            }
            FreshPin.constants.preventReload = true;
            return;
        }
        if (h('q') && h('qt') && h('qt') == 'people') {
            FreshPin.constants.data = FreshPin.getState();
            param = { name: h('q') };
            if (FreshPin.constants.data && FreshPin.constants.preventReload) {
                getusers(true);
            } else {
                FreshPin.trackGACEvents('nails', 'Load', String.format('Query-{0},Cat-{1},Board-{2},Filter-{3},Page-{4}', h('q'), h('cat'), h('board'), h('filter'), FreshPin.constants.p));
                $.getJSON('GET?t=getqpeople', param, function (dt) {
                    FreshPin.pushState(dt);
                    FreshPin.constants.data = dt;
                    FreshPin.constants.dataType = 'query';
                    getusers(true);
                }, 'json').error(function (res) { alert(res.responseText); });
            }
            FreshPin.constants.preventReload = true;
            return;
        }
        if ((!h() || h('size') || h('style') || h('cat') || h('q') || h() == '')) {
            if (h('size')) rl = false;
            if (FreshPin.constants.pinClosed && _his[_his.length - 1] == _his[_his.length - 3]) {
                zoomClose(true);
                FreshPin.constants.pinClosed = false;
            }
            else {
                FreshPin.trackGACEvents('nails', 'Load', String.format('Query-{0},Cat-{1},Board-{2},Filter-{3},Page-{4}', h('q'), h('cat'), h('board'), h('filter'), FreshPin.constants.p));
                FreshPin.constants.data = FreshPin.getState();
                if (FreshPin.constants.data && FreshPin.constants.preventReload) {
                    get(true);
                } else {
                    $.getJSON('GET?t=getimages', {
                        cat: h('cat'),
                        q: h('q'),
                        style: h('style'),
                        p: FreshPin.constants.p,
                        filter: h('filter'),
                        board: h('board')
                    }, function (dt) {
                        FreshPin.pushState(dt);
                        FreshPin.constants.data = dt;
                        FreshPin.constants.dataType = 'pins';
                        get(true);
                    });
                }
                FreshPin.constants.preventReload = true;
            }
            return;
        }
    }
    function load() {
        FreshPin.trackGACPV();
        var pin = h('pin');
        if (pin) {
            FreshPin.trackGACEvents('pin', 'Load', String.format('Pin-{0}', h('pin')));
            FreshPin.appendScriptElement(String.format('GET?t=getpin&pre=FreshPin.constants.pin&pin={0}', pin));
            return;
        }
        if (h('q') && h('qt') && h('qt') == 'boards') {
            FreshPin.trackGACEvents('nails', 'Load', String.format('Query-{0},Cat-{1}', h('q'), h('cat')), h('pin'));
            FreshPin.appendScriptElement(String.format('GET?t=getqboards&pre=FreshPin.constants.data&{0}', FreshPin.serialize({ name: h('q') })));
            FreshPin.constants.dataType = 'boards';
            return;
        }
        if (h('q') && h('qt') && h('qt') == 'people') {
            FreshPin.trackGACEvents('nails', 'Load', String.format('Query-{0},Cat-{1}', h('q'), h('cat')), h('pin'));
            FreshPin.appendScriptElement(String.format('GET?t=getqpeople&pre=FreshPin.constants.data&{0}', FreshPin.serialize({ name: h('q') })));
            FreshPin.constants.dataType = 'query';
            return;
        }
        if (!h() || h('size') || h('cat') || h('q') || h('style') || h() == '') {
            FreshPin.trackGACEvents('nails', 'Load', String.format('Query-{0},Cat-{1}', h('q'), h('cat')), h('pin'));
            var param = {
                cat: h('cat'),
                q: h('q'),
                style: h('style'),
                p: FreshPin.constants.p,
                filter: h('filter'),
                board: h('board')
            };
            FreshPin.appendScriptElement(String.format('GET?t=getimages&pre=FreshPin.constants.data&{0}', FreshPin.serialize(param)));
            FreshPin.constants.dataType = 'pins';
            return;
        }
        FreshPin.emit('load');
    }
    load();
    $s.togglePageScroll = togglePageScroll;
    $s.pageScroll = pageScroll;
    $s.stopScroll = stopScroll;
    $s.templates = templates;
    $s.setpin = setpin;
    $s.reload = reload;
    $s.get = get;
    $s.getqboards = getqboards;
    $s.getusers = getusers;
    $s._his = _his;
})(window);
$(function () {

    if (!$.support.transition)
        $.fn.transition = $.fn.animate;
    $.fx.interval = 1, _.templateSettings = {
        evaluate: /<#([\s\S]+?)#>/g,
        interpolate: /<#=([\s\S]+?)#>/g,
        escape: /<#-([\s\S]+?)#>/g
    };
    $.extend( doT.templateSettings, {
        evaluate: /\{\{([\s\S]+?)\}\}/g,
        interpolate: /\{\{=([\s\S]+?)\}\}/g,
        encode: /\{\{!([\s\S]+?)\}\}/g,
        use: /\{\{#([\s\S]+?)\}\}/g,
        define: /\{\{##\s*([\w\.$]+)\s*(\:|=)([\s\S]+?)#\}\}/g,
        conditional: /\{\{\?(\?)?\s*([\s\S]*?)\s*\}\}/g,
        iterate: /\{\{~\s*(?:\}\}|([\s\S]+?)\s*\:\s*([\w$]+)\s*(?:\:\s*([\w$]+))?\s*\}\})/g    
    });
    $('#PinZoomClose').click(zoomClose);
    $('#topcontrol').click(function () { scrollTo(0); });
    templates.picTemplate = $("#picTemplate").template();
    templates.boards = $("#boards").template();
    templates.users = $("#usertpl").template();
    templates.pin = _.template($('#pintmpl').html());
    $('#pin').jqm({
        onShow: function (hash) {
            $(document.body).css({ overflow: 'hidden' });
            hash.o.css({ opacity: .5 }).show();
            hash.w.show();
        },
        forceclose: function (hash) {
            FreshPin.constants.pinClosed = hash.w.css('display') != 'none';
            hash.w.hide();
            hash.o.css({ opacity: 0 }).hide();
            $(document.body).css({ overflow: 'auto' });
            $('#pinCloseupImage').attr('src', '');
            $('#likepint div').removeClass('pinDisLike');
        },
        onHide: function (hash) {
            FreshPin.constants.pinClosed = hash.w.css('display') != 'none';
            hash.w.hide();
            hash.o.css({ opacity: 0 }).hide();
            $(document.body).css({ overflow: 'auto' });
            $('#pinCloseupImage').attr('src', '');
            $('#likepint div').removeClass('pinDisLike');
        }
    });
    function load() {
        setsizeEl();
        $('#scroll_button').click(function () {
            togglePageScroll();
        });
        $('a[name="buttons"]').click(function () {
            FreshPin.closeall();
            $('#signUpWraper').jqmShow();
        });
        var pin = h('pin');
        if (pin) {
            setpin();
            return;
        }
        if (h('q') && h('qt') && h('qt') == 'boards') {
            FreshPin.pushState(FreshPin.constants.data);
            getqboards(true);
            return;
        }
        if (h('q') && h('qt') && h('qt') == 'people') {
            FreshPin.pushState(FreshPin.constants.data);
            getusers(true);
            return;
        }
        if (!h() || h('size') || h('cat') || h('q') || h('style') || h() == '') {
            FreshPin.pushState(FreshPin.constants.data);
            get(true);
        }
    }
    load();
    $(window).scroll(function () {
        var sp = $(document).scrollTop();
        var ch = $(document).height();
        var ph = $(window).height() * .8;
        if (sp > ph) {
            $('#topcontrol').fadeIn('slow');
            if (!FreshPin.constants.showHeader)
                $('#line1').slideUp('fast', function () { FreshPin.constants.showHeader = true; });
        } else {
            $('#topcontrol').fadeOut('slow');
            if (FreshPin.constants.showHeader)
                $('#line1').slideDown('fast', function () { FreshPin.constants.showHeader = false; });
        }
        if (!FreshPin.constants.loading && !FreshPin.constants.noscroll) {
            if (sp > 0 && (ch - ph - sp) < 500) {
                if (h('stores')) {
                    if (FreshPin.constants.data && FreshPin.constants.s >= FreshPin.constants.data.length) {
                        FreshPin.constants.loading = true;
                        $.getJSON('GET?t=getstores', {
                            p: ++FreshPin.constants.p
                        }, function (dt, status, res) {
                            $.each(dt, function (ind, item) { FreshPin.constants.data.push(item); });
                            getstores(false);
                            FreshPin.constants.loading = false;
                        });
                    } else getstores(false);
                    return;
                }
                if (h('articles')) {
                    if (FreshPin.constants.data && FreshPin.constants.s >= FreshPin.constants.data.length) {
                        FreshPin.constants.loading = true;
                        $.getJSON('GET?t=getarticles', {
                            p: ++FreshPin.constants.p
                        }, function (dt, status, res) {
                            $.each(dt, function (ind, item) { FreshPin.constants.data.push(item); });
                            getarticles(false);
                            FreshPin.constants.loading = false;
                        });
                    } else getarticles(false);
                    return;
                }
                if (!h() || h('size') || h('cat') || h('q') || h() == '') {
                    if (FreshPin.constants.data && FreshPin.constants.s >= FreshPin.constants.data.length) {
                        FreshPin.constants.loading = true;
                        $.getJSON('GET?t=getimages', {
                            cat: h('cat'),
                            q: h('q'),
                            p: ++FreshPin.constants.p,
                            filter: h('filter')
                        }, function (dt) {
                            $.each(dt, function (ind, item) { FreshPin.constants.data.push(item); });
                            get(false);
                            FreshPin.constants.loading = false;
                        });
                    } else get(false);
                }
            }
        }
    });
    $(window).bind('hashchange', function (e) {
        reload(e);
        FreshPin.emit('hashchange', null, [e]);
    });
    function setRelative(menu, submenu) {
        var pos = $(menu).offset();
        var left = pos.left + "px";
        $(submenu).css({
            left: left
        });
    }
    setRelative('#mlevel1', '#submenu1');
    setRelative('#mlevel2', '#submenu2');
    setRelative('#mlevel3', '#submenu3');
    setRelative('#mlevel4', '#submenu4');

    FreshPin.ourHover('#mlevel1', '#submenu1');
    FreshPin.ourHover('#mlevel2', '#submenu2');
    FreshPin.ourHover('#mlevel3', '#submenu3');
    FreshPin.ourHover('#mlevel4', '#submenu4');

    $('#fbd').css(cssCenterY('#fbd'))
        .jqm({
            onShow: function (hash) {
                hash.o.show().transition({
                    opacity: .5
                }, 'fast');
                hash.w.show().transition(cssCenterX(hash.w), 'fast');
            },
            forceclose: function (hash) {
                hash.o.unbind().css({ opacity: 0 }).hide();
                hash.w.css({ top: 0 }).hide();
            },
            onHide: function (hash) {
                hash.o.unbind().css({ opacity: 0 }).hide();
                hash.w.css({ top: 0 }).hide();
            }
        });
    $('#fbdclose').click(function () { $('#fbd').jqmHide(); });
    $('input[name="choosefbd"]').change(function () {
        var pts = FreshPin.readCookieValue('pts');
        if ($.grep(pts.ids, function (o) { return o == parseInt(FreshPin.constants.selectedRec); }).length == 0) {
            $.post('POST?t=review', {
                bimid: FreshPin.constants.selectedRec,
                ques: $.trim($(this).parents('ul').find('.ques').html()),
                ans: $.trim($(this).next().html())
            }, function (res, opts) {
                pts.ids = res;
                pts.total += 100;
                $('#pts').html(String.format('{0} {1}', pts.total, strings.Points));
                FreshPin.writeCookieValue('pts', pts);
            }, 'json');
        }
    });
    $('#earnPointsCriteria').css(cssCenterY('#earnPointsCriteria'))
            .jqm({
                onShow: function (hash) {
                    hash.o.click(function () {
                        hash.w.jqmHide();
                    }).show().transition({
                        opacity: .98
                    }, 'fast');
                    $(window).scrollTop(0);
                    hash.w.show().transition(cssCenterX('#earnPointsCriteria'), 'fast');
                },
                forceclose: function (hash) {
                    hash.o.unbind().hide();
                    hash.w.css({ top: 0 }).hide();
                },
                onHide: function (hash) {
                    hash.o.unbind().hide();
                    hash.w.css({ top: 0 }).hide();
                }
            });
    $('#earnpoints').click(function () {
        FreshPin.closeall();
        $('#earnPointsCriteria').jqmShow();
    });
    $('#login').css(cssCenterY('#login')).jqm({
        onShow: function (hash) {
            hash.o.click(function () {
                hash.w.jqmHide();
            }).show().transition({
                opacity: 1
            }, 'fast');
            hash.w.show().transition(cssCenterX('#login'), 'fast');
        },
        forceclose: function (hash) {
            hash.o.unbind().css({ opacity: 0 }).hide();
            hash.w.css({ top: 0 }).hide();
        },
        onHide: function (hash) {
            hash.o.unbind().css({ opacity: 0 }).hide();
            hash.w.css({ top: 0 }).hide();
        }
    });
    $('*[name="logint"]').click(function () {
        FreshPin.closeall();
        $('#login').jqmShow();
    });
    if (!FreshPin.authenticated()) {
        var emailregex = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$/, fup = false;
        $('#signUpWraper').css(cssCenterY('#signUpWraper'))
            .jqm({
                onShow: function (hash) {
                    hash.o.click(function () {
                        hash.w.jqmHide();
                    }).show().transition({
                        opacity: 1
                    }, 'fast');
                    hash.w.show().transition(cssCenterX('#signUpWraper'), 'fast');
                },
                forceclose: function (hash) {
                    hash.o.unbind().css({ opacity: 0 }).hide();
                    hash.w.css({ top: 0 }).hide();
                },
                onHide: function (hash) {
                    hash.o.unbind().css({ opacity: 0 }).hide();
                    hash.w.css({ top: 0 }).hide();
                }
            });
        $('#reqin').click(function () {
            FreshPin.closeall();
            $('#signUpWraper').jqmShow();
        });
        function EmailProcess() {
            var email = $('#invitationBox').val();
            if (email != '' && emailregex.test(email)) {
                FreshPin.trackGACEvents('register', 'sendrequest');
                $.post('POST?t=ri', { email: email }, function (data) {
                    set();
                    $('#content').html(data).show();
                    FreshPin.emit('loadsignup');
                    FreshPin.constants.loading = true;
                }, 'html').error(function (res) {
                    $('#err').html(res.responseText);
                });
            } else $('#err').html(strings.InvalidEmail);
        }
        $('#invitationBox').keypress(function (e) {
            var k = (e.keyCode ? e.keyCode : e.which);
            if (k == 13) {
                EmailProcess();
            }
        });
        $('#reqinvite').click(function () { EmailProcess(); });

        $('#rilogint').click(function () {
            FreshPin.closeall();
            $('#login').jqmShow();
        });
        function Login() {
            var user = $('#user').val();
            var pass = $('#pass').val();
            pass = $.trim(pass);
            if (!fup) {
                if (user != '' && pass != '') {
                    $.post('POST?t=applogin', { user: user, pass: pass }, function (data, res) {
                        if (data != '') $('#err1').html(data);
                        else $(window.location).attr('href', '.');
                    }, 'text');
                } else $('#err1').html(strings.Login_Error);
            } else {
                if (user != '' && emailregex.test(user)) {
                    $.post('POST?t=resetpass', { email: user }, function (data, res) {
                        if (data != '') $('#err1').html(data);
                        else $(window.location).attr('href', '.');
                    }, 'text').error(function (res) {
                        $('#err1').html(res.responseText);
                    });
                } else $('#err1').html(strings.InvalidEmail);
            }
            return false;
        }
        $('#fup').click(function () {
            if (!fup) {
                $('#pass').slideUp();
                $('#loginbutton').attr('value', strings.Reset);
                fup = true;
                $('#fup').html(strings.Login_Back);

            } else {
                $('#pass').slideDown();
                fup = false;
                $('#loginbutton').attr('value', strings.Login);
                $('#fup').html(strings.Pass_Forgot);
            }
        });
        $('#loginbutton').click(Login);
        $('#user').keypress(function (e) {
            var k = (e.keyCode ? e.keyCode : e.which);
            if (k == 13) Login();
        });
        $('#pass').keypress(function (e) {
            var k = (e.keyCode ? e.keyCode : e.which);
            if (k == 13) Login();
        });
    }
});
FreshPin.attach(function () {
    var fn, formvalid = true;
    $(document).bind('keydown', 'return', function (evt) { $('#_esc').addClass('dirty'); return false; });
    //FreshPin.setru('settings', _his[_his.length - 2] || '');
    $("#speciality input:checkbox").click(function () {
        checkedState = $(this).attr('checked');
        $('#speciality input:checkbox').each(function () {
            $(this).attr('checked', false);
        });
        $(this).attr('checked', checkedState);
    });
    $('#ftsettings').fileupload({
        url: 'POST?t=up1',
        replaceFileInput: false,
        singleFileUploads: true,
        dataType: 'json',
        acceptFileTypes: /(\.|\/)(gif|GIF|jpe?g|JPE?G|png|PNG)$/i
    }).bind('fileuploadadd', function (e, data) {
        if (/(.*?)\.(jpg|jpeg|png|gif|tiff|tif|bmp)$/.test((data.files[0].name).toLowerCase())) $('#lmuplimg').show();
        else {
            e.preventDefault();
            alert(strings.Image_Invalid);
            $('#lmuplimg').hide();
            throw 'invalid file type';
        }
    }).bind('fileuploaddone', function (e, data) {
        fn = (data.result.file).toLowerCase();
        $('#uploadedUserImage').attr('src', fn + '?width=170');
        $('#uploadedUserImage').show();
        $('#lmuplimg').hide();
    }).bind('fileuploadfail', function (e, data) {
        alert(strings.File_Fail);
    });
    $('#username1').change(function () {
        var me = $(this);
        var val = me.val();
        var chkUN = /^[^<>+%&*#?.\/]+$/;
        if ((chkUN.test(val)))
            $.post('POST?t=usernameavail', { un: val }, function (data) {
                $('#usernameview').text(data);
                me.removeClass('errinput');
                formvalid = true;
            }, 'text').error(function (res) {
                $('#usernameview').text(res.responseText);
                me.addClass('errinput');
                formvalid = false;
            });
        else {
            alert(strings.UserName_Alert);
            me.addClass('errinput');
            formvalid = false;
        }
    });
    $('#sp').click(function () {
        if (!formvalid) {
            alert(strings.Fields_Red);
            return;
        }
        var pass2 = $('#pass2').val();
        var pass3 = $('#pass3').val();

        var first_name = $('#first_name').val();
        var email = $('#email').val();
        var name = $('#username1').val();
        var about = $('#aboutu').val();
        var loc = $('#location').val();
        var website = $('#website').val();
        var speciality = $("#speciality input:checkbox[checked]").val();
        var imgurlarr = $('#uploadedUserImage').attr('src').split('?');
        var imgurl = (imgurlarr.length > 0) ? imgurlarr[0] : '';
        if (name == '') {
            alert(strings.Enter_User_Name);
            $('#username1').addClass('errinput');
            return;
        }
        else if (first_name == '') {
            alert(strings.Enter_Full_Name);
            $('#first_name').addClass('errinput');
            return;
        }
        else if (pass2 == "") {
            alert(strings.Password_New);
            $('#pass2').addClass('errinput');
            return;
        } else if (pass3 == "") {
            alert(strings.Password_New);
            $('#pass3').addClass('errinput');
            return;
        } if (pass2 != pass3) {
            alert(strings.Password_Mismatch);
            $('#pass2').addClass('errinput');
            $('#pass3').addClass('errinput');
            return;
        }
        $.post('POST?t=addprofile', { email: email, name: name, pass: pass2, fn: fn, first_name: first_name, about: about, location: loc, website: website, speciality: speciality }, function (dt, res, opts) {
            $('#p_about').text(about);
            set();
            location.reload(true);
        }, 'text').error(function (res) {
            alert(res.responseText);
        });
    })
}, null, null, 'loadsignup');
