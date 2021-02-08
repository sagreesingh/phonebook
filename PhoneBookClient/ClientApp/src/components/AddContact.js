import React, { Component } from 'react';

export class AddContact extends Component {
  static displayName = AddContact.name;

  constructor(props) {
    super(props);
    this.state = {
      bookid: 1, 
      msg: "",
      name: "",
      number: ""
    }   
  }

  render() {
    return (
      <div>
        <h1>Add Contact</h1>
        <p aria-live="polite">{this.state.msg}</p>
        <div className="row formcontrol">
          <div className="col-3">
            <label>Name</label>
          </div>
          <div className="col">
            <input type="text" name="name" value={this.state.name} onChange={this.onChangedName.bind(this)} />
          </div>
        </div>
        <div className="row formcontrol">
          <div className="col-3">
            <label>Number</label>
          </div>
          <div className="col">
            <input type="text" name="number" value={this.state.number} onChange={this.onChangedNumber.bind(this)} />
          </div>
        </div>
        <div className="row">
          <div className="col-3">
          </div>
          <div className="col">
            <button className="btn btn-primary" onClick={this.onSubmit.bind(this)}>Add</button>
          </div>
        </div>
      </div>
    );
  }

  onChangedName(event) { this.setState({ name: event.target.value }); }
  onChangedNumber(event) { this.setState({ number: event.target.value }); }

  async onSubmit(event) {
    event.preventDefault();

    await fetch('contact/add', {
      method: 'POST', 
      headers: { 'Content-Type': 'application/json', },
      body: JSON.stringify({
        "BookId": this.state.bookid,
        "Name": this.state.name,
        "Number": this.state.number
      }),
    })
      .then(response => {
        this.setState({ msg: "Contact added.", defaultName: "", defaultNumber: "" });
      })
      .catch((error) => {
        this.setState({ msg: "An error occurred when adding the contact. Please try again." });
      });
  }
}
