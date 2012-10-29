/// <reference path="scripts/jquery-1.7.1.js" />
///<reference src="scripts/scrolltopcontrol.js" ></script>
///<reference src="scripts/jquery-jquery-tmpl/jquery.tmpl.js" ></script>
///<reference src="scripts/jquery-jquery-tmpl/jquery.tmplPlus.js" ></script>
///<reference src="scripts/desandro-masonry/jquery.masonry.js" ></script>
///<reference src="scripts/jquery.fancybox-1.3.4/fancybox/jquery.fancybox-1.3.4.js"/>
/// <reference path="scripts/cookies.js" />
if (!Array.prototype.filter) {
    Array.prototype.filter = function (fun /*, thisp*/) {
        var len = this.length;
        if (typeof fun != "function")
            throw new TypeError();
        var res = new Array();
        var thisp = arguments[1];
        for (var i = 0; i < len; i++) {
            if (i in this) {
                var val = this[i]; // in case fun mutates this
                if (fun.call(thisp, val, i, this))
                    res.push(val);
            }
        }
        return res;
    };
}
var FreshPin = {
    constants: {
        data: null,
        cdn: '.',
        fpauth: 'auth',
        nauth: 'nauth',
        boards: null,
        charCount: 500,
        file: null,
        categories: null,
        menuShowInterval: 500,
        addpinpreviewwidth: 170,
        selectedCategory: null,
        logout: '#logout',
        logoutUrl: '.',
        selectedImageID: null,
        selectedBoard: null
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
    getCookie: function () {
        return $.parseJSON(Cookie.get(this.constants.fpauth));
    },
    setCookie: function () {
        return $.parseJSON(Cookie.get(this.constants.fpauth));
    },
    getUN: function () {
        var o = this.getCookie();
        return o.name ? o.name : 'User Name';
    },
    getEmail: function () {
        var o = this.getCookie();
        return o.email;
    },
    getAV: function () {
        var o = $.parseJSON(Cookie.get(this.constants.fpauth));
        return o.avatar ? o.avatar : this.constants.cdn + 'img/userImage.gif';
    },
    _funcs: [],
    _emitted: [],
    attach: function (fn, scope, group) {
        this._funcs.push({ fn: fn, args: args, scope: scope, group: group });
        if (this._emitted.indexOf(group) !== -1)
            fn.apply(scope || this, args || []);
    },
    emit: function (group, scope) {
        this._emitted.push(group);
        var filtered = this._funcs.filter(function (it, ind, arr) {
            return it.group == group;
        });
        for (var i = 0, l = filtered.length; i < l; i++) {
            var o = filtered[i];
            try {
                o.fn.apply(o.scope || scope || this, o.args || []);
            } catch (e) {
                console.error(e);
            }
        }
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
    /**************************************************login html snippet*******************************************************************/

    if (FreshPin.authenticated()) {
        $('.logininfo').html('<a class="nav" style="padding: 20px 27px 11px 40px;"><span '
                        + 'class="userProfilePic">'
                       + String.format('<img  id="userProfilePic" src="{0}?width=20"/></span><p id="username">{1}</p>', FreshPin.getAV(), FreshPin.getUN())
                       + ' <span></span></a>'
                       + ' <ul>'
                       + '<li><a href="boards">Boards</a></li>'
                        + '<li class="beforeDivider"><a href=".?filter=pins">Pins</a></li>'
                        + '<li class="divider"><a href=".?filter=likes">Likes</a></li>'
                         + '<li><a href="settings">Settings</a></li>'
                         + '<li><a id="logout" href="javascript:void(0)">Log Out</a></li>'
                       + ' </ul>');
        $('#logout').click(function () {
            Cookie.remove(FreshPin.constants.fpauth);
            $(window.location).attr('href', '.');
        });
        $('#username').html(FreshPin.getUN());
        $('#userProfilePic').attr('src', FreshPin.getAV() + '?width=20');
    } else {
        $('.logininfo').html('<a href="login" class="nav">Login</a>');
    }
    /**************************************************End Of login html snippet*******************************************************************/
    $(FreshPin.constants.logout).click(function () {
        Cookie.remove(FreshPin.constants.fpauth);
        $(window.location).attr('href', FreshPin.constants.logoutUrl);
    });
    $('.searchfield').focus(function () { if ($(this).val() == 'Search over 500,000 Pins') { $(this).val(''); } });
    $('.searchfield').blur(function () { if ($(this).val() == '') { $(this).val('Search over 500,000 Pins'); } });
    var search = function () {
        var q1 = $('.searchfield').val();
        if (q1 != '') {
            params = ['q=' + q1];
            $(location).attr('href', '?' + params.join('&'));
        }
    };
    $('.searchbutton').click(this, search);
    $('.searchfield').bind('keyup', 'return', function () {
        var q1 = $('.searchfield').val();
        if (q1 != '') {
            params = ['q=' + q1];
            $(location).attr('href', '?' + params.join('&'));
        }
    });
});

