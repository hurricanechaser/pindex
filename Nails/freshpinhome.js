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
function getWidth() {
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
    return size;
}
function getHeight(o, w) {
    return Math.round(w || getWidth() * o.height / o.width);
}
function getUrl(o, t) {
    switch (t) {
        case 'p':
            return 'http://pinterest.com/pin/create/bookmarklet?media=' + absolutizeURI(o.url) + '&url=Pin?pin=' + o.PinID + '&alt=' + escape(o.title) + '&title=' + escape(o.title) + '&is_video=false';
            break;
    }
}
function isgamify(o) {
    return o.gamify;
}
function rcat(v) {
    params = ['cat=' + escape(v)];
    $(location).attr('href', '#' + params.join('&'));
}
function set() {
    FreshPin.constants.s = 50, FreshPin.constants.p = 0;
    FreshPin.constants.loading = false;
    $("#gallery").empty().hide();
    $("#articlesLayout").empty();
    $("#articlesCont").hide();
    $("#storesCont").empty().hide();
    $("#content").empty().hide();
    $("#boardsCont").empty().hide();
    $("#users").empty().hide();
    $(document).scrollTop(0);
    FreshPin.closeall();
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

(function ($s) {
    var masonryOpts = {
        itemSelector: '.box',
        columnWidth: 220,
        gutterWidth: 15,
        isAnimated: false,
        isFitWidth: true
    }, masonryOptsArticles = {
        itemSelector: '.containerBox',
        gutterWidth: 30,
        isAnimated: false,
        isFitWidth: true
    }, rl, rl1,rl2, _his = [location.hash], templates = {
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
            $("#gallery").masonry(masonryOpts);
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
            $(this).find('.buttons').slideDown(100);
            $(this).find('.roulette-icon').slideDown(100);
            $(this).find('.boardsPinBoards').slideDown(100);
            $(this).find('.gamify').slideDown(100);
        }, function () {
            $(this).find('.buttons').slideUp(100);
            $(this).find('.roulette-icon').slideUp(100);
            $(this).find('.boardsPinBoards').slideUp(100);
            $(this).find('.gamify').slideUp(100);
        });
        tmpl.find('img[name="social"]').click(function () {
            var url = $(this).attr('href');
            if (url) {
                var w = window.open(url, 'pinit', 'width=800,height=600,resizable=yes,menubar=no,location=no,left=100px,top=100px,scrollbars=no', false);
                w.focus();
            }
        });
        tmpl.find('a[name="buttons"]').click(function () { $('#signUpWraper').jqmShow(); });
        tmpl.find('img[name="gmfht"]').click(function () {
            var bimid = $(this).attr('bimid');
            FreshPin.constants.selectedRec = bimid;
            $('#fbd').jqmShow();
        });
        FreshPin.emit('bindimagetmpl', {
            tmpl: tmpl
        });
    }
        function cbstores(dt) {
            $("#storesCont").show().append($.tmpl(templates.stores, dt));
            if (rl2) {
                $("#storesCont").masonry('reload');
            } else {
                $("#storesCont").masonry(masonryOpts);
                rl2 = true;
            }
        }
        function cbarticles(dt) {
            var tmpl = $.tmpl(templates.articles, dt);
            $("#articlesCont").show();
            tmpl.appendTo("#articlesLayout");
            if (rl1) {
                $("#articlesLayout").masonry('reload');
            } else {
                $("#articlesLayout").masonry(masonryOptsArticles);
                rl1 = true;
            }
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
        function getarticles(sc) {
            if (sc) {
                set();
                cbarticles(FreshPin.constants.data.slice(0, FreshPin.constants.s));
            } else cbarticles(FreshPin.constants.data.slice(FreshPin.constants.s, FreshPin.constants.s += FreshPin.constants.ps));
        }
        function getstores(sc) {
            if (sc) {
                set();
                cbstores(FreshPin.constants.data.slice(0, FreshPin.constants.s));
            } else cbstores(FreshPin.constants.data.slice(FreshPin.constants.s, FreshPin.constants.s += FreshPin.constants.ps));
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
        if (o.editable && FreshPin.authenticated()) {
            $('#editpint').css('display', '');
            $('#likepint').css('display', 'none');
        } else {
            $('#editpint').css('display', 'none');
            $('#likepint').css('display', '');
            if (o.liked) $('#likepint div').addClass('pinDisLike');
        }
        $('#pin').css({
            'margin-left': 0 - ((o.width / 2) + 30),
            'top': $(window).scrollTop() + 100
        });
        $('#pinCloseupImage').attr('src', o.url);
        $('#pinCloseupImage').attr('alt', o.title);
        $('#pinimgsource').attr('href', o.imgsource);
        $('#pintitle').html(o.title);
        $('#pintitle').width(o.width + 60);
        $('#pin').jqmShow();
    }
    function reload(e) {
        _his.push(location.hash);
        FreshPin.trackGACPV();
        setsizeEl();
        FreshPin.emit('reload');
        var param;
        if (h('pin')) {
            var pin = h('pin');
            FreshPin.trackGACEvents('pin', 'Load', String.format('Pin-{0}', pin));
            if (FreshPin.constants.dataType == 'pins') {
                var pino = $.grep(FreshPin.constants.data, function (item) { return item.PinID == pin; });
                if (pino.length > 0) {
                    FreshPin.constants.pin = pino[0];
                    setpin();
                    return;
                }
            }
            $.getJSON('GET?t=getpin', { pin: pin }, function (dt) {
                FreshPin.constants.pin = dt;
                setpin();
            });
            return;
        }
                    if (h('stores')) {
                        FreshPin.closeall();
                        set();
                        FreshPin.trackGACEvents('stores', 'Load', 'Loading Stores');
                          FreshPin.constants.data = FreshPin.getState();
            if (FreshPin.constants.data && FreshPin.constants.preventReload) {
                getstores(true);
            } else {
                $.getJSON('GET?t=getstores', {
                        p: FreshPin.constants.p
                    }, function(dt, status, res) {
                        FreshPin.constants.data = dt;
                        FreshPin.pushState(dt);
                        FreshPin.constants.dataType = 'stores';
                        getstores(true);
                    });
            }
                        FreshPin.constants.preventReload = true;
                        return;
                    }
                    if (h('articles')) {
                        FreshPin.closeall();
                        set();
                        FreshPin.trackGACEvents('articles', 'Load', 'Loading Articles');
                         FreshPin.constants.data = FreshPin.getState();
            if (FreshPin.constants.data && FreshPin.constants.preventReload) {
                getarticles(true);
            } else {
                $.getJSON('GET?t=getarticles', {
                        p: FreshPin.constants.p
                    }, function(dt, status, res) {
                        FreshPin.constants.data = dt;
                        FreshPin.pushState(dt);
                        FreshPin.constants.dataType = 'articles';
                        getarticles(true);
                    });
            }
                        FreshPin.constants.preventReload = true;
                        return;
                    }
                    if (h('size') && !h('cat') && FreshPin.constants.dataType == 'pins') {
                        FreshPin.trackGACEvents('size', 'Load');
                        get(true);
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
            return;
        }
    }
    function load() {
        FreshPin.trackGACPV();
                if (h() == 'articles') {
                    FreshPin.trackGACEvents('articles', 'Load', 'Loading Articles');
                    FreshPin.appendScriptElement(String.format('GET?t=getarticles&pre=FreshPin.constants.data&p={0}', FreshPin.constants.p));
                    FreshPin.constants.dataType = 'articles';
                    return;
                }
                if (h() == 'stores') {
                    FreshPin.trackGACEvents('stores', 'Load', 'Loading Stores');
                    FreshPin.appendScriptElement(String.format('GET?t=getstores&pre=FreshPin.constants.data&p={0}', FreshPin.constants.p));
                    FreshPin.constants.dataType = 'stores';
                    return;
                }
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
    $s.getarticles = getarticles;
    $s.getqboards = getqboards;
    $s.getstores = getstores;
    $s.getusers = getusers;
    $s._his = _his;
})(window);
$(function () {
    $('#topcontrol').click(function () {
        $('html').animate({ scrollTop: 0 }, 'slow'); //IE, FF
        $('body').animate({ scrollTop: 0 }, 'slow'); //chrome, don't know if safary works
    });

    templates.picTemplate = $("#picTemplate").template();
    templates.articles = $("#articles").template();
    templates.stores = $("#stores").template();
    templates.boards = $("#boards").template();
    templates.users = $("#usertpl").template();
    $('#pin').jqm({
        overlay: 100,
        modal: true,
        onShow: function (hash) {
            FreshPin.closeall(hash);
            hash.w.show();
            hash.o.show();
        },
        onHide: function (hash) {
            hash.w.hide();
            hash.o.hide().remove();
            $('#pinCloseupImage').attr('src', '');
            $('#likepint div').removeClass('pinDisLike');
        }
    });
    function load() {
                if (h() == 'articles') {
                    getarticles(true);
                    return;
                }
                if (h() == 'stores') {
                    getstores(true);
                    return;
                }
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
        setsizeEl();
        //$('#pts').html(String.format('{0} {1}', (function (o) { return o ? o.total : 0; })(FreshPin.readCookieValue('pts')), strings.Points));
        $('#scroll_button').click(function () {
            togglePageScroll();
        });
        $('a[name="buttons"]').click(function () { $('#signUpWraper').jqmShow(); });
    }
    load();
    $('#fbd').jqm({
        overlay: 75,
        onShow: function (hash) {
            FreshPin.closeall(hash);
            hash.w.show();
            hash.w.animate({
                top: '20%'
            }, 'fast', 'swing');
        },
        onHide: function (hash) {
            hash.w.animate({
                top: '100%'
            }, 'fast', 'swing', function () {
                hash.w.hide();
                hash.o.hide().remove();
            });
        }
    });
    $('#fbdclose').click(function () { $('#fbd').jqmHide(); });
    $('#fb').click(function () {
        $('#fbd').jqmShow();
    });
    $('#savefeedback').click(function () {
        $.post('POST?t=fb', { fb: $('#tafb').val() }, function () { });
    });
    $(window).scroll(function () {
        var sp = $(document).scrollTop();
        var ch = $(document).height();
        var ph = $(window).height() * .8;
        if (sp > ph)
            $('#topcontrol').fadeIn('slow');
        else
            $('#topcontrol').fadeOut('slow');
        if (!h('rank')) {
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
        }
    });
    $(window).bind('hashchange', function (e) {
        reload(e);
        FreshPin.emit('hashchange', null, [e]);
    });
    function setRelative(menu, submenu) {
        var pos = $(menu).offset();
        var left = pos.left + "px";
        //show the menu directly over the placeholder  
        $(submenu).css({
            left: left
        });
    }
    setRelative('#mlevel1', '#submenu1');
    setRelative('#mlevel2', '#submenu2');
   

    FreshPin.ourHover('#mlevel1', '#submenu1');
    FreshPin.ourHover('#mlevel2', '#submenu2');
    

    $('#fbd').css(cssCenterY('#fbd'))
        .jqm({
            overlay: 75,
            onShow: function (hash) {
                FreshPin.closeall(hash);
                hash.w.show();
                hash.w.animate(cssCenterX(hash.w), 'fast', 'swing');
            },
            onHide: function (hash) {
                hash.w.animate({
                    top: '100%'
                }, 'fast', 'swing', function () {
                    hash.w.hide();
                    hash.o.hide().remove();
                });
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
                overlay: 100,
                onShow: function (hash) {
                    FreshPin.closeall(hash);
                    hash.w.show();
                    hash.w.animate(cssCenterX('#earnPointsCriteria'), 'fast', 'swing', function () {
                        $(window).scrollTop(0);
                    });
                },
                onHide: function (hash) {
                    hash.w.animate({
                        top: '100%'
                    }, 'fast', 'swing', function () {
                        hash.w.hide();
                        hash.o.hide().remove();
                    });
                }
            });
    $('#earnpoints').click(function () {
        $('#earnPointsCriteria').jqmShow();
    });
    $('#login').css(cssCenterY('#login')).jqm({
        overlay: 100,
        trigger: '*[name="logint"]',
        onShow: function (hash) {
            FreshPin.closeall(hash);
            hash.w
                .show();
            hash.w
                .animate(cssCenterX('#login'), 'fast', 'swing');
        },
        onHide: function (hash) {
            hash.w
                .animate({
                    top: '100%'
                }, 'fast', 'swing', function () {
                    hash.w
                    .hide();
                    hash.o
                    .hide()
                    .remove();
                });
        }
    });

    if (!FreshPin.authenticated()) {
        var emailregex = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$/, fup = false;
        $('#signUpWraper').css(cssCenterY('#signUpWraper'))
            .jqm({
                overlay: 100,
                trigger: '#reqin',
                onShow: function (hash) {
                    FreshPin.closeall(hash);
                    hash.w.show();
                    hash.w.animate(cssCenterX('#signUpWraper'), 'fast', 'swing');
                },
                onHide: function (hash) {
                    hash.w.animate({
                        top: '100%'
                    }, 'fast', 'swing', function () {
                        hash.w.hide();
                        hash.o.hide().remove();
                    });
                }
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
            $('#signUpWraper').jqmHide();
            $('#login').jqmShow();
        });
        function Login() {
            var user = $('#user').val();
            var pass = $('#pass').val();
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
        $.post('POST?t=addprofile', {email:email, name: name, pass: pass2, fn: fn, first_name: first_name, about: about, location: loc, website: website }, function (dt, res, opts) {
            //$(location).attr('hash', FreshPin.getru('settings'));
            $('#p_about').text(about);
            set();
            location.reload(true);
        }, 'text').error(function (res) {
            alert(res.responseText);
        });
    })
}, null, null, 'loadsignup');
