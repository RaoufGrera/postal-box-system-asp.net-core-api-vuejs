import {
    userService
} from "../_services";
import { router} from '../router'


const user = JSON.parse(localStorage.getItem("user"));
const state = user ? { status: {  loggedIn: true }, user} : {status: {},user: null};

const actions = {
    login({ dispatch, commit }, data) {
        commit("loginRequest", data);

        userService.login(data)
            .then(
                user => {
                    commit("loginSuccess", user);
                    router.push("/admin/dashboard");
                },
                error => {
                    commit("loginFailure", error);
                    dispatch("alert/error", error, {
                        root: true
                    });
                }
            );
    },

    loginPhone({ dispatch, commit }, data) {
        commit("loginRequest", data);

        userService.login(data)
            .then(
                user => {
                    commit("loginSuccess", user);
                    localStorage.setItem('customer', 1);

                    router.push("/home");
                },
                error => {
                    commit("loginFailure", error);
                    dispatch("alert/error", error, {
                        root: true
                    });
                }
            );
    },
    loginFacebook({
        dispatch,
        commit
    }, {
        AccessToken
    }) {
        commit("loginRequest", { AccessToken});

        userService.loginFacebook(AccessToken)
            .then(
                user => {
                    commit("loginSuccess", user);
                    router.push("/admin/dashboard");
                },
                error => {
                    commit("loginFailure", error);
                    dispatch("alert/error", error, {
                        root: true
                    });
                }
            );
    },
    logout({
        commit
    }) {
        console.log(" Vuex Logout off");
        userService.logout();
        commit("logout");
        router.push("/admin/login");
    },
    logoutt({
        commit
    }) {
        console.log(" Vuex logoutUser off");

        userService.logout();
        commit("logoutt");
        router.push("/login");
    },
    addUser({  dispatch,commit},userData){
        userService.addUser(userData).then( userData => {
            
                setTimeout(() => {
                    // display success message after route change completes ss 
                    dispatch("alert/success", "Registration successful", {
                        root: true
                    });
                });
            },
            error => {
               // commit("registerFailure", error);
                dispatch("alert/error", error, {
                    root: true
                });
            }
        );
    },
    register({
        dispatch,
        commit
    }, user) {
        commit("registerRequest", user);

        userService.register(user)
            .then(
                user => {
                    commit("registerSuccess", user);
                    router.push("/admin/login");
                    setTimeout(() => {
                        // display success message after route change completes
                        dispatch("alert/success", "Registration successful", {
                            root: true
                        });
                    },2000);
                },
                error => {
                    commit("registerFailure", error);
                    dispatch("alert/error", error, {
                        root: true
                    });
                }
            );
    }
};

const mutations = {
    loginRequest(state, user) {
        state.status = {
            loggingIn: true
        };
        state.user = user;
    },
    loginSuccess(state, user) {
        state.status = {
            loggedIn: true
        };
        state.user = user;
    },
    loginFailure(state) {
        state.status = {};
        state.user = null;
    },
    logout(state) {
        state.status = {};
        state.user = null;
    },
    logoutt(state) {
        state.status = {};
        state.user = null;
    },
    registerRequest(state, user) {
        state.status = {
            registering: true
        };
    },
    registerSuccess(state, user) {
        state.status = {};
    },
    registerFailure(state, error) {
        state.status = {};
    }
};

export const account = {
    namespaced: true,
    state,
    actions,
    mutations
};