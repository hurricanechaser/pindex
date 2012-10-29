/// <reference path="scripts/jquery-1.7.1.js" />
///<reference src="scripts/scrolltopcontrol.js" ></script>
///<reference src="scripts/jquery-jquery-tmpl/jquery.tmpl.js" ></script>
///<reference src="scripts/jquery-jquery-tmpl/jquery.tmplPlus.js" ></script>
///<reference src="scripts/desandro-masonry/jquery.masonry.js" ></script>
///<reference src="scripts/jquery.fancybox-1.3.4/fancybox/jquery.fancybox-1.3.4.js"/>
/// <reference path="scripts/cookies.js" />

$(function () {

    var fn, name = FreshPin.getUN(), avatar = FreshPin.getAV();
    var emailregex = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$/;
    $('#username1').val(name);
    $('#uploadedImage').attr('src', avatar + '?width=170');
    $.getJSON('GET?t=getprofile', function (dt, status, res) {
        $('#email').val(dt.Email);
        $('#first_name').val(dt.FirstName);
        $('#aboutu').val(dt.About);
        $('#location').val(dt.Location);
        $('#website').val(dt.Website);
    });
    $('#ft').click(function () {
        $('#fu').trigger('click');
    });
    $('#fu').fileupload({
        url: 'POST?t=up',
        singleFileUploads: true,
        dataType: 'json',
        acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i
    }).bind('fileuploadadd', function (e, data) {
        if (/(.*?)\.(jpg|jpeg|png|gif)$/.test(data.files[0].name))
            $('#lmuplimg').show();
        else {
            e.preventDefault();
            alert('Select only image files to upload');
            $('#lmuplimg').hide();
            throw 'invalid file type';
        }
    })
    .bind('fileuploadsubmit', function (e, data) { })
    .bind('fileuploadsend', function (e, data) { })
    .bind('fileuploaddone', function (e, data) {
        file = data.result.file;
        $('#uploadedImage').attr('src', file + '?width=170');
        $('#lmuplimg').hide();
    })
    .bind('fileuploadfail', function (e, data) {

    })
    .bind('fileuploadalways', function (e, data) { })
    .bind('fileuploadprogress', function (e, data) { })
    .bind('fileuploadprogressall', function (e, data) { })
    .bind('fileuploadstart', function (e) { })
    .bind('fileuploadstop', function (e) { })
    .bind('fileuploadchange', function (e, data) { })
    .bind('fileuploadpaste', function (e, data) { })
    .bind('fileuploaddrop', function (e, data) { })
    .bind('fileuploaddragover', function (e) { });

    $('#sp').click(function () {
        var email = $('#email').val();
        if (email == "" || !emailregex.test(email))
            alert('Please enter a valid Email Address');
        var first_name = $('#first_name').val();
        var name = $('#username1').val();
        var about = $('#aboutu').val();
        var location = $('#location').val();
        var website = $('#website').val();
        var imgurlarr = $('#uploadedImage').attr('src').split('?');
        var imgurl = (imgurlarr.length > 0) ? imgurlarr[0] : '';
        if (name == '')
            alert('Please enter username');
        else
            $.post('POST?t=saveprofile', { email: email, name: name, fn: fn, first_name: first_name, about: about, location: location, website: website }, function (dt, res, opts) {
                if (dt != '')
                    alert(dt);
                else
                    $(window.location).attr('href', '.');
            }, 'text');
    });
});
