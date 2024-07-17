// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function showPassword(eyeIcon) {
    // Get the input field associated with this eye icon
    var inputId = eyeIcon.getAttribute("data-target");
    var input = document.getElementById(inputId);

    // Toggle the input type
    if (input.type === "password") {
        input.type = "text";
    } else {
        input.type = "password";
    }

    // Toggle the eye icon class
    if (eyeIcon.classList.contains("bi-eye-slash")) {
        eyeIcon.classList.remove("bi-eye-slash");
        eyeIcon.classList.add("bi-eye-fill");
    } else {
        eyeIcon.classList.remove("bi-eye-fill");
        eyeIcon.classList.add("bi-eye-slash");
    }
}

//function showPassword(passtype) {
//    var input = document.getElementById("password-input");
//    if (passtype == "confirm") {
//        input = document.getElementById("confirm-input");
//        var eyeIconConfirm = document.getElementById("eye-icon-confirm");
//        if (eyeIconConfirm.className == "bi bi-eye-slash") {
//            eyeIconConfirm.className = "bi bi-eye-fill";
//        } else {
//            eyeIconConfirm.className = "bi bi-eye-slash";
//        }
//    } else if (passtype == "pass") {
//        var eyeIconPass = document.getElementById("eye-icon-pass");
//        if (eyeIconPass.className == "bi bi-eye-slash") {
//            eyeIconPass.className = "bi bi-eye-fill";
//        } else {
//            eyeIconPass.className = "bi bi-eye-slash";
//        }
//    }
//    if (input.type === "password") {
//        input.type = "text";
//    } else {
//        input.type = "password";
//    }
//}
