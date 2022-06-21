
<template>
  <div>
    <transition name="modal">
      <div class="modal-mask">
        <div class="modal-wrapper">
          <div class="modal-dialog">
            <div class="modal-content">
              <div dir="rtl" class="modal-header">
                <h5 class="modal-title">{{ addEditTitle }} - {{recivedData.userName }}</h5>
                <button type="button" class="close" @click="close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <form @submit.prevent="handleSubmit">
                  <div class="mymodal">
                    <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="exampleInputEmail13">ر.م</label>
                      <div class="col-sm-9">
                        <input
                          type="text"
                          v-model="recivedData.userId"
                          class="form-control"
                          readonly
                        >
                      </div>
                    </div>
                    <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="Name">الأسم</label>
                      <div class="col-sm-9">
                        <input
                          type="text"
                          v-model="recivedData.name"
                          name="Name"
                          class="form-control"
                        >
                      </div>
                    </div>

                    <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="PhoneNumber">رقم الهاتف</label>
                      <div class="col-sm-9">
                        <input
                          type="text"
                          v-model="recivedData.phoneNumber"
                          name="PhoneNumber"
                          class="form-control"
                        >
                      </div>
                    </div>

                    <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="UserName">اسم المستخدم</label>
                      <div class="col-sm-9">
                        <input
                          type="text"
                          v-model="recivedData.userName"
                          name="UserName"
                          class="form-control"
                        >
                      </div>
                    </div>
                    <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="exampleInputPassword1">الرقم السري</label>
                      <div class="col-sm-9">
                        <input
                          type="text"
                          v-model="recivedData.password"
                          class="form-control"
                          id="exampleInputPassword1"
                          placeholder="الرقم السري"
                        >
                      </div>
                    </div>
                    <div class="form-group row">
                      <label class="col-sm-3 col-form-label" for="exampleCheck1">المكتب</label>

                      <div class="col-sm-9">
                        <select v-model="recivedData.officeId" class="form-control">
                          <option
                            v-for="office in offices"
                            :key="office.id"
                            :value="office.id"
                            :selected="office.id == recivedData.officeId"
                          >{{ office.name }}</option>
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
import { userService } from "../../_services/user.service";

export default {
  name: "modal",
  props: ["addEditTitleComp", "dataComp"],

  data() {
    return {
      offices: [],
      submitted: false,
      recivedData: this.dataComp,
      addEditTitle: this.addEditTitleComp
    };
  },

  created() {
    this.getAllOffices();
    console.log(this.recivedData);
  },
  methods: {
    close() {
      this.$emit("close");
      console.log("modal is closing");
    },

    updateTable() {
      this.$emit("updateTable");
    },
    getAllOffices() {
      userService.getAllOffice().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.offices = resultArray;
        console.log(this.offices);
      });
    },
    handleSubmit(e) {
      this.submitted = true;

      this.$validator.validate().then(valid => {
        if (valid) {
          if (this.recivedData.userId == "") {
            userService.addUser(this.recivedData).then(
              userData => {
                setTimeout(() => {
                  this.$store.state.alert.message = userData;
                  this.$store.state.alert.type = "alert-success";
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
          } else {
            userService.editUser(this.recivedData).then(
              userData => {
                setTimeout(() => {
                  this.$store.state.alert.message = userData;
                  this.$store.state.alert.type = "alert-success";
                  this.updateTable();
                  this.close();
                }, 2000);
              },

              error => {
                // commit("registerFailure", error);
                this.$store.state.alert.message = error;
                this.$store.state.alert.type = "alert-danger";
                console.log(error);
              }
            );
          }
        }
      });
    }
  }
};
</script>
<style>
.modal-body {
    padding: 0;
}
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