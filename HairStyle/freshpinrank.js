/// <reference path="scripts/jquery-1.7.1.js" />
///<reference src="scripts/scrolltopcontrol.js" ></script>
///<reference src="scripts/jquery-jquery-tmpl/jquery.tmpl.js" ></script>
///<reference src="scripts/jquery-jquery-tmpl/jquery.tmplPlus.js" ></script>
///<reference src="scripts/desandro-masonry/jquery.masonry.js" ></script>
///<reference src="scripts/jquery.fancybox-1.3.4/fancybox/jquery.fancybox-1.3.4.js"/>
/// <reference path="scripts/cookies.js" />

function getVal(o, p) {
    return p ? o[p] : o;
}
function getUrl(o, t) {
    switch (t) {
        case 'p':
            return 'http://pinterest.com/pin/create/bookmarklet?media=' + $(location).prop('protocol') + '//' + $(location).prop('host') + '/' + escape(o.url) + '&url=Pin.aspx?pin=' + o.PinID + '&alt=' + escape(o.title) + '&title=' + escape(o.title) + '&is_video=false';
            break;
        case 'l':
            return 'http://lockerz.com/grab?media=' + escape(o.url) + '&url=Pin.aspx?pin=' + o.PinID + '&alt=' + escape(o.title);
            break;
        case 'r':
            return 'http://proxy.boxresizer.com/convert?quality=100&resize=160x160&source=' + escape(o);
            break;
    }
}
function q(v) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == v) {
            return unescape(pair[1]);
        }
    }
}
function rcat(v) {
    params = ['cat=' + escape(v)];
    $(location).attr('href', '.?' + params.join('&'));
}
FreshPin.attach(function () {
    $.ajaxSetup({
        timeout: 600000
    });

    $('.searchfield').focus(function () { if ($(this).val() == 'Search over 500,000 Pins') { $(this).val(''); } });
    $('.searchfield').blur(function () { if ($(this).val() == '') { $(this).val('Search over 500,000 Pins'); } });
    //    $('.pinterest').jqm({
    //        overlay: 0,
    //        closeClass: 'close',
    //        onShow: function (hash) {
    //            $('#load').css('display', 'block');
    //            $('.popup').attr('src', $(hash.t).attr('href'));
    //            hash.w.css('display', 'block');
    //            hash.w.animate({
    //                top: '20%'
    //            }, 'fast', 'swing');
    //        },
    //        onHide: function (hash) {
    //            $('.popup').attr('src', 'about:blank');
    //            hash.w.animate({
    //                top: '100%'
    //            }, 'fast', 'swing', function () {
    //                hash.w.css('display', 'block');
    //            });
    //        }
    //    });
    var masonryOpts = {
        itemSelector: '.box',
        columnWidth: 220,
        gutterWidth: 15,
        isAnimated: false,
        isFitWidth: true
    }, ps = 10, loading = false, 
    var $q = q('q');
    if ($q)
        $('.header .line2').css('display', 'none');
    var get = function (o, rl) {
        var qt = o || 'mf';
        $.getJSON('GET?t=getpeople', { ps: ps, qt: qt }, function (dt, status, res) {
            if (dt)
                cb(dt, rl || false);
        });
    };
    get();
    var search = function () {
        var q1 = $('.searchfield').val();
        if (q1 != '') {
            params = ['q=' + q1];
            $(location).attr('href', 'nails.html?' + params.join('&'));
        }
    };
    $('#submenu3 div a').click(function () {
        get($(this).attr('name'), true);
    });
    $('.searchbutton').click(this, search);
    $('.searchfield').bind('keyup', 'return', search);
    //    $("#popup").load(function () {
    //        $('#load').css('display', 'none');
    //    });

    FreshPin.ourHover('#mlevel1', '#submenu1', { rel: true });
    FreshPin.ourHover('#mlevel2', '#submenu2', { rel: true });
    FreshPin.ourHover('#mlevel3', '#submenu3', { rel: true });
}, null, this, 'load');
