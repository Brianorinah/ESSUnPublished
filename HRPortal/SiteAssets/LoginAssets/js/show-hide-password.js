$(function () {
    // Register Submit Button Disabled
    //Deprecated $(':input[type="submit"]').prop('disabled', true);

    $(".confirm-input").prop("checked", false);

    $('#submitDisabled').prop('disabled', true);

    // Password
    $('#showPassword').hover(function () {
        if ($('#password').attr('type') === 'password') {
            $('#password').attr('type', 'text');
        } else {
            $('#password').attr('type', 'password');
        }

        if ($('.icon-password').hasClass('fa fa-eye-slash')) {
            $('.icon-password').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
        } else {
            $('.icon-password').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
        }
    });

    // Confirm Password
    $('#showConfirmPassword').hover(function () {
        if ($('#confirmPassword').attr('type') === 'password') {
            $('#confirmPassword').attr('type', 'text');
        } else {
            $('#confirmPassword').attr('type', 'password');
        }

        if ($('.icon-confirm-password').hasClass('fa fa-eye-slash')) {
            $('.icon-confirm-password').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
        } else {
            $('.icon-confirm-password').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
        }
    });

    // Submit Button Toggled
    $(".confirm-input").change(function () {
        if (this.checked) {
            $('#submitDisabled').prop('disabled', false);
        } else {
            $('#submitDisabled').prop('disabled', true);
        }
    });


    $('input.numeric-phone-number').keyup(function (event) {
        if (event.which !== 8 && event.which !== 0 && event.which < 48 || event.which > 57) {
            $(this).val(function (index, value) {
                return value.replace(/\D/g, "");
            });
        }
    });
});