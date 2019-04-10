const handleEmailInputType = 'HANDLE_EMAIL_INPUT'
const handlePasswordInputType = 'HANDLE_PASSWORD_INPUT'
const handleNameInputType = 'HANDLE_NAME_INPUT'
const emailOnBlurType = 'EMAIL_BLUR'
const passwordOnBlurType = 'PASSWORD_BLUR'
const nameOnBlurType = 'NAME_BLUR'
const loginRequestType = 'LOGIN_REQUEST'
const loginSuccessType = 'LOGIN_SUCCESS'
const loginErrorType = 'LOGIN_ERROR'
const registerRequestType = 'REGISTER_REQUEST'
const registerSuccessType = 'REGISTER_SUCCESS'
const registerErrorType = 'REGISTER_ERROR'
const beforeUnmountType = 'BEFORE_UNMOUNT'

const initialState = {
    email: "",
    password: "",
    name: "",
    formErrors: { email: '', password: '', name: '' },
    formValid: false,
    validateEmail: false,
    validatePassword: false,
    validateName: false,
    loginRequest: false,
    registerRequest: false,
    loginSuccess: false,
    registerSuccess: false,
    alertMessage: null,
    alertId: 0,
    alertType: "",
};

export const actionCreators = {
    handleUserEmailInput: (e) => ({ type: handleEmailInputType, e }),
    handlePasswordInput: (e) => ({ type: handlePasswordInputType, e }),
    handleNameInput: (e) => ({ type: handleNameInputType, e }),
    emailOnBlur: () => ({ type: emailOnBlurType }),
    passwordOnBlur: () => ({ type: passwordOnBlurType }),
    nameOnBlur: () => ({ type: nameOnBlurType }),
    onLoginRequest: () => ({ type: loginRequestType }),
    onLoginSuccess: () => ({ type: loginSuccessType }),
    onLoginError: (message) => ({ type: loginErrorType, message }),
    onRegisterRequest: () => ({ type: registerRequestType }),
    onRegisterSuccess: () => ({ type: registerSuccessType }),
    onRegisterError: (message) => ({ type: registerErrorType, message }),
    onBeforeUnmount: () => ({ type: beforeUnmountType })
}

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === handleEmailInputType) {
        const value = action.e.target.value;
        return {
            ...state,
            email: action.e.target.value,
            formErrors: {
                ...state.formErrors, email: state.validateEmail ? (value.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i) ? "" : "Please input valid email") : ""
            }
        }
    }

    if (action.type === handlePasswordInputType) {
        const value = action.e.target.value;
        return {
            ...state,
            password: action.e.target.value,
            formErrors: { ...state.formErrors, password: state.validatePassword ? (value.length > 5 ? "" : "Password length should be more than 6 symbols") : "" }
        }
    }

    if (action.type === handleNameInputType) {
        const value = action.e.target.value;
        return {
            ...state,
            name: action.e.target.value,
            formErrors: { ...state.formErrors, name: state.validateName ? (value.length > 0 ? "" : "Name should not be empty") : "" }
        }
    }

    if (action.type === emailOnBlurType) {
        if (state.loginRequest) {
            return state;
        }

        return {
            ...state,
            validateEmail: true,
            formErrors: { ...state.formErrors, email: state.email.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i) ? "" : "Please input valid email" }
        }
    }

    if (action.type === passwordOnBlurType) {
        if (state.loginRequest) {
            return state;
        }

        return {
            ...state,
            validatePassword: true,
            formErrors: { ...state.formErrors, password: state.password.length > 5 ? "" : "Password length should be more than 6 symbols" }
        }
    }

    if (action.type === nameOnBlurType) {
        if (state.registerRequest) {
            return state;
        }

        return {
            ...state,
            validateName: true,
            formErrors: { ...state.formErrors, name: state.name.length > 0 ? "" : "Name should not be empty" }
        }
    }

    if (action.type === registerRequestType) {
        const formErrors = {
            ...state.formErrors,
            password: state.password.length > 5 ? "" : "Password length should be more than 6 symbols",
            email: state.email.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i) ? "" : "Please input valid email",
            name: state.name.length > 0 ? "" : "Name should not be empty"
        }
        if (formErrors.password.length > 0 || formErrors.email.length > 0 || formErrors.name.length > 0) {
            return {
                ...state,
                alertMessage: "The user data is invalid",
                alertId: state.alertId + 1,
                formErrors: formErrors,
                alertType: "warning",
                registerRequest: false
            }
        }
        return {
            ...state,
            registerRequest: true
        }
    }

    if (action.type === loginRequestType) {
        const formErrors = {
            ...state.formErrors,
            password: state.password.length > 5 ? "" : "Password length should be more than 6 symbols",
            email: state.email.match(/^([\w.%+-]+)@([\w-]+\.)+([\w]{2,})$/i) ? "" : "Please input valid email"
        }
        if (formErrors.password.length > 0 || formErrors.email.length > 0) {
            return {
                ...state,
                alertMessage: "The user data is invalid",
                alertId: state.alertId + 1,
                formErrors: formErrors,
                alertType: "warning",
                loginRequest: false
            }
        }
        return {
            ...state,
            loginRequest: true
        }
    }

    if (action.type === loginSuccessType) {
        return {
            ...state,
            loginRequest: false,
            loginSuccess: true
        }
    }

    if (action.type === registerErrorType) {
        return {
            ...state,
            registerSuccess: false,
            registerRequest: false,
            alertMessage: action.message,
            alertId: state.alertId + 1,
            alertType: "danger"
        }
    }

    if (action.type === loginErrorType) {
        return {
            ...state,
            loginSuccess: false,
            loginRequest: false,
            alertMessage: action.message,
            alertId: state.alertId + 1,
            alertType: "danger"
        }
    }

    
    if (action.type === registerSuccessType) {
        return {
            ...state,
            registerRequest: false,
            registerSuccess: true
        }
    }

    if (action.type === beforeUnmountType) {
        return {
            ...state,
            registerRequest: false,
            registerSuccess: false,
            loginSuccess: false,
            loginError: false,
            alertMessage: null,
            alertId: 0,
            email: "",
            password: "",
            validateEmail: false,
            validatePassword: false,
            validateName: false,
            name: "",
        }
    }

    return state;
}