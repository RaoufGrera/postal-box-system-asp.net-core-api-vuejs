<template>
  <div>
    <h5>
      <i class="fa fa-home"/> الرئيسية
    </h5>
    <hr>
    <br>
    <div class="row">
      <!--  <div class="col-md-3 m10"><a class="btn btn-block btn-default" href="/admin/dashboard">
                       <i class=" fa fa-home"/>
                       <br>  <br><span>الرئيسية</span></a>
                </div>
      -->

      <div class="col-md-3 m10">
        <a class="btn btn-block btn-default" href="/admin/users">
          <i class="fa fa-users"/>
          <br>
          <br>
          <span>ادارة المستخدمين</span>
        </a>
      </div>

      <div class="col-md-3 m10">
        <a class="btn btn-block btn-default" href="/admin/box">
          <i class="fa fa-cube"/>
          <br>
          <br>
          <span>حجز صندوق</span>
        </a>
      </div>
      <div class="col-md-3 m10">
        <a class="btn btn-block btn-default" href="/admin/booked">
          <i class="fa fa-cubes"/>
          <br>
          <br>
          <span>طلبات الحجز</span>
        </a>
      </div>

      <div class="col-md-3 m10">
        <a class="btn btn-block btn-default" href="/admin/customer">
          <i class="fa fa-address-card-o"/>
          <br>
          <br>
          <span>العملاء</span>
        </a>
      </div>
    </div>
    <br>
    <div class="row">
      <div class="col-md-3 m10">
        <a class="btn btn-block btn-default" href="/admin/notification">
          <i class="fa fa-paper-plane-o"/>
          <br>
          <br>
          <span>إرسال اشعارات</span>
        </a>
      </div>
      <div class="col-md-3 m10">
        <a class="btn btn-block btn-default" href="/admin/notification/history">
          <i class="fa fa-archive"/>
          <br>
          <br>
          <span>الإشعارات المرسلة</span>
        </a>
      </div>
      <div class="col-md-3 m10">
        <a class="btn btn-block btn-default" href="/admin/help">
          <i class="fa fa-life-ring"/>
          <br>
          <br>
          <span>مساعدة</span>
        </a>
      </div>
    </div>

    <br>
    <br>
    <h3>مكتب بريد</h3>
    <hr>
    <div class="row" v-if="boxs">
      <div class="col-xl-3 col-md-6 mb-1" v-for="box in boxs" :key="box.typeCount">
        <div
          class="card border-left-success shadow h-100 py-2"
          style="
    border-right: .25rem solid #1cc88a!important;
"
        >
          <div class="card-body">
            <div class="row no-gutters align-items-center">
              <div class="col mr-2">
                <div class="text-xs font-weight-bold text-success text-uppercase mb-1">{{box.name}}</div>
                <div class="h5 mb-0 font-weight-bold text-gray-800">
                  {{box.typeCount}}
                  <span style="font-size:14px">
                    <span>[</span>
                    {{ box.ejarCount }} مؤجر
                    <span>]</span>
                  </span>
                </div>
              </div>
              <div class="col-auto">
                <i
                  class="fa h5 fa-cubes mb-0 text-gray-800"
                  style="
    font-size: 35px;
    color: #31cc88a;
    color: #1cc88a;
"
                ></i>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!--  <line-chart  class=" col-md-4"></line-chart> -->
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
import { customerService } from "../_services/customer.service";
//import { c } from 'c'
//  import LineChart from './charts/LineChart'

export default {
  // components:{c},
  //    extends: Bar,
  // components: { "line-chart": LineChart },

  data() {
    return {
      boxs: []
    };
  },

  created() {
    this.boxs.loading = true;

    this.getInfoBoxbyOffice();
  },

  methods: {
    getInfoBoxbyOffice() {
      customerService.getInfoBoxbyOffice().then(data => {
        const resultArray = [];
        for (let key in data) {
          resultArray.push(data[key]);
        }
        this.boxs = resultArray;
        console.log(this.boxs);
      });
    }
  }
};
</script>

