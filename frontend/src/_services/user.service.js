//import config from 'config';
import {  option ,method,handleResponse} from '../_helpers';
const ROOT_API = process.env.VUE_APP_ROOT_API;

 export const userService = {
    login,
    loginFacebook,
    logout,
    register,
    getAllOffice,
    getAll,
    getById,
    update,
    addUser,
    editUser,
    deleteUser,
    authPhone,
    phoneLogin,
    authCheck,
    delete: _delete
};

function login(data) {
    const requestOptions = option(method.POST, data, false);

    return fetch(`${ROOT_API}/Auth/login`, requestOptions)
        .then(handleResponse)
        .then(result => {
            if (result.token) {
                localStorage.setItem('user', JSON.stringify(result));
            }
            return result;
        });
}

function phoneLogin(data) {
    const requestOptions = option(method.POST, data, false);

    return fetch(`${ROOT_API}/Auth/phoneLogin`, requestOptions).then(handleResponse);
}

function loginFacebook(data) {
    const requestOptions = option(method.POST,data,false)

    return fetch(`${ROOT_API}/Auth/facebookAuth`, requestOptions)
        .then(handleResponse)
        .then(result => {
            if (result.token) {
                localStorage.setItem('user', JSON.stringify(result));
               

            }

            return result;
        });
}

function 
logout() {

    console.log(" USER Services Logout ")
    // remove user from local storage to log user out
    localStorage.removeItem('state');
    localStorage.removeItem('user');

    localStorage.removeItem('customer');
    

}

function register(user) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    };

    return fetch(`${ROOT_API}/Auth/register`, requestOptions).then(handleResponse);
}

function addUser(data) {
    const requestOptions = option(method.POST, data);
    return fetch(`${ROOT_API}/Auth/addUser`, requestOptions).then(handleResponse);
}

function editUser(data){
    const requestOptions = option(method.POST,data);
    return fetch(`${ROOT_API}/Auth/editUser`,requestOptions).then(handleResponse);
}

function authPhone(data){
    const requestOptions = option(method.POST,data);
    return fetch(`${ROOT_API}/Auth/phoneAuth`,requestOptions).then(handleResponse);
}

function authCheck(data){
    const requestOptions = option(method.POST,data);
    return fetch(`${ROOT_API}/Auth/phoneCheck`,requestOptions).then(handleResponse);
}

function deleteUser(userId){
    const requestOptions = option(method.DELETE,null,true);
    return fetch(`${ROOT_API}/Auth/deleteUser/${userId}`,requestOptions).then(handleResponse);
}
function getAll() {
    return fetch(`${ROOT_API}/Auth/users`,option(method.GET)).then(handleResponse);
}

function getAllOffice() {
    const requestOptions = option(method.GET);
    return fetch(`${ROOT_API}/Auth/offices`, requestOptions).then(handleResponse);
}


function getById(id) {
    const requestOptions = request("GET", null, true)
    return fetch(`${ROOT_API}/users/${id}`, requestOptions).then(handleResponse);
}

function update(user) {
    const requestOptions = {
        method: 'PUT',
        headers: { ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    };

    return fetch(`${ROOT_API}/users/${user.id}`, requestOptions).then(handleResponse);
}

// prefixed function name with underscore because delete is a reserved word in javascript
function _delete(id) {
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };

    return fetch(`${ROOT_API}/users/${id}`, requestOptions).then(handleResponse);
}