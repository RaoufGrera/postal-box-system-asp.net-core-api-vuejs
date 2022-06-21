<template>
  <div>

        <h5>إدارة المستخدمين</h5>
       
<br>
   <em v-if="users.loading">الرجاء الأنتظار...</em>

   <table class="table"  v-if="users">
  <thead class="thead-light">
    <tr>
      <th scope="col">#</th>
      <th scope="col">الأسم</th>
      <th scope="col">اسم المستخدم</th>
      <th scope="col">مكتب البريد</th>
       <th colspan="2" scope="col"></th>
    </tr>
  </thead>
  <tbody>
    <tr   v-for="user in users" :key="user.id">
      <th scope="row">{{user.userId }}</th>
      <td>{{user.name }}</td>
      <td>{{user.userName }}</td>
      <td>{{user.officeName }}</td>
       <td><button @click="showModal(user.userId)" class="btn btn-info">تعديل</button></td>
        <td><button class="btn btn-danger">حذف</button></td>

    </tr>
   
  </tbody>
</table>
    <ul v-if="users">
      <li v-for="user in users" :key="user.id">{{user.userName }}</li>
    </ul>

       <div>
  <div v-if="isModalShow">
    <transition name="modal">
      <div class="modal-mask">
        <div class="modal-wrapper">
          <div class="modal-dialog">
            <div class="modal-content">
              <div dir="rtl" class="modal-header">
                <h5 class="modal-title">{{ addEditTitle}} </h5>
                <button type="button" class="close" @click="closeModal">
                  <span aria-hidden="true">&times;</span>
                </button>
                
              </div>
              <div class="modal-body">
               
          <form @submit.prevent="handleSubmit">
            <div class="mymodal">
  <div class="form-group row">
    <label class="col-sm-3 col-form-label" for="exampleInputEmail13">ر.م</label>
            <div class="col-sm-9">

    <input type="text" v-model="userData.userId" class="form-control" readonly></div>
   </div>
         <div class="form-group row">
        <label class="col-sm-3 col-form-label" for="Name">الأسم</label>
                <div class="col-sm-9">

        <input
          type="text"
            
         v-model="userData.name"
           name="Name"
          class="form-control"
  
        >
 
                </div>
      </div>

      <div class="form-group row">
        <label class="col-sm-3 col-form-label"  for="PhoneNumber">رقم الهاتف</label>
                       <div class="col-sm-9">

        <input
          type="text"
            
          v-model="userData.phoneNumber"
           name="PhoneNumber"
          class="form-control"
 
        >
   </div>
      </div>

               <div class="form-group row">
        <label  class="col-sm-3 col-form-label" for="UserName">اسم المستخدم</label>
        <div class="col-sm-9"> <input
          type="text"
            
          v-model="userData.userName"
           name="UserName"
          class="form-control"
   
        >
       </div>
      </div>
  <div class="form-group row">
    <label  class="col-sm-3 col-form-label" for="exampleInputPassword1">الرقم السري</label>
        <div class="col-sm-9">

    <input type="text"  v-model="userData.password" class="form-control" id="exampleInputPassword1" placeholder="الرقم السري">
  </div>
   </div>
  <div class="form-group row">
     
    <label  class="col-sm-3 col-form-label"   for="exampleCheck1">المكتب</label>

  <div class="col-sm-9">
<select v-model="userData.officeId" class="form-control" >
  
  <option value="" >إختيار مكتب</option>
  <option v-for="office in offices"  :key="office.id" :value="office.id"
   :selected="office.id == myOffice">
      {{ office.name }}
  </option>
</select>

  </div>
  </div>
  </div>
  <div class="modal-footer">
  <button type="submit" class="btn btn-primary">حفظ</button> 

    <span  @click="closeModal" class="btn btn-default">إلغاء</span>

  </div>
</form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </transition>
  </div>
    <button id="show-modal" @click="showModal(0)">إضافة</button>

  </div>

   
           </div>
 
</template>
<script>
import { userService } from "../../_services/user.service";
import { mapState, mapActions } from "vuex";

 


  export default {
 
   data() {
    return {
      users: [],
            isModalShow:false,
              userData: {
              userId: '',
              phoneNumber: '',
              name: '',
              officeId: '',

              userName: '',
              password: ''
      },
          submitted: false,
    myOffice:'',
    addEditTitle:"تعديل",
      offices:[],
      myArray : [{'id':'73','foo':'bar'},{'id':'45','foo':'bar'}]


    };
  },

  created() {
    
    this.user = JSON.parse(localStorage.getItem("user"));
    this.users.loading = true;

   this.getAllUsers();
      userService.getAllOffice().then(data => {
      const resultArray = [];
      for (let key in data) {
        resultArray.push(data[key]);
      }
      this.offices = resultArray;
      console.log(this.offices);
    });
  },
  methods:{
        ...mapActions("account", ["addUser"]),


getAllUsers(){
   userService.getAll().then(data => {
      const resultArray = [];
      for (let key in data) {
        resultArray.push(data[key]);
      }
      this.users = resultArray;
      console.log(this.users);
    });
},
          showModal(id) {
        this.isModalShow = true;
  console.log(id);
    var p = this.users.find(x => x.userId == id);

 
     if(p){
      this.myOffice = p.officeId;
      this.addEditTitle = "تعديل"

      this.userData.userId = p.userId;
      this.userData.userName = p.userName;
      this.userData.password = p.password;
            this.userData.officeId = p.officeSelected;



      console.log( this.myOffice);

     }else{
             this.userData.userId =0;

      this.addEditTitle = "إضافة"
      this.userData.userName = null;
      this.userData.password = null;
      this.myOffice = '';

     }
   

      },
      closeModal() {
        
        this.isModalShow = false;

      },
      handleSubmit(e) {
      this.submitted = true;
      this.$validator.validate().then(valid => {
        if (valid) {
          userService.addUser(this.userData).then( userData => {
            
                setTimeout(() => {
                this.$store.state.alert.message = userData;
                this.$store.state.alert.type = "alert-success";
                console.log(userData);
                this.closeModal();
                });
            },
            error => {
               // commit("registerFailure", error);
                this.$store.state.alert.message = error;
                this.$store.state.alert.type = "alert-danger";
                console.log(error);

            }
        );
         
        }
      });
      },
    },
 
};
</script>

<style>

  .modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, .5);
  display: table;
  transition: opacity .3s ease;
}

.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}
.modal-header .close {
    padding: 1rem;
    margin: -1rem auto -1rem -1rem;
}
.modal-header{
      background-color: #f3f3f3;
    border-bottom: 1px solid #ddd;
}
.modal-body{
    max-height: 70vh;
    overflow-y: auto;
}
.mymodal{
      padding: 25px;
}
.modal-footer{
      display: block;
    /* display: flex; */
    -ms-flex-align: center;
    /* align-items: center; */
    -ms-flex-pack: end;
    justify-content: flex-end;
    padding: 1rem;
    padding: 15px;
    text-align: right;
    background: #f5f5f5;
    border-top: 1px solid #e5e5e5;
    border-top: 1px solid #e9ecef
}
.modal-footer .btn+.btn {
    margin-right: 10px;
    margin-bottom: 0;
}
.table{
      border: 1px solid #d3d3d3;
}
.table td, .table th {
     vertical-align: middle;
 }
</style>