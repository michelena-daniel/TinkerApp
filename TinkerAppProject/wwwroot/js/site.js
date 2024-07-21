// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showPassword(eyeIcon) {
    var inputId = eyeIcon.getAttribute("data-target");
    var input = document.getElementById(inputId);

    if (input.type === "password") {
        input.type = "text";
    } else {
        input.type = "password";
    }

    if (eyeIcon.classList.contains("bi-eye-slash")) {
        eyeIcon.classList.remove("bi-eye-slash");
        eyeIcon.classList.add("bi-eye-fill");
    } else {
        eyeIcon.classList.remove("bi-eye-fill");
        eyeIcon.classList.add("bi-eye-slash");
    }
}
