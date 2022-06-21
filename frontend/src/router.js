 import Vue from "vue";
 import Router from "vue-router";
 import MainPage from "./components/MainPage";
 import LogoutUser from "./components/LogoutUser";

 import Logout from "./components/Logout";
 import CustomerPage from "./components/customer/CustomerPage";
 import BookedBox from "./components/customer/BookedBox";

 import UsersPage from "./components/user/UsersPage1";
 import BoxPage from "./components/boxes/BoxPage";

 import LoginPage from "./components/login/LoginPage";

 import Help from "./components/public/Help";


 import NotificationPage from "./components/notification/NotificationPage";
 import NotificationSendPage from "./components/notification/NotificationSendPage";

 import PublicRegisterPage from "./components/public/RegisterPage";
 import PublicLoginPage from "./components/public/LoginPage";
 import PublicHomePage from "./components/public/HomePage";
 import PublicDashBoardPage from "./components/public/DashBoardPage";
 import PublicBoxCustomerPage from "./components/public/BoxCustomerPage";

 import Customer from "./components/public/Customer";

 import CreateBox from "./components/AddBox";

 import PublicBoxCustomer from "./components/public/BoxCustomer";
 import NotificationCustomer from "./components/public/NotificationCustomer";


 import MapPage from "./components/public/MapPage";

 //import NavbarPage from '../layout/NavbarPage.vue'

 Vue.use(Router);

 export const router = new Router({
     mode: "history",
     base: process.env.BASE_URL,

     routes: [{
             path: "/admin/dashboard",
             component: MainPage,
             name: ""
         },
         {
             path: "/logout",
             component: LogoutUser,
             name: "logoutt"
         },
         {
             path: "/admin/logout",
             component: Logout,
             name: "logout"
         },

         {
             path: "/admin/users",
             component: UsersPage,
             name: "users"
         },
         {
             path: "/admin/login",
             component: LoginPage,
             name: "loginAdmin"
         },
         {
             path: "/login",
             component: PublicLoginPage,
             name: "login"
         },
         {
             path: "/register",
             component: PublicRegisterPage,
             name: "register"
         },
         {
             path: "/home",
             component: PublicHomePage,
             name: "home"
         },
         {
             path: "/support",
             component: Help,
             name: "support"
         },

         {
             path: "/admin/help",
             component: Help,
             name: "help"
         },


         {
             path: "/user",
             component: Customer,
             name: "user"
         },
         {
             path: "/notification",
             component: NotificationCustomer,
             name: "note"
         },

         {
             path: "/admin/customer",
             component: CustomerPage,
             name: "customer"
         },
         {
             path: "/admin/box/:id",
             component: BoxPage,
             name: 'box'
         },
         {
             path: "/admin/box",
             component: BoxPage,
             name: 'box'
         },
         {
             path: "/admin/booked",
             component: BookedBox,
             name: 'booked'
         },


         {
             path: "/admin/boxonline",
             component: BoxPage,
             name: 'box'
         },

         {
             path: "/admin/createbox",
             component: CreateBox,
             name: "createbox",
         },

         {
             path: "/admin/notification",
             component: NotificationPage,
             name: "notification"
         },
         {
             path: "/admin/notification/history",
             component: NotificationSendPage,
             name: "notificationHistory"
         },





         {
             path: "/dashboard",
             component: PublicDashBoardPage,
             name: "dashboard"
         },

         {
             path: "/map",
             component: MapPage,
             name: "map"
         },

         {
             path: "/mybox",
             component: PublicBoxCustomerPage,
             name: "mybox"
         },

         {
             path: "/addbox",
             component: PublicBoxCustomer,
             name: "addbox"
         },


         {
             path: "/",
             component: PublicHomePage,
             name: "/x"
         },

         // otherwise redirect to home
         {
             path: "*",
             redirect: "/"
         }
     ]


 });

 router.beforeEach((to, from, next) => {
     // redirect to login page if not logged in and trying to access a restricted page
     const publicPages = ["register", "login", "loginAdmin"];
     const AdminPages = ["", "logout", "users", "customer", "box", "notification", "notificationHistory", "booked"];
     const OfficePages = ["createbox", "users", "", "logout", "customer", "box", "notification", "notificationHistory", "booked", "help"];
     const UserPages = ["home", "", "logoutt", "mybox", "addbox", "map", "note", "user", "support"];

     const authAdminRequired = AdminPages.includes(to.name);
     const authOfficeRequired = OfficePages.includes(to.name);
     const authUserRequired = UserPages.includes(to.name);

     const authRequired = !publicPages.includes(to.name);
     const loggedIn = localStorage.getItem("user");
     if (loggedIn) {
         const userData = JSON.parse(loggedIn);
         const isAdmin = userData.isAdmin;
         const isCustomer = userData.isCustomer;
         console.log(loggedIn);
         console.log("Auth Router " + authOfficeRequired);
         console.log("isAdmin Router " + authOfficeRequired);
         console.log("isCustomer Router " + authUserRequired);



         if (isCustomer && !authUserRequired) {


             return next("/logout");
             console.log("Iam In Is Customer Check ");

             console.log(" USER Services Logout ")
             // remove user from local storage to log user out
             localStorage.removeItem('state');
             localStorage.removeItem('user');

             localStorage.removeItem('customer');


         }
         if (!isAdmin && !authOfficeRequired && !isCustomer) {
             console.log(!isAdmin && !authOfficeRequired);
             return next("/admin/logout");
         }

         if (isAdmin && !authAdminRequired && !isCustomer) {
             return next("/admin/logout");
         }
     }



     if (authRequired && !loggedIn) {


         return next("/login");
     }

     next();
 });