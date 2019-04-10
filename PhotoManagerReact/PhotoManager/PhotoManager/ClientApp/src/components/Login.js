import axios from './AxiosInstance';
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

        if (this.props.loginRequest) {
            var userViewModel = {
                email: this.props.email,
                password: this.props.password
            }
            const onLoginSuccess = this.props.onLoginSuccess
            const onLoginError = this.props.onLoginError
            axios.post(`api/Users/Login`, userViewModel)
                .then(function (response) {
                    if (response.status === 200) {
                        localStorage.setItem('token', response.data.token)
                        localStorage.setItem('user', response.data.name)
                        onLoginSuccess()
                    }
                })
                .catch(function (error) {
                    if (error.response && error.response.data) {
                        onLoginError(error.response.data)
                    }
                });
        }
        if (this.props.loginSuccess) {
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
            <h2>Sign in</h2>
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

            <button className="btn btn-primary" onClick={this.props.onLoginRequest}>Sign in</button>
        </div>)
    }
}

export default connect(
    state => state.login,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(Login);
