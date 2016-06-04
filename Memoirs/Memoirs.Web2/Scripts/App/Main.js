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

    var RecordsList = Backbone.Collection.extend({
        model: RecordModel,
        url: '/api/records',
        wait: true,
        //localStorage: new Backbone.LocalStorage('RecordsListLocalStorage'),//??
        comparator: 'Id'//is it works?
    });
    var RecordForTableView = Backbone.View.extend({
        isEditingOn: false,
        tagName: 'div',
        //className: 'list-group-item row',
        template: _.template($('#template_record_item').html()),
        itemId: '',
        render: function () {
            //var tt = this.template(this.model.attributes);
            //this.$el.html(tt);
            $(this.el).html(this.template(this.model.attributes));
            return this;

        },
        updateModel: function (record) {
            record.attributes.DateCreatedDate = this.model.attributes.DateCreatedDate;
            this.model = record;
        },
        initialize(options) {
            this.itemId = options.itemId;
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
        }
    });
    var RecordsTableView = Backbone.View.extend({
        template: _.template($('#template_record_items_table').html()),
        recordTemplate: _.template($('#template_record_item').html()),
        initialize: function (options) {

            var firstDayOffset = new Date(CurrentPageDateMonthYear.getFullYear(), CurrentPageDateMonthYear.getMonth(), 1).getDay();
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
                            itemId: itemId,
                            
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

            //records_table.render

            return this;

        },
        addToTable: function (record) {
            //var recordDate = new Date(record.attributes.DateCreatedString);
            var recordDate = new Date(record.attributes.DateCreatedDate);

            var firstDayOffset = new Date(recordDate.getFullYear(), recordDate.getMonth(), 1).getDay();
            var i = (recordDate.getDate() / 7 | 0);
            var j = (recordDate.getDay() - 1);

            var date = new Date(CurrentPageDateMonthYear.getFullYear(), CurrentPageDateMonthYear.getMonth(), (i * 7 + (j + 1) - (firstDayOffset - 1)));

            //this.table.trs[i].tds[j] = {
            //    locator: 'record_' + j + '_' + i,
            //    date: date.getDate(),
            //    item: record.attributes,
            //    recordTemplate: this.recordTemplate
            //};
            this.tableViews.trs[i].tds[j].view.updateModel(record);
            this.tableViews.trs[i].tds[j].view.render();
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
        }
    });



    var App = Backbone.View.extend({
        recordsList: new RecordsList,
        el: $('#app'),
        events: {
            'keypress #new_record_label': 'createOnEnter',
            'keypress #new_record_text': 'createOnEnter'
        },
        tableView: new RecordsTableView(),
        initialize: function () {
            this.newRecordLabel = this.$('#new_record_label');
            this.newRecordText = this.$('#new_record_text');
            this.listenTo(this.recordsList, 'add', this.addOne);
            this.listenTo(this.recordsList, 'reset', this.addAll);
            this.listenTo(this.recordsList, 'all', this.render);

            this.recordsList.fetch({ data: $.param({ monthyear: (CurrentPageDateMonthYear.getMonth() + 1) + '.' + CurrentPageDateMonthYear.getFullYear() }) });
            //$('temp_id').append();
            //var tableTemplate = new RecordsTableView();
            if (location.hash === '#table') {
                // this.tableView.
                this.$el.append(this.tableView.render().el);
            }
        },
        render: function () {

        },
        addOne: function (record) {
            if (location.hash === '#table') {


                if (record.attributes.DateCreatedDate == undefined) {
                    if (record.attributes.DateCreatedString == undefined || record.attributes.DateCreatedString == '') {
                        record.attributes.DateCreatedDate = CurrentPageDateMonthYear;
                        record.attributes.DateCreatedString = CurrentPageDateMonthYear.toDateString();
                    } else {

                        record.attributes.DateCreatedDate = new Date(record.attributes.DateCreated);
                    }
                }
                // $('#record_' + locator).append(record.attributes.Label);
                this.tableView.addToTable(record);
                //  this.tableView.render();
            } else {
                var view = new RecordView({
                    model: record
                });
                this.$('#prev_records').prepend(view.render().el);
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
            record = this.recordsList.create(
                newRecord,
                {
                    wait: true, error: function (model, responce) {
                        //temp solution
                        alert("error!: " + responce.responseText);
                    },
                    success: function (a1, a2) {
                        record.attributes.Id = a2;
                    }
                }
            );
            this.newRecordLabel.val('');
            this.newRecordText.val('');
        }
    });
    var a = new App;
});
