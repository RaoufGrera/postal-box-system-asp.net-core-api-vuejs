
<template>
  <div>
    <vue-navbar></vue-navbar>

    <div class="container-fluid">
      <div v-if="islogin" class="row">
        <div id="side" class="col-md-2">
          <vue-sidebar></vue-sidebar>
        </div>

        <div class="col-md-10">
          <div class="proot">
            <vue-alert></vue-alert>

            <router-view></router-view>
          </div>
        </div>
      </div>

      <div v-else>
        <vue-alert></vue-alert>
        <router-view></router-view>
      </div>
    </div>
    <br>
    <br>
    <br>
    <br>
    <br>
    <br>
    <div class="conteainer-fluid">
      <vue-footer></vue-footer>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import NavbarPage from "./components/layout/NavbarPage";
import SideBar from "./components/layout/SideBar";
import AlertBar from "./components/layout/Alert";
import Footer from "./components/layout/Footer";

export default {
  name: "app",
  components: {
    "vue-navbar": NavbarPage,
    "vue-footer": Footer,
    "vue-sidebar": SideBar,
    "vue-alert": AlertBar
  },
  computed: {
    islogin() {
      console.log(this.$store.state.account.status.loggedIn);
      return this.$store.state.account.status.loggedIn;
    }
  }
};
</script>


<style>
@import url("https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css");
@font-face {
  font-family: "JF Flat";
  src: url("assets/jf.woff") format("woff");
}
#side {
  /* border-left: 3px solid #176ac2;
    /* background: #176ac2; */
  padding: 1px;
  border-left: 2px solid #176ac296;
  padding-top: 40px;
}

body {
  text-align: right;
  /* background: #f1f1f1;*/
}
nav {
  background: #176ac2;
}
.navbar {
  border-bottom: 1px solid #cccccc;
}

/* Footer */
section {
  padding: 60px 0 30px 0;
}

section .section-title {
  text-align: center;
  color: #333;
  margin-bottom: 30px;
  text-transform: uppercase;
}
#footer {
  background: #3c3c3c !important;
}
#footer h5 {
  padding-right: 10px;
  border-right: 3px solid #eeeeee;
  padding-bottom: 6px;
  margin-bottom: 20px;
  color: #ffffff;
}
#footer a {
  color: #ffffff;
  text-decoration: none !important;
  background-color: transparent;
  -webkit-text-decoration-skip: objects;
}
#footer ul.social li {
  padding: 3px 0;
}
#footer ul.social li a i {
  margin-right: 5px;
  font-size: 25px;
}
#footer ul.social li:hover a i {
  font-size: 30px;
  margin-top: -10px;
}
#footer ul.social li a,
#footer ul.quick-links li a {
  color: #ffffff;
}
#footer ul.social li a:hover {
  color: #eeeeee;
}
#footer ul.quick-links li {
  padding: 3px 0;
}
#footer ul.quick-links li:hover {
  padding: 3px 0;
  margin-left: 5px;
  font-weight: 700;
}
#footer ul.quick-links li a i {
  margin-right: 5px;
}
#footer ul.quick-links li:hover a i {
  font-weight: 700;
}

@media (max-width: 767px) {
  #footer h5 {
    padding-right: 0;
    border-right: transparent;
    padding-bottom: 0px;
    margin-bottom: 10px;
  }
}

.proot {
  background: #fff;
  padding: 25px;
  padding-top: 37px;
  border-radius: 8px;
  /* border: 1px solid #ddd; */
}

.m10 > a {
  padding: 15px;
}
.btn-default:active,
.btn-default.active,
.open > .dropdown-toggle.btn-default {
  background-image: none;
}
.btn-default:active,
.btn-default.active,
.open > .dropdown-toggle.btn-default {
  color: #333;
  background-color: #e6e6e6;
  border-color: #adadad;
}
.btn-default:hover {
  color: #333;
  background-color: #e6e6e6;
  border-color: #adadad;
}
.btn-default:focus,
.btn-default.focus {
  color: #333;
  background-color: #e6e6e6;
  border-color: #8c8c8c;
}

.btn-default {
  color: #333;

  background-color: #fff;
  border-color: #ccc;
}
</style>
