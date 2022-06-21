<template>
  <div>
    <h5>إدارة شؤون العملاء</h5>
    <hr>
    <br>
    <div class="card border-primary">
      <div
        style="    color: #fefefe;
    background-color: #1083f6;
    border-color: #0980f8;"
        class="card-header"
      >بحث</div>
      <div class="card-body">
        <form @submit.prevent="handleSubmit">
          <div class="col-md-6">
            <div class="col-md-12">
              <div class="form-group row">
                <label class="col-sm-4 col-form-label" for="Name">الأسم</label>
                <div class="col-sm-8">
                  <input type="text" name="Name" v-model="name" class="form-control">
                </div>
              </div>
            </div>
            <div class="col-md-12">
              <div class="form-group row">
                <label class="col-sm-4 col-form-label" for="BoxNumber">رقم الصندوق</label>
                <div class="col-sm-8">
                  <input type="text" name="BoxNumber" v-model="boxnumber" class="form-control">
                </div>
              </div>
            </div>
            <div class="col-md-12">
              <div class="form-group row">
                <label class="col-sm-4 col-form-label" for="PhoneNumber">رقم الهاتف</label>
                <div class="col-sm-8">
                  <input type="text" name="PhoneNumber" v-model="phonenumber" class="form-control">
                </div>
              </div>
            </div>
          </div>
          <button type="submit" style="margin: 10px 4px;" class="btn btn-primary">بحث</button>
          <a
            href="/admin/customer"
            style="margin: 10px 4px; color:#fff"
            class="btn btn-danger"
          >إزالة معايير البحث</a>
        </form>
      </div>
    </div>
    <br>
    <br>
    <em v-if="customers.loading">الرجاء الأنتظار...</em>

    <table class="table" v-if="customers">
      <thead class="thead-light">
        <tr>
          <!--  <th scope="col">#</th> -->
          <th scope="col">الأسم</th>
          <!--   <th scope="col">العنوان</th> -->
          <th scope="col">الهاتف</th>
          <th scope="col">الحجم</th>
          <th scope="col">ر.الصندوق</th>
          <th scope="col">بداية الاشتراك</th>
          <th scope="col">نهاية الإشتراك</th>
          <th scope="col">ر.الفاتورة</th>

          <th colspan="4" scope="col"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="customer in customers" :key="customer.billsNumber">
          <!-- <th scope="row">{{customer.customerId }}</th>-->
          <td>{{customer.name }}</td>
          <!-- <td>{{customer.address }}</td> -->
          <td>{{customer.phonNumber1 }}</td>
          <td>
            <span v-if="customer.typeBox != ''">{{customer.typeBox }}</span>
            <span v-else>-</span>
          </td>

          <td>
            <span v-if="customer.numberBox != 0">{{customer.numberBox }}</span>
            <span v-else>-</span>
          </td>
          <td>
            <span v-if="customer.dateBills != ''">{{customer.dateBills }}</span>
            <span v-else>-</span>
          </td>

          <td>
            <span v-if="customer.expiretDate != '' ">{{customer.expiretDate }}</span>
            <span v-else>-</span>
          </td>
          <td>
            <span v-if="customer.billsNumber != 0">{{customer.billsNumber }}</span>
            <span v-else>-</span>
          </td>

          <td>
            <button @click="showModal(customer.customerId)" class="btn btn-default">
              <i class="fa fa-pencil"></i> تعديل
            </button>
          </td>

          <!--  <td>
             <a :href="'/admin/box/' + customer.customerId/+'add'" class="btn btn-default" ><i class="fa fa-plus"></i> إضافة صندوق </a>
          </td>-->

          <td>
            <div v-if="customer.billsId != 0 && customer.isActive">
              <a :href="'/admin/box/' + customer.billsId" class="btn btn-default">
                <i class="fa fa-refresh"></i> تجديد
              </a>
            </div>
            <div class="text-center" v-else>-</div>
          </td>
          <td>
            <div v-if="customer.billsNumber != 0 && customer.isActive">
              <button @click="confimdelte(customer.billsId)" class="btn btn-danger">
                <i class="fa fa-times"></i> إنهاء الأشتراك
              </button>
            </div>
            <div class="text-center" v-else>-</div>
          </td>

          <td>
            <!-- <div v-if="customer.billsNumber != 0 && customer.isActive ">
            <button @click="confimdelte(customer.billsId)" class="btn btn-danger"> <i class="fa fa-paper-plane-o"></i>  تنبيه</button>
    </div>
        <div class="text-center" v-else>
              -
            </div>-->

            <div v-if="customer.billsNumber != 0">
              <button class="btn btn-info" @click="print(customer.billsNumber)">
                <i class="fa fa-print"></i>طباعة
              </button>
            </div>
            <div v-else>-</div>
          </td>
        </tr>
      </tbody>
    </table>
    <div dir="rtl" id="printMe">
      <div dir="rtl">
        <h4 style="text-align:center">شركة بريد ليبيا</h4>
        <br>
        <label style="text-align:right">
          <span>رقم الفاتورة:</span>
          {{printNumber}} #
        </label>
        <br>

        <label style="text-align:right">{{printName}}</label>
      </div>
    </div>
    <modal
      :addEditTitleComp="addEditTitle"
      :dataComp="objData"
      v-if="isModalShow"
      @close="closeModal"
      @updateTable="getCustomer"
    ></modal>
    <button
      v-if="seen"
      style="margin: 0 auto;
    display: block;"
      class="btn btn-info text-center"
      @click="showMore"
    >عرض المزيد</button>
  </div>
