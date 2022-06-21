<template>
  <div>
    <h5>
      <i class="fa fa-cubes"></i> طلبات الحجز
    </h5>
    <hr>
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

          <th colspan="2" scope="col"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="customer in customers" :key="customer.customerId">
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
            <span v-if="customer.billsNumber != null">{{customer.billsNumber }}</span>
            <span v-else>-</span>
          </td>

          <td>
            <button @click="confimdelte(customer.billsId)" class="btn btn-danger">
              <i class="fa fa-times"></i> رفض الإشتراك
            </button>
          </td>

          <td>
            <button @click="confimOk(customer.billsId)" class="btn btn-primary">
              <i class="fa fa-times"></i> قبول الإشتراك
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import { customerService } from "../../_services/customer.service";

export default {
  data() {
    return {
      customers: [],
      isModalShow: false,
      objData: {},
      submitted: false,
      addEditTitle: "تعديل"
    };
  },

  created() {
    this.user = JSON.parse(localStorage.getItem("user"));
    this.customers.loading = true;

    this.getAllBooked();
  },
  methods: {
    getAllBooked() {
      customerService.getAllBooked().then(
        data => {
          const resultArray = [];
          for (let key in data) {
            resultArray.push(data[key]);
          }
          this.customers = resultArray;
          console.log(this.customers);
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

    confimdelte(id) {
      Swal.fire({
        title: "هل أنت متأكد من رفض إشتراك الصندوق؟",
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
                this.getAllBooked();
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
    confimOk(id) {
      Swal.fire({
        title: "هل أنت متأكد من قبول الإشتراك؟",
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
          customerService.OkBills(id).then(
            userData => {
              setTimeout(() => {
                this.$store.state.alert.message = userData;
                this.$store.state.alert.type = "alert-success";
                console.log(userData);
                this.getAllBooked();
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
    }
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