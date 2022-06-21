<template>
  <div>
    <h5><i class="fa fa-bell-o"></i> الإشعارات</h5>
<hr>
    <br>
    <em v-if="users.loading">الرجاء الأنتظار...</em>

    <table class="table" v-if="users">
      <thead class="thead-light">
        <tr>
           <th scope="col">اسم الزبون</th>
          <th scope="col">رقم الهاتف</th>
          <th scope="col">رقم الصندوق</th>
           <th scope="col">نوع الاشعار</th>

          <th scope="col">تاريخ الإشعار</th>

          
        </tr>
      </thead>
      <tbody>
        <tr v-for="user in users" :key="user.id">
           <td>{{user.name }}</td>
          <td>{{user.phone }}</td>
          <td>{{user.numberBox }}</td>
              <td>{{user.noteTypeName }}</td>

          <td>{{user.createDate }}</td>

 
        </tr>
      </tbody>
    </table>

        <div v-if="users.length ==0" class="alert alert-info" role="alert">
  <p>لاتوجد أشعارات</p>
 </div>
     
  </div>
</template>
<script>
import { customerService } from "../../_services/customer.service";

 
export default {
  data() {
    return {
      users: [],
       objData: {},
      };
  },
 

  created() {
    this.user = JSON.parse(localStorage.getItem("user"));
    this.users.loading = true;

    this.getAllNotes();
  },
  methods: {
 
 
    getAllNotes() {
      customerService.getAllNoteSendCustomer().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.users = resultArray;
        console.log(this.users);
      });
    },
     
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