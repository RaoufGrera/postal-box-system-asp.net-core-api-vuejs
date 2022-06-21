
<template>
  <div>
    <transition name="modal">
      <div class="modal-mask">
        <div class="modal-wrapper">
          <div class="modal-dialog">
            <div class="modal-content">
              <div dir="rtl" class="modal-header">
                <h5 class="modal-title">{{ addEditTitle }} - {{recivedData.name }}</h5>
                <button type="button" class="close" @click="close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <form @submit.prevent="handleSubmit">
                  <div class="mymodal">
   
                    <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="Name">الأسم</label>
                      <div class="col-sm-9">
                        <input
                          type="text"
                          v-model="recivedData.name"
                          name="Name"
                          class="form-control"
                           required
                        >
                      </div>
                    </div>
                    <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="Address">العنوان</label>
                      <div class="col-sm-9">
                        <input
                          type="text"
                          v-model="recivedData.address"
                          name="Address"
                          class="form-control"
                           required
                        >
                      </div>
                    </div>
                    <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="PhoneNumber">رقم الهاتف</label>
                      <div class="col-sm-9">
                        <input
                          type="text"
                          v-model="recivedData.phonNumber1"
                          name="PhoneNumber"
                          class="form-control"
                                            @keypress="restrictChars($event)"

                          required
                        >
                      </div>
                    </div>

                                        <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="IdentityNumber">رقم إثبات الهوية</label>
                      <div class="col-sm-9">
                        <input
                          type="text"
                          v-model="recivedData.identityNumber"
                          name="IdentityNumber"
                          class="form-control"
                          required
                        >
                      </div>
                    </div>

         <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="exampleCheck1">الوظيفة</label>

                      <div class="col-sm-9">
                        <select v-model="recivedData.customerJobsId" class="form-control">
                           <option
                            v-for="job in jobs"
                            :key="job.id"
                            :value="job.id"
                            :selected="job.id == recivedData.customerJobsId"
                          >{{ job.name }}</option>
                        </select>
                      </div>
                    </div>


                    
         <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="exampleCheck1">نوع الاثبات</label>

                      <div class="col-sm-9">
                        <select v-model="recivedData.customerIdentityId" class="form-control">
                           <option
                            v-for="Identity in Identitys"
                            :key="Identity.id"
                            :value="Identity.id"
                            :selected="Identity.id == recivedData.customerIdentityId"
                          >{{ Identity.name }}</option>
                        </select>
                      </div>
                    </div>
                   
                          <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="exampleCheck1">نوع العميل</label>

                      <div class="col-sm-9">
                        <select v-model="recivedData.customerTypeId" class="form-control">
                           <option
                            v-for="type in types"
                            :key="type.id"
                            :value="type.id"
                            :selected="type.id == recivedData.customerTypeId"
                          >{{ type.name }}</option>
                        </select>
                      </div>
                    </div>

                  </div>
                  <div class="modal-footer">
                    <button type="submit" class="btn btn-primary">حفظ</button>
                    
                    <span @click="close" class="btn btn-default">إلغاء</span>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>
<script>
import { customerService } from "../../_services/customer.service";

export default {
  name: "modal",
  props: ["addEditTitleComp", "dataComp"],

  data() {
    return {
      submitted: false,
      recivedData: this.dataComp,
      addEditTitle: this.addEditTitleComp,
            jobs:[],
             Identitys:[],
             types:[]
    };
  },

created(){
console.log(this.recivedData);
this.getAllJobType();
this.getAllCustomerType();
this.getAllCustomerIdentity();
},

  methods: {
  restrictChars: function($event) {
    if ($event.charCode === 0 || /\d/.test(String.fromCharCode($event.charCode))) {
        return true
    } else {
        $event.preventDefault();
    }
},
        getAllJobType() {
      customerService.getAllCustomerJobs().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.jobs = resultArray;
        console.log(this.jobs);
      });
    },
            getAllCustomerIdentity() {
      customerService.getAllCustomerIdentity().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.Identitys = resultArray;
        console.log(this.Identitys);
      });
            },
                  getAllCustomerType() {
      customerService.getAllCustomerType().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.types = resultArray;
        console.log(this.types);
      });
    },
    close() {
      this.$emit("close");
       this.updateTable();
      console.log("modal is closing");
    },
    

    updateTable() {
      this.$emit("updateTable");
    },

    handleSubmit(e) {
      this.submitted = true;

      this.$validator.validate().then(valid => {
        if (valid) {
  
            customerService.editCustomer(this.recivedData).then(
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
                  this.updateTable();
                  this.close();
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
      }
    }
}

</script>
<style>
.modal-mask {
  position: fixed;
  z-index: 9998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: table;
  transition: opacity 0.3s ease;
}
.modal-body {
    padding: 0;
}
.modal-wrapper {
  display: table-cell;
  vertical-align: middle;
}
.modal-header .close {
  padding: 1rem;
  margin: -1rem auto -1rem -1rem;
}
.modal-header {
  background-color: #f3f3f3;
  border-bottom: 1px solid #ddd;
}
.modal-body {
  max-height: 70vh;
  overflow-y: auto;
}
.mymodal {
  padding: 25px;
}
.modal-footer {
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
  border-top: 1px solid #e9ecef;
}
.modal-footer .btn + .btn {
  margin-right: 10px;
  margin-bottom: 0;
}
</style>