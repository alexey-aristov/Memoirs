$(document).ready(function () {
    var Record = Backbone.Model.extend({
        defaults: function () {
            return {
                Id: 0,
                Label: '',
                Text: ''
            };
        }
    });

    var RecordsList = Backbone.Collection.extend({
        model: Record,
        url: '/api/records',
        //localStorage: new Backbone.LocalStorage('RecordsListLocalStorage'),//??
        comparator: 'Id'//is it works?
    });

    var RecordsView = Backbone.View.extend({
        tagName: 'div',
        className:'list-group-item',
        template: _.template($('#item-template').html()),
        events: {
            'dblclick .view': 'edit',
            'keypress .edit': 'updateOnEnter'
        },
        edit: function () {
            alert(this.$el.text);
        },
        initialize: function () {
            this.listenTo(this.model, 'change', this.render);
        },
        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            this.input = this.$('.edit');
            return this;
        }//,
        //updateOnEnter: function() {
        //    if(e.keyCode == 13)
        //        this.close
        //}
    });

    var Records = new RecordsList;

    var App = Backbone.View.extend({
        el: $('#app'),
        events: {
            'keypress #new_record_label': 'createOnEnter',
            'keypress #new_record_text': 'createOnEnter'
        },
        initialize: function () {
            this.newRecordLabel = this.$('#new_record_label');
            this.newRecordText = this.$('#new_record_text');
            this.listenTo(Records, 'add', this.addOne);
            this.listenTo(Records, 'reset', this.addAll);
            this.listenTo(Records, 'all', this.render);

            Records.fetch();
        },
        render: function () {
        },
        addOne: function (record) {
            var view = new RecordsView({
                model: record
            });
            this.$('#prev_records').prepend(view.render().el);
        },
        addAll: function () {
            Records.each(this.addOne, this);
        },
        createOnEnter: function (e) {
            if (e.keyCode != 13) return;
            if (!this.newRecordLabel.val()) return;

            Records.create({ Label: this.newRecordLabel.val(), Text: this.newRecordText.val() });
            this.newRecordLabel.val('');
            this.newRecordText.val('');
        }
    });
    var a = new App;
});
