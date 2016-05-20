require('Singleton');

var ResorcesDispatcher = {
    NewRecord:
        {
            data:
            {
                'Label_placeholder': 'Label',
                'Text_placeholder': 'Type your best day event here!'
            },
            GetResource: function (res) {
                var r = this.data[res];
                if (r == undefined) {
                    console.error('Resource "' + res + '" not found!');
                    return res;
                }
                return r;
            }
        }
};

var NewRecord = React.createClass
({
    recordsManager: new RecordsManger(),
    getInitialState: function () {
        return {
            Label: '',
            Text: ''
        };
    },
    handleSubmit: function (e) {
        e.preventDefault();
        this.recordsManager.AddRecord(this.state, function () {
            this.setState(this.getInitialState());
        }.bind(this));
        //$.ajax(
        //{
        //    url: '/api/rest/PostRecord',
        //    dataType: 'json',
        //    type: 'POST',
        //    data: JSON.stringify(this.state),
        //    contentType: 'application/json',
        //    success: function (newRecord) {
        //        //this.props.onFormSubmit(newRecord);
        //        this.setState(this.getInitialState());
        //    }.bind(this),
        //    error: function (xhr, status, err) {
        //        console.error(this.props.url, status, err.toString());
        //    }.bind(this)
        //});
    },
    handleTextChange: function (e) {
        this.setState({ Text: e.target.value });
    },
    handleLabelChange: function (e) {
        e.preventDefault();
        this.setState({ Label: e.target.value });
    },
    render: function () {
        return (
            <form className="add_new_record row" onSubmit={this.handleSubmit}>
                <div className="col-md-2"></div>
                <div className="col-md-8">
                    <input type="text" placeholder={ResorcesDispatcher.NewRecord.GetResource('Label_placeholder')}
                           value={this.state.Label}
                           className="form-control"
                           onChange={this.handleLabelChange} />
                    <textarea placeholder={ResorcesDispatcher.NewRecord.GetResource('Text_placeholder')}
                              value={this.state.Text}
                              className="form-control"
                              rows="10"
                              cols="10"
                              onChange={this.handleTextChange} />
                    <input type="submit" className="btn btn-sm btn-success" value="Post" />
                </div>
                <div className="col-md-2"></div>
            </form>
        );
    }
});

ReactDOM.render
(
          <NewRecord />,
          document.getElementById('new_record')
);