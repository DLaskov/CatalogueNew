$(document).ready(function () {
    $("#registerForm").submit(function (event) {
        validator.validateUsernamePassword("#UserName", "#Password", event);
        validator.confirmPassword("#Password", "#passwordConfirm", event);
    });
});

var validator = (function () {
    function confirmPassword(passwordInputID, confirmPasswordInputID, event) {
        if ($(passwordInputID).val() != $(confirmPasswordInputID).val()) {
            $(confirmPasswordInputID).after("<p style=\"color: red\">Password doesn't match!</p>");
            event.preventDefault();
        }
    }
    function validateUsernamePassword(usernameInputID, passwordInputID, event) {
        if ($(usernameInputID).val() == $(passwordInputID).val()) {
            $(passwordInputID).after("<p style=\"color: red\">Password must be different from username!</p>");
            event.preventDefault();
        }
    }
    return {
        confirmPassword: confirmPassword,
        validateUsernamePassword: validateUsernamePassword
    }
})();