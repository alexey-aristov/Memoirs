﻿var Templates = {
    RecordItemForList:
        '<div class="col-md-11 col-lg-11 col-sm-11 col-xs-11" id="records-prev-item-<%-Id%>">' +
            '<h4 class="list-group-item-heading records-prev-label">' +
                '<%- Label %>' +
                '<%- DateCreatedString %>' +
            '</h4>' +
            '<p class="list-group-item-text records-prev-text">' +
                '<%- Text %>' +
            '</p>' +
            '<% if(Editable){ %>' +
            '<input type="text" class="form-control records-prev-edit-label" placeholder="Best event!">' +
            '<textarea class="form-control records-prev-edit-text" placeholder="Event description"></textarea>' +
            '<%}%>' +
        '</div>' +
        '<div class="col-md-1 col-lg-1 col-sm-1 col-xs-1">' +
            '<% if(Editable){ %>' +
                '<span title="Edit" class="glyphicon glyphicon-edit records-prev-edit-btn"></span>' +
                '<span title="Undo" class="glyphicon glyphicon-refresh records-prev-edit-undo"></span>' +
                '<span title="Save" class="glyphicon glyphicon-save records-prev-edit-confirm"></span>' +
            '<%}%>' +
        '</div>',

    RecordsTable:
    '<div class="row">' +
        '<div class="col-md-5 col-lg-5 col-sm-5 col-xs-5">' +
            '<button id="records_table_prev_month">prev</button>' +
            '<button id="records_table_now_month">now</button>' +
            '<button id="records_table_next_month">next</button>' +
        '</div>' +
        '<h4 class="col-md-5 col-lg-5 col-sm-5 col-xs-5">' +
            '<%-month%>,<%-year%>' +
        '</h4>' +
    '</div>' +
    '<table class="table table-bordered">' +
        '<% _.each(trs,function(tr){%>' +
            '<tr>' +
                '<% _.each(tr.tds,function(td){%>' +
                    '<td id="<%-td.itemId%>" class="records_table_day<%if(!td.isCurrentMonth){%> records_table_day_another_month<%}%>"/>' +
                '<%});%>' +
            '</tr>' +
        '<%});%>' +
    '</table>',

    RecordItemForTable:
    '<div class="row">' +
        '<div class="pull-right">' +
            '<%- DateCreatedDate.getDate() %>' +
            '<% if(Editable){ %>' +
                '<span title="Edit" class="glyphicon glyphicon-edit records-prev-edit-btn"></span>' +
                '<span title="Undo" class="glyphicon glyphicon-refresh records-prev-edit-undo"></span>' +
                '<span title="Save" class="glyphicon glyphicon-save records-prev-edit-confirm"></span>' +
            '<%}%>' +
        '</div>' +
    '</div>' +
    '<div class="row">' +
        '<h5 class="list-group-item-heading records-prev-label">' +
            '<%- Label %>' +
        '</h5>' +
        '<% if(Editable){ %>' +
            '<input type="text" class="form-control records-prev-edit-label" placeholder="Best event!">' +
        '<%}%>' +
    '</div>' +
    '<div class="row">' +
        '<div class="list-group-item-text records-prev-text">' +
            '<%- Text %>' +
        '</div>' +
        '<% if(Editable){ %>' +
            '<textarea class="form-control records-prev-edit-text" placeholder="Event description"></textarea>' +
        '<%}%>' +
    '</div>',

    ModalView:
        '<div class="modal-view">' +
        '</div>',

    AddNewRecordView:
        '<div class="row">' +
            '<div>' +
                '<h1>New record</h1>' +
            '</div>' +
            '<div>' +
                '<input class="new_record_label" type="text" class="form-control" placeholder="Best event!">' +
            '</div>' +
            '<div>' +
            '   <textarea class="new_record_text" class="form-control" placeholder="Event description"></textarea>' +
            '</div>' +
            '<div>' +
                '<button class="new_record_save_btn">Save!</button>' +
            '</div>' +
        '</div>'
}