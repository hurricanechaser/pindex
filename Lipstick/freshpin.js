/// <reference path="scripts/jquery-1.7.1.js" />
///<reference src="scripts/scrolltopcontrol.js" />
///<reference src="scripts/jquery-jquery-tmpl/jquery.tmpl.js" />
///<reference src="scripts/jquery-jquery-tmpl/jquery.tmplPlus.js" />
///<reference src="scripts/desandro-masonry/jquery.masonry.js" />
///<reference src="scripts/jquery.fancybox-1.3.4/fancybox/jquery.fancybox-1.3.4.js"/>
/// <reference path="scripts/cookies.js" />

String.format = function (text) {
    if (arguments.length <= 1) {
        return text;
    }
    var tokenCount = arguments.length - 2;
    for (var token = 0; token <= tokenCount; token++) {
        text = text.replace(new RegExp("\\{" + token + "\\}", "gi"), arguments[token + 1]);
    }
    return text;
};

function parseURI(url) {
    var m = String(url).replace(/^\s+|\s+$/g, '').match(/^([^:\/?#]+:)?(\/\/(?:[^:@]*(?::[^:@]*)?@)?(([^:\/?#]*)(?::(\d*))?))?([^?#]*)(\?[^#]*)?(#[\s\S]*)?/);
    return (m ? {
        href: m[0] || '',
        protocol: m[1] || '',
        authority: m[2] || '',
        host: m[3] || '',
        hostname: m[4] || '',
        port: m[5] || '',
        pathname: m[6] || '',
        search: m[7] || '',
        hash: m[8] || ''
    } : null);
}

function absolutizeURI(base, href) {
    function removeDotSegments(input) {
        var output = [];
        input.replace(/^(\.\.?(\/|$))+/, '')
         .replace(/\/(\.(\/|$))+/g, '/')
         .replace(/\/\.\.$/, '/../')
         .replace(/\/?[^\/]*/g, function (p) {
             if (p === '/..') {
                 output.pop();
             } else {
                 output.push(p);
             }
         });
        return output.join('').replace(/^\//, input.charAt(0) === '/' ? '/' : '');
    }
    href = parseURI(href || '');
    base = parseURI(base || '');
    return !href || !base ? null : (href.protocol || base.protocol) +
         (href.protocol || href.authority ? href.authority : base.authority) +
         removeDotSegments(href.protocol || href.authority || href.pathname.charAt(0) === '/' ? href.pathname : (href.pathname ? ((base.authority && !base.pathname ? '/' : '') + base.pathname.slice(0, base.pathname.lastIndexOf('/') + 1) + href.pathname) : base.pathname)) +
         (href.protocol || href.authority || href.pathname ? href.search : (href.search || base.search)) +
         href.hash;
}
(function (s) {
    function get(query, v) {
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == v) {
                return unescape(pair[1]);
            }
        }
    }
    s.q = function (v) {
        var query = window.location.search.substring(1);
        return v ? get(query, v) : query;
    };
    s.h = function (v) {
        var query = window.location.hash.substring(1);
        return v ? get(query, v) : query;
    };
})(window);


var FreshPin = {
    constants: {
        data: null,

        //        domain: 'http://freshpindev.botcodelocal.com/',
        //        cdn: 'http://freshpindev.botcodelocal.com/cdn/',
        //        upl: 'http://freshpindev.botcodelocal.com/nails/uploaded/',

        //Staging
        //        domain: 'http://photogallery.botcodelocal.com/nails/',
        //        cdn: 'http://photogallery.botcodelocal.com/cdn/',
        //        upl: 'http://photogallery.botcodelocal.com/cdn/content/nailsuploaded/',

        //Prod
        domain: 'http://pinpolish.com/',
        cdn: 'http://freshpin.com/cdn/',
        upl: 'http://freshpin.com/cdn/freshpinprod/nailsuploaded/',
        authcookie: 'nauth',
        noblock: false,
        infocookie: 'info',
        sessioncookie: 'session',
        boards: null,
        charCount: 500,
        file: null,
        pin: null,
        categories: null,
        menuShowInterval: 100,
        addpinpreviewwidth: 170,
        selectedCategory: null,
        selectedCategoryName: null,
        logout: '#logout',
        logoutUrl: '.',
        userBlankImg: 'img/userImage.gif',
        res: null,
        selectedImageID: null,
        selectedRec: null,
        selectedBoard: null,
        s: 50,
        ps: 50,
        p:0,
        loading: false
    },
    resetValues: function () {
        this.constants.selectedImageID = this.constants.selectedRec = this.constants.selectedBoard =
            this.constants.selectedCategoryName = this.constants.selectedCategory = null;
    },
    readCookieValue: function (key, cookie) {
        var o = JSON.parse(Cookie.get(cookie || this.constants.sessioncookie));
       return o ? o[key] : null;
    },
    writeCookieValue: function (key, value, cookie) {
        var o = JSON.parse(Cookie.get(cookie || this.constants.sessioncookie));
        o[key] = value;
        Cookie.set(cookie || this.constants.sessioncookie, JSON.stringify(o), { path: '/' });
    },
    _mask: null,
    trackGACEvents: function (category, action, label, value, ni) {
        _gaq.push(['_trackEvent', category, action, label, value, ni]);
    },
    trackGACPV: function () {
        _gaq.push(['_trackPageview', location.pathname + location.search + location.hash]);
    },
    blockUI: function () {
        $(document.body).css('display', 'block');
        var msk = this._mask = $('<div  />');
        msk.css({
            "-moz-opacity": "0.9",
            "opacity": "0.9",
            "filter": "alpha(opacity=90)",
            "width": "100%",
            "background-color": "#FFFFFF",
            "height": "100%",
            "z-index": "4",
            "top": "0",
            "left": "0",
            "position": "fixed"
        });
        var ld = $(String.format('<img src="{0}img/dots64.gif"  />', FreshPin.constants.cdn));
        ld.css({
            "z-index": "10",
            "position": "fixed",
            "top": "48%",
            "left": "48%",
            "background-repeat": "no-repeat"
        });
        msk.append(ld).appendTo(document.body);
    },
    serialize: function (obj) {
        var str = [];
        for (var p in obj) {
            if (p != undefined && obj[p] != undefined) {
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
            }
        }
        return str.join("&");
    },
    unblockUI: function () {
        this._mask.remove();
    },
    appendScriptElement: function (url) {
        document.write(String.format('<script src="{0}" type="text/javascript"></script>', url));
    },

    loadCss: function (url) {
        if (document.createStyleSheet) {
            document.createStyleSheet(this.constants.cdn + url);
        }
        else {
            $("head").append($("<link rel='stylesheet' href='" + this.constants.cdn + url + "' type='text/css' media='screen' />"));
        }
    },
    tobool: function (s) {
        return "true" == s;
    },
    imgMap: function (s, u, attr) {
        $(s).attr(attr || 'src', this.constants.cdn + u);
    },
    authenticated: function () {
        return document.cookie.indexOf(this.constants.authcookie) != -1;
    },
    visited: function () {
        var o = this.getCookie();
        return o && !isNaN(o.vuID);
    },
    getCVal: function (p) {
        var val = $.parseJSON(Cookie.get(this.constants.infocookie));
        return val[p];
    },
    getCookie: function () {
        var val = Cookie.get(this.constants.infocookie);
        return val ? $.parseJSON(val) : null;
    },
    getUN: function () {
        var o = this.getCookie();
        return o && o.name ? o.name : 'User Name';
    },
    getEmail: function () {
        var o = this.getCookie();
        return o && o.email ? o.email : null;
    },

    getAV: function () {
        var o = this.getCookie();
        return o && o.avatar ? o.avatar : this.constants.cdn + this.constants.userBlankImg;
    },
    _funcs: [],
    _emitted: [],
    attach: function (fn, args, scope, group) {
        this._funcs.push({ fn: fn, args: args, scope: scope, group: group });
        var emits = $.grep(this._emitted, function (item) {
            return item.group == group;
        });
        $.each(emits, function (ind, o) {
            try {
                fn.apply(o.scope || scope || this, o.args || args || []);
            } catch (e) {
                console.error(e);
            }
        });

    },
    emit: function (group, scope, args) {
        this._emitted.push({ group: group, scope: scope, args: args });
        var filtered = $.grep(this._funcs, function (it, ind, arr) {
            return it.group == group;
        });
        for (var i = 0, l = filtered.length; i < l; i++) {
            var o = filtered[i];
            try {
                o.fn.apply(scope || o.scope || this, args || o.args || []);
            } catch (e) {
                console.error(e);
            }
        }
    },
    _returnurl: {},
    setru: function (group, url, force) {
        if (this._returnurl[group] == undefined || force != undefined)
            this._returnurl[group] = url;
    },
    getru: function (group) {
        var tmp = this._returnurl[group];
        this._returnurl[group] = undefined;
        return tmp;
    },
    ourHover: function (menu, submenu, opts) {
        var submenuhovered;
        $(menu).mouseenter(opts, function (e) {
            var hovered = true;
            var me = this;
            var opts = e.data;
            setTimeout(function () {
                if (hovered) {
                    if (opts && opts.menuMouseIn) opts.menuMouseIn.call(me);
                    if (opts && opts.rel) {
                        var pos = $(me).offset();
                        var eWidth = $(me).outerWidth();
                        var mWidth = $(submenu).outerWidth();
                        var left = pos.left + "px";
                        var top = 20 + pos.top + "px";
                        //show the menu directly over the placeholder  
                        $(submenu).css({
                            left: left
                        });
                        $(submenu).show();
                    }
                    else
                        $(submenu).show();
                }
            }, FreshPin.constants.menuShowInterval);

        }).mouseleave(opts, function (e) {
            var hovered = false;
            var me = this;
            var opts = e.data;
            setTimeout(function () {
                if (!hovered) {
                    if (!submenuhovered) {
                        if (opts && opts.menuMouseOut) opts.menuMouseOut.call(me);
                        $(submenu).hide();
                    }
                }
            }, FreshPin.constants.menuShowInterval);
        });
        $(submenu).mouseenter(opts, function (e) {
            var opts = e.data;
            if (opts && opts.submenuMouseIn) opts.submenuMouseIn.call(this);
            submenuhovered = true;
        }).mouseleave(opts, function (e) {
            var opts = e.data;
            if (opts && opts.submenuMouseOut) opts.submenuMouseOut.call(this);
            submenuhovered = false;
            $(this).hide();
        });
    }
}
$(function () {
    var contentpagesflg = $.grep(['about.html', 'copyright.html', 'privacy.html', 'terms.html', 'faq.html', 'help.html', 'pinpractices.html'], function (i) {
        return $(location).attr('href').toLowerCase().indexOf(i.toLowerCase()) != -1;
    }).length > 0;
    $(FreshPin.constants.logout).click(function () {
        Cookie.remove(FreshPin.constants.authcookie);
        $(window.location).attr('href', FreshPin.constants.logoutUrl);
    });

    $('#query_button').off();
    $('#query').focus(function () { if ($(this).val() == 'Search over 500,000 Pins') { $(this).val(''); } });
    $('#query').blur(function () { if ($(this).val() == '') { $(this).val('Search over 500,000 Pins'); } });
    function search() {
        if (contentpagesflg)
            FreshPin.constants.noblock = true;
        var q1 = $('#query').val();
        if (q1 != '') {
            params = ['q=' + q1];
            $(location).attr('href', '.#' + params.join('&'));
        }
    }
    $('#query_button').click(this, search);
    $('#query').bind('keyup', 'return', search);
    var pre = $(location).prop('pathname') == '/' ? '' : '.';
    /**************************************************login html snippet*******************************************************************/

    if (FreshPin.authenticated()) {
        $('#logininfo').html('<a class="nav" style="padding: 20px 27px 11px 40px;"><span '
                        + 'class="userProfilePic">'
                       + String.format(' <img  id="userProfilePic" src="{0}?width=20"/></span><p id="username">{1}</p>', FreshPin.getAV(), FreshPin.getUN())
                       + ' <span></span></a>'
                       + ' <ul>'
                       + String.format('<li><a href="{0}#boards">Boards</a></li>', pre)
                        + String.format('<li class="beforeDivider"><a href="{0}#filter=pins">Pins</a></li>', pre)
                        + String.format('<li class="divider"><a href="{0}#filter=likes">Likes</a></li>', pre)
                         + String.format('<li><a href="{0}#settings">Settings</a></li>', pre)
                         + '<li><a id="logout" href="javascript:void(0)">Log Out</a></li>'
                       + ' </ul>');
        $('#logout').click(function () {
            Cookie.remove(FreshPin.constants.authcookie);
            $(window.location).attr('href', '.');
        });
    } else if (FreshPin.visited()) {
        $('#logininfo').html('<a class="nav" style="padding: 20px 27px 11px 40px;"><span '
                        + 'class="userProfilePic">'
                       + String.format(' <img  id="userProfilePic" src="{0}?width=20"/></span><p id="username">{1}</p>', FreshPin.getCVal('vuavatar'), FreshPin.getCVal('vuname'))
                       + ' <span></span></a>'
                       + ' <ul>'
                       + String.format('<li><a name="noblock" href="{0}#boards">Boards</a></li>', pre)
                        + String.format('<li class="beforeDivider"><a name="noblock" href="{0}#filter=pins">Pins</a></li>', pre)
                        + String.format('<li class="divider"><a name="noblock" href="{0}#filter=likes">Likes</a></li>', pre)
                       + ' </ul>');
        $('#addtrigger').css('display', 'none');
    } else {
        $('#logininfo').html(String.format('<a name="noblock" id="logint" href="{0}" class="nav">Login</a>', contentpagesflg ? 'login' : 'javascript:void(0);'));
        $(window).bind('beforeunload', function () {
            var str = String.format('You have unsaved points:{0}.You can save these points by signing up for an account.Click Ok to to sign up for an account (or) cancel to continue', (function (o) { return o ? o.total : 0; })(FreshPin.readCookieValue('pts')));
            if (/Firefox[\/\s](\d+)/.test(navigator.userAgent) && new Number(RegExp.$1) >= 4) {
                if ((function (o) { return o ? o.total : 0; })(FreshPin.readCookieValue('pts')) == 0 || FreshPin.constants.noblock || !confirm(str)) {
                    history.go();
                } else {
                    setTimeout(function () { window.stop(); }, 1);
                }
            } else {
                return String.format('You have unsaved points:{0}.You can save these points by signing up for an account.', FreshPin.constants.points.total);
            }
        });
        $('a[name="noblock"]').click(function () { FreshPin.constants.noblock = true; });
    }
    /**************************************************End Of login html snippet*******************************************************************/
});

