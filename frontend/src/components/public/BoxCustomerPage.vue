<template>
  <div>
        <h5><i class="fa fa-cubes"></i> قائمة الصناديق</h5>
<hr>
    <br>

   <em v-if="boxs.loading">الرجاء الأنتظار...</em>

    <table class="table" v-if="boxs">
      <thead class="thead-light">
        <tr>
        <!--  <th scope="col">#</th> -->
          <!--  <th scope="col">الأسم</th> -->
       <!--   <th scope="col">العنوان</th> -->
          <th scope="col">مكتب بريد</th>
            <th scope="col">الحجم</th>
          <th scope="col">رقم الصندوق</th>
           <th scope="col">بداية الاشتراك</th>
          <th scope="col">نهاية الإشتراك</th>
        
           <th scope="col">وضع الصندوق</th>

          <th colspan="2" scope="col"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="box in boxs" :key="box.id">
         <!-- <th scope="row">{{customer.customerId }}</th>-->
          <td>{{box.officeName }}</td>
           <td>{{box.typeBox }}</td>
     
 
          <td> <span v-if="box.numberBox != 0">{{box.numberBox }} </span><span v-else> - </span></td>
                      <td> <span v-if="box.dateBills != ''">{{box.dateBills }} </span><span v-else> - </span></td>

                     <td> <span v-if="box.expiretDate != '' ">{{box.expiretDate }} </span><span v-else> - </span></td>
 
     
      
                        <td>
    <div v-if="box.isBooked  && !box.isActive">

     غير مفعل. في انتظار الدفع

    </div>
        <div class="text-center" v-else-if="!box.isBooked  && !box.isActive">
              أنتهت صلاحية الصندوق
            </div>
             <div class="text-center" v-else-if="(!box.isBooked  && box.isActive) || (box.isBooked  && box.isActive)">
             مفعل
            </div>
          </td>
             <td>

            <a href="/notification" class="btn btn-default"> <i class="fa fa-bell-o"></i> عرض محتوي الصندوق</a>
    
     
          </td>
              <td>
    <div v-if="box.billsNumber != 0 && box.isActive">
            <button @click="confimdelte(box.billsId)" class="btn btn-danger"> <i class="fa fa-times"></i> إنهاء الأشتراك</button>
    </div>
        <div class="text-center" v-else>
              -
            </div>
          </td>
     
          

   <!--                <td>
    <div v-if="customer.billsNumber != 0 && customer.isActive ">
            <button @click="confimdelte(customer.billsId)" class="btn btn-danger"> <i class="fa fa-paper-plane-o"></i>  تنبيه</button>
    </div>
        <div class="text-center" v-else>
              -
            </div>
          </td> -->
        </tr>
      </tbody>
    </table>
    <div>
     

<a href="/addbox" class="btn btn-info"> + الإشتراك في صندوق جديد</a>
      <br> <br>
       <br> <br>
    </div>
        <div class="alert alert-info" role="alert">
  <p>تستمر فترة حجز الصندوق لمدة 15 يوماً من تاريخ الحجز، في حالة عدم دفع تكاليف الصندوق سيتم الغاء حجز الصندوق تلقائياً</p>
  <hr>
  <p class="mb-0">عند التأخر في تجديد صلاحية الصندوق سيتم الغاء الإشتراك تلقائياً بعد 15 يوماً من انتهاء صلاحية الصندوق وعندها سيتم تخصيصه لشخص اخر.</p>
</div>
  </div>
</template>

<script>
import { onlineService } from "../../_services/online.service";
 
export default {
 
  data() {
    return {
        
      boxs: [],
           
    };
  },
  created() {
          console.log(this.$route.params.id);

    this.boxs.loading = true;

 
    this.getAllJobType();
 
  },
  methods: {
    
    getAllJobType() {
      onlineService.getMyBoxs().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.boxs = resultArray;
        console.log(this.boxs);
      });
    },
      


    
  
  }
     
  };
 
</script>

<style>
.forms {
  font-size: 95%;
}
.col-form-label {

    /*font-size: 94%;*/

}


.form-control {
  font-size: inherit;}
</style>
