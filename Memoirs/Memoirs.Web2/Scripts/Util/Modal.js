function generateUUID() {
    var d = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (d + Math.random() * 16) % 16 | 0;
        d = Math.floor(d / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
};

var Modal = Backbone.View.extend({
    template: _.template(Templates.ModalView),

    initialize: function (options) {
        if (options.Template != undefined) {
            this.innerTemplate = _.template(options.Template);
            this.$el.html(this.template());
            this.modalElement = this.$el.find('.modal-view');
            this.modalElement.append(this.innerTemplate());
            $('body').append(this.$el);
            return;
        }
        if (options.View != undefined) {
            this.$el.html(this.template());
            this.modalElement = this.$el.find('.modal-view');
            this.modalElement.append(options.View.el);
            $('body').append(this.$el);
            this.innerView = options.View;
            return;
        }
    },
    render: function () {

    },
    getBackLayer: function () {
        return $('#modal-back-layer');
    },
    show: function () {

        if (this.LastCloseType == ModalCloseType.Destroy) {
            throw "cannot show destoyed modal";
        }
        var backLayer = this.getBackLayer();
        if (backLayer.size() == 0) {
            $('body').append('<div id="modal-back-layer"></div>');
            backLayer = this.getBackLayer();
        }
        backLayer.fadeIn(300);
        this.modalElement.fadeIn(300);
        //Set the center alignment padding + border
        var popMargTop = (this.modalElement.height() + 24) / 2;
        var popMargLeft = (this.modalElement.width() + 24) / 2;

        this.modalElement.css({
            'margin-top': -popMargTop,
            'margin-left': -popMargLeft
        });
        var self = this;
        backLayer.click(function () {
            self.hide();
        });
    },
    close: function (options) {
        this.LastCloseType = options.CloseType;
        switch (options.CloseType) {
            case ModalCloseType.Hide:
                var backLayer = this.getBackLayer();
                backLayer.remove();
                this.modalElement.fadeOut(300);
                break;
            case ModalCloseType.Destroy:
                break;
            default:
        }
    },
    hide: function () {
        this.close({
            CloseType: ModalCloseType.Hide
        });
    }
});

var ModalCloseType = Object.freeze({
    Unknown: 0,
    Success: 1,
    Hide: 2,
    Destroy: 3,
    Cancel: 4
});