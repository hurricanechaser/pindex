var emailregex = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$/, returnurl;
function getUD(o) {
    return FreshPin.constants.domain + 'users/' + o.Name;
}
function getUplUImg(o) {
    return o.Avatar ? FreshPin.constants.upl + o.Avatar : FreshPin.constants.cdn + FreshPin.constants.userBlankImg + '?width=36';
}
$(function () {
    $.get('GET', { t: 'getcategoriesandboards' }, function (dt, status, res) {        
        FreshPin.constants.categories = dt.categories;
        FreshPin.constants.boards = dt.boards;        
        FreshPin.emit('set', null, [dt.userObj, dt.boardsCnt, dt.pinsCnt, dt.likesCnt]);
        FreshPin.emit('onboardsadded');
        FreshPin.emit('oncatsadded');
    }, 'json');
});
FreshPin.attach(function () {
    $("#boardsCont").empty();
    $("#boardsCont").hide();
    $("#content").empty();
    $("#content").hide();        
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

    function getboards(sc) {
        set();
        cbboards(FreshPin.constants.data);
    }

    $s.getboards = getboards;
})(window);
$(function () {
    var src, contributors = [], boardContributorFlag = false;
    templates.boards = $("#boards").template();
    templates.ebboardCollaborators = $("#ebboardCollaborators").template();
    function loadCategories() {
        var cattpl = $('#cattpl');
        if (cattpl.length > 0) {
            $('#boardcategory').css('visibility', 'visible');
            var cbcats = cattpl.tmpl(FreshPin.constants.categories);
            cbcats.appendTo("#cbcat");
            FreshPin.ourHover(cbcats, '#cbsubcat', {
                menuMouseIn: function () {
                    $('#cbsubcat').empty();
                    var index = parseInt($(this).attr('index'));
                    var sc = FreshPin.constants.categories[index].SubCategories;
                    if (sc) {
                        var subcats = $('#subcattpl').tmpl(sc).appendTo("#cbsubcat");
                        subcats.find('li').click(function () {
                            var jme = $(this);
                            FreshPin.constants.selectedCategoryName = jme.text();
                            FreshPin.constants.selectedCategory = parseInt(jme.attr('catid'));
                            $("#cbselcat").html(jme.find('a').html());
                        });
                    }
                }
            });
            cbcats = cattpl.tmpl(FreshPin.constants.categories);
            cbcats.appendTo("#ecbcat");
            FreshPin.ourHover(cbcats, '#ecbsubcat', {
                menuMouseIn: function () {
                    $('#ecbsubcat').empty();
                    var index = parseInt($(this).attr('index'));
                    var sc = FreshPin.constants.categories[index].SubCategories;
                    if (sc) {
                        var subcats = $('#subcattpl').tmpl(sc).appendTo("#ecbsubcat");
                        subcats.find('li').click(function () {
                            var jme = $(this);
                            FreshPin.constants.selectedCategoryName = jme.text();
                            FreshPin.constants.selectedCategory = parseInt(jme.attr('catid'));
                            $("#ecbselcat").html(jme.find('a').html());
                        });
                    }
                }
            });
        }
    }

    function loadBoards() {
        var dt = FreshPin.constants.boards;
        if (dt.length > 0) {
            $("#dropDownBoxAP ul").empty();
            $("#dropDownBoxUP ul").empty();
            $("#dropDownBoxRP ul").empty();
            $("#titleAP").html(dt[0].Name);
            $("#titleUP").html(dt[0].Name);
            $("#titleRP").html(dt[0].Name);
            FreshPin.constants.selectedBoard = dt[0].ID;
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
            FreshPin.trackGACEvents('boards', 'Load', 'Load Settings');
            $.getJSON('GET?t=getboards', function (dt) {
                $.each(dt, function (ind, item) {
                    var l = item.images.length;
                    while (l++ < 5) item.images.push({ url: FreshPin.constants.cdn + 'img/paper.jpg', ID: null, height: 258, width: 258 });
                });
                FreshPin.constants.data = dt;
                set();
                getboards(false);
            });
            return;
        }

        if (h('settings')) {
            FreshPin.trackGACEvents('settings', 'Load', 'Load Settings');
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.get('settings', function (dt) {
                set();
                $("#userProfile").hide();
                $("#content").show();
                $('#content').html(dt);
                FreshPin.emit('loadsettings');
            }, 'html');
            return;
        }

        if (h('changepassword')) {
            FreshPin.trackGACEvents('changepassword', 'Load', 'Load ChangePassword');
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.get('changepassword', function (dt) {
                set();
                $("#content").show();
                $('#content').html(dt);
                FreshPin.emit('loadchangepassword');
            }, 'html');
            return;
        }

        if (h('filter') || h('board')) {
            FreshPin.trackGACEvents('PindexProd', 'Load', String.format('Query-{0},Cat-{1},Board-{2},Filter-{3},Page-{4}', h('q'), h('cat'), h('board'), h('filter'), FreshPin.constants.p));
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.getJSON('GET?t=getimages', {
                cat: h('cat'),
                q: h('q'),
                p: FreshPin.constants.p,
                filter: h('filter'),
                board: h('board')
            }, function (dt, status, res) {
                FreshPin.constants.data = dt;
                get(true);
            });
            return;
        }
    }

    function reload() {

        if (h('boards')) {
            FreshPin.trackGACEvents('boards', 'Load', 'Load Boards');
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.getJSON('GET?t=getboards', function (dt) {
                $.each(dt, function (ind, item) {
                    var l = item.images.length;
                    while (l++ < 5) item.images.push({ ID: null, url: FreshPin.constants.cdn + 'img/paper.jpg' });
                });
                FreshPin.constants.data = dt;
                getboards(true);
            });
            return;
        }

        if (h('settings')) {
            FreshPin.trackGACEvents('settings', 'Load', 'Load Settings');
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.get('settings', function (dt) {
                set();
                $("#userProfile").hide();
                $("#content").show();
                $('#content').html(dt);
                FreshPin.emit('loadsettings');
            }, 'html');
            return;
        }

        if (h('changepassword')) {
            FreshPin.trackGACEvents('changepassword', 'Load', 'Load ChangePassword');
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.get('changepassword', function (dt) {
                set();
                $("#content").show();
                $('#content').html(dt);
                FreshPin.emit('loadchangepassword');
            }, 'html');
            return;
        }

        if (h('filter') || h('board')) {
            FreshPin.trackGACEvents('PindexProd', 'Load', String.format('Query-{0},Cat-{1},Board-{2},Filter-{3},Page-{4}', h('q'), h('cat'), h('board'), h('filter'), FreshPin.constants.p));
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.getJSON('GET?t=getimages', {
                cat: h('cat'),
                q: h('q'),
                p: FreshPin.constants.p,
                filter: h('filter'),
                board: h('board')
            }, function (dt, status, res) {
                FreshPin.constants.data = dt;
                get(true);
            });
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
    function bindebCollaboratorTmpl(o) {
        $('#boardCollaborators').empty().append($.tmpl(templates.ebboardCollaborators, o.boardCollaborators).
                on('click', 'a[name="rem"]', function () {
                    var un = $(this).attr('un');
                    var me = this;
                    var o = FreshPin.constants.selectedRec.boardCollaborators;
                    var sr = FreshPin.constants.selectedRec;
                    if (!sr.removeBoardCollaborators) sr.removeBoardCollaborators = [];
                    sr.removeBoardCollaborators.push(o.splice($.inArray($.grep(o, function (i) { return i.Name == un; })[0], o), 1)[0]);
                    $(me).parent('div[name = "cont"]').remove();
                }));
    }
    function bindboardstmpl() {
        this.tmpl.on('click', 'div[name="edit"]', function () {
            var id = parseInt($(this).attr('boardid'));
            var o = FreshPin.constants.selectedRec = $.grep(FreshPin.constants.data, function (i) {
                return i.id == id;
            })[0];
            $('#id_name').val(o.name);
            $('#ecbselcat').html(o.catname);
            bindebCollaboratorTmpl(o);
            $('#editboard').jqmShow();
        });
    }

    $('#delEB').click(function () {
        if (confirm('Do you really want to delete the board.This will remove all the images within the board')) {
            var rec = FreshPin.constants.selectedRec;
            $.post('POST?t=delboard', { boardid: rec.id }, function () {
                var o = FreshPin.constants.data;
                o.splice($.inArray($.grep(o, function (i) { return i.id == rec.id; })[0], o), 1);
                $('#editboard').jqmHide();
                getboards();
            });
        }
    });
    $('#saveEB').click(function () {
        var o = FreshPin.constants.selectedRec;
        var name = $('#id_name').val();
        if (name == '') {
            alert('Board name cannot be blank!');
            return;
        }
        o.name = name;
        $.post('POST?t=saveeditboard', { name: name, catid: FreshPin.constants.selectedCategory, boardid: o.id, removebc: JSON.stringify(o.removeBoardCollaborators || []), bc: JSON.stringify(o.boardCollaborators || []) }, function () {
            if (FreshPin.constants.selectedCategory)
                o.catid = FreshPin.constants.selectedCategory;
            if (FreshPin.constants.selectedCategoryName)
                o.catname = FreshPin.constants.selectedCategoryName;
            $('#editboard').jqmHide();
        });
    });
    function attach() {
        FreshPin.attach(loadBoards, null, null, 'onboardsadded');
        FreshPin.attach(loadCategories, null, null, 'oncatsadded');
        FreshPin.attach(load, null, null, 'load');
        FreshPin.attach(reload, null, null, 'reload');
        FreshPin.attach(scroll, null, null, 'scroll');
        FreshPin.attach(bindboardstmpl, null, null, 'bindboardstmpl');
    }

    attach();
    $('#uplPCharCount').html(FreshPin.constants.charCount);
    //////////////////////Modals/////////////////////////////////////////////////////////////////
    $('#add').css({ left: '50%', margin: '0 0 0 -' + ($('#add').width() / 2) + 'px' })
    .jqm({
        overlay: 75,
        //modal: true,
        trigger: '#addtrigger',
        onShow: function (hash) {
            if (FreshPin.constants.boards.length == 0) {
                $('#uploadpint').css('color', '#CCCCCC');
                $('#addpint').css('color', '#CCCCCC');
            } else {
                $('#uploadpint').css('color', '#211922');
                $('#addpint').css('color', '#211922');
            }
            hash.w.show();
            hash.w.animate({ top: '50%', left: '50%', margin: '-' + (hash.w.height() / 2) + 'px 0 0 -' + (hash.w.width() / 2) + 'px' }, 'fast', 'swing');
        },
        onHide: function (hash) {
            hash.w.animate({
                top: '100%'
            }, 'fast', 'swing', function () {
                hash.w.hide();
                hash.o.hide();
            });
        }
    });
    $('#addclose').click(function () { $('#add').jqmHide(); });
    $('#add').draggable({ handle: ".title" });

    $('#uploadPin').css({ left: '50%', margin: '0 0 0 -' + ($('#uploadPin').width() / 2) + 'px' })
    .jqm({
        overlay: 75,
        onShow: function (hash) {
            FreshPin.constants.selectedBoard = FreshPin.constants.boards[0].ID;
            $('#add').jqmHide();
            hash.w.show();
            hash.w.animate({ top: '50%', left: '50%', margin: '-' + (hash.w.height() / 2) + 'px 0 0 -' + (hash.w.width() / 2) + 'px' }, 'fast', 'swing');
        },
        onHide: function (hash) {
            FreshPin.constants.selectedBoard = null;
            hash.w.animate({
                top: '100%'
            }, 'fast', 'swing', function () {
                hash.w.hide();
                hash.o.hide();
            });
        }
    });
    $('#uploadpint').click(function () {
        if (FreshPin.constants.boards.length != 0)
            $('.uploadPin').jqmShow();
    });
    $('#uploadPin').draggable({ handle: ".title" });
    $('#uploadPinclose').click(function () { $('#uploadPin').jqmHide(); });

    $('#addPin').css({ left: '50%', margin: '0 0 0 -' + ($('#addPin').width() / 2) + 'px' })
    .jqm({
        overlay: 75,
        onShow: function (hash) {
            FreshPin.constants.selectedBoard = FreshPin.constants.boards[0].ID;
            $('#add').jqmHide();
            hash.w.show();
            hash.w.animate({ top: '50%', left: '50%', margin: '-' + (hash.w.height() / 2) + 'px 0 0 -' + (hash.w.width() / 2) + 'px' }, 'fast', 'swing');
        },
        onHide: function (hash) {
            FreshPin.constants.selectedBoard = null;
            hash.w.animate({
                top: '100%'
            }, 'fast', 'swing', function () {
                hash.w.hide();
                hash.o.hide();
            });

        }
    });
    $('#addpint').click(function () {
        if (FreshPin.constants.boards.length != 0)
            $('#addPin').jqmShow();
    });
    $('#addPinclose').click(function () { $('#addPin').jqmHide(); });
    $('#addPin').draggable({ handle: ".title" });

    $('#Repin').css({ left: '50%', margin: '0 0 0 -' + ($('#Repin').width() / 2) + 'px' })
    .jqm({
        overlay: 75,
        onShow: function (hash) {
            FreshPin.constants.selectedBoard = FreshPin.constants.boards[0].ID;
            hash.w.show();
            hash.w.animate({ top: '50%', left: '50%', margin: '-' + (hash.w.height() / 2) + 'px 0 0 -' + (hash.w.width() / 2) + 'px' }, 'fast', 'swing');
        },
        onHide: function (hash) {
            FreshPin.constants.selectedBoard = null;
            hash.w.animate({
                top: '100%'
            }, 'fast', 'swing', function () {
                hash.w.hide();
                hash.o.hide();
            });
        }
    });
    $('#Repinclose').click(function () { $('#Repin').jqmHide(); });
    $('#Repin').draggable({ handle: ".title" });

    $('#CreateBoard').css({ left: '50%', margin: '0 0 0 -' + ($('#CreateBoard').width() / 2) + 'px' })
    .jqm({
        overlay: 75,
        trigger: '#createboard',
        onShow: function (hash) {
            $('#add').jqmHide();
            hash.w.show();
            hash.w.animate({ top: '50%', left: '50%', margin: '-' + (hash.w.height() / 2) + 'px 0 0 -' + (hash.w.width() / 2) + 'px' }, 'fast', 'swing');
        },
        onHide: function (hash) {
            hash.w.animate({
                top: '100%'
            }, 'fast', 'swing', function () {
                hash.w.hide();
                hash.o.hide();
            });
        }
    });
    $('#CreateBoardclose').click(function () { $('#CreateBoard').jqmHide(); });
    $('#CreateBoard').draggable({ handle: ".title" });

    $('#editpin').css({ left: '50%', margin: '0 0 0 -' + ($('#editpin').width() / 2) + 'px' })
    .jqm({
        overlay: 75,
        onShow: function (hash) {
            hash.w.show();
            hash.w.animate({ top: '50%', left: '50%', margin: '-' + (hash.w.height() / 2) + 'px 0 0 -' + (hash.w.width() / 2) + 'px' }, 'fast', 'swing');
        },
        onHide: function (hash) {
            hash.w.animate({
                top: '100%'
            }, 'fast', 'swing', function () {
                hash.w.hide();
                hash.o.hide();
            });
        }
    });
    $('#closeedit').click(function () { $('#editpin').jqmHide(); });
    $('#editpin').draggable({ handle: ".title" });

    $('#comment').css({ left: '50%', margin: '0 0 0 -' + ($('#comment').width() / 2) + 'px' })
    .jqm({
        overlay: 75,
        onShow: function (hash) {
            hash.w.show();
            hash.w.animate({ top: '50%', left: '50%', margin: '-' + (hash.w.height() / 2) + 'px 0 0 -' + (hash.w.width() / 2) + 'px' }, 'fast', 'swing');
        },
        onHide: function (hash) {
            hash.w.animate({
                top: '100%'
            }, 'fast', 'swing', function () {
                hash.w.hide();
                hash.o.hide();
            });
        }
    });
    $('#commentclose').click(function () { $('#comment').jqmHide(); });
    $('#comment').draggable({ handle: ".title" });

    $('#editboard').css({ left: '50%', margin: '0 0 0 -' + ($('#editboard').width() / 2) + 'px' })
    .jqm({
        overlay: 75,
        onShow: function (hash) {
            hash.w.show();
            hash.w.animate({ top: '50%', left: '50%', margin: '-' + (hash.w.height() / 2) + 'px 0 0 -' + (hash.w.width() / 2) + 'px' }, 'fast', 'swing');
        },
        onHide: function (hash) {
            hash.w.animate({
                top: '100%'
            }, 'fast', 'swing', function () {
                hash.w.hide();
                hash.o.hide();
                FreshPin.resetValues();
            });
        }
    });
    $('#editboardclose').click(function () {
        $('#ebcollaboratorImg').attr('src', FreshPin.getAV() + '?width=36');
        $('#editboard').jqmHide();
    });
    $('#editboard').draggable({ handle: ".title" });
    $('#ebcontribute').click(function () {
        var contributor = $('#ebcontributor').val();
        if (!contributor) alert('Enter a contributor\'s Email address or Username and then click "Add"');
        else if (contributor == FreshPin.getEmail()) alert('You cannot be the contributor to the board you have created');
        else if (contributor != '')
            $.post('POST?t=validatecontributor', { contributor: contributor }, function (data, res) {
                var sr = FreshPin.constants.selectedRec;
                sr.boardCollaborators.push(data);
                bindebCollaboratorTmpl(sr);
            }, 'json').error(function (res) {
                alert(res.responseText);
            });
    });
    ///////////////////////////////////////////////End///////////////////////////////////////////

    /////////////////////////Repin, Like, Comment//////////////////////////////////////////////////
    FreshPin.attach(function () {
        if (FreshPin.authenticated()) {
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
                else alert('You have to create a board first');
            });
            this.tmpl.on('click', '.buttons a[name="comment"]', function () {
                $(this).parent().parent().find('.addComment').toggle();
                $(this).toggleClass('buttonCommentDis');
                $(".gallery").masonry('reload');
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
            //        this.tmpl.on('blur', 'textarea[name="addc"]', function () {
            //            var val = $(this).val();
            //            if (val != '')
            //                $(this).val('Add a comment...');
            //        });
            //        this.tmpl.on('focus', 'textarea[name="addc"]', function () {
            //            var val = $(this).val();
            //            if (val != 'Add a comment...')
            //                $(this).val('');
            //        });
        }
        if (FreshPin.visited()) {
            this.tmpl.find('a[name="like"]').attr('href', 'signup');
            this.tmpl.find('a[name="pin"]').attr('href', 'signup');
            this.tmpl.find('a[name="comment"]').attr('href', 'signup');
        }
    }, null, null, 'bindimagetmpl');

    if (FreshPin.authenticated()) {
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
                if (o.liked)
                    $(me).find('div').css('color', '#CCCCCC');
                else
                    $(me).find('div').css('color', '#777176');
                $(String.format('.gallery a[name="like"][bimid={0}]', id)).toggleClass('buttonLikeDis').attr('liked', o.liked);
            }, 'json');
        });
        $("#repint").click(function () {
            var o = FreshPin.constants.pin;
            $('#imgRP').attr('src', o.url);
            if (FreshPin.constants.boards.length != 0) $('#Repin').jqmShow();
        });
        $("#commentt").click(function () {
            $("#comment").jqmShow();
        });
    } else {
        $('#likepint').attr('href', 'signup');
        $('#editpint').attr('href', 'signup');
        $('#repint').attr('href', 'signup');
        $('#commentt').attr('href', 'signup');
    }
    $('#saveRP').click(function () {
        var desc = $('#RPDesc').val();
        var o = FreshPin.constants.pin;
        var board = FreshPin.constants.selectedBoard;
        $.post('POST?t=saverepin', { id: o.ID, board: board, desc: desc, source: o.imgsource }, function (data, res) { $('#Repin').jqmHide(); }, 'json');
    });
    $('#saveComment').click(function () {
        $.post('POST?t=addcomment', { comments: $('#commentDesc').val(), id: FreshPin.constants.pin.BIMID }, function (data, res) {
            $("#comment").jqmHide();
        }, 'json');
    });

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
    $('#fImages').click(function () {
        var url = $('#url').val();
        if (url && url != '') {
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
    $('#delete').click(function () {
        if (confirm('Are you sure you want to permanently delete this pin?')) {
            $.post('POST?t=deletepin', { BIMID: FreshPin.constants.selectedRec.BIMID }, function (data, res) {
                $('#editpin').jqmHide();
                reload();
            });
        }
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
            if (/(.*?)\.(jpg|jpeg|png|gif)$/.test(data.files[0].name)) $('#lmuplimg').show();
            else {
                e.preventDefault();
                alert('Select only image files to upload');
                $('#lmuplimg').hide();
                throw 'invalid file type';
            }
            file = data.result.file;
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

    function trimTA(e) {
        var cc = FreshPin.constants.charCount;
        if (e.keyCode > 47 && cc <= $(this).val().length) {
            $(this).val($(this).val().substring(0, cc));
        }
    }

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
    // $("textarea[elastic]").elastic();
    $('#saveUploadPin').click(function () {
        var desc = $('#uplPDesc').val();
        var img = $('#uploadedImage').attr('src');
        if (!img) alert('Please select an Image by clicking the Choose File button above');
        else {
            var board = FreshPin.constants.selectedBoard;
            $.post('POST?t=saveuploadedpin', {
                board: board,
                img: img,
                desc: desc
            }, function (data, res) {
                $('.uploadPin').jqmHide();
                $(location).attr('hash', 'pin=' + data.PinID);
            }, 'json');
        }
    });
    $('#saveAddPin').click(function () {
        var desc = $('#addPDesc').val();
        var img = $('#urlimages').attr('src');
        var board = FreshPin.constants.selectedBoard;
        $.post('POST?t=saveaddedpin', { board: board, img: img, desc: desc, src: src }, function (data, res) {
            $('.addPin').jqmHide();
            $(location).attr('hash', 'pin=' + data.PinID);
        }, 'json');
    });
    $('#saveeditpin').click(function () {
        var o = FreshPin.constants.selectedRec;
        var desc = $('#description_pin_edit').val();
        var source = $('#id_link').val();
        var board = parseInt($(String.format('#id_board option[id="i{0}"]', $('#id_board').prop('selectedIndex'))).attr('boardid'));
        $.post('POST?t=saveeditpin', { board: board, source: source, desc: desc, bimid: o.BIMID }, function (data, res) {
            $('#editpin').jqmHide();
            reload();
        }, 'json');
    });
    $('#contribute').click(function () {
        var contributor = $('#contributor').val();
        if (!contributor) alert('Enter a contributor\'s Email address and then click "Add"');
        else if (contributor == FreshPin.getEmail()) alert('You cannot be the contributor to the board you have created');
        else if (contributor != '')
            $.post('POST?t=validatecontributor', { contributor: contributor }, function (data, res) {
                contributors.push(data.Email);
                $('#contributorslist').append(String.format('<span style="padding-left: 25px;">{0}</span>', data));
            }, 'json').error(function (res) {
                alert(res.responseText);
            });
    });
    $('#saveCreateBoard').click(function () {
        var name = $('#cbname').val();
        var cat = FreshPin.constants.selectedCategory;
        if (!name) alert('Enter a Board Name');
        else if (!cat) alert('Select Category');
        else if (boardContributorFlag && !contributors[0]) alert('Enter a contributor\'s Email address and then click "Add" ');
        else {
            $.post('POST?t=savecreatedboard', { cat: cat, name: name, contributors: contributors }, function (data, res) {
                if (!isNaN(data)) {
                    FreshPin.constants.boards.push({ Name: name, ID: parseInt(data) });
                    FreshPin.emit('onboardsadded');
                    $('.CreateBoard').jqmHide();
                    FreshPin.constants.selectedCategory = null;
                    alert("Board was created successfully");
                } else alert(data);
            }, 'text');
        }
    });
    $('input[name="change_BoardCollaborators"]').change(function () {
        $('#emailBox').css('display', $(this).attr('validation'));
        if ($(this).attr('validation') == "none") {
            boardContributorFlag = false;
            $('#contributorslist').empty();
            contributors = [];
        } else boardContributorFlag = true;
    });

    $('#p_board_cnt').click(function () {
        $(this).attr('class', 'selected');
        $("#p_pins_cnt").attr('class', '');
        $("#p_likes_cnt").attr('class', '');
    });

    $('#p_pins_cnt').click(function () {
        $(this).attr('class', 'selected');
        $("#p_board_cnt").attr('class', '');
        $("#p_likes_cnt").attr('class', '');
    });

    $('#p_likes_cnt').click(function () {
        $(this).attr('class', 'selected');
        $("#p_board_cnt").attr('class', '');
        $("#p_pins_cnt").attr('class', '');
    });
    /////////////////////////////////////////////End//////////////////////////////////////////////////////////////
});
FreshPin.attach(function (userObj, boardCnt, pinCnt, likesCnt) {
    $("#p_name").append(userObj.Name);
    $("#p_about").append(userObj.About);
    $("#p_board_cnt").append("<strong>" + boardCnt + "</strong> Boards");
    $("#p_pins_cnt").append("<strong>" + pinCnt + "</strong> Pins");
    $("#p_likes_cnt").append("<strong>" + likesCnt + "</strong> Likes");
    $("p_image_href").attr("href", FreshPin.getAV());
}, null, null, 'set');

FreshPin.attach(function () {
    var fn, name = FreshPin.getUN(), avatar = FreshPin.getAV();
    FreshPin.setru('settings', _his[_his.length - 2] || '');
    $('#ftsettings').fileupload({
        url: 'POST?t=up',
        singleFileUploads: true,
        dataType: 'json',
        acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i
    }).bind('fileuploadadd', function (e, data) {
        if (/(.*?)\.(jpg|jpeg|png|gif)$/.test(data.files[0].name)) $('#lmuplimg').show();
        else {
            e.preventDefault();
            alert('Select only image files to upload');
            $('#lmuplimg').hide();
            throw 'invalid file type';
        }
    }).bind('fileuploaddone', function (e, data) {
        fn = data.result.file;
        $('#uploadedUserImage').attr('src', fn + '?width=170');
        $('#lmuplimg').hide();
    }).bind('fileuploadfail', function (e, data) {

    });
    $('#username1').change(function () {
        var val = $(this).val();
        if (/^[a-zA-Z0-9_]*$/.test(val))
            $.post('POST?t=usernameavail', { un: $(this).val() }, function (data) {
                $('#usernameview').html(data);
            }, 'text');
        else
            $('#usernameview').html('No spaces or special characters');
    });
    $('#sp').click(function () {
        var email = $('#email').val();
        if (email == "" || !emailregex.test(email)) {
            alert('Please enter a valid Email Address');
            return;
        }
        var first_name = $('#first_name').val();
        var name = $('#username1').val();
        var about = $('#aboutu').val();
        var loc = $('#location').val();
        var website = $('#website').val();
        var imgurlarr = $('#uploadedUserImage').attr('src').split('?');
        var imgurl = (imgurlarr.length > 0) ? imgurlarr[0] : '';
        if (name == '') alert('Please enter username');
        else
            $.post('POST?t=saveprofile', { email: email, name: name, fn: fn, first_name: first_name, about: about, location: loc, website: website }, function (dt, res, opts) {
                $('#userProfilePic').attr('src', FreshPin.getAV() + '?width=20');
                $('#username').html(FreshPin.getUN());
                $(location).attr('hash', FreshPin.getru('settings'));
            }, 'text').error(function (res) {
                alert(res.responseText);
            });
    })
}, null, null, 'loadsettings');

FreshPin.attach(function () {
    FreshPin.setru('cp', _his[_his.length - 2]);
    $('#sp').click(function () {
        var pass2 = $('#pass2').val();
        var pass3 = $('#pass3').val();
        if (pass2 == "" || pass3 == "") alert('Please enter the new password in the last two textboxes');
        else if (pass2 == pass3)
            $.post('POST?t=changepassword', { pass: pass2 }, function (data) {
                alert(data);

                $(location).attr('hash', FreshPin.getru('cp') || '');
            });
        else alert('Password Mismatch');
    });
}, null, null, 'loadchangepassword');