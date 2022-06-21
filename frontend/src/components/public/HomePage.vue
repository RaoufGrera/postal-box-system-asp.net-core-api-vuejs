<template>
    <div>

          <h5>الرئيسية </h5>
          <br>
       <div class="row">
     



         <div class="col-md-3 m10"><a class="btn btn-block btn-default" href="/home">
                        <i class=" fa  fa fa-home"/>
                        <br> <br><span>الرئيسية</span></a>
                </div>


                <div class="col-md-3 m10"><a class="btn btn-block btn-default" href="/user">
                       <i class=" fa fa-user"/>
                        <br> <br><span>تعديل الملف الشخصي</span></a>
                </div>

               <div class="col-md-3 m10"><a class="btn btn-block btn-default" href="/notification">
                        <i class=" fa fa-bell-o"/>
                         <br><br><span>الإشعارات</span></a>
                </div>
                <div class="col-md-3 m10"><a class="btn btn-block btn-default" href="/addbox">
                        <i class=" fa  fa fa-cube"/>
                        <br> <br><span>حجز صندوق</span></a>
                </div>

       

          
    </div>
    <br>
    <div class="row">
            
                  <div class="col-md-3 m10"><a class="btn btn-block btn-default" href="/mybox">
                        <i class=" fa  fa fa-cubes"/>
                        <br> <br><span>قائمة الصناديق</span></a>
                </div>
            <div class="col-md-3 m10"><a class="btn btn-block btn-default" href="/support">
                        <i class="fa fa-life-ring"/>
                        <br> <br><span>مساعدة</span></a>
                </div>
    </div>
       
   
    </div>
 
   
</template>

<script>
import { mapState, mapActions } from 'vuex'
import * as axios from "vue";

export default {
    
};

</script>
