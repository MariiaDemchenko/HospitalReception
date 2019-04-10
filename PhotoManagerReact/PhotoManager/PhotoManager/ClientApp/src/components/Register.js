import axios from 'axios';
import React, { Component } from 'react';
import "react-datepicker/dist/react-datepicker.css";
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { bindActionCreators } from 'redux';
import { actionCreators } from '../store/Login';
import NotifierGenerator from './Notification';

class Login extends Component {

    componentWillUnmount() {
        this.props.onBeforeUnmount();
    }

    render() {
        if (this.props.registerRequest) {
            var userViewModel = {
                email: this.props.email,
                password: this.props.password,
                name: this.props.name
            }
            const onRegisterSuccess = this.props.onRegisterSuccess
            const onRegisterError = this.props.onRegisterError

            axios.post(`api/Users/Register`, userViewModel)
                .then(function (response) {
                    if (response.status === 200) {
                        localStorage.setItem('token', response.data.token)
                        localStorage.setItem('user', response.data.name)
                        onRegisterSuccess()
                    }
                })
                .catch(function (error) {
                    if (error.response && error.response.data) {
                        onRegisterError(error.response.data)
                    }
                });
        }
        if (this.props.registerSuccess) {
            return (<Redirect push to={{ pathname: `/albums` }} />)
        }
        if (localStorage.getItem("token")) {
            localStorage.removeItem("token");
            return (<Redirect push to={{ pathname: `/albums` }} />)
        }
        return (<div>
            <NotifierGenerator messageText={this.props.alertMessage}
                messageId={this.props.alertId}
                messageType={this.props.alertType} />
            <h2>Sign up</h2>
            <div className={`form-group`}>
                <label htmlFor="name" >Name</label>
                <input type="name"
                    className={`form-control col-md-5 ${this.props.formErrors.name.length === 0 ? '' : 'has-error'}`}
                    value={this.props.name}
                    onChange={this.props.handleNameInput}
                    onBlur={this.props.nameOnBlur}
                    name="name" />
                <label>{this.props.formErrors.name}</label>
            </div>
            <div className={`form-group`}>
                <label htmlFor="email" >Email address</label>
                <input type="email"
                    className={`form-control col-md-5 ${this.props.formErrors.email.length === 0 ? '' : 'has-error'}`}
                    value={this.props.email}
                    onChange={this.props.handleUserEmailInput}
                    onBlur={this.props.emailOnBlur}
                    name="emai" />
                <label>{this.props.formErrors.email}</label>
            </div>
            <div className="form-group">
                <label htmlFor="password" >Password</label>
                <input type="password"
                    className={`form-control col-md-5 ${this.props.formErrors.password.length === 0 ? '' : 'has-error'}`}
                    value={this.props.password}
                    onChange={this.props.handlePasswordInput}
                    onBlur={this.props.passwordOnBlur}
                    name=" password" />
                <label>{this.props.formErrors.password}</label>
            </div>

            <button className="btn btn-primary" onClick={this.props.onRegisterRequest}>Sign up</button>
        </div>)
    }
}

export default connect(
    state => state.login,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Login);
