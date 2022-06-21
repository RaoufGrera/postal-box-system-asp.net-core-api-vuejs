<template>
  <nav class="navbar navbar-light navbar-expand-md bg-faded justify-content-center">
    <div class="container-fluid">
      <a href="/" class="navbar-brand d-flex mr-left">
        <img width="30" height="30" src="../../assets/pac.png" alt="logo"><span style="
    font-size: 90%;
    text-align: center;
    color: #fff;
    margin-right: 16px;
    margin-top: 2px;
    display: block;
    vertical-align: text-bottom;
"> إدارة الصناديق البريدية </span>
      </a>
      <button
        class="navbar-toggler"
        type="button"
        data-toggle="collapse"
        data-target="#collapsingNavbar3"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="navbar-collapse collapse w-100" id="collapsingNavbar3">
       <!--  <ul class="navbar-nav w-100 justify-content-left">
          <li class="nav-item">
            <a class="nav-link" href="#">الرئيسية</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#">المكاتب البريدية</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#">الأسعار</a>
          </li>
        </ul>-->
        <ul class="nav navbar-nav ml-auto w-100 justify-content-end">


          <li v-if="!islogin" class="nav-item">
            <a class="btn btn-block btn-primary" href="/login">دخول</a>
          </li>
               <li v-if="islogin" class="nav-item mar">
                 <span class="nav-linkk">
                   
                   <span v-if="!iscustomer" class="info fa fa-building-o"></span> {{office}} 
       <span v-if="!iscustomer" style="margin:0 4px;"> | </span>
                   <span class="info fa fa-user-circle"></span> {{name}} </span>
               </li>
      
          <li v-if="islogin" class="nav-item">

              <a v-if="iscustomer" class="btn btn-block btn-danger" href="/logout">تسجيل خروج</a>
              <a v-if="!iscustomer" class="btn btn-block btn-danger" href="/admin/logout">تسجيل خروج</a>

          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>
<script>
import { mapState, mapActions } from 'vuex'

export default {

  computed: {
            islogin(){
             console.log(this.$store.state.account.status.loggedIn);


            return  this.$store.state.account.status.loggedIn;
        },
        iscustomer(){
            return  this.$store.state.account.user.isCustomer;

        },
        office(){
                                    return  this.$store.state.account.user.officeName ;


        },
        name(){
          return this.$store.state.account.user.name;
        }
  },
  methods:{
 
            ...mapActions('account', [ "logout" ])

  }
}
</script>
<style>
.pointer{
  cursor: pointer;
}
.info{
  font-size: 88%;
  color: #555;
}
.btn{
  font-size: inherit;
}
.nav-linkk {
    display: block;
    padding: .5rem 1rem;
    color: #363636;
    background-color: #ffffff;
    cursor: default;
    border: 1px solid #006cc2;
    font-size: 90%;
    border-radius: 8px;
    margin: 0 8px;
}
.nav-linkkk {
    display: block;
    padding: .5rem 1rem;
        color: rgba(0,0,0,.5);
            cursor: pointer;
}
.mar{
      display: block;
    margin: auto 0;
}
</style>
