///<reference path="~/freshpin.js" />
///<reference path="~/freshpinhome.js" />
var emailregex = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$/, returnurl;
function getUD(o) {
    return FreshPin.constants.domain + 'users/' + o.Name;
}


Array.prototype.pluck = function (property) {
    var i, rv = [];

    for (i = 0; i < this.length; ++i) {
        rv[i] = this[i][property];
    }
    return rv;
};
function toggleprofile(show) {
    if (show) {
        $('#profileCont,#ContextBar').show();
        $('#gallery,#boardsCont,#users').css({
            top: '325px'
        });
    } else {
        $('#profileCont,#ContextBar').hide();
        $('#gallery,#boardsCont,#users').css({
            top: '0px'
        });
    }
}
var cookieRegistry = [];
function listenCookieChange(cookieName, callback) {
    setInterval(function () {
        var cookieval = Cookies.get(cookieName);
        var oldcookieval = cookieRegistry[cookieName];
        if (cookieval != oldcookieval) {
            if (cookieval != cookieRegistry[cookieName]) {
                cookieRegistry[cookieName] = cookieval;
                callback();
            }
        } else {
            cookieRegistry[cookieName] = cookieval;
        }
    }, 3000);
}

function FollowUnFollowUser(f_ID, f_img, f_txt) {
    if (FreshPin.authenticated()) {
        var FButtonText = $("#" + f_txt);
        var FImageSrc = $("#" + f_img);
        if (FButtonText.text().toLowerCase().indexOf("unfollow") >= 0) {
            $.post('POST?t=unfollowuser', { F_ID: f_ID }, function () {
                FImageSrc.attr('src', GetFImage("follow"));
                if (f_ID == 0) FButtonText.text(strings.Follow_Me);
                else FButtonText.text(strings.Follow);
            }, 'text').error(function (res) { alert(res.responseText); });
        } else if (FButtonText.text().toLowerCase().indexOf("follow") >= 0) {
            $.post('POST?t=followuser', { F_ID: f_ID }, function () {
                FImageSrc.attr('src', GetFImage("unfollow"));
                if (f_ID == 0) FButtonText.text(strings.UnFollow_Me);
                else FButtonText.text(strings.UnFollow);
            }, 'text').error(function (res) { alert(res.responseText); });
        }
    } else $('#signUpWraper').jqmShow();
}


