<template>
  <div>
    <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
      <div class="card card-signin my-5">
        <div class="card-body">
          <h5 class="card-title text-center">إنشاء حساب </h5>
          <hr>
          <form @submit.prevent="handleSubmit" v-show="!send">

            <div class="form-group">
              <label for="phoneNumber">رقم الهاتف</label>
              <input type="text" v-model="phoneNumber" name="phoneNumber" class="form-control"
                :class="{ 'is-invalid': submitted && !phoneNumber }">
              <div v-show="submitted && !phoneNumber" class="invalid-feedback">الحقل مطلوب</div>
            </div>
            <div class="form-group">
              <label for="Password">الرقم السري</label>
              <input type="Password" v-model="Password" name="Password" class="form-control"
                :class="{ 'is-invalid': submitted && !Password }">
              <div v-show="submitted && !Password" class="invalid-feedback">الرقم السري مطلوب</div>
            </div>

            <div class="form-group">
              <button class="btn btn-block btn-primary" :disabled="loggingIn">إنشاء حساب</button>
              <img class="loading" v-show="loggingIn"
                src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==">
            </div>
            <!--  <template>
  <button v-facebook-signin-button="appId" class="facebook-signin-button"> تسجيل الدخول</button>
</template> -->
          </form>

          <form @submit.prevent="handleSubmit2" v-show="send">
            <code>code: 1 </code>
            <div v-show="send" class="form-group">
              <label for="code">الرمز</label>
              <input type="text" v-model="code" name="code" class="form-control">
            </div>
            <div class="form-group">
              <button class="btn btn-block btn-primary" :disabled="loggingIn">ارسال</button>
              <img class="loading" v-show="loggingIn"
                src="data:image/gif;base64,R0lGODlhEAAQAPIAAP///wAAAMLCwkJCQgAAAGJiYoKCgpKSkiH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQJCgAAACwAAAAAEAAQAAADMwi63P4wyklrE2MIOggZnAdOmGYJRbExwroUmcG2LmDEwnHQLVsYOd2mBzkYDAdKa+dIAAAh+QQJCgAAACwAAAAAEAAQAAADNAi63P5OjCEgG4QMu7DmikRxQlFUYDEZIGBMRVsaqHwctXXf7WEYB4Ag1xjihkMZsiUkKhIAIfkECQoAAAAsAAAAABAAEAAAAzYIujIjK8pByJDMlFYvBoVjHA70GU7xSUJhmKtwHPAKzLO9HMaoKwJZ7Rf8AYPDDzKpZBqfvwQAIfkECQoAAAAsAAAAABAAEAAAAzMIumIlK8oyhpHsnFZfhYumCYUhDAQxRIdhHBGqRoKw0R8DYlJd8z0fMDgsGo/IpHI5TAAAIfkECQoAAAAsAAAAABAAEAAAAzIIunInK0rnZBTwGPNMgQwmdsNgXGJUlIWEuR5oWUIpz8pAEAMe6TwfwyYsGo/IpFKSAAAh+QQJCgAAACwAAAAAEAAQAAADMwi6IMKQORfjdOe82p4wGccc4CEuQradylesojEMBgsUc2G7sDX3lQGBMLAJibufbSlKAAAh+QQJCgAAACwAAAAAEAAQAAADMgi63P7wCRHZnFVdmgHu2nFwlWCI3WGc3TSWhUFGxTAUkGCbtgENBMJAEJsxgMLWzpEAACH5BAkKAAAALAAAAAAQABAAAAMyCLrc/jDKSatlQtScKdceCAjDII7HcQ4EMTCpyrCuUBjCYRgHVtqlAiB1YhiCnlsRkAAAOwAAAAAAAAAAAA==">
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>
 

<script>
import { mapState, mapActions } from "vuex";
import FacebookSignInButton from 'vue-facebook-signin-button-directive';
import { userService } from "../../_services/user.service";
import { router } from '../../router';

export default {
  directives: {
    FacebookSignInButton
  },
  data() {
    return {
      phoneNumber: "",
      Password: "",
      submitted: false,
      appId: '1389759387825879',
      code: '',
      loggingIn: false,
      send: false

    };
  },
  computed: {
    ...mapState("account", ["status"])
  },
  created() {
    // reset login status
    //this.logout();

  },
  methods: {
    ...mapActions("account", ["login", "logout", "loginFacebook"]),

    OnFacebookAuthSuccess(AccessToken) {
      console.log("ID Token: " + AccessToken);
      userService.loginFacebook(AccessToken)
        .then(
          user => {
            commit("loginSuccess", user);
            router.push("/");
          },
          error => {
            this.loggingIn = false;
            commit("loginFailure", error);
            dispatch("alert/error", error, {
              root: true
            });
          }
        );
      // Receive the idToken and make your magic with the backend
      //  this.loginFacebook({idToken});
    },
    OnFacebookAuthFail(error) {
      console.log(error)
    },

    handleSubmit2(e) {
      this.submitted = true;

      if (this.code) {

        this.loggingIn = true;

        userService.authCheck(this.code)
          .then(userData => {

            this.$store.state.alert.message = userData.message;
            this.$store.state.alert.type = "alert-success";
            this.loggingIn = false;

            setTimeout(function () {

              router.push("/login");
            }, 2000);

          },
            error => {
              this.loggingIn = false;

              this.$store.state.alert.message = error.message;
              this.$store.state.alert.type = "alert-danger";
              console.log(error);
            }
          );
      }


    },

    handleSubmit(e) {
      this.submitted = true;
      const { phoneNumber, Password } = this;
      if (phoneNumber && Password) {

        this.loggingIn = true;

        userService.authPhone({ phoneNumber, Password })
          .then(userData => {

            this.$store.state.alert.message = userData.message;
            this.$store.state.alert.type = "alert-success";
            this.loggingIn = false;

            this.send = true;
          },
            error => {
              this.loggingIn = false;

              this.$store.state.alert.message = error;
              this.$store.state.alert.type = "alert-danger";
              console.log(error);
            }
          );
      }


    },

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

