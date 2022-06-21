<template>
  <div id="sidebar-wrapper">
    <ul v-if="!iscustomer" class="sidebar-nav">
      <li>
        <a href="/admin/dashboard">
          <i class="fa fa-home"></i>الرئيسية
        </a>
      </li>
      <li>
        <a href="/admin/users">
          <i class="fa fa-users"></i> إدارة المستخدمين
        </a>
      </li>
      <li>
        <a href="/admin/box">
          <i class="fa fa-cube"></i> حجز صندوق
        </a>
      </li>
      <li>
        <a href="/admin/booked">
          <i class="fa fa-cubes"></i> طلبات الحجز
        </a>
      </li>
      <li>
        <a href="/admin/customer">
          <i class="fa fa-address-card-o"></i> العملاء
        </a>
      </li>

      <!--      <li>
                    <a href="#"><i class="fa fa-file-text"></i> الفواتير </a>
                </li>
                <li>
                    <a href="#"><i class="fa fa-archive"></i> إدارة المحتوي </a>
      </li>-->
      <li>
        <a href="/admin/createbox">
          <i class="fa fa-cube"></i>إنشاء صناديق
        </a>
      </li>
      <li>
        <a href="/admin/notification">
          <i class="fa fa-paper-plane-o"></i> إرسال اشعارت
        </a>
      </li>
      <li>
        <a href="/admin/notification/history">
          <i class="fa fa-archive"></i>الإشعارات المرسلة
        </a>
      </li>

     <!-- <li>
        <a href="/admin/notification/history">
          <i class="fa fa-archive"></i>التقارير
        </a>
      </li>-->
      <li>
        <a href="/admin/help">
          <i class="fa fa-life-ring"></i> مساعدة
        </a>
      </li>
      <li class="sidebar-brand">
        <!--  <a href="#">
                        Start Bootstrap
        </a>-->
      </li>
    </ul>

    <ul v-if="iscustomer" class="sidebar-nav">
      <li>
        <a href="/home">
          <i class="fa fa-home"></i>الرئيسية
        </a>
      </li>
      <li>
        <a href="/user">
          <i class="fa fa-user"></i>الملف الشخصي
        </a>
      </li>
      <li>
        <a href="/notification">
          <i class="fa fa-bell-o"></i>الإشعارات
          <span class="note">0</span>
        </a>
      </li>
      <li>
        <a href="/addbox">
          <i class="fa fa-cube"></i> حجز صندوق
        </a>
      </li>

      <li>
        <a href="/mybox">
          <i class="fa fa-cubes"></i>قائمة الصناديق
        </a>
      </li>

      <li>
        <a href="/support">
          <i class="fa fa-life-ring"></i> مساعدة
        </a>
      </li>
    </ul>
  </div>
</template>
<script>
export default {
  computed: {
    iscustomer() {
      return this.$store.state.account.user.isCustomer;
    }
  }
};
</script>


<style>
.note {
  background: #f44336;
  padding: 0 8px;
  font-size: 95%;
  border-radius: 25px;
  /* left: 0; */
  color: white;
}
.fa {
  margin-left: 8px !important;
}
#sidebar-wrapper {
  z-index: 1000;

  /* min-height: calc(100vh - 58px);*/

  overflow-y: auto;
  background: #fff;
}

.fa {
  display: inline;
}
/* Sidebar Styles */

.sidebar-nav {
  top: 0;
  margin: 0;
  padding: 0;
  list-style: none;
}

.sidebar-nav li {
  text-indent: 8px;
  line-height: 48px;
  border-top: 1px solid #f1f1f1;
}

.sidebar-nav li a {
  display: block;
  text-decoration: none;
  color: #3a3a3a;
}

.sidebar-nav li a:hover {
  text-decoration: none;
  color: rgb(36, 36, 36);
  background: #ddd;
}

.sidebar-nav li a:active,
.sidebar-nav li a:focus {
  text-decoration: none;
}

.sidebar-nav > .sidebar-brand {
  height: 40px;
  font-size: 18px;
  line-height: 60px;
}

.sidebar-nav > .sidebar-brand a {
  color: #555;
}

.sidebar-nav > .sidebar-brand a:hover {
  color: rgb(36, 36, 36);
  background: none;
}
.fa {
  margin-left: 4px;
}
</style>
