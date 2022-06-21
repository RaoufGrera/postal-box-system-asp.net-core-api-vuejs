import {
    option,
    method,
    handleResponse,
    authHeader
} from '../_helpers';


const ROOT_API = process.env.VUE_APP_ROOT_API;

export const onlineService = {

  
    getMyBoxs


};
function getMyBoxs() {
    const requestOptions = {
        method: "GET",
        headers: authHeader()

    };
        return fetch(`${ROOT_API}/Box/mybox`, requestOptions).then(handleResponse);

      
}
