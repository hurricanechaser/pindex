///<reference path="~/freshpin.js" />
///<reference path="~/freshpinhome.js" />
var emailregex = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$/, returnurl;
$(function () {
    $.get('GET', { t: 'getcategoriesandboards' }, function (dt, status, res) {
        FreshPin.constants.categories = dt.categories;
        FreshPin.constants.boards = dt.boards;
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
        tmpl.appendTo("#boardsCont");
        if (rl1) {
            $("#boardsCont").masonry('reload');
        } else {
            $("#boardsCont").masonry(masonryOptsBoards);
            rl1 = true;
        }
    }

    function getboards(sc) {
        set();
        cbboards(FreshPin.constants.data);
    }

    $s.getboards = getboards;
})(window);
$(function() {
    var src;
    templates.boards = $("#boards").template();

    function loadCategories() {
        var cattpl = $('#cattpl');
        if (cattpl.length > 0) {
            $('#boardcategory').css('visibility', 'visible');
            var cbcats = cattpl.tmpl(FreshPin.constants.categories);
            cbcats.appendTo("#cbcat");
            FreshPin.ourHover(cbcats, '#cbsubcat', {
                menuMouseIn: function() {
                    $('#cbsubcat').empty();
                    var index = parseInt($(this).attr('index'));
                    var sc = FreshPin.constants.categories[index].SubCategories;
                    if (sc) {
                        var subcats = $('#subcattpl').tmpl(sc).appendTo("#cbsubcat");
                        subcats.find('li').click(function() {
                            var jme = $(this);
                            FreshPin.constants.selectedCategory = parseInt(jme.attr('catid'));
                            $("#cbselcat").html(jme.find('a').html());
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
            $.each(dt, function(ind, item) { opts.push(String.format('<option boardid="{1}" id="i{0}">{2}</option>', ind, item.ID, item.Name)); });
            $('#id_board').empty();
            $('#id_board').append(opts.join(' '));
        }
    }

    function load() {
        if (h() == 'boards') {
            $.getJSON('GET?t=getboards', function(dt, status, res) {
                $.each(dt, function(ind, item) {
                    var l = item.images.length;
                    while (l++ < 5) item.images.push(FreshPin.constants.cdn + 'img/paper.jpg');
                });
                FreshPin.constants.data = dt;
                set();
                getboards(true);
            });
            return;
        }
        if (h('settings')) {
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.get('settings', function(dt) {
                set();
                $("#content").show();
                $('#content').html(dt);
                FreshPin.emit('loadsettings');
            }, 'html');
            return;
        }
        if (h('changepassword')) {
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.get('changepassword', function(dt) {
                set();
                $("#content").show();
                $('#content').html(dt);
                FreshPin.emit('loadchangepassword');
            }, 'html');
            return;
        }
        if (h('filter') || h('board')) {
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.getJSON('GET?t=getimages', {
                    cat: h('cat'),
                    q: h('q'),
                    p: p,
                    filter: h('filter'),
                    board: h('board')
                }, function(dt, status, res) {
                    FreshPin.constants.data = dt;
                    get(true);
                });
            return;
        }
    }

    function reload() {
        if (h('boards')) {
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.getJSON('GET?t=getboards', function(dt) {
                $.each(dt, function(ind, item) {
                    var l = item.images.length;
                    while (l++ < 5) item.images.push({ ID: null, url: FreshPin.constants.cdn + 'img/paper.jpg' });
                });
                FreshPin.constants.data = dt;
                getboards(true);
            });
            return;
        }
        if (h('settings')) {
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.get('settings', function(dt) {
                set();
                $("#content").show();
                $('#content').html(dt);
                FreshPin.emit('loadsettings');
            }, 'html');
            return;
        }
        if (h('changepassword')) {
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.get('changepassword', function(dt) {
                set();
                $("#content").show();
                $('#content').html(dt);
                FreshPin.emit('loadchangepassword');
            }, 'html');
            return;
        }
        if (h('filter') || h('board')) {
            if (_pinshown()) {
                _forcepinclose = true;
                $('#pin').jqmHide();
            }
            $.getJSON('GET?t=getimages', {
                    cat: h('cat'),
                    q: h('q'),
                    p: p,
                    filter: h('filter'),
                    board: h('board')
                }, function(dt, status, res) {
                    FreshPin.constants.data = dt;
                    get(true);
                });
            return;
        }
    }

    function attach() {
        FreshPin.attach(loadBoards, null, null, 'onboardsadded');
        FreshPin.attach(loadCategories, null, null, 'oncatsadded');
        FreshPin.attach(load, null, null, 'load');
        FreshPin.attach(reload, null, null, 'reload');
    }

    attach();
    $('#uplPCharCount').html(FreshPin.constants.charCount);
    //////////////////////Modals/////////////////////////////////////////////////////////////////
    var add, uploadpin, createboard, addpin, addol, uploadpinol, createboardol;
    $('.add').jqm({
        overlay: 75,
        //modal: true,
        trigger: '#addtrigger',
        onShow: function(hash) {
            (add = hash.w).show();
            add.animate({
                top: '20%'
            }, 'fast', 'swing');
        },
        onHide: function(hash) {
            var o = hash.o;
            add.animate({
                    top: '100%'
                }, 'fast', 'swing', function() {
                    add.hide();
                    o.hide();
                });
        }
    });
    $('.add .modal .header1 .close').click(function() { $('.add').jqmHide(); });
    $('.uploadPin').jqm({
        overlay: 75,
        trigger: '#uploadpin',
        onShow: function(hash) { if (FreshPin.constants.boards.length == 0) alert("No Boards are Available. Please create one before you Upload a Pin");
        else {
                                     $('.add').jqmHide();
                                     (uploadpin = hash.w).show();
                                     uploadpin.animate({
                                         top: '20%'
                                     }, 'fast', 'swing');
                                 } },
        onHide: function(hash) {
            var o = hash.o;
            uploadpin.animate({
                    top: '100%'
                }, 'fast', 'swing', function() {
                    uploadpin.hide();
                    o.hide();
                });
        }
    });
    $('.uploadPin .modal .header1 .close').click(function() { $('.uploadPin').jqmHide(); });
    $('.addPin').jqm({
        overlay: 75,
        trigger: '#addpin',
        onShow: function(hash) { if (FreshPin.constants.boards.length == 0) alert("No Boards are Available. Please create one before you Add a Pin");
        else {
                                     $('.add').jqmHide();
                                     (addpin = hash.w).show();
                                     addpin.animate({
                                         top: '20%'
                                     }, 'fast', 'swing');
                                 } },
        onHide: function(hash) {
            var o = hash.o;
            addpin.animate({
                    top: '100%'
                }, 'fast', 'swing', function() {
                    addpin.hide();
                    o.hide();
                });

        }
    });
    $('.Repin').jqm({
        overlay: 75,
        onShow: function(hash) {
            var w = hash.w;
            w.show();
            w.animate({
                top: '20%'
            }, 'fast', 'swing');
        },
        onHide: function(hash) {
            var w = hash.w;
            var o = hash.o;
            w.animate({
                    top: '100%'
                }, 'fast', 'swing', function() {
                    w.hide();
                    o.hide();
                });

        }
    });
    $('.addPin .modal .header1 .close').click(function() { $('.addPin').jqmHide(); });
    $('.Repin .modal .header1 .close').click(function() { $('.Repin').jqmHide(); });
    $('.CreateBoard').jqm({
        overlay: 75,
        trigger: '#createboard',
        onShow: function(hash) {
            $('.add').jqmHide();
            (createboard = hash.w).show();
            createboard.show();
            createboard.animate({
                top: '20%'
            }, 'fast', 'swing');
        },
        onHide: function(hash) {
            var o = hash.o;
            createboard.animate({
                    top: '100%'
                }, 'fast', 'swing', function() {
                    createboard.hide();
                    o.hide();
                });
        }
    });
    $('.CreateBoard .modal .header1 .close').click(function() { $('.CreateBoard').jqmHide(); });
    $('#editpin').jqm({
        overlay: 75,
        onShow: function(hash) {
            var w = hash.w;
            w.show();
            w.animate({
                top: '20%'
            }, 'fast', 'swing');
        },
        onHide: function(hash) {
            var w = hash.w;
            var o = hash.o;
            w.animate({
                    top: '100%'
                }, 'fast', 'swing', function() {
                    w.hide();
                    o.hide();
                });
        }
    });
    $('#closeedit').click(function() { $('#editpin').jqmHide(); });
    ///////////////////////////////////////////////End///////////////////////////////////////////

    /////////////////////////Repin, Like, Comment//////////////////////////////////////////////////
    FreshPin.attach(function() {
        var edits = this.tmpl.find('a[name="edit"]');
        edits.click(function() {
            var BIMID = parseInt($(this).attr('bimid'));
            var o = $.grep(FreshPin.constants.data, function(item) { return item.BIMID == BIMID; })[0];
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
        this.tmpl.on('click', '.buttons a[name="like"]', function() {
            var liked = FreshPin.tobool(($(this).attr('liked')));
            var id = parseInt($(this).attr('bimid'));
            var me = this;
            $.post('POST?t=savelike', { id: id, liked: !liked }, function(data, res) {
                $(me).attr('liked', !liked);
                $(me).toggleClass('buttonLikeDis');
            }, 'json');
        });
        this.tmpl.on('click', '.buttons a[name="pin"]', function() {
            var BIMID = parseInt($(this).attr('bimid'));
            var o = $.grep(FreshPin.constants.data, function(item) { return item.BIMID == BIMID; })[0];
            FreshPin.constants.selectedRec = o;
            $('#imgRP').attr('src', o.url);
            if (FreshPin.constants.boards.length != 0) $('.Repin').jqmShow();
            else alert('You have to create a board first');
        });
        this.tmpl.on('click', '.buttons a[name="comment"]', function() {
            $(this).parent().parent().find('.addComment').toggle();
            $(this).toggleClass('buttonCommentDis');
            $(".gallery").masonry('reload');
        });
        this.tmpl.on('click', 'a[sc]', function() {
            var comments = $(this).prev('textarea').val();
            var id = parseInt($(this).attr('bimid'));
            var me = this;
            $.post('POST?t=addcomment', { comments: comments, id: id }, function(data, res) {
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
    }, null, null, 'bindimagetmpl');

    $("#editpin").click(function() {
        var BIMID = parseInt($(this).attr('bimid'));
        var o = $.grep(FreshPin.constants.data, function(item) { return item.BIMID == BIMID; })[0];
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
    $("#likepin").click(function() {
        var liked = FreshPin.tobool(($(this).attr('liked')));
        var id = parseInt($(this).attr('bimid'));
        var me = this;
        $.post('POST?t=savelike', { id: id, liked: !liked }, function(data, res) {
            $(me).attr('liked', !liked);
            $(me).toggleClass('buttonLikeDis');
        }, 'json');
    });
    $("#repin").click(function() {
        var BIMID = parseInt($(this).attr('bimid'));
        var o = $.grep(FreshPin.constants.data, function(item) { return item.BIMID == BIMID; })[0];
        FreshPin.constants.selectedRec = o;
        $('#imgRP').attr('src', o.url);
        if (FreshPin.constants.boards.length != 0) $('.Repin').jqmShow();
        else alert('You have to create a board first');
    });
    $('#saveRP').click(function () {
        var desc = $('#RPDesc').val();
        var o = FreshPin.constants.selectedRec;
        var board = FreshPin.constants.selectedBoard;
        $.post('POST?t=saverepin', { id: o.ID, board: board, desc: desc, source: o.imgsource }, function (data, res) { $('.Repin').jqmHide(); }, 'json');
    });
    /////////////////////////////End/////////////////////////////////////////////////////////////// 


    ///////////////////////////////////////////////////Modal Func/////////////////////////////////////////

    function setUpBoardsDD(sel, dd, title) {
        $(sel).click(function () { $(dd).show(); });
        $(dd + ' ul li').live('click', function () {
            $(title).html($(this).html());
            FreshPin.constants.selectedBoard = parseInt($(this).attr('boardid'));
            $(dd).hide();
            ;
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
            $("#urlimages").attr('src', 'img/spiral.gif');
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
        acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i
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
    var contributors = [];
    var boardContributorFlag = false;
    $('#contribute').click(function () {
        var contributor = $('#contributor').val();
        if (!contributor) alert('Enter a contributor\'s Email address and then click "Add"');
        else if (contributor == FreshPin.getEmail()) alert('You cannot be the contributor to the board you have created');
        else if (contributor != '')
            $.post('POST?t=validatecontributor', { contributor: contributor }, function (data, res) {
                if (data == '') contributors.push(contributor);
                else alert(data);
            }, 'text');
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
        if ($(this).attr('validation') == "none") boardContributorFlag = false;
        else boardContributorFlag = true;
    });
    /////////////////////////////////////////////End//////////////////////////////////////////////////////////////
});
FreshPin.attach(function () {
    var fn, name = FreshPin.getUN(), avatar = FreshPin.getAV();
    FreshPin.setru('settings', _his[_his.length - 2] || '');
    $('#ft').click(function () { $('#fu').trigger('click'); });
    $('#fu').fileupload({
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
    })
        .bind('fileuploadsubmit', function (e, data) {
        })
        .bind('fileuploadsend', function (e, data) {
        })
        .bind('fileuploaddone', function (e, data) {
            fn = data.result.file;
            $('#uploadedImage').attr('src', fn + '?width=170');
            $('#lmuplimg').hide();
        })
        .bind('fileuploadfail', function (e, data) {

        })
        .bind('fileuploadalways', function (e, data) {
        })
        .bind('fileuploadprogress', function (e, data) {
        })
        .bind('fileuploadprogressall', function (e, data) {
        })
        .bind('fileuploadstart', function (e) {
        })
        .bind('fileuploadstop', function (e) {
        })
        .bind('fileuploadchange', function (e, data) {
        })
        .bind('fileuploadpaste', function (e, data) {
        })
        .bind('fileuploaddrop', function (e, data) {
        })
        .bind('fileuploaddragover', function (e) {
        });
    $('#username1').change(function () {
        var val = $(this).val();
        if (/^[a-zA-Z0-9_]*$/.test(val))
            $.post('POST?t=usernameavail', { un: $(this).val() }, function (data) {
                $('#usernameview').html(data);
            }, 'text');
        else
            $('#usernameview').html('No spaces or special charecters');
    });
    $('#sp').click(function () {
        var email = $('#email').val();
        if (email == "" || !emailregex.test(email)) alert('Please enter a valid Email Address');
        var first_name = $('#first_name').val();
        var name = $('#username1').val();
        var about = $('#aboutu').val();
        var loc = $('#location').val();
        var website = $('#website').val();
        var imgurlarr = $('#uploadedImage').attr('src').split('?');
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