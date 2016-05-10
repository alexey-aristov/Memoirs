

var PostsList = React.createClass(
{
    recordsManager: new RecordsManger(),
    loadFromServer: function () {
        this.recordsManager.GetRecords(function(data1) {
            this.setState({ data: data1 });
        }.bind(this));
        //$.ajax({
        //    url: '/api/rest/GetRecords',
        //    dataType: 'json',
        //    cache: false,
        //    success: function (data1) {
        //        this.setState({ data: data1 });
        //    }.bind(this),
        //    error: function (xhr, status, err) {
        //        console.error(this.props.url, status, err.toString());
        //    }.bind(this)
        //});
    },
    componentDidMount: function () {
        this.loadFromServer();
    },
    getInitialState: function () {
        return { data: [] };
    },
    render: function () {
        var records = this.state.data.map(function (record) {
            return (
                    <Record key={record.Id} record={record} />
                );
        });
        return (
            <div className="row">
                <div className="col-md-3"></div>
                <div className="list-group col-md-6">
                    {records}
                </div>
                <div className="col-md-3"></div>
            </div>);
    }
});
var Record = React.createClass({
    render: function () {
        return (
        <div className="list-group-item">
            <h4 className="list-group-item-heading">
                {this.props.record.Label}
            </h4>
            <p className="list-group-item-text">
                {this.props.record.Text}
            </p>
        </div>);
    }
});

ReactDOM.render(
          <PostsList />,
          document.getElementById('prev_records')
);