/// <reference path="scripts/jquery-1.7.1.js" />
///<reference src="scripts/scrolltopcontrol.js" ></script>
///<reference src="scripts/jquery-jquery-tmpl/jquery.tmpl.js" ></script>
///<reference src="scripts/jquery-jquery-tmpl/jquery.tmplPlus.js" ></script>
///<reference src="scripts/desandro-masonry/jquery.masonry.js" ></script>
///<reference src="scripts/jquery.fancybox-1.3.4/fancybox/jquery.fancybox-1.3.4.js"/>
/// <reference path="scripts/cookies.js" />

var masonryOpts = {
    itemSelector: '.pinBoard',
    columnWidth: 240,
    gutterWidth: 15,
    isAnimated: false,
    isFitWidth: true
}, ps = 50, s = 50, p = 0, loading = false;

function getVal(o, p) {
    return p ? o[p] : o;
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
    $(location).attr('href', '?' + params.join('&'));
}

$.getJSON('GET?t=getboards', { cat: q('cat'), q: q('q'), p: p }, function (dt, status, res) {
    $.each(dt,function (i, ind, arr) {
        var l = i.images.length;
        while (l++ < 5)
            i.images.push(FreshPin.constants.cdn + 'img/paper.jpg');
    });
    FreshPin.constants.data = dt;

    FreshPin.emit('loadboards');
});

FreshPin.attach(function () {
    function cb(dt, rl) {
        var tmpl = $("#picTemplate").tmpl(dt);
        tmpl.appendTo(".gallery");
        if (rl) {
            $(".gallery").masonry('reload');
        }
        else
            $(".gallery").masonry(masonryOpts);
        tmpl.css('display', 'inline'); 
    };
    $q = q('q')
    if ($q)
        $('.header .line2').hide();
    function get() {        
        if (p == 0)
            cb(FreshPin.constants.data.slice(0, s), false);
        else {
            s = 0;
            cb(FreshPin.constants.data.slice(0, s += ps), true);
        }
        ++p;
    }
    FreshPin.attach(get, null, this, 'loadboards');   
}, null, this, 'load');

