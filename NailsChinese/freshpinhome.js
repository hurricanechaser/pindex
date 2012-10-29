///<reference path="~/freshpin.js" />
var _refresh = true, _forcepinclose = false;

function getVal(o, p) {
    return p ? o[p] : o;
}

function _pinshown() {
    return $('#pin').css('display') != 'none';
}
function getHeight(o, w) {
    return (w || 190.00) * o.height / o.width;
}
function getUrl(o, t) {
    switch (t) {
        case 'p':
            return 'http://pinterest.com/pin/create/bookmarklet?media=' + absolutizeURI(o.url) + '&url=Pin?pin=' + o.PinID + '&alt=' + escape(o.title) + '&title=' + escape(o.title) + '&is_video=false';
            break;
    }
}
function rcat(v) {
    params = ['cat=' + escape(v)];
    $(location).attr('href', '#' + params.join('&'));
}
var ps = 50, s=50, p=0, loading = false;
function set() {
    s = 50, p = 0;
    $(".gallery").empty();
    $("#articlesLayout").empty();
    $("#articlesCont").hide();
    $(".gallery").hide();
    $(document).scrollTop(0);
    FreshPin.emit('set');
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
    }, rl, rl1, _his = [location.hash], templates = {
        picTemplate: null,
        articles: null,
        boards: null,
        stores: null
    };
    function cb(dt) {
        $(".gallery").show();
        var tmpl = $.tmpl(templates.picTemplate, dt);
        tmpl.appendTo(".gallery");
        if (rl) {
            $(".gallery").masonry('reload');
        } else {
            $(".gallery").masonry(masonryOpts);
            rl = true;
        }
        tmpl.find('.pinit').click(function () {
            var url = $(this).attr('href');
            if(url) {
                var w = window.open(url, 'pinit', 'width=800,height=600,resizable=yes,menubar=no,location=no,left=100px,top=100px,scrollbars=no', false);
                w.focus();
            }
        });
        tmpl.hover(function () {
            $(this).find('.buttons').slideDown(100);
            var boards = $(this).find('.boards');

            boards.slideDown(100);
        }, function () {
            $(this).find('.buttons').slideUp(100);
            $(this).find('.boards').slideUp(100);
        });
        FreshPin.emit('bindimagetmpl', {
            tmpl: tmpl
        });
    }
    function cbstores(dt) {
        $(".gallery").show();
        var tmpl = $.tmpl(templates.stores, dt);
        tmpl.appendTo(".gallery");
        if (rl) {
            $(".gallery").masonry('reload');
        } else {
            $(".gallery").masonry(masonryOpts);
            rl = true;
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
    function rankcb(dt) {
        $(".gallery").show();
        var images = [];
        for (var i = 0, l = dt.length; i < l; i++) {
            images.push(dt[i].url);
        }
        $("#loading").show();
        i = 0;
        var tmpl = $("#rank").tmpl(dt);
        tmpl.appendTo(".gallery");
        if (rl) $(".gallery").masonry('reload');
        else {
            $(".gallery").masonry(masonryOpts);
            rl = true;
        }
        $.imgpreload(images, {
            each: function () {
                var src = $(this).attr('src');
                var o = dt.filter(function (item) { return item.url == src; })[0];
                $(String.format("a[name={0}]", o.i)).append(this);
                $(".gallery").masonry('reload');
            },
            all: function () {
                //                                setTimeout(function () {             
                //                                   $(".gallery").masonry('reload');
                //                                }, 500);
            }
        });
    }
    function getrank(sc) {
        if (sc) {
            set();
            rankcb(FreshPin.constants.data);
        } else rankcb(FreshPin.constants.data);
    }
    function get(sc) {
        if (sc) {
            set();
            cb(FreshPin.constants.data.slice(0, s));
            s += ps;
        } else cb(FreshPin.constants.data.slice(s, s += ps));
    }
    function getarticles(sc) {
        if (sc) {
            set();
            cbarticles(FreshPin.constants.data.slice(0, s));
            s += ps;
        } else cbarticles(FreshPin.constants.data.slice(s, s += ps));
    }
    function getstores(sc) {
        if (sc) {
            set();
            cbstores(FreshPin.constants.data.slice(0, s));
            s += ps;
        } else cbstores(FreshPin.constants.data.slice(s, s += ps));
    }
    function setpin() {
        var o = FreshPin.constants.pin;
        if (o.editable && FreshPin.authenticated()) {
            $('#editpint').css('display', '');
            $('#likepint').css('display', 'none');
        } else {
            $('#editpint').css('display', 'none');
            $('#likepint').css('display', '');
            if (o.liked)
                $('#likepint div').css('color', '#CCCCCC');
        }
        $('#pin').css({
            'margin-left': 0 - ((o.width / 2) + 30),
            'top': $(window).scrollTop() + 100
        });
        $('#pinCloseupImage').attr('src', o.url);
        $('#pinCloseupImage').attr('alt', o.title);
        $('#pintitle').html(o.title);
        $('#pintitle').width(o.width + 60);
        $('#pin').jqmShow();
    }
    function reload() {
        _his.push(location.hash);
        FreshPin.trackGACPV();
        if (h() != 'norefresh' && _refresh) {
            var pin = h('pin');
            if (pin) {
                FreshPin.trackGACEvents('pin', 'Load', String.format('Pin-{0}', h('pin')));
                if (FreshPin.constants.data.length > 0) {
                    var pino = $.grep(FreshPin.constants.data, function (item) { return item.PinID == pin; });
                    if (pino.length > 0) {
                        FreshPin.constants.pin = pino[0];
                        setpin();
                        return;
                    }
                }
                $.getJSON('GET?t=getpin', { pin: pin }, function (dt, status, res) {
                    FreshPin.constants.pin = dt;
                    setpin();
                });
                return;
            }

            if (h('stores')) {
                FreshPin.trackGACEvents('stores', 'Load', 'Loading Stores');
                if (_pinshown()) {
                    _forcepinclose = true;
                    $('#pin').jqmHide();
                }
                $.getJSON('GET?t=getstores', {
                    p: p
                }, function (dt, status, res) {
                    FreshPin.constants.data = dt;
                    getstores(true);
                });
                return;
            }
            if (h('articles')) {
                FreshPin.trackGACEvents('articles', 'Load', 'Loading Articles');
                if (_pinshown()) {
                    _forcepinclose = true;
                    $('#pin').jqmHide();
                }
                $.getJSON('GET?t=getarticles', {
                    p: p
                }, function (dt, status, res) {
                    FreshPin.constants.data = dt;
                    getarticles(true);
                });
                return;
            }
            var q = h('rank');
            if (q) {
                FreshPin.trackGACEvents('rank', 'Load');
                if (_pinshown()) {
                    _forcepinclose = true;
                    $('#pin').jqmHide();
                }
                $.getJSON('GET?t=getpeople', {
                    ps: 100,
                    qt: q
                }, function (dt, status, res) {
                    FreshPin.constants.data = dt;
                    getrank(true);
                });
                return;
            }
            if (!h() || h('cat') || h('q') || h() == 'norefresh') {
                FreshPin.trackGACEvents('nails', 'Load', String.format('Query-{0},Cat-{1},Board-{2},Filter-{3},Page-{4}', h('q'), h('cat'), h('board'), h('filter'), p));
                $.getJSON('GET?t=getimages', {
                    cat: h('cat'),
                    q: h('q'),
                    p: p,
                    filter: h('filter'),
                    board: h('board')
                }, function (dt, status, res) {
                    if (_pinshown()) {
                        _forcepinclose = true;
                        $('#pin').jqmHide();
                    }
                    FreshPin.constants.data = dt;
                    get(true);
                });
            }
        } else {
            FreshPin.trackGACEvents('nails', 'Load', String.format('Query-{0},Cat-{1},Board-{2},Filter-{3},Page-{4}', h('q'), h('cat'), h('board'), h('filter'), p));
            _refresh = true;
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            if (!FreshPin.constants.data) {
                $.getJSON('GET?t=getimages', {
                    cat: h('cat'),
                    q: h('q'),
                    p: p,
                    filter: h('filter'),
                    board: h('board')
                }, function (dt, status, res) {
                    FreshPin.constants.data = dt;
                    get(true);
                });
            }
        }
        FreshPin.emit('reload');
    }
    function load() {
        FreshPin.trackGACPV();
        if (h() == 'articles') {
            FreshPin.trackGACEvents('articles', 'Load', 'Loading Articles');
            FreshPin.appendScriptElement(String.format('GET?t=getarticles&pre=FreshPin.constants.data&p={0}', p));
            return;
        }
        if (h() == 'stores') {
            FreshPin.trackGACEvents('stores', 'Load', 'Loading Stores');
            FreshPin.appendScriptElement(String.format('GET?t=getstores&pre=FreshPin.constants.data&p={0}', p));
            return;
        }
        var q = h('rank');
        if (q) {
            FreshPin.trackGACEvents('rank', 'Load', String.format('Query-{0}', q));
            FreshPin.appendScriptElement(String.format('GET?t=getpeople&pre=FreshPin.constants.data&ps=100&qt={0}', q));
            return;
        }
        var pin = h('pin');
        if (pin) {
            FreshPin.trackGACEvents('pin', 'Load', String.format('Pin-{0}', h('pin')));
            FreshPin.appendScriptElement(String.format('GET?t=getpin&pre=FreshPin.constants.pin&pin={0}', pin));
            return;
        }
        if (!h() || h('cat') || h('q') || h() == 'norefresh') {
            FreshPin.trackGACEvents('nails', 'Load', String.format('Query-{0},Cat-{1}', h('q'), h('cat')), h('pin'));
            FreshPin.appendScriptElement(String.format('GET?t=getimages&pre=FreshPin.constants.data&{0}', FreshPin.serialize({ p: p, cat: h('cat'), q: h('q') })));
        }
        FreshPin.emit('load');
    }
    load();
    $s.templates = templates;
    $s.setpin = setpin;
    $s.reload = reload;
    $s.get = get;
    $s.getrank = getrank;
    $s.getarticles = getarticles;
    $s.getstores = getstores;
    $s._his = _his;
})(window);
$(function () {
    templates.picTemplate = $("#picTemplate").template();
    templates.articles = $("#articles").template();
    templates.stores = $("#stores").template();
    $('#pin').jqm({
        overlay: 75,
        onShow: function (hash) {
            var w = hash.w;
            var o = hash.o;
            w.show();
            o.show();
        },
        onHide: function (hash) {
            var w = hash.w;
            var o = hash.o;
            if (_forcepinclose) {
                w.hide();
                o.hide();
                $('#pinCloseupImage').attr('src', '');
                _forcepinclose = false;
            } else {
                var _back = _his[_his.length - 2];
                if (_back != undefined) {
                    w.hide();
                    o.hide();
                    $('#pinCloseupImage').attr('src', '');
                    _refresh = false;
                    $(location).attr('href', _back || '#norefresh');
                }

            }
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
        var q = h('rank');
        if (q) {
            getrank(true);
            return;
        }
        var pin = h('pin');
        if (pin) {
            setpin();
            return;
        }
        if (!h() || h('cat') || h('q') || h() == 'norefresh') {
            get(true);
        }
    }
    load();
    //    $('#fbd').jqm({
    //        overlay: 75,
    //        onShow: function (hash) {
    //            hash.w.show();
    //            hash.w.animate({
    //                top: '20%'
    //            }, 'fast', 'swing');
    //        },
    //        onHide: function (hash) {
    //            hash.w.animate({
    //                top: '100%'
    //            }, 'fast', 'swing', function () {
    //                hash.w.hide();
    //                hash.o.hide();
    //            });
    //        }
    //    });
    //    $('#fbdclose').click(function () { $('#fbd').jqmHide(); });
    //    $('#fb').click(function () {
    //        $('#fbd').jqmShow();
    //    });
    //    $('#savefeedback').click(function () {
    //        $.post('POST?t=fb',{fb:$('#tafb').val()},function() {
    //        });
    //    
    $(document).scroll(function () {
        var sp = $(document).scrollTop();
        var ch = $(document).height();
        var ph = $(window).height();
        if (!h('rank')) {
            if (!loading) {
                if ((ch - ph - sp) < 500) {
                    if (h('stores')) {
                        if (s >= FreshPin.constants.data.length) {
                            loading = true;
                            $.getJSON('GET?t=getstores', {
                                p: ++p
                            }, function (dt, status, res) {
                                $.each(dt, function (ind, item) { FreshPin.constants.data.push(item); });
                                getstores(false);
                                loading = false;
                            });
                        } else getstores(false);
                        return;
                    }
                    if (h('articles')) {
                        if (s >= FreshPin.constants.data.length) {
                            loading = true;
                            $.getJSON('GET?t=getarticles', {
                                p: ++p
                            }, function (dt, status, res) {
                                $.each(dt, function (ind, item) { FreshPin.constants.data.push(item); });
                                getarticles(false);
                                loading = false;
                            });
                        } else getarticles(false);
                        return;
                    }
                    if (!h() || h('cat') || h('q') || h() == 'norefresh') {
                        if (s >= FreshPin.constants.data.length) {
                            loading = true;
                            $.getJSON('GET?t=getimages', {
                                cat: h('cat'),
                                q: h('q'),
                                p: ++p,
                                filter: h('filter')
                            }, function (dt, status, res) {
                                $.each(dt, function (ind, item) { FreshPin.constants.data.push(item); });
                                get(false);
                                loading = false;
                            });
                        } else get(false);
                    }
                }
            }
        }
        FreshPin.emit('scroll', null, [sp, ch, ph]);
    });
    $(window).bind('hashchange', function () {
        reload();
        FreshPin.emit('hashchange');
    });
    FreshPin.ourHover('#mlevel2', '#submenu2', {
        rel: true
    });
    FreshPin.ourHover('#mlevel3', '#submenu3', {
        rel: true
    });
    FreshPin.ourHover('#mlevel5', '#submenu5', {
        rel: true
    });
});