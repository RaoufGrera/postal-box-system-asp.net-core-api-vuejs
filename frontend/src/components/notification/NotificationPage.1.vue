<template>
  <div>
    <h5>إرسال اشعارات</h5>

    <br>
    <em v-if="boxes.loading">الرجاء الأنتظار...</em>

    <br>
    <span>بحث</span>
        <input type="text" v-model="search" class="form-control" />

<br> 
       <form @submit.prevent="handleSubmit">
                <div class="col-md-4">


          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">نوع الأشعار</label>

   <div class="col-sm-8">
              <select class="form-control" required   v-model="typeId">
              
                <option
                  v-for="noteTyp in noteType"
                  :key="noteTyp.id"
                  :value="noteTyp.id"
                >{{ noteTyp.name }}</option>
              </select>
            </div>
          </div>
                </div>

    <table class="table" v-if="boxes">
      <thead class="thead-light">
        <tr>
          <th scope="col">#</th>
          <th scope="col">الأسم</th>
          <th scope="col">رقم الهاتف</th>
          <th scope="col">رقم الصندوق</th>
  <th>
                    <label class="form-checkbox">
    <input type="checkbox" v-model="selectAll" @click="select">
    <i class="form-icon"></i>
  </label>
                </th>
        </tr>
      </thead>
      <tbody>
     
        <tr v-for="box in countriesResults "   :key="box.billsId">
          <th scope="row">{{box.billsId }}</th>
          <td>{{box.name }}</td>
          <td>{{box.phonNumber1 }}</td>
          <td>{{box.numberBox }}</td>
<td>
                    <label class="form-checkbox">
                        <input  name="selected" type="checkbox" :value="box.billsId" v-model="selected">
                        <i class="form-icon"></i>
                    </label>
                </td>
        </tr>
      </tbody>
    </table>
 
                    <button @click="handleSubmit" class="btn btn-primary">إرسال</button>
                <a href="/notification" style="margin:0 15px;" class="btn btn-danger">إلغاء</a>
       </form>

   </div>
</template>
<script>
import { customerService } from "../../_services/customer.service";


export default {
  data() {
    return {
      boxes: [],
      noteType:[],
      isModalShow: false,
      objData: {},
  selected: [],
  typeId:"1",
        selectAll: false,
            search: '',
    };
  },

  created() {
     this.boxes.loading = true;
this.getAllType();
    this.getAllBoxRent();
  },

  computed: {
  countriesResults() {
     // Filter results
     return this.boxes.filter((boxes) => {
       return boxes.name.toLowerCase()
       .indexOf(this.search.toLowerCase()) > -1;
     });
  }
},
  methods: {

        getAllType() {
      customerService.getAllTypeNote().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.noteType = resultArray;
        console.log(this.noteType);
      });
    },
 handleSubmit() {
      this.submitted = true;

      
  
              var objData = {};
           objData.data = this.selected;
           objData.typeId= this.typeId;
            customerService.sendNote(objData).then(
              userData => {
               
                  this.$store.state.alert.message = userData;
                  this.$store.state.alert.type = "alert-success";
                  console.log(userData);
   
              },
              error => {
                // commit("registerFailure", error);
                this.$store.state.alert.message = error;
                this.$store.state.alert.type = "alert-danger";
                console.log(error);
              }
            );
          
  
  
      },
  select() {
            this.selected = [];
            if (!this.selectAll) {
                for (let i in this.boxes) {
                    this.selected.push(this.boxes[i].billsId);
                }
            }
        },
    getAllBoxRent() {
      customerService.getBoxRent().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.boxes = resultArray;
        console.log(this.boxes);
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