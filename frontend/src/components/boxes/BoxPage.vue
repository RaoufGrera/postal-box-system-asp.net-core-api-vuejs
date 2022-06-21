<template>
  <div>
         <h5><i class=" fa fa-cube"/> حجز صندوق</h5>
<hr>
    <br>
                <form @submit.prevent="handleSubmit" class="forms">
     <!-- <h6>بيانات الزبون</h6>-->
      <br>
      <div class="row users">
        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="Name">الأسم</label>
            <div class="col-sm-8">
              <input type="text" name="Name" v-model="name" :disabled="(isOld) ? true : false"  required class="form-control">
            </div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="address">العنوان</label>
            <div class="col-sm-8">
              <input type="text" name="address" v-model="address" required :disabled="(isOld) ? true : false" class="form-control">
            </div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="PhonNumber">رقم الهاتف</label>
            <div class="col-sm-8">
              <input type="text" name="PhonNumber"                   @keypress="restrictChars($event)"
 v-model="phonNumber1" required :disabled="(isOld) ? true : false" class="form-control">
            </div>
          </div>
        </div>
   <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="emailAddress">البريد الإلكتروني</label>
            <div class="col-sm-8">
              <input type="text" name="emailAddress" v-model="emailAddress" :disabled="(isOld) ? true : false" class="form-control">
            </div>
          </div>
        </div>
   
      </div>
      <div class="row">
             <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">نوع العميل</label>

            <div class="col-sm-8">
              <select class="form-control" v-model="customerTypeId" :disabled="(isOld) ? true : false" required >
                                <option value>اختر نوع العميل</option>

                <option v-for="type in types" :key="type.id" :readonly="(isOld) ? true : false" :value="type.id">{{ type.name }}</option>
              </select>
            </div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">نوع الاثبات</label>

            <div class="col-sm-8">
              <select class="form-control" required :readonly="(isOld) ? true : false" :disabled="(isOld) ? true : false" v-model="customerIdentityId">
                <option value>اختر نوع الأثبات</option>
                <option
                  v-for="Identity in Identitys"
                  :key="Identity.id"
                  :value="Identity.id"
                >{{ Identity.name }}</option>
              </select>
            </div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="IdentityNumber">رقم الهوية</label>
            <div class="col-sm-8">
              <input type="text" name="IdentityNumber" required :disabled="(isOld) ? true : false" v-model="IdentityNumber" class="form-control" >
            </div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" required for="exampleCheck1">الوظيفة</label>

            <div class="col-sm-8">
              <select class="form-control" required v-model="customerJobsId" :disabled="(isOld) ? true : false">
                <option value>اختر الوظيفة</option>

                <option v-for="job in jobs" :key="job.id" :value="job.id">{{ job.name }}</option>
              </select>
            </div>
          </div>
        </div>
      </div>
       <hr> <br>
 <!-- <h6>بيانات الصندوق</h6>-->
 
    <br>
      <div class="row">
        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">نوع الصندوق</label>

            <div class="col-sm-8">
              <select class="form-control" @change="getAllOfficeBoxs" v-model="typekey" required :disabled="(isOld) ? true : false">
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

        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">الصناديق</label>

            <div class="col-sm-8">
              <select class="form-control" v-model="numberBox" required :disabled="(isOld) ? true : false">
                <option value>اختر رقم الصندوق</option>

                <option
                  v-for="officeBox in officeBoxs"
                  :key="officeBox.numberBox"
                  :value="officeBox.numberBox"
                >{{ officeBox.numberBox }}</option>
              </select>
            </div>
          </div>
        </div>
        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">بداية الحجز</label>
            <div class="col-sm-8">
              <datepicker :format="format" :disabled="(isOld) ? true : false" v-on:input="onChangeDateStart" v-model="dateStartRent" :value="dateStartRent" :language="ar"></datepicker>
            </div>
          </div>
        </div>

        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">مدة الحجز</label>
            <div class="col-sm-8">
              <select  v-model="rentYears" :value="rentYears" required @change="onChangeDateStart" class="form-control">
                 <option value="1">1 سنة</option>

                <option value="2">2 سنتين</option>
                <option value="3">3 سنوات</option>
                <option value="4">4 سنوات</option>
                <option value="5">5 سنوات</option>
                <option value="6">6 سنوات</option>
                <option value="7">7 سنوات</option>
                <option value="8">8 سنوات</option>
                <option value="9">9 سنوات</option>
                <option value="10">10 سنوات</option>
              </select>
            </div>
          </div>
        </div>
      </div>

