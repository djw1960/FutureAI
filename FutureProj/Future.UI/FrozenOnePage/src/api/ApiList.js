
import axios from 'axios'

axios.interceptors.response.use(function (response) {
    // 对响应数据做处理
    if (response.data.code==-4) {
        window.location.pathname="/";
    }
    return response;
}, function (error) {
    // 对响应错误做处理
    return Promise.reject(error);
});
//使用此方式处理跨域问题
const querystring = require('querystring');
export const getdataPost=params=>{
    // Send a POST request
    return axios({
        method: 'post',
        url: 'https://api.stroll.site/api/FutureCenter',
        data: querystring.stringify(params),
        responseType:'json'
      });
};
export const getdataGet=params=>{
    // Send a get request
    return axios({
        method: 'get',
        url: 'https://api.stroll.site/api/FutureCenter',
        data: querystring.stringify(params),
        responseType:'json'
      });
};