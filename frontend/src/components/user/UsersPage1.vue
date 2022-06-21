<template>
  <div>
    <h5>إدارة المستخدمين</h5>
<hr>
    <br>
    <em v-if="users.loading">الرجاء الأنتظار...</em>

    <table class="table" v-if="users">
      <thead class="thead-light">
        <tr>
          <th scope="col">#</th>
          <th scope="col">الأسم</th>
          <th scope="col">اسم المستخدم</th>
          <th scope="col">مكتب البريد</th>
          <th colspan="2" width="20%" scope="col"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="user in users" :key="user.id">
          <th scope="row">{{user.userId }}</th>
          <td>{{user.name }}</td>
          <td>{{user.userName }}</td>
          <td>{{user.officeName }}</td>
          <td>
            <button @click="showModal(user.userId)" class="btn btn-default"><i class="fa fa-pencil"></i> تعديل</button>
          </td>
          <td>
            <button  @click="confimdelte(user.userId)" class="btn btn-danger"><i class="fa fa-times"></i> حذف</button>
          </td>
        </tr>
      </tbody>
    </table>

    <modal
      :addEditTitleComp="addEditTitle"
      :dataComp="objData"
      v-if="isModalShow"
      @close="closeModal"
      @updateTable="getAllUsers"

    ></modal>
    <button id="show-modal" class="btn btn-info" @click="showModal(0)">إضافة</button>
  </div>
</template>
<script>
import { userService } from "../../_services/user.service";

import { mapState, mapActions } from "vuex";
import modal from "./modal";
export default {
  data() {
    return {
      users: [],
      isModalShow: false,
      objData: {},
      submitted: false,
      addEditTitle: "تعديل"
    };
  },
  components: { modal },

  created() {
    this.user = JSON.parse(localStorage.getItem("user"));
    this.users.loading = true;

    this.getAllUsers();
  },
  methods: {
    ...mapActions("account", ["addUser"]),

    confimdelte(ID){
      if (confirm('متأكد من عملية الحذف ?')) {
   console.log("Deee");
        userService.deleteUser(ID).then(
              userData => {
                setTimeout(() => {
                  this.$store.state.alert.message = userData;
                  this.$store.state.alert.type = "alert-success";
                                Swal.fire(
  'تنبيه',
  userData.message,
  'success'
) 
                  console.log(userData);
                   this.getAllUsers();
                });
              },
              error => {
                // commit("registerFailure", error);
                this.$store.state.alert.message = error;
                this.$store.state.alert.type = "alert-danger";
                console.log(error);
              }
            );


      } else {
          // Do nothing!
      }
    },
    getAllUsers() {
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
      var filtterFromList = this.users.find(x => x.userId == id);
      if (filtterFromList) {
        this.objData = filtterFromList;
        this.addEditTitle = "تعديل";
      } else {
        this.objData = {};
        this.addEditTitle = "إضافة";
      }
    },
    closeModal() {
      this.isModalShow = false;
          this.getAllUsers();

    }
  }
};
</script>

<style>
.table {
  border: 1px solid #d3d3d3;
}
.table td,
.table th {
  vertical-align: middle;
}
</style>