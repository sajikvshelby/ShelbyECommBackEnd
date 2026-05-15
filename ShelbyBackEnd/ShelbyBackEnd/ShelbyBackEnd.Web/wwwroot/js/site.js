// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//function NoImg(i) {

//    i.src = "../images/nophoto.gif";
//}




$(document).ready(function () {

    $('.busyanchor').not('[target="_blank"]').not('[href^="#"]').click(function () {
        $('#busy_ic').show();
    });

    $('.busybutton').click(function () {
        $('#busy_ic').show();
    });

    $('.busyselect').change(function () {
        $('#busy_ic').show();
    });
  

});


$(window).on('pageshow', function () {
    $('#busy_ic').hide();
});




