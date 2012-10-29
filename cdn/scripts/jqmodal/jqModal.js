/*
* jqModal - Minimalist Modaling with jQuery
*   (http://dev.iceburg.net/jquery/jqModal/)
*
* Copyright (c) 2007,2008 Brice Burgess <bhb@iceburg.net>
* Dual licensed under the MIT and GPL licenses:
*   http://www.opensource.org/licenses/mit-license.php
*   http://www.gnu.org/licenses/gpl.html
* 
* $Version: 03/01/2009 +r14
*/
(function ($) {
    $.jqm = {
        hash: []
    };
    $.fn.jqm = function (o) {
        if ($(this).length > 0) {
            $(this).prop('jqm', o);
            $.jqm.hash.push({ w: $(this), opts: o });
        }
    };
    $.fn.jqmShow = function (t) {
        var o = $(this).prop('jqm');
        var z = parseInt($(this).css('z-index'));
        var ovl = ($('#jqmOverlay').length > 0 ? $('#jqmOverlay') : $('<div id="jqmOverlay" class="jqmOverlay"></div>').appendTo(document.body)).css('z-index', z > 0 ? (z - 1) : 0);
        o.onShow({ w: $(this), o: ovl });
    };
    $.fn.jqmfClose = function (t) {
        var o = $(this).prop('jqm');
        var z = parseInt($(this).css('z-index'));
        var ovl = ($('#jqmOverlay').length > 0 ? $('#jqmOverlay') : $('<div id="jqmOverlay" class="jqmOverlay"></div>').appendTo(document.body)).css('z-index', z > 0 ? (z - 1) : 0);
        o.forceclose({ w: $(this), o: ovl });
    };
    $.fn.jqmHide = function (t) {
        var o = $(this).prop('jqm');
        var ovl = ($('#jqmOverlay').length > 0 ? $('#jqmOverlay') : $('<div id="jqmOverlay" class="jqmOverlay"></div>').appendTo(document.body));
        o.onHide({ w: $(this), o: ovl });
    };
})(jQuery);