<hr>
     <br>
 <!-- <h6>بيانات الفاتورة</h6> -->
      <br>
      <div class="row">
        
        <div class="col-md-3">


          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">طريقة الدفع</label>

            <div class="col-sm-8">
              <select class="form-control" disabled v-model="payTypeId" required>
                <option value>اختر طريقة الدفع</option>

                <option
                  v-for="payType in payTypes"
                  :key="payType.id"
                  :value="payType.id"
                >{{ payType.name }}</option>
              </select>
            </div>
          </div>
        </div>


        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">رقم الفاتورة</label>

            <div class="col-sm-8">
              <input type="text" name="numberBill" disabled v-model="numberBill" class="form-control">
            </div>
          </div>
        </div>

               <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">سعر المفتاح</label>

            <div class="col-sm-8">
              <input type="text" name="priceKey" value="3" v-model="priceKey" @keyup="onChangeDateStart()" class="form-control" >
            </div>
          </div>
        </div>
 
        <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">المبلغ الأجمالي</label>

            <div class="col-sm-8">
              <input
                type="text"
                name="totalPrice"
                readonly
                v-model="totalPrice"
                class="form-control"
              >
            </div>
          </div>
        </div>
      </div>
                <button type="submit" class="btn btn-primary">حفظ</button>
                <a href="/admin/box" style="margin:0 15px;" class="btn btn-danger">إلغاء</a>

    </form>
  </div>
</template>

<script>
import { customerService } from "../../_services/customer.service";
import Datepicker from "vuejs-datepicker";
import { ar } from "vuejs-datepicker/dist/locale";

