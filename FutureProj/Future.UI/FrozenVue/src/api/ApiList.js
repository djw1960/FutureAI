import axios from 'axios'
//使用此方式处理跨域问题
const querystring = require('querystring');
export const getdataPost=params=>{
    // Send a POST request
    return axios({
        method: 'post',
        url: 'http://api.stroll.site/api/m',
        data: querystring.stringify(params),
        responseType:'json'
      });
};
export const getdataGet=params=>{
    // Send a get request
    return axios({
        method: 'get',
        url: 'http://api.stroll.site/api/m',
        data: querystring.stringify(params),
        responseType:'json'
      });
};