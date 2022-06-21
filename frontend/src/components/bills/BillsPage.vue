<template>
  <div>
    <h5>إدارة الفواتير</h5>

    <br>
    <em v-if="users.loading">الرجاء الأنتظار...</em>

    <table class="table" v-if="users">
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
        <tr v-for="user in users" :key="user.id">
          <th scope="row">{{user.userId }}</th>
          <td>{{user.name }}</td>
          <td>{{user.userName }}</td>
          <td>{{user.officeName }}</td>
          <td>
            <button @click="showModal(user.userId)" class="btn btn-info">تعديل</button>
          </td>
          <td>
            <button class="btn btn-danger">حذف</button>
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
    <button id="show-modal" @click="showModal(0)">إضافة</button>
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