// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//function NoImg(i) {

//    i.src = "../images/nophoto.gif";
//}




$(document).ready(function () {

    $('a').not('[target="_blank"]').not('[href^="#"]').click(function () {
        $('#loader-wrapper').show();
    });
  
    $('form').submit(function () {
        $('#loader-wrapper').show();
    });

    $('select').change(function () {
        $('#loader-wrapper').show();
    });


});


$(window).on('pageshow', function () {
    $('#loader-wrapper').hide();
});




