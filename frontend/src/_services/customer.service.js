import {
    option,
    method,
    handleResponse,
    authHeader
} from '../_helpers';


const ROOT_API = process.env.VUE_APP_ROOT_API;

export const customerService = {

    getAll,
    editCustomer,
    getAllCustomerJobs,
    getAllCustomerIdentity,
    getAllCustomerType,
    getAllTypeBox,
    getAllTypeNote,
    getAllBoxOfficeNotUsed,
    postCheckRent,
    saveBox,
    getAllPayType,
    getAllNote,
    getBoxRent,
    reSaveGetData,
    sendNote,
    getBillID,
    saveBoxRe,
    deleteBills,
    getAllNoteSend,
    getAllBooked,
    getAllOffice,
    getAllRegion,
    getAllBoxOfficeNotUsedById,
    getRandomBox,
    saveBoxCustomer,
    getAllNoteSendCustomer,
    getCustomerData,
    OkBills,
    createboxs,
    deleteboxs,
    updateboxs,
    getInfoBoxbyOffice,
    getAllbox,

};

function getAllbox() {
    const requestOptions = {
        method: 'GET',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        }
    };
    return fetch(`${ROOT_API}/box/allboxoffice`, requestOptions).then(handleResponse);
}

function getInfoBoxbyOffice() {
    const requestOptions = {
        method: "GET",

        headers: authHeader()

    }

    return fetch(`${ROOT_API}/main/boxs`, requestOptions).then(handleResponse);
}

function getCustomerData() {
    const requestOptions = {
        method: "GET",
        headers: authHeader()

    }

    return fetch(`${ROOT_API}/box/data`, requestOptions).then(handleResponse);
}

function getAllBooked() {
    const requestOptions = {
        method: "GET",
        headers: authHeader()

    }

    return fetch(`${ROOT_API}/box/booked`, requestOptions).then(handleResponse);
}

function getRandomBox() {

    const requestOptions = {
        method: "GET",
        headers: authHeader()

    }

    return fetch(`${ROOT_API}/box/randombox`, requestOptions).then(handleResponse);
}

function getAllNoteSend() {
    const requestOptions = {
        method: "GET",
        headers: authHeader()

    }

    return fetch(`${ROOT_API}/box/notehistory`, requestOptions).then(handleResponse);
}

function getAllNoteSendCustomer() {
    const requestOptions = {
        method: "GET",
        headers: authHeader()

    }

    return fetch(`${ROOT_API}/box/notehistorycustomer`, requestOptions).then(handleResponse);
}

function deleteBills(userId) {
    const requestOptions = option(method.DELETE, null, true);
    return fetch(`${ROOT_API}/box/${userId}`, requestOptions).then(handleResponse);
}

function OkBills(userId) {

    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        //body: JSON.stringify(data)
    };
    //  const requestOptions = option(method.POST, null, true);
    return fetch(`${ROOT_API}/box/${userId}`, requestOptions).then(handleResponse);
}

function addUser(data) {
    const requestOptions = option(method.POST, data);
    return fetch(`${ROOT_API}/Auth/addUser`, requestOptions).then(handleResponse);
}

function editCustomer(data) {
    const requestOptions = option(method.POST, data);
    return fetch(`${ROOT_API}/Customer/editCustomer`, requestOptions).then(handleResponse);
}

function getAll(pageId, name, boxnumber, phonenumber) {

    const requestOptions = {
        method: "GET",
        headers: authHeader()

    }
    return fetch(`${ROOT_API}/Customer/search?page=${pageId}&name=${name}&boxNumber=${boxnumber}&phoneNumber=${phonenumber}`, requestOptions).then(handleResponse);
}

function getAllCustomerJobs() {
    const requestOptions = option(method.GET);
    return fetch(`${ROOT_API}/Customer/customerjobs`, requestOptions).then(handleResponse);
}

function getAllCustomerIdentity() {
    const requestOptions = option(method.GET);
    return fetch(`${ROOT_API}/Customer/customeridentity`, requestOptions).then(handleResponse);
}

