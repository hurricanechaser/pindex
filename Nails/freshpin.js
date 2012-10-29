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
    var m = String(url).replace(/^\s+|\s+$/g, '')
        .match(/^([^:\/?#]+:)?(\/\/(?:[^:@]*(?::[^:@]*)?@)?(([^:\/?#]*)(?::(\d*))?))?([^?#]*)(\?[^#]*)?(#[\s\S]*)?/);
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
        return output.join('')
            .replace(/^\//, input.charAt(0) === '/' ? '/' : '');
    }
    href = parseURI(href || '');
    base = parseURI(base || '');
    return !href || !base ? null : (href.protocol || base
        .protocol) + (href.protocol || href
        .authority ? href
        .authority : base
        .authority) + removeDotSegments(href.protocol || href
        .authority || href
        .pathname
        .charAt(0) === '/' ? href
        .pathname : (href.pathname ? ((base.authority && !base
        .pathname ? '/' : '') + base.pathname
        .slice(0, base.pathname
        .lastIndexOf('/') + 1) + href
        .pathname) : base
        .pathname)) + (href.protocol || href
        .authority || href
        .pathname ? href
        .search : (href.search || base
        .search)) + href.hash;
}
function deparam(params, coerce) {
    var obj = {}, decode = decodeURIComponent,
      coerce_types = { 'true': !0, 'false': !1, 'null': null };
    $.each(params.replace(/\+/g, ' ').split('&'), function (j, v) {
        var param = v.split('='),
        key = decode(param[0]),
        val,
        cur = obj,
        i = 0,
        keys = key.split(']['),
        keys_last = keys.length - 1;
        if (/\[/.test(keys[0]) && /\]$/.test(keys[keys_last])) {
            keys[keys_last] = keys[keys_last].replace(/\]$/, '');
            keys = keys.shift().split('[').concat(keys);
            keys_last = keys.length - 1;
        } else {
            keys_last = 0;
        }
        if (param.length === 2) {
            val = decode(param[1]);
            if (coerce) {
                val = val && !isNaN(val) ? +val
            : val === 'undefined' ? undefined
            : coerce_types[val] !== undefined ? coerce_types[val]
                : val;
            }

            if (keys_last) {
                for (; i <= keys_last; i++) {
                    key = keys[i] === '' ? cur.length : keys[i];
                    cur = cur[key] = i < keys_last
              ? cur[key] || (keys[i + 1] && isNaN(keys[i + 1]) ? {} : [])
              : val;
                }

            } else {
                if ($.isArray(obj[key])) {
                    obj[key].push(val);
                } else if (obj[key] !== undefined) {
                    obj[key] = [obj[key], val];
                } else {
                    obj[key] = val;
                }
            }

        } else if (key) {
            obj[key] = coerce
          ? undefined
          : '';
        }
    });

    return obj;
};
function diffArray(a, b) {
    var seen = [],
        diff = [];
    for (var i = 0; i < b.length; i++)
        seen[b[i]] = true;
    for (var i = 0; i < a.length; i++)
        if (!seen[a[i]]) diff.push(a[i]);
    return diff;
}
(function (s) {
    var mat = [
        ['cat', 'size', 'style']
    ];

    function get(query, v) {
        var vars = query.split("&");
        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");
            if (pair[0] == v) {
                return unescape(pair[1]);
            }
        }
    }
    s.q = function (v, o) {
        if (o) {
            location.hash += String
                .format('{0}{1}={2}', location.hash == '' ? '?' : '&', v, o);
        } else {
            var query = window.location
                .search
                .substring(1);
            return v ? get(query, v) : query;
        }
    };
    s.rh = function (k) {
        var query = window.location
            .hash
            .substring(1);
        if (query != '') {
            var split = query.split('&');
            var pairs = [];
            for (var i = 0; i < split.length; i++) {
                var split1 = split[i].split('=');
                var key = split1[0];
                var value = split1[1];
                if (key != k) pairs.push(value ? String.format('{0}={1}', key, value) : key);
            }
            location.hash = pairs
                .length > 0 ? pairs
                .join('&') : '';
        }
    };
    s.ho = function (q) {
        var query = q || window.location
            .hash
            .substring(1);
        var k = {};
        k.equals = function (x) {
            var p;
            for (p in this) {
                if (typeof (x[p]) == 'undefined') {
                    return false;
                }
            }
            for (p in this) {
                if (this[p]) {
                    switch (typeof (this[p])) {
                        case 'object':
                            if (!this[p].equals(x[p])) {
                                return false;
                            }
                            break;
                        case 'function':
                            if (typeof (x[p]) == 'undefined' || (p != 'equals' && this[p].toString() != x[p]
                            .toString())) return false;
                            break;
                        default:
                            if (this[p] != x[p]) {
                                return false;
                            }
                    }
                } else {
                    if (x[p]) return false;
                }
            }
            for (p in x) {
                if (typeof (this[p]) == 'undefined') {
                    return false;
                }
            }
            return true;
        };
        if (query != '') {
            var split = query.split('&');
            for (var i = 0; i < split.length; i++) {
                var split1 = split[i].split('=');
                var key = split1[0];
                var value = split1[1];
                k[key] = value ? value : null;
            }
        }
        return k;
    },
    s.h = function (k, v) {
        var query = window.location
            .hash
            .substring(1);
        if (v) {
            if (query != '') {
                var split = query.split('&');
                var pairs = [],
                    keys = [];
                for (var i = 0; i < split.length; i++) {
                    var split1 = split[i].split('=');
                    var key = split1[0];
                    var value = (key == k) ? v : split1[1];
                    pairs.push(value ? String.format('{0}={1}', key, value) : key);
                    keys.push(key);
                }
                if ($.inArray(k, keys) == -1) {
                    pairs.push(v ? String.format('{0}={1}', k, v) : k);
                    keys.push(k);
                }
                var tmp;
                for (i = 0; i < mat.length; i++) {
                    var it = mat[i];
                    var diff = diffArray(it, keys);
                    tmp = diff.length == (it.length - keys
                        .length);
                    if (tmp) break;
                }
                location.hash = tmp ? pairs
                    .join('&') : String
                    .format('{0}={1}', k, v);
            } else location.hash = String
                .format('{0}={1}', k, v);
        } else {
            return k ? get(query, k) : query;
        }
    };
})(window);