export default {
  components: {
    Datepicker
  },
  data() {
    return {
      ar: ar,
      submitted: false,
      recivedData: this.dataComp,
      addEditTitle: this.addEditTitleComp,
      name: "",
      phonNumber1: "",
      address: "",
      IdentityNumber: "",
      customerJobsId:"",
      customerIdentityId:"",
      jobs: [],
            oldData: {},

      Identitys: [],
      types: [],
      officeBoxs: [],
      typeBoxs: [],
       payTypes: [],
emailAddress:"",
      typekey: "",      typekey1: "",
customerId:0,
      totalPrice:"",
      boxPrice:"",
      numberBill:"",
      payTypeId:1,
      priceKey:3,
            numberBox:"",
                        numberBox1:"",
                        isOld:false,

customerTypeId:"",
      dateStartRent:new Date(),
        rentYears: 2,
      format:"dd-MM-yyyy",
      rent:{
         typeBoxId : 0,
  dateStartRent: "",
        priceKey:3,

  rentYears: 0
      }
    };
  },
  created() {
          console.log(this.$route.params.id);


    this.getAllTypeBox();
    this.getAllJobType();
    this.getAllCustomerType();
    this.getAllCustomerIdentity();
this.getAllPayType();
    this.getAllOfficeBoxs();
    this.isResave();
    var r =  this.$route.params.id;
if(r)
this.reSaveGetData();
  },
  methods: {
    customFormatter(date) {
      return moment(date).format('MMMM Do YYYY, h:mm:ss a');
    },
    isResave(){
      
      $("users input, users textarea,users select").prop("readonly", true);

    },
    reSaveGetData(){
var r =  this.$route.params.id;
 

            customerService.reSaveGetData(r).then(data => {
      
        this.oldData = data;
        this.name = data.name;
        this.phonNumber1 = data.phonNumber1;
        this.address = data.address;
        this.IdentityNumber = data.identityNumber;
        this.customerTypeId = data.customerTypeId;
        this.customerJobsId = data.customerJobsId;
        this.dateStartRent = data.dateStartRent;
        this.customerIdentityId = data.customerIdentityId;
        this.emailAddress = data.emailAddress;
        this.typekey = data.typeKey;
        this.numberBox1 = data.numberBox;
        this.numberBox = data.numberBox;
        this.isOld= true;

        this.customerId = data.customerId;
        this.officeBoxs.push({'numberBox':data.numberBox});
        console.log(this.oldData);
        
       this.rent.typeBoxId =  data.typeKey;
  this.rent.dateStartRent = this.dateStartRent;
  this.rent.rentYears = this.rentYears
    this.rent.priceKey = this.priceKey
          
          customerService.postCheckRent(this.rent).then(
              userData => {
            console.log(userData);
                            this.totalPrice = userData.result;

              },
              error => {
          
                console.log(error);
              }
            );
      });
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
        getAllPayType() {
         /* customerService.getBillID().then(data =>{
           this.numberBill = data;  
                   console.log(this.numberBill);

          });*/
      customerService.getAllPayType().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.payTypes = resultArray;
        console.log(this.payTypes);
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
    getAllTypeBox() {
      customerService.getAllTypeBox().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.typeBoxs = resultArray;
        console.log(this.typeBoxs);
      });



    },

    getAllOfficeBoxs() {
      var ii = this.typekey;
      customerService.getAllBoxOfficeNotUsed(ii).then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.officeBoxs = resultArray;
    var r =  this.$route.params.id;
if(r)
        this.officeBoxs.push({'numberBox':this.numberBox1});

        console.log(this.officeBoxs);

      });

      this.onChangeDateStart();
    },

    onChangeDateStart(){
      console.log(this.dateStartRent);

      var pp = this.dateStartRent;
  
  this.rent.typeBoxId = this.typekey;
  this.rent.dateStartRent = this.dateStartRent;
  this.rent.rentYears = this.rentYears
    this.rent.priceKey = this.priceKey

            customerService.postCheckRent(this.rent).then(
              userData => {
            console.log(userData);
                            this.totalPrice = userData.result;

              },
              error => {
          
                console.log(error);
              }
            );
    },  restrictChars: function($event) {
    if ($event.charCode === 0 || /\d/.test(String.fromCharCode($event.charCode))) {
        return true
    } else {
        $event.preventDefault();
    }
},
      handleSubmit(e) {
      this.submitted = true;

      
   var ii = this.typekey;
   console.log(ii);
              var objData = {};
              objData= {
                name: this.name,
                phonNumber1: this.phonNumber1,
                address:this.address,
                emailAddress:this.emailAddress,
                IdentityNumber:this.IdentityNumber,
                customerJobsId:this.customerJobsId,
                customerIdentityId:this.customerIdentityId,
                    customerTypeId:this.customerTypeId,
                    typeBoxId:ii,
        dateStartRent:this.dateStartRent,
         rentYears:this.rentYears,
                totalPrice:this.totalPrice,
                 billsNumber:this.numberBill,
                boxId :this.numberBox,
                numberBox:this.numberBox,
                priceKey:this.priceKey,
                payTypeId:this.payTypeId,
                customerId: this.customerId
              }
              console.log(objData)
                 var r =  this.$route.params.id;
if(r){
   customerService.saveBoxRe(objData,r).then(
              userData => {
               
                  this.$store.state.alert.message = userData.message;
                  this.$store.state.alert.type = "alert-success";
                  console.log(userData);
  
                Swal.fire(
  'تنبيه',
  userData.message,
  'success'
) 

                  setTimeout(function(){ window.location.href ="/admin/customer" ;
 }, 1000);

   
              },
              error => {
                // commit("registerFailure", error);
                this.$store.state.alert.message = error.message;
                this.$store.state.alert.type = "alert-danger";
                console.log(error);
              }
            );
              }else{
             
            customerService.saveBox(objData).then(
              userData => {
               
                  this.$store.state.alert.message = userData.message;
                  this.$store.state.alert.type = "alert-success";
                  console.log(userData);
                    
                Swal.fire(
  'تنبيه',
  userData.message,
  'success'
) 
                  setTimeout(function(){ location.reload();
 }, 2000);

   
              },
              error => {
                // commit("registerFailure", error);
                this.$store.state.alert.message = error.message;
                this.$store.state.alert.type = "alert-danger";
                console.log(error);
              }
            );
          
  
  }
      }
  }
};
</script>

<style>
 


.form-control {
  font-size: inherit;}
</style>
