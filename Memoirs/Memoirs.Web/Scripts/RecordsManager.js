var RecordsManger = Singleton(function() {
    this.GetRecords = function(successCallback, errorCallback) {
        $.ajax({
            url: '/api/rest/GetRecords',
            dataType: 'json',
            cache: false,
            success: function(data1) {
                successCallback(data1);
            },
            error: function(xhr, status, err) {
                console.error("GetRecords fail", status, err.toString());
                errorCallback();
            }
        });
    };
    
    this.AddRecord = function(record, successCallback, errorCallback)
    {
        $.ajax(
        {
            url: '/api/rest/PostRecord',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify(record),
            contentType: 'application/json',
            success: function (newRecord) {
                successCallback(newRecord);
            },
            error: function (xhr, status, err) {
                console.error("AddRecord fail", status, err.toString());
                errorCallback();
            }
        });
    };
});