var FreshPin = {
    constants: {
        data: null,
        noblock: false,
        boards: null,
        charCount: 500,
        contributors: [],
        state: {},
        file: null,
        pin: null,
        categories: null,
        menuShowInterval: 100,
        addpinpreviewwidth: 170,
        selectedCategory: null,
        preventReload: true,
        selectedCategoryName: null,
        dataType: null,
        logout: '#logout',
        logoutUrl: '.',
        userBlankImg: 'img/userImage.gif',
        res: null,
        selectedImageID: null,
        selectedRec: null,
        selectedBoard: null,
        s: 50,
        ps: 50,
        p: 0,
        loading: false
    },
    server: {},
    closeall: function (hash) {
        $.each($.jqm
            .hash, function (ind, item) {
                if (item != hash && item.o) {
                    item.w
                    .jqmHide();
                }
            });
    },
    resetValues: function () {
        FreshPin.constants
            .selectedImageID = FreshPin
            .constants
            .selectedRec = FreshPin
            .constants
            .selectedBoard = FreshPin
            .constants
            .selectedCategoryName = FreshPin
            .constants
            .selectedCategory = null;
        FreshPin.constants
            .contributors = [];
    },
    writeCookieValue: function (key, value, cookie) {
        var o = deparam(Cookies.get(cookie || FreshPin.constants
            .infocookie));
        o[key] = value;
        Cookies.set(cookie || FreshPin.constants
            .infocookie, $.param(o));
    },
    _mask: null,
    trackGACEvents: function (category, action, label, value, ni) {
        _gaq.push(['_trackEvent', category, action, label, value, ni]);
    },
    trackGACPV: function () {
        _gaq.push(['_trackPageview', location.pathname + location.search + location.hash]);
    },
    _state: null,
    pushState: function (v) {
        if (!FreshPin._state) FreshPin._state = new Hashtable();
        var tmp = FreshPin._state;
        var k = ho();
        tmp.put(k, v);
    },
    getState: function () {
        if (FreshPin._state) {
            return FreshPin._state
                .get(ho());
        }
    },
    remState: function (o) {
        if (FreshPin._state) {
            return FreshPin._state
                .remove(o || ho());
        }
    },
    blockUI: function () {
        if (!FreshPin._mask) {
            $(document.body).css('display', 'block');
            var msk = FreshPin._mask = $('<div class="jqmOverlayfull"  />');
            msk.css({
                "-moz-opacity": "0.3",
                "opacity": "0.3",
                "filter": "alpha(opacity=30)",
                "width": "100%",
                "height": "100%",
                "z-index": "9",
                "top": "0",
                "left": "0",
                "position": "fixed"
            });
            var ld = $(String.format('<img src="{0}img/dots64.gif"  />', FreshPin.constants
                .cdn));
            ld.css({
                "z-index": "10",
                "position": "fixed",
                "top": "48%",
                "left": "48%",
                "background-repeat": "no-repeat"
            });
            msk.append(ld)
                .appendTo(document.body);
        }
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
        if (FreshPin._mask) {
            FreshPin._mask
                .remove();
            FreshPin._mask = null;
        }
    },
    appendScriptElement: function (url) {
        document.write(String.format('<script src="{0}" type="text/javascript"></script>', url));
    },

    loadCss: function (url) {
        if (document.createStyleSheet) {
            document.createStyleSheet(FreshPin.constants
                .cdn + url);
        } else {
            $("head").append($("<link rel='stylesheet' href='" + FreshPin.constants
                .cdn + url + "' type='text/css' media='screen' />"));
        }
    },
    tobool: function (s) {
        return "true" == s;
    },
    imgMap: function (s, u, attr) {
        $(s).attr(attr || 'src', FreshPin.constants
            .cdn + u);
    },
    authenticated: function () {
        return document.cookie
            .indexOf(FreshPin.constants
            .authcookie) != -1;
    },
    visited: function () {
        var o = FreshPin.getCookie();
        return o && o != undefined && o.vuID != undefined && !isNaN(o.vuID);
    },
    getCVal: function (p, c) {
        var val = deparam(Cookies.get(c || FreshPin.constants
            .infocookie));
        return val[p];
    },
    getCookie: function () {
        var val = Cookies.get(FreshPin.constants
            .infocookie);
        return val && val != undefined ? deparam(val, true) : null;
    },
    getUN: function () {
        var o = FreshPin.getCookie();
        return o && o != undefined && o.name ? o
            .name : 'User Name';
    },
    getEmail: function () {
        var o = FreshPin.getCookie();
        return o && o != undefined && o.email ? o
            .email : null;
    },
    getAV: function (params) {
        var o = FreshPin.getCookie();
        //if (!params['404']) params['404'] = FreshPin.constants.defaultThumbnail;
        return   String.format('{0}?{1}',o && o != undefined && o.avatar? o.avatar:FreshPin
            .constants
            .cdn + FreshPin
            .constants
            .userBlankImg, $.param(params)) ;
    },
    _funcs: [],
    _emitted: [],
    attach: function (fn, args, scope, group) {
        FreshPin._funcs
            .push({
                fn: fn,
                args: args,
                scope: scope,
                group: group
            });
        var emits = $.grep(FreshPin._emitted, function (item) {
            return item.group == group;
        });
        $.each(emits, function (ind, o) {
            try {
                fn.apply(o.scope || scope || FreshPin, o
                    .args || args || []);
            } catch (e) {
                console.error(e);
            }
        });
    },
    emit: function (group, scope, args) {
        FreshPin._emitted
            .push({
                group: group,
                scope: scope,
                args: args
            });
        var filtered = $.grep(FreshPin._funcs, function (it, ind, arr) {
            return it.group == group;
        });
        for (var i = 0, l = filtered.length; i < l; i++) {
            var o = filtered[i];
            try {
                o.fn
                    .apply(scope || o.scope || FreshPin, args || o
                    .args || []);
            } catch (e) {
                console.error(e);
            }
        }
    },
    //    _returnurl: {},
    //    setru: function (group, url, force) {
    //        if (this._returnurl[group] == undefined || force != undefined)
    //            this._returnurl[group] = url;
    //    },
    //    getru: function (group) {
    //        var tmp = this._returnurl[group];
    //        this._returnurl[group] = undefined;
    //        return tmp;
    //    },
    ourHover: function (menu, submenu, opts) {
        var submenuhovered;
        $(menu).hover(function (e) {
            var hovered = true;
            setTimeout(function () {
                if (hovered) {
                    $(submenu).show();
                }
            }, FreshPin.constants
                .menuShowInterval);
        }, function (e) {
            var hovered = false;
            setTimeout(function () {
                if (!hovered) {
                    if (!submenuhovered) {
                        $(submenu).hide();
                    }
                }
            }, FreshPin.constants
                .menuShowInterval);
        });
        $(submenu).hover(function (e) {
            submenuhovered = true;
        }, function (e) {
            submenuhovered = false;
            $(this).hide();
        });
    }
}
$(function () {
    $.ajaxSetup({
        beforeSend: function () {
            FreshPin.blockUI();
        },
        complete: function () {
            FreshPin.unblockUI();
        }
    });
    //$('#sautocomplete').css({ left: $('#query').offset().left });
    $('input:text[placeholder], textarea[placeholder]').placeholder();
    var contentpagesflg = $.grep(['about.aspx', 'faq.html'], function (i) {
        return $(location).attr('href')
            .toLowerCase()
            .indexOf(i.toLowerCase()) != -1;
    })
        .length > 0;
    $('#query_button').off();
  

    function search() {
        if (contentpagesflg) FreshPin.constants
            .noblock = true;
        var q1 = $('#query').val();
        if ((q1 == strings.Search_Slogan) || (!q1)) {
            alert(strings.Search_Alert_1);
        } else if (q1 != "") {
            var sch = $('input[name="st"]:checked').val();
            if (!sch) {
                alert(strings.Search_Alert_2);
            } else {
                params = [String.format('q={0}', q1), String
                    .format('qt={0}', sch)];
                $(location).attr('href', '.#' + params.join('&'));
            }
        }
    }
    $('#query_button').click(this, search);
    $('#query').bind('keyup', 'return', search);
    FreshPin.ourHover('#query', '#sautocomplete', {
        rel: true
    });
});

function LimitText(text) {
    var len = text.length;
    if (len > 12) {
        text = text.substring(0, 12) + "...";
    }
    return text;
}
