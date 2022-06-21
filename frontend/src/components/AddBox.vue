
<template>
  <div>
  
 
    <div class="card border-primary">
       <div  style="    color: #fefefe;
    background-color: #1083f6;
    border-color: #0980f8;" class="card-header  ">
    
            <h5 class="card-title text-center">إضافة صناديق</h5>


  </div>
      <div class="card-body">
        <hr>
        <form @submit.prevent="handleSubmit">
          <div class="row users">
            <div class="col-md-6 offset-6">
              <div class="form-group row">
                <label class="col-sm-4 col-form-label" for="start">الرقم من</label>
                <div class="col-sm-8">
                  <input
                    type="text"
                    name="start"
                  @keypress="restrictChars($event)"
                    v-model="start"
                    placeholder="0000"
                    required
                    class="form-control"
                  >
                </div>
              </div>
            </div>
            <div class="col-md-6 offset-6">
              <div class="form-group row">
                <label class="col-sm-4 col-form-label" for="end">الي</label>
                <div class="col-sm-8">
                  <input
                    type="text"
                    name="end"
                                      @keypress="restrictChars($event)"

                    placeholder="0000"
                    v-model="end"
                    required
                    class="form-control"
                  >
                </div>
              </div>
            </div>
            <div class="col-md-6 offset-6">
              <div class="form-group row">
                <label class="col-sm-4 col-form-label" for="exampleCheck1">نوع الصندوق</label>

                <div class="col-sm-8">
                  <select class="form-control" v-model="typeBoxId" required>
                    <option value>اختر نوع الصندوق</option>

                    <option
                      v-for="typeBox in typeBoxs"
                      :key="typeBox.id"
                      :value="typeBox.id"
                    >{{ typeBox.name }}</option>
                  </select>
                </div>
              </div>
            </div>
            <div class="col-md-6 offset-6">
              <div class="form-group row">
                <div class="col-sm-12">
                  <button class="btn btn-block btn-primary" :disabled="loggingIn">إنشاء صناديق</button>
                  <img
                    class="loading"
                    v-show="loggingIn"
                    src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA=="
                  >
                </div>
              </div>
            </div>
          </div>

          <!-- <template>
  <button v-facebook-signin-button="appId" class="facebook-signin-button"> تسجيل الدخول</button>
          </template>-->
        </form>
      </div>
    </div>
    <br>
    <br>
          <div >
       
        <div class="row">
          <div class="col-md-6">
            <h4>قائمة الصناديق</h4>
            <hr>
            <table class="table" v-if="boxs">
              <thead class="thead-light">
                <tr>
                  <th scope="col">
                    <span style>رقم الصندوق</span>
                    <br>
                    <input type="text" style="    width: 200px;" v-model="search">
                  </th>
                  <th scope="col">
                    <span style>نوع الصندوق</span>
                  </th>
                  <th scope="col"></th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="box in fillterBox" :key="box.numberBox">
                  <td>{{box.numberBox }}</td>
                  <td>{{box.typeName }}</td>
                  <td>
                    <label class="form-checkbox">
                      <input
                        name="selected"
                        type="checkbox"
                        :value="box.numberBox"
                        v-model="selected"
                      >
                      <i class="form-icon"></i>
                    </label>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>

          <div class="col-md-6">
            <br><br><br>
           <div class="card border-danger">
       <div  style="    color: #fefefe;
    background-color: #dc3545;
    border-color: #dc3545;" class="card-header  ">
    
        <h5 class="card-title text-center">تعديل الصناديق</h5>


  </div>
      <div class="card-body">
            <form @submit.prevent="handleSubmitChange">
              <div class="col-md-10 offset-2">
                <div class="form-group row">
                  <label class="col-sm-4 col-form-label" for="exampleCheck1">نوع الصندوق</label>

                  <div class="col-sm-8">
                    <select class="form-control" v-model="typeBoxId" required>
                      <option value>اختر نوع الصندوق</option>

                      <option
                        v-for="typeBox in typeBoxs"
                        :key="typeBox.id"
                        :value="typeBox.id"
                      >{{ typeBox.name }}</option>
                    </select>
                  </div>
                </div>
              </div>
              <div class="col-md-10 offset-2">
                <div class="form-group row">
                  <div class="col-sm-12">
                    <button class="btn btn-block btn-primary" :disabled="loggingIn">تعديل الصناديق</button>
                    <img
                      class="loading"
                      v-show="loggingIn"
                      src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA=="
                    >
                  </div>
                </div>
              </div>
            </form>
            <hr>
            <br>
            <br>
            <form @submit.prevent="handleSubmitDelete">
              <button class="btn btn-block btn-danger">حذف الصناديق</button>
            </form>
          </div>
        </div>
      </div>
    </div>
   </div></div>
