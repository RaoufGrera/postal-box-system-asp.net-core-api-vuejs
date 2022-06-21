<template>
  <div>
    <h2>انشاء حساب</h2>
    <form @submit.prevent="handleSubmit">
      <div class="form-group">
        <label for="FirstName">First Name</label>
        <input
          type="text"
          v-model="user.FirstName"
          v-validate="'required'"
          name="FirstName"
          class="form-control"
          :class="{ 'is-invalid': submitted && errors.has('FirstName') }"
        >
        <div
          v-if="submitted && errors.has('FirstName')"
          class="invalid-feedback"
        >{{ errors.first('FirstName') }}</div>
      </div>
      <div class="form-group">
        <label for="UserName">اسم المستخدم</label>
        <input
          type="text"
          v-model="user.UserName"
          v-validate="'required'"
          name="UserName"
          class="form-control"
          :class="{ 'is-invalid': submitted && errors.has('UserName') }"
        >
        <div
          v-if="submitted && errors.has('UserName')"
          class="invalid-feedback"
        >{{ errors.first('UserName') }}</div>
      </div>
      <div class="form-group">
        <label for="Password">الرقم السري</label>
        <input
          type="Password"
          v-model="user.Password"
          v-validate="{ required: true, min: 6 }"
          name="Password"
          class="form-control"
          :class="{ 'is-invalid': submitted && errors.has('Password') }"
        >
        <div
          v-if="submitted && errors.has('Password')"
          class="invalid-feedback"
        >{{ errors.first('Password') }}</div>
      </div>
      <div class="form-group">
        <button class="btn btn-primary" :disabled="status.registering">إنشاء حساب</button>
        <img
          v-show="status.registering"
          src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA=="
        >
        <router-link to="/login" class="btn btn-link">إلغاء</router-link>
      </div>
    </form>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";

export default {
  data() {
    return {
      user: {
        FirstName: "",
        // lastName: '',
        UserName: "",
        Password: ""
      },
      submitted: false
    };
  },
  computed: {
    ...mapState("account", ["status"])
  },
  methods: {
    ...mapActions("account", ["register"]),
    handleSubmit(e) {
      this.submitted = true;
      this.$validator.validate().then(valid => {
        if (valid) {
          this.register(this.user);
        }
      });
    }
  }
};
</script>