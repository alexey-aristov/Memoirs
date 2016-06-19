Date.prototype.getWeeksCount = function () {
    var year = this.getFullYear();
    var month_number = this.getMonth();
    var firstOfMonth = new Date(year, month_number - 1, 1);
    var lastOfMonth = new Date(year, month_number, 0);
    var used = firstOfMonth.getDay() + lastOfMonth.getDate();
    return Math.ceil(used / 7);
}
$(document).ready(function () {
    var CurrentPageDateMonthYear = new Date();

    var RecordModel = Backbone.Model.extend({
        defaults: function () {
            return {
                Id: 0,
                Label: '',
                Text: '',
                Editable: false,
                DateCreatedString: ''
            };
        },
        initialize: function (options) {
            this.attributes.DateCreatedDate = options.DateCreatedDate;
        }
    });

    var RecordsUrl = '/api/records';

    var RecordsList = Backbone.Collection.extend({
        model: RecordModel,
        url: RecordsUrl,
        wait: true,
        comparator: 'Id'//is it works?
    });
    var RecordForTableView = Backbone.View.extend({
        isEditingOn: false,
        tagName: 'div',
        template: _.template($('#template_record_item').html()),
        itemId: '',
        render: function () {
            $(this.el).html(this.template(this.model.attributes));
            return this;
        },
        updateModel: function (record) {
            record.attributes.DateCreatedDate = this.model.attributes.DateCreatedDate;
            this.model.url = function() {
                return record.url();
            };
            this.model.set(record.attributes);
        },
        initialize(options) {
            this.itemId = options.itemId;
            this.listenTo(this.model, 'change', this.render);
        },
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
        confirmEditOnKey(e) {
            if (e.keyCode == 13)
                this.confirmEdit();
        },
        confirmEdit: function () {
            this.isEditingOn = false;
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
        },
        undoEdit: function () {
            this.isEditingOn = false;
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
        },
        dispose: function () {
            this.undelegateEvents();
            this.remove();
        }
    });
    var RecordsTableView = Backbone.View.extend({
        template: _.template($('#template_record_items_table').html()),
        recordTemplate: _.template($('#template_record_item').html()),
        initialize: function (options) {
            var firstDayOffset = new Date(CurrentPageDateMonthYear.getFullYear(), CurrentPageDateMonthYear.getMonth(), 1).getDay();
            //TODO possible memory leaks
            this.tableViews = { trs: [] };
            this.table = { trs: [] };
            for (var i = 0; i < new Date().getWeeksCount() ; i++) {
                var tds = [];
                for (var j = 0; j < 7; j++) {
                    var itemId = 'record_' + j + '_' + i;
                    var dateCreatedDate = new Date(CurrentPageDateMonthYear.getFullYear(), CurrentPageDateMonthYear.getMonth(), (i * 7 + (j + 1) - (firstDayOffset - 1)));
                    var view = {
                        view: new RecordForTableView({
                            model: new RecordModel({
                                DateCreatedDate: dateCreatedDate
                            }),
                            itemId: itemId
                        }),
                        itemId: itemId,
                        isCurrentMonth: dateCreatedDate.getMonth() == CurrentPageDateMonthYear.getMonth()
                    };
                    tds.push(view);
                }
                this.tableViews.trs.push({
                    'tds': tds
                });
            }
        },
        tableViews: { trs: [] },
        table: { trs: [] },
        render: function () {
            $(this.el).html(this.template(this.tableViews));
            var self = this;
            _.each(this.tableViews.trs, function (tds) {
                _.each(tds.tds, function (td) {
                    td.view.setElement(self.$el.find('#' + td.itemId)).render();
                });
            });
            return this;
        },
        addToTable: function (record) {
            var recordDate = new Date(record.attributes.DateCreatedDate);
            var firstDayOffset = new Date(recordDate.getFullYear(), recordDate.getMonth(), 1).getDay();
            var i = (recordDate.getDate() / 7 | 0);
            var j = (recordDate.getDay() - 1);
            if (j == -1)
                j = 6;
            var date = new Date(CurrentPageDateMonthYear.getFullYear(), CurrentPageDateMonthYear.getMonth(), (i * 7 + (j + 1) - (firstDayOffset - 1)));

            if (j >= 0) {
                this.tableViews.trs[i].tds[j].view.updateModel(record);
                this.tableViews.trs[i].tds[j].view.render();
            }
        },
        dispose: function () {
            _.each(this.tableViews.trs, function (tds) {
                _.each(tds.tds, function (td) {
                    td.view.dispose();
                });
            });
            this.tableViews = { trs: [] };
            this.table = { trs: [] };
            this.undelegateEvents();
            this.remove();
        }
    });
    var RecordView = Backbone.View.extend({
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
            this.isEditingOn = false;
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
        },
        undoEdit: function () {
            this.isEditingOn = false;
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
        },
        dispose: function () {}
    });

    var App = Backbone.View.extend({
        recordsList: new RecordsList,
        recordsAsTable: false,
        el: $('#app'),
        events: {
            'keypress #new_record_label': 'createOnEnter',
            'keypress #new_record_text': 'createOnEnter'
        },
        tableView: new RecordsTableView(),
        initialize: function (options) {
            this.recordsAsTable = options.recordsAsTable;
            this.newRecordLabel = this.$('#new_record_label');
            this.newRecordText = this.$('#new_record_text');
            this.listenTo(this.recordsList, 'add', this.addOne);
            this.listenTo(this.recordsList, 'reset', this.addAll);
            this.listenTo(this.recordsList, 'all', this.render);
            this.listenTo(this.recordsList, 'sync', this.recodsListSync);

            this.recordsList.fetch({ data: $.param({ monthyear: (CurrentPageDateMonthYear.getMonth() + 1) + '.' + CurrentPageDateMonthYear.getFullYear() }) });
            if (this.recordsAsTable) {
                this.$el.append(this.tableView.render().el);
            }
        },
        render: function() {
            
        },
        addOne: function (record) {
            if (record.Id == 0) {
                return;//it have to be handle in sync
            }
            this.addRecordToView(record);

        },
        addRecordToView: function(record) {
            if (this.recordsAsTable) {
                if (record.attributes.DateCreatedDate == undefined) {
                    if (record.attributes.DateCreatedString == undefined || record.attributes.DateCreatedString == '') {
                        record.attributes.DateCreatedDate = CurrentPageDateMonthYear;
                        record.attributes.DateCreatedString = CurrentPageDateMonthYear.toDateString();
                    } else {
                        record.attributes.DateCreatedDate = new Date(record.attributes.DateCreated.split('T')[0]);
                    }
                }
                this.tableView.addToTable(record);

            } else {
                var view = new RecordView({
                    model: record
                });

                var prevRecord = this.$('#records-prev-item-' + record.attributes.Id);
                if (prevRecord.length == 0) {
                    this.$('#prev_records').prepend(view.render().el);
                } else {
                    prevRecord.parent().replaceWith(view.render().el);
                }
            }
        },
        addAll: function () {
            this.recordsList.each(this.addOne, this);
        },
        createOnEnter: function (e) {
            if (e.keyCode != 13) return;
            if (!this.newRecordLabel.val()) return;
            var newRecord = {
                Label: this.newRecordLabel.val(),
                Text: this.newRecordText.val(),
                DateCreatedDate: CurrentPageDateMonthYear,
                Editable: true
            };
            var record;
            var self = this;
            record = this.recordsList.create(
                newRecord,
                {
                    wait: true, error: function (model, responce) {
                        //temp solution
                        alert("error!: " + responce.responseText);
                    },
                    success: function (a1, a2) {
                        a1.set({ Id: a2 });
                    }
                }
            );
            this.newRecordLabel.val('');
            this.newRecordText.val('');
        },
        reinit: function (options) {
            this.recordsAsTable = options.recordsAsTable;
            this.$('#prev_records').html('');
            this.tableView.undelegateEvents();
            this.tableView.dispose();
            this.tableView.remove();
            this.recordsList.fetch({ data: $.param({ monthyear: (CurrentPageDateMonthYear.getMonth() + 1) + '.' + CurrentPageDateMonthYear.getFullYear() }) });

            if (this.recordsAsTable) {
                this.tableView = new RecordsTableView();
                this.$el.append(this.tableView.render().el);
            }
        },
        recodsListSync: function (model, options) {
            if(model.models==undefined) //filter event on initial sync
                this.addRecordToView(model);
        }
    });
    var application = undefined;
    var Router = Backbone.Router.extend({
        routes: {
            '': 'index',
            'index': 'index',
            'table': 'table'
        },
        index: function () {
            if (application == undefined) {
                application = new App({
                    recordsAsTable: false
                });
            } else {
                application.reinit({
                    recordsAsTable: false
                });
            }
        },
        table: function () {
            if (application == undefined) {
                application = new App({
                    recordsAsTable: true
                });
            } else {
                application.reinit({
                    recordsAsTable: true
                });
            }
        }
    });

    var router = new Router();
    Backbone.history.start();

});