</template>
 

<script>
import { customerService } from "../_services/customer.service";

export default {
  /* directives: {
    FacebookSignInButton
  },*/
  data() {
    return {
      start: "",
      end: "",
      submitted: false,
      loggingIn: false,
      typeBoxId: "",
      typeBoxs: [],
      selected: [],
      boxs: [],
      search: "",
      searchType: ""
    };
  },
  computed: {
    fillterBox() {
      // Filter results
      return this.boxs.filter(boxs => {
        return boxs.numberBox.toString().indexOf(this.search) > -1;
      });
    }
  },

  created() {
    this.getAllOfficeBoxs();
    this.getAllbox();
  },

  methods: {
    getAllOfficeBoxs() {
      customerService.getAllTypeBox().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.typeBoxs = resultArray;

        console.log(this.typeBoxs);
      });
    },
    getAllbox() {
      customerService.getAllbox().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.boxs = resultArray;

        console.log(this.boxs);
      });
    },
    handleSubmit() {
      this.submitted = true;

      var mStart = this.start;
      var mEnd = this.end;

      var numbers = [];
      for (mStart; mStart <= mEnd; mStart++) {
        numbers.push(mStart);
      }
      console.log(numbers);
      var objData = {};
      objData.number = numbers;
      objData.typeId = this.typeBoxId;
      customerService.createboxs(objData).then(
        userData => {
          this.$store.state.alert.message = userData.message;
          this.$store.state.alert.type = "alert-success";
          console.log(userData);
          Swal.fire("تنبيه", userData.message, "success");

          setTimeout(function() {
            location.reload();
          }, 2000);
        },
        error => {
          // commit("registerFailure", error);
          this.$store.state.alert.message = error;
          this.$store.state.alert.type = "alert-danger";
          console.log(error);
        }
      );
    },
    handleSubmitChange() {
      this.submitted = true;

      var objData = {};
      objData.number = this.selected;
      objData.typeId = this.typeBoxId;
      customerService.updateboxs(objData).then(
        userData => {
          this.$store.state.alert.message = userData.message;
          this.$store.state.alert.type = "alert-success";
          console.log(userData);
          Swal.fire("تنبيه", userData.message, "success");

          setTimeout(function() {
            location.reload();
          }, 2000);
        },
        error => {
          // commit("registerFailure", error);
          this.$store.state.alert.message = error;
          this.$store.state.alert.type = "alert-danger";
          console.log(error);
        }
      );
    },
  restrictChars: function($event) {
    if ($event.charCode === 0 || /\d/.test(String.fromCharCode($event.charCode))) {
        return true
    } else {
        $event.preventDefault();
    }
},
    handleSubmitDelete() {
      this.submitted = true;

      var objData = {};
      objData.number = this.selected;

      customerService.deleteboxs(objData).then(
        userData => {
          this.$store.state.alert.message = userData.message;
          this.$store.state.alert.type = "alert-success";
          console.log(userData);
          Swal.fire("تنبيه", userData.message, "success");

          setTimeout(function() {
            location.reload();
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
};
</script>
<style>
.loading {
  display: block;
  margin: 8px auto;
}
.facebook-signin-button {
  color: white;
  background-color: #3b5998;
  height: 50px;
  font-size: 16px;
  border-radius: 10px;
  padding: 10px 20px 25px 20px;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
}
</style>

