// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function tupian(v) {
    var reader = new FileReader();
    reader.onload = function (evt) {

        $("#ImgAvatar")[0].src = evt.target.result;
    }
    reader.readAsDataURL(v.files[0]);
}