// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

function readURL(input) {
    var reader = new FileReader();

    reader.onload = function (e) {
        document.getElementById("Img").setAttribute("src", e.target.result);
    };
    reader.readAsDataURL(input.files[0]);
}   