function getAllCustomerType() {
    const requestOptions = option(method.GET);
    return fetch(`${ROOT_API}/Customer/customertype`, requestOptions).then(handleResponse);
}

function getAllRegion() {
    const requestOptions = option(method.GET);
    return fetch(`${ROOT_API}/Box/Region`, requestOptions).then(handleResponse);
}

function getAllOffice(typeid) {
    if (!typeid) {
        typeid = 0;
    }
    const requestOptions = option(method.GET);
    return fetch(`${ROOT_API}/Box/office?type=${typeid}`, requestOptions).then(handleResponse);
}

function getAllPayType() {
    const requestOptions = option(method.GET);
    return fetch(`${ROOT_API}/Customer/paytype`, requestOptions).then(handleResponse);
}

function getAllTypeNote() {
    const requestOptions = option(method.GET);
    return fetch(`${ROOT_API}/box/note`, requestOptions).then(handleResponse);
}

function getAllBoxOfficeNotUsed(typeid) {
    const requestOptions = {
        method: "GET",
        headers: authHeader()

    }
    if (!typeid) {
        typeid = 0;
    }
    return fetch(`${ROOT_API}/box/officebox?type=${typeid}`, requestOptions).then(handleResponse);
}


function getAllBoxOfficeNotUsedById(typeid, officeid) {
    const requestOptions = {
        method: "GET",
    }
    if (!typeid) {
        typeid = 0;
    }
    if (!officeid) {
        officeid = 0;
    }
    return fetch(`${ROOT_API}/box/officeboxbyid?type=${typeid}&office=${officeid}`, requestOptions).then(handleResponse);
}

function getAllTypeBox() {
    const requestOptions = option(method.GET);
    return fetch(`${ROOT_API}/box/typebox`, requestOptions).then(handleResponse);
}

function postCheckRent(data) {
    const requestOptions = option(method.POST, data);
    return fetch(`${ROOT_API}/box/checkcost`, requestOptions).then(handleResponse);
}

function saveBox(data) {

    var hed = authHeader();

    var d = hed.Authorization;
    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    return fetch(`${ROOT_API}/box/save`, requestOptions).then(handleResponse);
}

function saveBoxCustomer(data) {

    var hed = authHeader();

    var d = hed.Authorization;
    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    return fetch(`${ROOT_API}/box/saveboxcustomer`, requestOptions).then(handleResponse);
}

function saveBoxRe(data, id) {

    var hed = authHeader();

    var d = hed.Authorization;
    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    return fetch(`${ROOT_API}/box/resave/${id}`, requestOptions).then(handleResponse);
}

function getAllBills() {
    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };
    return fetch(`${ROOT_API}/box/bills`, requestOptions).then(handleResponse);
}

function getBoxRent() {
    const requestOptions = {
        method: 'GET',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        }
    };
    return fetch(`${ROOT_API}/box/boxrent`, requestOptions).then(handleResponse);
}

function getAllNote() {
    const requestOptions = option(method.GET);
    return fetch(`${ROOT_API}/Customer/note`, requestOptions).then(handleResponse);
}

function createboxs(data) {

    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };
    return fetch(`${ROOT_API}/box/createboxs`, requestOptions).then(handleResponse);
}

function updateboxs(data) {

    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };
    return fetch(`${ROOT_API}/box/updateboxs`, requestOptions).then(handleResponse);
}

function deleteboxs(data) {

    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };
    return fetch(`${ROOT_API}/box/deleteboxs`, requestOptions).then(handleResponse);
}

function sendNote(data) {

    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };
    return fetch(`${ROOT_API}/box/send`, requestOptions).then(handleResponse);
}

function reSaveGetData(id) {

    const requestOptions = option(method.GET);

    return fetch(`${ROOT_API}/box/resave/${id}`, requestOptions).then(handleResponse);
}


function getBillID() {

    const requestOptions = {
        method: 'GET',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        }
    };
    return fetch(`${ROOT_API}/box/billid`, requestOptions).then(handleResponse);
}