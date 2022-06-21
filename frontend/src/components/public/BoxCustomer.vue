<template>
  <div>
        <h5><i class="fa fa-cube"></i> حجز صندوق</h5>
<hr>
    <br>
                <form @submit.prevent="handleSubmit" class="forms">
     
      <br>
     
           <div class="row">
           <div class="col-md-6 offset-6">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" required for="region">المنطقة</label>

            <div class="col-sm-8">
              <select class="form-control"  @change="getAllOfficeData" required v-model="regionId" :disabled="(isOld) ? true : false">
                <option value>اختر المنطقة</option>

                <option v-for="region in regions" :key="region.id" :value="region.id">{{ region.name }}</option>
              </select>
            </div>
          </div>
        </div>
     
        <div class="col-md-6 offset-6">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" required for="offices">المكتب</label>
<!-- @change="resetType" -->
            <div class="col-sm-8">
              <select class="form-control" required v-model="officeId" :disabled="(enoffice) ? true : false">
                <option value>اختر المكتب</option>

                <option v-for="office in offices" :key="office.id"   :value="office.id">{{ office.name }}</option>
              </select>
            </div>
          </div>
        </div>

          <div class="col-md-6 offset-6">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">نوع الصندوق</label>

            <div class="col-sm-8">
              <select class="form-control" @change="onChangeDateStart" v-model="typekey" required :disabled="(isOld) ? true : false">
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

        <!--<div class="col-md-6 offset-6">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">رقم الصندوق</label>

            <div class="col-sm-8">
                            <input type="text" name="numberBox" required disabled v-model="numberBox" class="form-control">

             
            </div>
          </div>
        </div> -->


        <div class="col-md-6 offset-6">
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
           <div class="col-md-6 offset-6">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">نهاية الحجز</label>
            <div class="col-sm-8">
                            <input type="text" name="endDate"  required disabled v-model="endDate" class="form-control">
            </div>
          </div>
        </div>
         <div class="col-md-6 offset-6">


          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">طريقة الدفع</label>

            <div class="col-sm-8">
              <select class="form-control" :disabled="true" v-model="payTypeId" required>
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


       <!-- <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">رقم الفاتورة</label>

            <div class="col-sm-8">
              <input type="text" name="numberBill" disabled v-model="numberBill" class="form-control">
            </div>
          </div>
        </div> -->

         <!--      <div class="col-md-3">
          <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="exampleCheck1">سعر المفتاح</label>

            <div class="col-sm-8">
              <input type="text" name="priceKey" value="3" v-model="priceKey" class="form-control" readonly>
            </div>
          </div>
        </div> -->
 
        <div class="col-md-6 offset-6">
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
      

<hr>
    <div class="col-md-6 offset-6">
                <button type="submit" class="btn  btn-lg btn-primary">حجــز صندوق</button>

                <a href="/mybox" style="margin:0 15px;" class="btn btn-danger">إلغاء</a>
  </div>
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
      
      enoffice:true,
    
      endDate:"",
      jobs: [],
            oldData: {},

      Identitys: [],
      types: [],
      officeBoxs: [],
      typeBoxs: [],
       payTypes: [],
       regions:[],
        offices:[],
emailAddress:"",
      typekey:"",      typekey1: "",
customerId:0,
      totalPrice:"",
      boxPrice:"",
     // numberBill:"",
      payTypeId:1,
      priceKey:3,
            numberBox:"",
                        numberBox1:"",
                        isOld:false,

customerTypeId:"",
regionId:"",
officeId:"",
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
  watch(){

  },
  created() {
          console.log(this.$route.params.id);


//this.getAllOfficeData();

    this.getAllTypeBox();
    this.getAllJobType();
    this.getAllCustomerType();
    this.getAllCustomerIdentity();
this.getAllPayType();
this.getAllRegionData();
   // this.getAllOfficeBoxs();

  },
  methods: {
    resetType(){
      /*this.typekey="";
      this.numberBox="الرجاء اختيار نوع الصندوق";
      console.log(this.typekey);*/
    },
    getAllRegionData(){

customerService.getAllRegion().then(data=>{
      const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
      //  this.typekey="";
        this.regions = resultArray;
        console.log(this.regions);

});
      

  

 
      

    },
    
    getAllOfficeData(){
     
    // this.enoffice=false;
       var ii = this.regionId;
customerService.getAllOffice(ii).then(data=>{
      const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.offices = resultArray;
        this.officeId ="";
        if(this.offices.length != 0){
          this.enoffice=false;
           this.numberBox="";
          }else{
          this.enoffice=true;
          this.numberBox="";}
      //  this.typekey="";


          
        console.log(this.offices);
        console.log(resultArray.length);

});
      

    },
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
          this.onChangeDateStart();
        console.log(this.typeBoxs);
      });



    },

   /* getAllOfficeBoxs() {
      var ii = this.typekey;
            var oof = this.officeId;

      customerService.getAllBoxOfficeNotUsedById(ii,oof).then(data => {
         if(data !=0)
        this.numberBox = data;
        else
        this.numberBox ="لاتوجد صناديق شاغرة";
 
 

        console.log(this.numberBox);
      

      });
       this.onChangeDateStart();
    },*/

    onChangeDateStart(){
      var d = new Date();
var year = d.getFullYear();
var a = parseInt(this.rentYears)
var c = new Date(year + a, 0, 1)

 

this.endDate = c.toLocaleDateString();
      console.log(this.endDate);
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
    },
      handleSubmit(e) {
       
var ii = this.typekey;
console.log(ii)
 
      this.submitted = true;

      
  
              var objData = {};
              objData= {
                
        dateStartRent:this.dateStartRent,
         rentYears:this.rentYears,
                totalPrice:this.totalPrice,
               //  billsNumber:this.numberBill,
               officeId:this.officeId,
               typeBoxId:ii,
                boxId :this.numberBox,
                numberBox:this.numberBox,
                priceKey:this.priceKey,
                payTypeId:this.payTypeId,
                customerId: this.customerId
              }
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

                  setTimeout(function(){ window.location.href ="/mybox" ;
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
             
            customerService.saveBoxCustomer(objData).then(
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
