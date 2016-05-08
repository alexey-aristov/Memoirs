

var PostsList = React.createClass(
{
    loadFromServer: function () 
    {
        $.ajax({
            url: '/api/rest/GetRecords',
            dataType: 'json',
            cache: false,
            success: function (data1) {
                this.setState({ data: data1 });
            }.bind(this),
            error: function (xhr, status, err) {
                console.error(this.props.url, status, err.toString());
            }.bind(this)
        });
    },
    componentDidMount: function() {
        this.loadFromServer();
    },
    getInitialState: function () {
        return { data: [] };
    },
    render: function () {
        var records = this.state.data.map(function (record) {
            return (
                <Record key={record.Id} />
            );
        });
        return (<div>{records}</div>);
    }
});
var Record = React.createClass({
    render:function() {
        return (<div>tt</div>);
    }
});

ReactDOM.render(
          <PostsList />,
          document.getElementById('prev_records')
);