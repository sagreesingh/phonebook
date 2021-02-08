import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { bookid: 0, bookname: "", contacts: [], loading: true, search: "", msg: "" };
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Home.renderContactsTable(this.state.contacts);

    return (
      <div>
        <div className="row">
          <div className="col">
            <h1 id="tabelLabel" >{this.state.bookname}</h1>
            <p aria-live="polite">{this.state.msg}</p>
          </div>
        </div>
        <div className="row">
          <div className="col">
            {contents}
          </div>
        </div>
        <div className="row">
          <div className="col text-right">
            <label className="formcontrol">
              Search:
              <input type="text" name="search" value={this.state.search} onChange={this.onChangedSearch.bind(this)} />
            </label>
          </div>
          <div className="col">
            <button className="btn btn-primary formcontrol" onClick={this.onLoadBook.bind(this)}>Search</button>
          </div>
        </div>
      </div>
    );
  }

  componentDidMount() {
    this.GetBook();
  }

  static renderContactsTable(contacts) {
    return (
      <div>
        <table className='table table-striped' aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Name</th>
              <th>Number</th>
            </tr>
          </thead>
          <tbody>
            {contacts.map(contact =>
              <tr key={contact.contactId}>
                <td>{contact.name}</td>
                <td>{contact.number}</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    );
  }

  onChangedSearch(event) { this.setState({ search: event.target.value }); }

  async GetBook() {
    await fetch('book/getlist', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json', },
      body: JSON.stringify({
      }),
    })
      .then(response => response.json())
      .then(data => {
        this.setState({ bookid: data[0].bookId, bookname: data[0].name });
        this.onLoadBook();  
      })
      .catch((error) => {
        this.setState({
          loading: false, msg: "An error retrieving book. Please try again."
        });
      });
  }

  async onLoadBook() {
    await fetch('contact/getlist', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json', },
      body: JSON.stringify({
        "BookId": this.state.bookid,
        "Name": this.state.search,
      }),
    })
      .then(response => response.json())
      .then(data => {
        this.setState({ contacts: data, loading: false, search: "", msg: "" });
      })
      .catch((error) => {
        this.setState({ contacts: [], loading: false, search: "", msg: "An error occurred when retrieving the contacts. Please try again."
        });
      });
  }
}