</template>
<style>
th {
  font-weight: 100;
}
.table .thead-light th {
  color: #fefefe;
  background-color: #1083f6;
  border-color: #0980f8;
}
</style>

<script>
import { customerService } from "../../_services/customer.service";
import modal from "./CustomerModal";

export default {
  components: { modal },

  data() {
    return {
      output: null,
      customers: [],
      isModalShow: false,
      objData: {},
      submitted: false,
      addEditTitle: "تعديل",
      name: "",
      billsid: 0,
      phonenumber: "",
      boxnumber: "",
      page: 1,
      printName: "",
      printNumber: "",
      printBox: "",
      printTotal: "",
      printOffice: "",
      seen: true
    };
  },
  created() {
    this.customers.loading = true;

    this.getCustomer();
  },
  methods: {
    sleep(milliseconds) {
      return new Promise(resolve => setTimeout(resolve, milliseconds));
    },
    print(id) {
      // Pass the element id here
      let obj = this.customers.find(o => o.billsNumber === id);
      console.log("print id " + id);

      this.printName = obj.name;
      this.printNumber = obj.billsNumber;
      this.sleep(700).then(() => {
        this.$htmlToPaper("printMe", () => {
          console.log("Printing done or got cancelled!");
        });

        //do stuff
      });
    },
    confimdelte(id) {
      Swal.fire({
        title: "هل أنت متأكد من إنهاء إشتراك الصندوق؟",
        text: "",
        type: "تنبيه",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "نعم",
        cancelButtonText: "لا"
      }).then(result => {
        if (result.value) {
          console.log("Deee");
          customerService.deleteBills(id).then(
            userData => {
              setTimeout(() => {
                this.$store.state.alert.message = userData;
                this.$store.state.alert.type = "alert-success";
                console.log(userData);
                this.getCustomer();
              });
            },
            error => {
              // commit("registerFailure", error);
              this.$store.state.alert.message = error;
              this.$store.state.alert.type = "alert-danger";
              console.log(error);
            }
          );
          Swal.fire("تنبيه!", userData.message, "success");
        }
      });
    },

    handleSubmit(e) {
      this.submitted = true;

      this.getCustomer();
    },

    clearData() {
      this.name = "";
      this.page = 1;

      this.phonenumber = "";
      this.boxnumber = "";
    },
    showMore() {
      this.page++;

      customerService
        .getAll(this.page, this.name, this.boxnumber, this.phonenumber)
        .then(
          data => {
            const resultArray = [];
            for (let key in data) {
              this.customers.push(data[key]);
            }
          },

          error => {
            this.seen = false;
            this.page = 1;
            this.$store.state.alert.message = error;
            this.$store.state.alert.type = "alert-danger";
            setTimeout(() => {
              // commit("registerFailure", error);

              this.$store.state.alert.message = "";

              console.log(error);
            }, 2000);
          }
        );
    },
    getCustomer() {
      this.seen = true;
      this.page = 1;
      customerService
        .getAll(this.page, this.name, this.boxnumber, this.phonenumber)
        .then(
          data => {
            const resultArray = [];
            for (let key in data) {
              resultArray.push(data[key]);
            }
            this.customers = resultArray;
            if (resultArray.length < 10) this.seen = false;
            console.log(this.customers);
            this.$store.state.alert.message = "";
          },

          error => {
            this.customers = [];
            this.seen = false;

            // commit("registerFailure", error);
            this.$store.state.alert.message = error;
            this.$store.state.alert.type = "alert-danger";
            console.log(error);
          }
        );
    },
    showModal(id) {
      var filtterFromList = this.customers.find(x => x.customerId == id);
      if (filtterFromList) {
        filtterFromList.Id = filtterFromList.customerId;
        this.objData = filtterFromList;
        this.addEditTitle = "تعديل";
        this.isModalShow = true;
      }
    },
    closeModal() {
      this.isModalShow = false;
    }
  }
};
</script>