FreshPin.attach(function () {
    $("#boardsCont").empty().hide();
}, null, null, 'set');
(function ($s) {
    var masonryOptsBoards = {
        itemSelector: '.pinBoard',
        columnWidth: 220,
        gutterWidth: 15,
        isAnimated: false,
        isFitWidth: true
    }, rl1;
    function cbboards(dt) {
        $("#boardsCont").show();
        var tmpl = $.tmpl(templates.boards, dt);
        $("#boardsCont").html(tmpl);
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
    function cbfollows(dt) {
        $("#users").html($.tmpl(templates.users, dt)).show();
    }
    function getboards(sc) {
        set();
        cbboards(FreshPin.constants.data);
    }
    function getfollows(sc) {
        set();
        cbfollows(FreshPin.constants.data);
    }
    function load() {
        if (h('cat') || !h()) {
            toggleprofile(false);
        }
        if (h() == 'boards') {
            FreshPin.trackGACEvents('boards', 'Load', 'Load Settings');
            toggleprofile(true);
            FreshPin.appendScriptElement('GET?t=getboards&pre=FreshPin.constants.data');
            return;
        }
        if (h('filter') || h('board')) {
            FreshPin.trackGACEvents('nails', 'Load', String.format('Query-{0},Cat-{1},Board-{2},Filter-{3},Page-{4}', h('q'), h('cat'), h('board'), h('filter'), FreshPin.constants.p));
            FreshPin.appendScriptElement(String.format('GET?t=getimages&pre=FreshPin.constants.data&{0}', FreshPin.serialize({
                cat: h('cat'),
                q: h('q'),
                p: FreshPin.constants.p,
                filter: h('filter'),
                board: h('board')
            })));
            return;
        }
        if (h('followings')) {
            FreshPin.trackGACEvents('followings', 'Load', 'Load Following');
            toggleprofile(true);
            FreshPin.appendScriptElement('GET?t=getfollowing&pre=FreshPin.constants.data');
            return;
        }

        if (h('followers')) {
            FreshPin.trackGACEvents('followers', 'Load', 'Load Follower');
            toggleprofile(true);
            FreshPin.appendScriptElement('GET?t=getfollower&pre=FreshPin.constants.data');
            return;
        }
    }
    $s.getboards = getboards;
    $s.getfollows = getfollows;
    FreshPin.attach(load, null, null, 'load');
})(window);
$(function () {
    var src, boardContributorFlag = false;

    templates.ebboardCollaborators = $("#ebboardCollaborators").template();
    $("#selectBox").click(function () {
        $("#catdropdown").toggle();
    });
    $('button[name="p_btn"]').bind('changecolor', function () {
        $('button[name="p_btn"]').removeClass('p_btn_bgc');
        $(this).addClass('p_btn_bgc');
    });
    $("a[name='sellist']").click(function () {
        $('#catseltext').html($(this).html()); $("#catdropdown").toggle();
        FreshPin.constants.selectedCategory = parseInt($(this).attr('catid'));
    });
    function loadBoards() {
        var dt = FreshPin.constants.boards;
        if (dt.length > 0) {
            $("#dropDownBoxAP ul").empty();
            $("#dropDownBoxUP ul").empty();
            $("#dropDownBoxRP ul").empty();
            var tmplAP = $("#boardstpl").tmpl(dt);
            tmplAP.appendTo("#dropDownBoxAP ul");
            var tmplUP = $("#boardstpl").tmpl(dt);
            tmplUP.appendTo("#dropDownBoxUP ul");
            var tmplRP = $("#boardstpl").tmpl(dt);
            tmplRP.appendTo("#dropDownBoxRP ul");
            var opts = [];
            $.each(dt, function (ind, item) { opts.push(String.format('<option boardid="{1}" id="i{0}">{2}</option>', ind, item.ID, item.Name)); });
            $('#id_board').empty();
            $('#id_board').append(opts.join(' '));
        }
    }
    function load() {
        if (h() == 'boards') {
            $.each(FreshPin.constants.data, function (ind, item) {
                var l = item.images.length;
                while (l++ < 5) item.images.push({ url: FreshPin.constants.cdn + 'img/nails/paper.jpg', ID: null, height: 232, width: 232 });
            });
            set();
            FreshPin.pushState(FreshPin.constants.data);
            $('#p_boards').trigger('changecolor');
            toggleprofile(true);
            getboards(false);
        }
        if (h('settings')) {
            FreshPin.trackGACEvents('settings', 'Load', 'Load Settings');
            toggleprofile(false);
            $.get('settings', function (dt) {
                set();
                $("#content").show().css({ top: '0px' }).html(dt);
                FreshPin.emit('loadsettings');
            }, 'html');
            return;
        }
        if (h('changepassword')) {
            FreshPin.trackGACEvents('changepassword', 'Load', 'Load ChangePassword');
            toggleprofile(true);
            $("#content").show();
            FreshPin.emit('loadchangepassword');
            return;
        }
        if (h('filter') || h('board')) {
            FreshPin.pushState(FreshPin.constants.data);
            toggleprofile(true);
            if (h('board')) $('#p_boards').trigger('changecolor');
            var filter = h('filter');
            if (filter == 'pins') $('#p_pins').trigger('changecolor');
            if (filter == 'likes') $('#p_likes').trigger('changecolor');
            get(true);
        }
        if (h('followings') || h('followers')) {
            FreshPin.pushState(FreshPin.constants.data);
            toggleprofile(true);
            if (h('followings')) $('#p_followings').trigger('changecolor');
            if (h('followers')) $('#p_followers').trigger('changecolor');
            getfollows(true);
        }
    }
    load();
    function reload() {
        if (h('cat') || h('articles') ||  h('stores') || !h()) {
            FreshPin.closeall();
            toggleprofile(false);
        }
        if (h('boards')) {
            FreshPin.closeall();
            FreshPin.trackGACEvents('boards', 'Load', 'Load Boards');
            $('#p_boards').trigger('changecolor');
            toggleprofile(true);
            FreshPin.constants.data = FreshPin.getState();
            if (FreshPin.constants.data && FreshPin.constants.preventReload) {
                getboards(true);
            } else {
                $.getJSON('GET?t=getboards', function (dt) {
                    $.each(dt, function (ind, item) {
                        var l = item.images.length;
                        while (l++ < 5) item.images.push({ ID: null, url: FreshPin.constants.cdn + 'img/nails/paper.jpg', height: 232, width: 232 });
                    });
                    FreshPin.pushState(dt);
                    FreshPin.constants.data = dt;
                    FreshPin.constants.dataType = 'userboards';
                    getboards(true);
                });
            }
            FreshPin.constants.preventReload = true;
            return;
        }
        if (h('settings')) {
            FreshPin.closeall();
            FreshPin.trackGACEvents('settings', 'Load', 'Load Settings');
            toggleprofile(false);
            $.get('settings', function (dt) {
                set();
                $("#content").show().css({ top: '0px' }).html(dt)
                    ;
                FreshPin.emit('loadsettings');
            }, 'html');
            return;
        }
        if ((h('q') && h('qt')) || !h() || h('size') || h('style') || h('cat') || h('q') || h() == '') {
            FreshPin.closeall();
            toggleprofile(false);
            return;
        }
        if (h('filter') || h('board')) {
            FreshPin.closeall();
            FreshPin.trackGACEvents('nails', 'Load', String.format('Query-{0},Cat-{1},Board-{2},Filter-{3},Page-{4}', h('q'), h('cat'), h('board'), h('filter'), FreshPin.constants.p));
            if (h('filter')) {
                var filter = h('filter');
                if (filter == 'pins') $('#p_pins').trigger('changecolor');
                if (filter == 'likes') $('#p_likes').trigger('changecolor');
                FreshPin.constants.dataType = String.format('filter{0}', filter);
            }
            if (h('board')) {
                $('#p_boards').trigger('changecolor');
                FreshPin.constants.dataType = 'boardpins';
            }
            toggleprofile(true);
            FreshPin.constants.data = FreshPin.getState();
            if (FreshPin.constants.data && FreshPin.constants.preventReload) {
                get(true);
            } else {
                $.getJSON('GET?t=getimages', {
                    cat: h('cat'),
                    q: h('q'),
                    p: FreshPin.constants.p,
                    filter: h('filter'),
                    board: h('board')
                }, function (dt) {
                    FreshPin.pushState(dt);
                    FreshPin.constants.data = dt;
                    get(true);
                });
            }
            FreshPin.constants.preventReload = true;
            return;
        }
        if (h('followings')) {
            FreshPin.closeall();
            FreshPin.trackGACEvents('followings', 'Load', 'Load Following');
            set();
            $('#p_followings').trigger('changecolor');
            toggleprofile(true);
            FreshPin.constants.data = FreshPin.getState();
            if (FreshPin.constants.data && FreshPin.constants.preventReload) {
                getfollows(true);
            } else {
                $.getJSON('GET?t=getfollowing', function (dt) {
                    FreshPin.pushState(dt);
                    FreshPin.constants.data = dt;
                    getfollows(true);
                }, 'json').error(function (res) { alert(res.responseText); });
                FreshPin.emit('followings');
            }
            FreshPin.constants.preventReload = true;
            return;
        }
        if (h('followers')) {
            FreshPin.closeall();
            FreshPin.trackGACEvents('followers', 'Load', 'Load Follower');
            set();
            $('#p_followers').trigger('changecolor');
            toggleprofile(true);
            FreshPin.constants.data = FreshPin.getState();
            if (FreshPin.constants.data && FreshPin.constants.preventReload) {
                getfollows(true);
            } else {
                $.getJSON('GET?t=getfollower', function (dt) {
                    FreshPin.pushState(dt);
                    FreshPin.constants.data = dt;
                    getfollows(true);
                }, 'json').error(function (res) { alert(res.responseText); });
                FreshPin.emit('followers');
            }
            FreshPin.constants.preventReload = true;
            return;
        }
    }

    function scroll(sp, ch, ph) {
        if ((ch - ph - sp) < 500) {
            if (FreshPin.constants.data) {
                if (h('board') || h('filter')) {
                    get(false);
                    return;
                }
            }
        }
    }
    ///////////////////attach//////////////////////////////
    FreshPin.attach(loadBoards, null, null, 'onboardsadded');
    FreshPin.attach(reload, null, null, 'reload');
    FreshPin.attach(scroll, null, null, 'scroll');
    ///////////////////attach//////////////////////////////
    if (FreshPin.visited()) {
        var av = FreshPin.getCVal('vuavatar', 'info');
        av = av ? av : FreshPin.constants.cdn + FreshPin.constants.userBlankImg;
        $('#p_points').text(FreshPin.getCVal('vupoints', 'info'));
        $('#p_image_src').attr('src', String.format('{0}?width=116', av));
        $("#p_board_cnt").text(FreshPin.getCVal('vuboards', 'info'));
        $("#p_pins_cnt").text(FreshPin.getCVal('vupins', 'info'));
        $("#p_likes_cnt").text(FreshPin.getCVal('vulikes', 'info'));
        $("#p_followings_cnt").text(FreshPin.getCVal('vufollowing', 'info'));
        $("#p_followers_cnt").text(FreshPin.getCVal('vufollower', 'info'));
        if (FreshPin.server.followingstatus) {
            $('#followallbtn').show();
            $('#followallimg').attr('src', GetFImage('unFollow'));
            $('#followallspan').text(strings.UnFollow_Me);
        }
        else {
            $('#followallbtn').show();
            $('#followallimg').attr('src', GetFImage('Follow'));
            $('#followallspan').text(strings.Follow_Me);
        }
        $("#p_edit_btn").hide();
        $('#p_about').text(FreshPin.server.abouttext);
    }
    ////////////////////////////Authenticated and not visited///////////////////////////////
    if (FreshPin.authenticated() && !FreshPin.visited()) {
        var tact = false;
        $('#jipeditable').on('keyup blur', function (e) {
            var maxlength = $(this).attr('maxlength');
            var val = $(this).val();
            trimTA.apply(this, [e]);
            var length = parseInt(maxlength) - $(this).val().length;
            $('#EPCharCount').html(length);
            if (val.length > maxlength) {
                $(this).val(val.slice(0, maxlength));
            }
        });
        $('#aboutcont').click(function () {
            if (!tact) {
                var tmp = $('#jipeditable');
                var ml = tmp.css('display', '').text($('#p_about').css('display', 'none').text()).focus().attr('maxlength');
                $('#pediticon').css('display', 'none');
                $('#EPCharCount').css('display', '').text(ml - tmp.val().length);
                tact = true;
            }
        });
        $('#jipeditable').blur(function () {
            var jipeditable = $('#jipeditable');
            $.post('POST?t=updateabout', { about: jipeditable.val() }, function () {
                $('#p_about').css('display', '').text(jipeditable.css('display', 'none').val());
                $('#pediticon').css('display', '');
                $('#EPCharCount').css('display', 'none');
                tact = false;
            });
        });
    }
    ////////////////////////////Authenticated///////////////////////////////
    if (FreshPin.authenticated()) {
        function trimTA(e) {
            var cc = FreshPin.constants.charCount;
            if (e.keyCode > 47 && cc <= $(this).val().length) {
                $(this).val($(this).val().substring(0, cc));
            }
        }
        function loaduserdetails() {
            if (!FreshPin.visited()) {
                var un = FreshPin.getUN();
                $('#username').text(un);
                $('#p_name').text(un);
                $('#p_points').text(FreshPin.getCVal('points', 'info'));
                $('#p_image_src').attr('src', FreshPin.getAV({ width: 116 }));
                $('#userProfilePic').attr('src', FreshPin.getAV({ width: 20 }));
                $("#p_board_cnt").text(FreshPin.getCVal('boards', 'info'));
                $("#p_pins_cnt").text(FreshPin.getCVal('pins', 'info'));
                $("#p_likes_cnt").text(FreshPin.getCVal('likes', 'info'));
                $("#p_followings_cnt").text(FreshPin.getCVal('flcnt', 'info'));
                $("#p_followers_cnt").text(FreshPin.getCVal('frcnt', 'info'));
            }
        }
        loaduserdetails();
        listenCookieChange('info', loaduserdetails);

        $.get('GET', { t: 'getloggedinloaddata' }, function (dt) {
            FreshPin.constants.boards = dt.boards;
            $('#p_about').text(dt.about);
            FreshPin.emit('onboardsadded');
        }, 'json').error(function (res) { alert(res.responseText); });

        function bindebCollaboratorTmpl(o, remfn) {
            $('#boardCollaborators').empty().append($.tmpl(templates.ebboardCollaborators, o).
                on('click', 'a[name="rem"]', remfn));
        }
        function ebremoveCollaborators() {
            var un = $(this).attr('un');
            var o = FreshPin.constants.selectedRec.boardCollaborators;
            var sr = FreshPin.constants.selectedRec;
            if (!sr.removeBoardCollaborators) sr.removeBoardCollaborators = [];
            sr.removeBoardCollaborators.push(o.splice($.inArray($.grep(o, function (i) { return i.Name == un; })[0], o), 1)[0]);
            $(this).parent('div[name = "cont"]').remove();
        }
        function removeContributors() {
            var un = $(this).attr('un');
            var o = FreshPin.constants.contributors;
            FreshPin.constants.contributors = $.grep(o, function (i) { return i.Name != un; });
            $(this).parent('div[name = "cont"]').remove();
        }
        function bindboardstmpl() {
            this.tmpl.on('click', 'div[name="edit"]', function () {
                var id = parseInt($(this).attr('boardid'));
                var o = FreshPin.constants.selectedRec = $.grep(FreshPin.constants.data, function (i) { return i.id == id; })[0];
                FreshPin.constants.selectedCategory = o.catid;
                $('#board_name').val(o.name);
                $('#catseltext').text($(String.format('a[name = "sellist"][catid="{0}"]', o.catid)).text());
                bindebCollaboratorTmpl(o.boardCollaborators, ebremoveCollaborators);
                $('#board_label').text(strings.Edit_Board);
                $('#board_lbl_upd').text(strings.Update_Board);
                $('#delEB').show();
                $('#editboard').jqmShow();
            });
        }

        $('#delEB').click(function () {
            if (confirm(strings.Del_Board_Confirm)) {
                var rec = FreshPin.constants.selectedRec;
                $.post('POST?t=delboard', { boardid: rec.id }, function (dt) {
                    var data = FreshPin.constants.data;
                    var boards = FreshPin.constants.boards;
                    data.splice($.inArray($.grep(data, function (i) { return i.id == rec.id; })[0], data), 1);
                    boards.splice($.inArray($.grep(data, function (i) { return i.ID == rec.id; })[0], data), 1);
                    $('#editboard').jqmHide();
                    getboards(true);
                    FreshPin.remState(ho('filter=pins'));
                    FreshPin.remState(ho(String.format('board={0}', rec.name)));
                    FreshPin.emit('onboardsadded');
                    FreshPin.resetValues();
                });
            }
        });
        $('#saveEB').click(function () {
            var name = $('#board_name').val();
            if (name == '') {
                alert(strings.Board_Blank);
                return;
            }
            var o = FreshPin.constants.selectedRec;
            if (o) {
                $.post('POST?t=saveeditboard', { name: name, catid: FreshPin.constants.selectedCategory, boardid: o.id, removebc: JSON.stringify(o.removeBoardCollaborators || []), bc: JSON.stringify(o.boardCollaborators || []) }, function (data) {
                    alert(strings.Board_Update_Msg);
                    $('#editboard').jqmHide();
                    FreshPin.constants.preventReload = false;
                    if (h() == 'boards')
                        $(window).trigger('hashchange');
                    else
                        location.hash = '#boards';
                }, 'text').error(function (res) { alert(res.responseText); });
            } else {
                if (FreshPin.constants.selectedCategory == null) {
                    alert(strings.Cat_DD);
                    return;
                }
                $.post('POST?t=savecreatedboard', { catid: FreshPin.constants.selectedCategory, name: name, contributors: FreshPin.constants.contributors.pluck('Email') }, function (dt) {
                    alert(strings.Board_Success);
                    $('#editboard').jqmHide();
                    FreshPin.constants.boards = dt.boards;
                    FreshPin.emit('onboardsadded');
                    FreshPin.constants.preventReload = false;
                    if (h() == 'boards')
                        $(window).trigger('hashchange');
                    else
                        location.hash = '#boards';
                }, 'json').error(function (res) {
                    alert(res.responseText);
                });
            }
            FreshPin.resetValues();
        });
        ///////////////////attach//////////////////////////////
        FreshPin.attach(bindboardstmpl, null, null, 'bindboardstmpl');
        ///////////////////attach//////////////////////////////
        $('#uplPCharCount').html(FreshPin.constants.charCount);
        //////////////////////Modals/////////////////////////////////////////////////////////////////
        $('#add').css(cssCenterY('#add'))
        .jqm({
            overlay: 100,
            modal: true,
            trigger: '#addtrigger',
            onShow: function (hash) {
                if (FreshPin.constants.boards.length == 0) {
                    $('#addpinicon').removeClass('pin');
                    $('#uploadpinicon').removeClass('arrow');
                    $('#addpint,#uploadpint').attr('title', strings.Pin_Alert_Title);
                    $('#uploadpint').css('color', '#CCCCCC');
                    $('#addpint').css('color', '#CCCCCC');
                } else {
                    $('#addpinicon').addClass('pin');
                    $('#uploadpinicon').addClass('arrow');
                    $('#addpint,#uploadpint').attr('title', '');
                    $('#uploadpint').css('color', '#211922');
                    $('#addpint').css('color', '#211922');
                }
                FreshPin.closeall(hash);
                hash.w.show();
                hash.w.animate(cssCenterX('#add'), 'fast', 'swing');
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
        $('#addclose').click(function () { $('#add').jqmHide(); });
        $('#add').draggable({ handle: ".title" });
//        $('#rwd').css(cssCenterY('#rwd')).jqm({
//            overlay: 50,
//            overlayClass: 'jqmOverlayfull',
//            modal: true,
//            trigger: '#rwdt',
//            onShow: function (hash) {
//                FreshPin.closeall(hash);
//                hash.w.show();
//                hash.w.animate(cssCenterX('#rwd'), 'fast', 'swing');
//            },
//            onHide: function (hash) {
//                hash.w.animate({
//                    top: '100%'
//                }, 'fast', 'swing', function () {
//                    hash.w.hide();
//                    hash.o.hide().remove();
//                });
//            }
//        });
//        var spinning = false;
//        $('#rwclose').click(function () { if (!spinning) $('#rwd').jqmHide(); });
//        $('#rwt').click(function () {
//            if (!spinning) {
//                var rnd = Math.random();
//                var spins = (rnd * 100 * 360);
//                $.post('POST?t=updateprize', null, function (data) {
//                    spinning = true;
//                    $("#rw").rotate({
//                        angle: 0,
//                        duration: rnd * 60000,
//                        animateTo: data.angle + (spins - (spins % 360)),
//                        callback: function () {
//                            spinning = false;
//                            alert(data.alert);
//                            FreshPin.writeCookieValue('points', data.points, 'info');
//                        }
//                    });

//                }, 'json').error(function (res) { alert(res.responseText); });
//            }
//        });
        $('#uploadPin').css(cssCenterY('#uploadPin'))
        .jqm({
            overlay: 100,
            onShow: function (hash) {
                var dt = FreshPin.constants.boards;
                $("#titleUP").html(dt[0].Name);
                FreshPin.constants.selectedBoard = dt[0].ID;
                FreshPin.closeall(hash);
                hash.w.show();
                hash.w.animate(cssCenterX('#uploadPin'), 'fast', 'swing');
            },
            onHide: function (hash) {
                FreshPin.constants.selectedBoard = null;
                hash.w.animate({
                    top: '100%'
                }, 'fast', 'swing', function () {
                    hash.w.hide();
                    hash.o.hide().remove();
                    $('#uplPDesc').val('');
                    $('#uplPCharCount').html(FreshPin.constants.charCount);
                    $('#uploadedImage').attr('src', FreshPin.constants.cdn + 'img/nails/default_thumbnail.jpg');
                });
            }
        });
        $('#uploadpint').click(function () { if (FreshPin.constants.boards.length != 0) $('.uploadPin').jqmShow(); });
        $('#uploadPin').draggable({ handle: ".title" });
        $('#uploadPinclose').click(function () { $('#uploadPin').jqmHide(); });
        $('#addPin').css(cssCenterY('#addPin')).jqm({
            overlay: 100,
            onShow: function (hash) {
                var dt = FreshPin.constants.boards;
                $("#titleAP").html(dt[0].Name);
                FreshPin.constants.selectedBoard = dt[0].ID;
                FreshPin.closeall(hash);
                hash.w.show();
                hash.w.animate(cssCenterX('#addPin'), 'fast', 'swing');
            },
            onHide: function (hash) {
                FreshPin.constants.selectedBoard = null;
                hash.w.animate({
                    top: '100%'
                }, 'fast', 'swing', function () {
                    hash.w.hide();
                    hash.o.hide().remove();
                    $('#url,#addPDesc,#url_upload').val('');
                    $('#addPCharCount').html(FreshPin.constants.charCount);
                    $('#urlimages').attr('src', FreshPin.constants.cdn + 'img/nails/default_thumbnail.jpg');
                });

            }
        });
        $('#addpint').click(function () { if (FreshPin.constants.boards.length != 0) $('#addPin').jqmShow(); });
        $('#addPinclose').click(function () { $('#addPin').jqmHide(); });
        $('#addPin').draggable({ handle: ".title" });
        $('#Repin').css(cssCenterY('#Repin'))
        .jqm({
            overlay: 100,
            onShow: function (hash) {
                var dt = FreshPin.constants.boards;
                $("#titleRP").html(dt[0].Name);
                FreshPin.constants.selectedBoard = dt[0].ID;
                FreshPin.closeall(hash);
                hash.w.show();
                hash.w.animate(cssCenterX('#Repin'), 'fast', 'swing');
            },
            onHide: function (hash) {
                FreshPin.constants.selectedBoard = null;
                hash.w.animate({
                    top: '100%'
                }, 'fast', 'swing', function () {
                    hash.w.hide();
                    hash.o.hide().remove();
                    $('#RPDesc').val('');
                    $('#RPCharCount').html(FreshPin.constants.charCount);
                });
            }
        });
        $('#Repinclose').click(function () { $('#Repin').jqmHide(); });
        $('#Repin').draggable({ handle: ".title" });
        $('#CreateBoard').css(cssCenterY('#CreateBoard'))
        .jqm({
            overlay: 100,
            trigger: '#createboardt',
            onShow: function (hash) {
                FreshPin.closeall(hash);

                hash.w.show();
                hash.w.animate(cssCenterX('#CreateBoard'), 'fast', 'swing');
            },
            onHide: function (hash) {
                hash.w.animate({
                    top: '100%'
                }, 'fast', 'swing', function () {
                    hash.w.hide();
                    hash.o.hide().remove();
                    $('#cbname').val('');
                    FreshPin.constants.selectedCategory = null;
                    $('#catseltext').html(strings.Cat_Choose);
                    $('#emailBox').empty();
                });
            }
        });
        $('#CreateBoardclose').click(function () { $('#CreateBoard').jqmHide(); });
        $('#CreateBoard').draggable({ handle: ".title" });
        $('#editpin').css({ left: '50%', margin: '0 0 0 -' + ($('#editpin').width() / 2) + 'px' })
        .jqm({
            overlay: 100,
            onShow: function (hash) {
                FreshPin.closeall(hash);
                hash.w.show();
                hash.w.animate({ top: '50%', left: '50%', margin: '-' + (hash.w.height() / 2) + 'px 0 0 -' + (hash.w.width() / 2) + 'px' }, 'fast', 'swing');
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
        $('#closeedit').click(function () { $('#editpin').jqmHide(); });
        $('#editpin').draggable({ handle: ".title" });
        $('#comment').css({ left: '50%', margin: '0 0 0 -' + ($('#comment').width() / 2) + 'px' })
        .jqm({
            overlay: 100,
            onShow: function (hash) {
                FreshPin.closeall(hash);
                hash.w.show();
                hash.w.animate({ top: '50%', left: '50%', margin: '-' + (hash.w.height() / 2) + 'px 0 0 -' + (hash.w.width() / 2) + 'px' }, 'fast', 'swing');
            },
            onHide: function (hash) {
                hash.w.animate({
                    top: '100%'
                }, 'fast', 'swing', function () {
                    hash.w.hide();
                    hash.o.hide().remove();
                    $('#commentDesc').val('');
                    $('#commentCharCount').html(FreshPin.constants.charCount);
                });
            }
        });
        $('#commentclose').click(function () { $('#comment').jqmHide(); });
        $('#comment').draggable({ handle: ".title" });
        $('#commentDesc').keyup(function (e) {
            trimTA.apply(this, [e]);
            var length = FreshPin.constants.charCount - $(this).val().length;
            $('#commentCharCount').html(length);
        });
        $('#editboard').css(cssCenterY('#editboard'))
        .jqm({
            overlay: 100,
            trigger: '#createboardt',
            onShow: function (hash) {
                FreshPin.closeall(hash);
                hash.w.show();
                hash.w.animate(cssCenterX('#editboard'), 'fast', 'swing');
            },
            onHide: function (hash) {
                hash.w.animate({
                    top: '100%'
                }, 'fast', 'swing', function () {
                    hash.w.hide();
                    hash.o.hide().remove();
                    FreshPin.resetValues();
                    $('#board_name,#ebcontributor').val('');
                    $('#catseltext').html(strings.Select_Category);
                    $('#catdropdown').hide();
                    $('#boardCollaborators').empty();
                    $('#delEB').hide();
                    $('#board_label').html(strings.Create_Board);
                    $('#board_lbl_upd').html(strings.Save_Board);
                });
            }
        });
        $('#editboardclose').click(function () {
            $('#ebcollaboratorImg').attr('src', FreshPin.getAV({ width: 36 }));
            $('#editboard').jqmHide();
        });
        $('#editboard').draggable({ handle: ".title" });
        $('#ebcontribute').click(function () {
            var contributor = $('#ebcontributor').val();
            if (!contributor) alert(strings.Contributor_Add);
            else if (contributor != '') {
                $.post('POST?t=validatecontributor', { contributor: contributor }, function (data, res) {
                    if (FreshPin.constants.selectedRec) {
                        var sr = FreshPin.constants.selectedRec;
                        if ($.grep(sr.boardCollaborators, function (it) { return it.Email == data.Email; }).length == 0) {
                            sr.boardCollaborators.push(data);
                            bindebCollaboratorTmpl(sr.boardCollaborators, ebremoveCollaborators);
                        } else {
                            alert(strings.Contributor_Usr_Warn);
                        }
                    } else {
                        var contributors = FreshPin.constants.contributors;
                        if ($.grep(contributors, function (it) {
                            return it.Email == data.Email;
                        }).length == 0) {
                            contributors.push(data);
                            bindebCollaboratorTmpl(contributors, removeContributors);
                        }
                        else {
                            alert(strings.Contributor_Usr_Warn);
                        }
                    }
                }, 'json').error(function (res) { alert(res.responseText); });
                $('#ebcontributor').val('');
            }
        });
        ///////////////////////////////////////////////End///////////////////////////////////////////

        /////////////////////////Repin, Like, Comment//////////////////////////////////////////////////
        FreshPin.attach(function () {
            var edits = this.tmpl.find('a[name="edit"]');
            edits.click(function () {
                var BIMID = parseInt($(this).attr('bimid'));
                var o = $.grep(FreshPin.constants.data, function (item) { return item.BIMID == BIMID; })[0];
                FreshPin.constants.selectedRec = o;
                $('#editpin').jqmShow();
                $('#description_pin_edit').val(o.title);
                $('#id_link').val(o.imgsource);
                var tmp = $('#id_board option');
                tmp.removeAttr('selected');
                $(String.format('#id_board option[boardid="{0}"]', o.BoardID)).attr('selected', 'selected');
                $('#pinimg').attr('src', String.format('{0}?width=190', o.url));
                $('#pinlink').attr('href', String.format('Pin?pin=${PinID}', o.PinID));
                $('#editPCharCount').html(FreshPin.constants.charCount - o.title.length);
            });
            this.tmpl.on('click', '.buttons a[name="like"]', function () {
                var id = parseInt($(this).attr('bimid'));
                var pino = $.grep(FreshPin.constants.data, function (item) { return item.BIMID == id; })[0];
                var liked = pino.liked;
                var me = this;
                $.post('POST?t=savelike', { id: id, liked: !liked }, function (data, res) {
                    $(me).attr('liked', !liked);
                    $(me).toggleClass('buttonLikeDis');
                    pino.liked = !liked;
                }, 'json');
            });
            this.tmpl.on('click', '.buttons a[name="pin"]', function () {
                var BIMID = parseInt($(this).attr('bimid'));
                var o = $.grep(FreshPin.constants.data, function (item) { return item.BIMID == BIMID; })[0];
                FreshPin.constants.pin = o;
                $('#imgRP').attr('src', o.url);
                if (FreshPin.constants.boards.length != 0) $('#Repin').jqmShow();
                else alert(strings.Board_First);
            });
            this.tmpl.on('click', '.buttons a[name="comment"]', function () {
                var id = parseInt($(this).attr('bimid'));
                var o = $.grep(FreshPin.constants.data, function (item) { return item.BIMID == id; })[0];
                FreshPin.constants.pin = o;
                $("#comment").jqmShow();
            });
            this.tmpl.on('click', 'a[sc]', function () {
                var comments = $(this).prev('textarea').val();
                var id = parseInt($(this).attr('bimid'));

                var me = this;
                $.post('POST?t=addcomment', { comments: comments, id: id }, function (data, res) {
                    var parent = $(me).parent();
                    var cont = parent.prev('.comments');
                    var size = cont.size();
                    var tmpl = $("#comments").tmpl({ Name: FreshPin.getUN(), Avatar: FreshPin.getAV(), Comments: comments });
                    if (size > 0) {
                        tmpl.appendTo(cont);
                    } else {
                        cont = $('<li class="comments"></li>');
                        tmpl.appendTo(cont);
                        parent.before(cont);
                    }
                    $(".gallery").masonry('reload');
                }, 'json');
            });
        }, null, null, 'bindimagetmpl');
        $("#editpint").click(function () {
            var o = FreshPin.constants.pin;
            $('#editpin').jqmShow();
            $('#description_pin_edit').val(o.title);
            $('#id_link').val(o.imgsource);
            var tmp = $('#id_board option');
            tmp.removeAttr('selected');
            $(String.format('#id_board option[boardid="{0}"]', o.BoardID)).attr('selected', 'selected');
            $('#pinimg').attr('src', String.format('{0}?width=190', o.url));
            $('#pinlink').attr('href', String.format('Pin?pin=${PinID}', o.PinID));
            $('#editPCharCount').html(FreshPin.constants.charCount - o.title.length);
        });
        $("#likepint").click(function () {
            var o = FreshPin.constants.pin;
            var liked = o.liked;
            var id = o.BIMID;
            var me = this;
            $.post('POST?t=savelike', { id: id, liked: !liked }, function (data, res) {
                o.liked = !liked;
                $(me).find('div').toggleClass('pinDisLike');
                $(String.format('#gallery a[name="like"][bimid={0}]', id)).toggleClass('buttonLikeDis').attr('liked', o.liked);
            }, 'json');
        });
        $("#repint").click(function () {
            var o = FreshPin.constants.pin;
            $('#imgRP').attr('src', o.url);
            if (FreshPin.constants.boards.length != 0) $('#Repin').jqmShow();
        });
        $("#commentt").click(function () { $("#comment").jqmShow(); });

        $('#saveRP').click(function () {
            var desc = $('#RPDesc').val();
            var o = FreshPin.constants.pin;
            var board = FreshPin.constants.selectedBoard;
            $.post('POST?t=saverepin', { id: o.ID, board: board, desc: desc, source: o.imgsource }, function () { $('#Repin').jqmHide(); }, 'json');
        });
        $('#saveComment').click(function () {
            var commentDesc = $('#commentDesc').val();
            if (!commentDesc) { alert(strings.Comment_Enter); }
            else {
                $.post('POST?t=addcomment', { comments: commentDesc, id: FreshPin.constants.pin.BIMID }, function (data) {
                    $("#comment").jqmHide();
                    if (!FreshPin.constants.pin.comments) FreshPin.constants.pin.comments = [];
                    FreshPin.constants.pin.comments.push(data);
                    $(window).trigger('hashchange');
                }, 'json');
            }
        });
        /////////////////////////////End///////////////////////////////////////////////////////////////


        /////////////////////////////End/////////////////////////////////////////////////////////////// 


        ///////////////////////////////////////////////////Modal Func/////////////////////////////////////////

        function setUpBoardsDD(sel, dd, title) {
            $(sel).click(function () { $(dd).show(); });
            $(dd + ' ul li').live('click', function () {
                $(title).html($(this).html());
                FreshPin.constants.selectedBoard = parseInt($(this).attr('boardid'));
                $(dd).hide();
            });
        }
        setUpBoardsDD('#selectAP', '#dropDownBoxAP', '#titleAP');
        setUpBoardsDD('#selectUP', '#dropDownBoxUP', '#titleUP');
        setUpBoardsDD('#selectRP', '#dropDownBoxRP', '#titleRP');
        var imgActive, urls;
        $('#FindImages').click(function () {
            var url = $('#url_upload').val();
            if (imageValidate(url)) {
                $('#lmfindimages').show();
                $("#urlimages").attr('src', FreshPin.constants.cdn + 'img/spiral.gif');
                $.get('GET', { t: 'getimagesinurl', url: url }, function (data, res) {
                    $('#lmfindimages').hide();
                    var l = data.length, i = 0;
                    urls = data;
                    imgActive = 0;
                    $("#urlimages").attr('src', urls[imgActive]);
                    $('#lmpreloadimages').show();
                    src = $('url').val();
                    $.imgpreload(data, {
                        each: function () { $('#lmpreloadimages span').html('Loading Images ' + (++i) + ' Of ' + l); },
                        all: function () { $('#lmpreloadimages').hide(); }
                    });
                }, 'json');
            }
        });
        $('#prev').click(function () { if (urls) if (imgActive != 0) $("#urlimages").attr('src', urls[--imgActive]); });
        $('#next').click(function () { if (urls) if (imgActive != (urls.length - 1)) $("#urlimages").attr('src', urls[++imgActive]); });
        function imageValidate(img, opt) {
            opt = opt || 0;
            var default_loc = FreshPin.constants.cdn + 'img/nails/default_thumbnail.jpg';
            if ((img == default_loc) || (img == "")) {
                if (opt == 1)
                    alert(strings.Pin_Upload_Msg);
                else
                    alert(strings.Pin_Add_Msg);
                return false;
            }
            else
                return true;
        }
        $('#deletepin').click(function () {
            if (confirm(strings.Pin_Del_Msg)) {
                $.post('POST?t=deletepin', { BIMID: FreshPin.constants.selectedRec.BIMID }, function (data, res) {
                    $('#editpin').jqmHide();
                    FreshPin.constants.preventReload = false;
                    if (h('filter') == 'pins')
                        $(window).trigger('hashchange');
                    else
                        location.hash = '#filter=pins';
                });
            }
            FreshPin.resetValues();
        });
        $('#fu').fileupload({
            url: 'POST?t=up',
            singleFileUploads: true,
            dataType: 'json',
            acceptFileTypes: /(\.|\/)(gif|GIF|jpe?g|JPE?G|png|PNG)$/i
        }).bind('fileuploadadd', function (e, data) { $('#lmuplimg').show(); })
            .bind('fileuploadsubmit', function (e, data) {
            })
            .bind('fileuploadsend', function (e, data) {
            })
            .bind('fileuploaddone', function (e, data) {
                if (/(.*?)\.(jpg|jpeg|png|gif|tiff|tif|bmp)$/.test((data.files[0].name).toLowerCase())) $('#lmuplimg').show();
                else {
                    e.preventDefault();
                    alert(strings.Image_Invalid);
                    $('#lmuplimg').hide();
                    throw 'invalid file type';
                }
                file = (data.result.file).toLowerCase();
                $('#uploadedImage').attr('src', file + '?width=170');
                $('#lmuplimg').hide(); //busy signal round animation
            });
        //    .bind('fileuploadfail', function (e, data) { })
        //    .bind('fileuploadalways', function (e, data) { })
        //    .bind('fileuploadprogress', function (e, data) { })
        //    .bind('fileuploadprogressall', function (e, data) { })
        //    .bind('fileuploadstart', function (e) { })
        //    .bind('fileuploadstop', function (e) { })
        //    .bind('fileuploadchange', function (e, data) { })
        //    .bind('fileuploadpaste', function (e, data) { })
        //    .bind('fileuploaddrop', function (e, data) { })
        //    .bind('fileuploaddragover', function (e) { });
        $('#uplPDesc').keyup(function (e) {
            trimTA.apply(this, [e]);
            var length = FreshPin.constants.charCount - $(this).val().length;
            $('#uplPCharCount').html(length);
        });
        $('#addPDesc').keyup(function (e) {
            trimTA.apply(this, [e]);
            var length = FreshPin.constants.charCount - $(this).val().length;
            $('#addPCharCount').html(length);
        });
        $('#RPDesc').keyup(function (e) {
            trimTA.apply(this, [e]);
            var length = FreshPin.constants.charCount - $(this).val().length;
            $('#RPCharCount').html(length);
        });
        $('#description_pin_edit').keyup(function (e) {
            trimTA.apply(this, [e]);
            var length = FreshPin.constants.charCount - $(this).val().length;
            $('#editPCharCount').html(length);
        });
        $('#saveUP').click(function () {
            var uplPCat = $('#uplPCat').val();
            var uplPDesc = $('#uplPDesc').val();
            $.post('savepin', { cat: uplPCat, desc: uplPDesc, img: file }, function () {
            });
        });
        $('#saveUploadPin').click(function () {
            var desc = $('#uplPDesc').val();
            var img = $('#uploadedImage').attr('src');
            var board = FreshPin.constants.selectedBoard;
            if (imageValidate(img, 1)) {
                $.post('POST?t=saveuploadedpin', {
                    board: board,
                    img: img,
                    desc: desc
                }, function (data) {
                    alert(strings.Image_Success_Upload);
                    $('#uploadPin').jqmHide();
                    FreshPin.remState({ filter: 'pins' });
                    $(location).attr('hash', 'pin=' + data.PinID);
                }, 'json').error(function (res) { alert(res.responseText); });
            }
        });
        $('#saveAddPin').click(function () {
            var desc = $('#addPDesc').val();
            var img = $('#urlimages').attr('src');
            var board = FreshPin.constants.selectedBoard;
            if (imageValidate(img)) {
                $.post('POST?t=saveaddedpin', {
                    board: board,
                    img: img,
                    desc: desc
                }, function (data) {
                    alert(strings.Image_Success_Added);
                    $('#addPin').jqmHide();
                    FreshPin.remState({ filter: 'pins' });
                    $(location).attr('hash', 'pin=' + data.PinID);
                }, 'json').error(function (res) { alert(res.responseText); });
            }
        });
        $('#saveeditpin').click(function () {
            var o = FreshPin.constants.selectedRec;
            if (!o)
                o = FreshPin.constants.pin;
            var BIMID = o.BIMID;
            var desc = $('#description_pin_edit').val();
            var source = $('#id_link').val();
            var board = parseInt($(String.format('#id_board option[id="i{0}"]', $('#id_board').prop('selectedIndex'))).attr('boardid'));
            $.post('POST?t=saveeditpin', { board: board, source: source, desc: desc, bimid: BIMID }, function (data, res) {
                $('#editpin').jqmHide();
                FreshPin.constants.preventReload = false;
                if (h('filter') == 'pins')
                    $(window).trigger('hashchange');
                else
                    location.hash = '#filter=pins';
            }, 'json').error(function (res) { alert(res.responseText); });
            FreshPin.resetValues();
        });
    }
    ////////////////////////////End Of Authenticated///////////////////////////////

    /////////////////////////////////////////////End//////////////////////////////////////////////////////////////
});
FreshPin.attach(function () {
    var fn, formvalid = true;
    $(document).bind('keydown', 'return', function (evt) { $('#_esc').addClass('dirty'); return false; });
    //FreshPin.setru('settings', _his[_his.length - 2] || '');
    $('#ftsettings').fileupload({
        url: 'POST?t=up',
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
    $('#changepassword').css({ left: '50%', margin: '0 0 0 -' + ($('#changepassword').width() / 2) + 'px' }).jqm({
        overlay: 100,
        trigger: '#changepasswordt',
        onShow: function (hash) {
            hash.w.show();
            hash.w.animate({ top: '50%', left: '50%', margin: '-' + (hash.w.height() / 2) + 'px 0 0 -' + (hash.w.width() / 2) + 'px' }, 'fast', 'swing');
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
    function ChangePassword() {
        var pass2 = $('#pass2').val();
        var pass3 = $('#pass3').val();
        if (pass2 == "" || pass3 == "") alert(strings.Password_New);
        else if (pass2 == pass3)
            $.post('POST?t=changepassword', { pass: pass2 }, function (data) {
                alert(data);
                $('#changepassword').jqmHide();
            });
        else alert(strings.Password_Mismatch);
    }
    $('#savechangepassword').click(function () {
        ChangePassword();
    });
    $('#pass3').keypress(function (e) {
        var k = (e.keyCode ? e.keyCode : e.which);
        if (k == 13) ChangePassword();
    });
    $('#sp').click(function () {
        if (!formvalid) {
            alert(strings.Fields_Red);
            return;
        }
        var first_name = $('#first_name').val();
        var name = $('#username1').val();
        var about = $('#aboutu').val();
        var loc = $('#location').val();
        var website = $('#website').val();
        var imgurlarr = $('#uploadedUserImage').attr('src').split('?');
        var imgurl = (imgurlarr.length > 0) ? imgurlarr[0] : '';
        if (name == '') {
            alert(strings.Enter_User_Name);
            $('#username1').addClass('errinput');
        }
        else if (first_name == '') {
            alert(strings.Enter_Full_Name);
            $('#first_name').addClass('errinput');
        }
        else
            $.post('POST?t=saveprofile', { name: name, fn: fn, first_name: first_name, about: about, location: loc, website: website }, function (dt, res, opts) {
                //$(location).attr('hash', FreshPin.getru('settings'));
                $('#p_about').text(about);
                set();
                location.hash = '#boards';
            }, 'text').error(function (res) {
                alert(res.responseText);
            });
    })
}, null, null, 'loadsettings');

