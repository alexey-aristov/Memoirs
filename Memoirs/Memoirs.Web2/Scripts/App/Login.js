var RegisterModel = Backbone.Model.extend({
    defaults: {
        "Login": "",
        "Password": "",
        "PasswordConfirm":"",
        "Email":""
    },
    validate: function() {
        var errors = [];
        var minPasswordLength = 6;
        if (this.attributes.Login == '') {
            errors.push({
                Name: 'Login',
                Message: 'Login must be not empty!'
            });
        }
        if (this.attributes.Password == '') {
            errors.push({
                Name: 'Password',
                Message: 'Password must be not empty!'
            });
        }
        if (this.attributes.Email == '') {
            errors.push({
                Name: 'Email',
                Message: 'Email must be not empty!'
            });
        }

        if (this.attributes.Password.length < minPasswordLength) {
            errors.push({
                Name: 'Password',
                Message: 'Password length must be more then '+minPasswordLength+'!'
            });
        }
        if ((this.attributes.Password != this.attributes.PasswordConfirm)) {
            errors.push({
                Name: 'PasswordConfirm',
                Message: 'Password confirmation and password not match!'
            });
        }
        return errors.length > 0 ? errors : false;
    }
});

var RegisterFormView = Backbone.View.extend({
    el: '#register_form',
    events: {
        'click #register_submit_btn': 'submitClicked'
    },
    initialize: function () {
    },
    render: function() {
        alert("render");
    },
    submitClicked: function (e) {
        var self = this;
        e.preventDefault();
        self.model.set({
            "Login": this.$('#register_login_input').val(),
            "Password": this.$('#register_password_input').val(),
            "PasswordConfirm": this.$('#register_password_confirm_input').val(),
            "Email": this.$('#register_email_input').val()
        });
        var validationResult = this.model.validate();
        self.$('#register_errors_list').html('');
        if (validationResult) {
            _.each(validationResult, function(error) {
                self.$('#register_errors_list').append("<li>" + error.Message + "</li>");
            });
        } else {
            $.ajax({
                url: '/api/account/register',
                data: JSON.stringify( self.model.toJSON()),
                method: 'POST',
                contentType: 'application/json',
                dataType: "json",
                success: function (data) {
                    if (data.RegisterStatus == "registered") {
                        self.$el.html("<div>You have been successfully registerd with login" + data.Login + "!</div><a href='/'>Start!</a>");
                    } else {
                        _.each(data.Errors, function (error) {
                            self.$('#register_errors_list').append("<li>" + error + "</li>");
                        });
                    }
                },
                error: function (xhr, status, errorThrown) {
                    var errors = [];
                    _.each(errors, function (error) {
                        self.$('#register_errors_list').append("<li> server error while registering :( </li>");
                    });
                }
            });
        }
    },
    model: new RegisterModel()
});

var registerView = new RegisterFormView();