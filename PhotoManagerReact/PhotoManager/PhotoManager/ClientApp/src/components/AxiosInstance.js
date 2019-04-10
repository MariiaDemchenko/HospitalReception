import axios from 'axios';
import promise from 'promise';

var axiosInstance = axios.create();

axiosInstance.interceptors.request.use(config => {
    var accessToken = localStorage.getItem('token')
    if (accessToken) {
        config.headers.authorization = 'Bearer ' + accessToken;
    }
    return config;
},
    error => {
        return promise.reject(error);
    });

axiosInstance.interceptors.response.use(response => {
    return response;
}, error => {
    if (401 === error.response.status) {
        window.location = '/login'
    } else {
        return Promise.reject(error);
    }
});

export default axiosInstance;