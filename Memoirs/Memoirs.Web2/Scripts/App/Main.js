$(document).ready(function () {
    var Record = Backbone.Model.extend({
        defaults: function () {
            return {
                Id: 0,
                Label: '',
                Text: '',
                Editable: true
            };
        }
    });

    var RecordsList = Backbone.Collection.extend({
        model: Record,
        url: '/api/records',
        wait: true,
        //localStorage: new Backbone.LocalStorage('RecordsListLocalStorage'),//??
        comparator: 'Id'//is it works?
    });

    var RecordsView = Backbone.View.extend({
        isEditingOn: false,
        tagName: 'div',
        className: 'list-group-item row',
        template: _.template($('#item-template').html()),
        events: {
            'click .records-prev-edit-btn': 'edit',
            'click .records-prev-edit-confirm': 'confirmEdit',
            'click .records-prev-edit-undo': 'undoEdit',
            'keypress .records-prev-edit-text': 'confirmEditOnKey',
            'keypress .records-prev-edit-label': 'confirmEditOnKey'
        },
        edit: function (param) {
            if (this.isEditingOn) {
                return;
            }
            this.isEditingOn = true;
            this.$('.records-prev-edit-text').val(this.model.attributes.Text);
            this.$('.records-prev-edit-label').val(this.model.attributes.Label);
            this.$('.records-prev-edit-text,.records-prev-edit-label,.records-prev-edit-confirm').show();
            this.$('.records-prev-text,.records-prev-label').hide();
            this.$('.records-prev-edit-confirm').show();
            this.$('.records-prev-edit-undo').show();
            this.$('.records-prev-edit-btn').hide();
        },
        initialize: function () {
            this.listenTo(this.model, 'change', this.render);
        },
        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            this.input = this.$('.edit');
            return this;
        },
        confirmEditOnKey(e) {
            if (e.keyCode == 13)
                this.confirmEdit();
        },
        confirmEdit: function () {
            this.model.set({
                "Label": this.$('.records-prev-edit-label').val(),
                "Text": this.$('.records-prev-edit-text').val()
            });
            Backbone.sync('update', this.model, {
                wait: true,
                error: function (responce) {
                    this.model.fetch({
                        url: this.model.url() + '/' + this.model.attributes.Id
                    });
                    this.render();
                    //temp
                    alert("update failed:" + responce.responseText);
                }.bind(this)
            });
            this.$('.records-prev-text,.records-prev-label').show();
            this.$('.records-prev-edit-label').hide();
            this.$('.records-prev-edit-text').hide();
            this.$('.records-prev-edit-confirm').hide();
            this.$('.records-prev-edit-undo').hide();
            this.$('.records-prev-edit-btn').show();
            this.isEditingOn = false;
        },
        undoEdit: function () {
            this.model.fetch({
                url: this.model.url() + '/' + this.model.attributes.Id
            });
            this.render();
            this.$('.records-prev-text,.records-prev-label').show();
            this.$('.records-prev-edit-label').hide();
            this.$('.records-prev-edit-text').hide();
            this.$('.records-prev-edit-confirm').hide();
            this.$('.records-prev-edit-undo').hide();
            this.$('.records-prev-edit-btn').show();
            this.isEditingOn = false;
        }
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

            Records.fetch({ data: $.param({ monthyear: (new Date().getMonth() + 1) + '.' + new Date().getFullYear() }) });
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

            Records.create(
                { Label: this.newRecordLabel.val(), Text: this.newRecordText.val() },
                {
                    wait: true, error: function (model, responce) {
                        //temp solution
                        alert("error!: " + responce.responseText);
                    }
                }
            );
            this.newRecordLabel.val('');
            this.newRecordText.val('');
        }
    });
    var a = new App;
});
