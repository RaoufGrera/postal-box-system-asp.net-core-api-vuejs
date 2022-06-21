import {
    authHeader
} from "./auth-header";
export const requestServices = {
    request,
    handleResponse,
    methodType,
    GET: "GET",
    POST: "POST"
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                // auto logout if 401 response returned from api
                logout();
                location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}

function request(type, data, isAuth = false) {
    const auth = (isAuth) ? authHeader() : null;
    const result = {
        method: type,
        headers: {
            'Content-Type': 'application/json',
            auth
        }
    }
    if (type != "GET") {
        result.body = JSON.stringify(data);

    }
    return result;
}


const methodType = {
    GET: "GET",
    POST: "POST"
};